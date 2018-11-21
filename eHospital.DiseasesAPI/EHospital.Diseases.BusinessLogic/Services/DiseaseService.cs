using EHospital.Diseases.Model;
using EHospital.Diseases.BusinessLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Services
{
    /// <summary>
    /// Represents business logic for handling Disease controller's requests
    /// </summary>
    public class DiseaseService : IDiseaseService
    {
        private readonly IUniteOfWork _unitOfWork;

        public DiseaseService(IUniteOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the collection of Diseases existing in the database
        /// </summary>
        /// <returns> The collection of Diseases objects sorted alphabetically</returns>
        public IQueryable<Disease> GetAllDiseases()
        {
            var result = _unitOfWork.Diseases.GetAll();           

            return result.OrderBy(d => d.Name);
        }

        /// <summary>
        /// Gets the collection of Diseases belonging to specified category
        /// </summary>
        /// <param name="categoryId">Id of category to look for</param>
        /// <returns> The collection of Diseases objects sorted alphabetically</returns>
        public IQueryable<Disease> GetDiseasedByCategory(int categoryId)
        {
            var diseases = _unitOfWork.Diseases.GetAll().Where(d => d.CategoryId == categoryId);

            return diseases.OrderBy(d => d.Name);
        }

        /// <summary>
        /// Looks for Disease object with requested Id
        /// </summary>
        /// <param name="diseaseId">Id of Disease to look for</param>
        /// <returns>Disease object with specified Id or NULL if not found</returns>
        public Disease GetDiseaseById(int diseaseId)
        {
            var disease = _unitOfWork.Diseases.Get(diseaseId);

            return disease;
        }

        /// <summary>
        /// Adds Disease object to the database
        /// Throws exception if object with the same name alrady exists in the database
        /// </summary>
        /// <param name="disease">New Disease object</param>
        /// <returns>Disease object that has been added </returns>
        public async Task<Disease> AddDiseaseAsync(Disease disease)
        {
            if (_unitOfWork.Diseases.GetAll().Any(d => d.Name == disease.Name))
            {
                throw new ArgumentException("Disease already exists.");
            }

            Disease diseaseInserted = _unitOfWork.Diseases.Insert(disease);
            await _unitOfWork.Save();

            return diseaseInserted;
        }

        /// <summary>
        /// Sets IsDisabled property of Disease instance to true 
        /// Throws exception if not found.
        /// Calls cascade delete procedure that sets IsDeleted to true for all entries in 
        /// PatientDiseases table where this Disease has been found.
        /// </summary>
        /// <param name="diseaseId">Disease object that has been disabled</param>
        public async Task<Disease> DeleteDiseaseAsync(int diseaseId)
        {
            var diseaseToDelete = _unitOfWork.Diseases.Get(diseaseId);

            if (diseaseToDelete == null)
            {
                throw new ArgumentException("No disease found.");
            }

            if (_unitOfWork.PatientDiseases.GetAll().Count() != 0)
            {
                if (_unitOfWork.PatientDiseases.GetAll().Any(d => d.DiseaseId == diseaseToDelete.Id))
                {
                    _unitOfWork.CascadeDeletePatientDisease(diseaseId);
                }
            }

            diseaseToDelete.IsDeleted = true;
            _unitOfWork.Diseases.Delete(diseaseToDelete);
            await _unitOfWork.Save();

            return diseaseToDelete;
        }
    }
}
