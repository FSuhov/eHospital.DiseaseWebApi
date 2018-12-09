using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHospital.Diseases.Model;
using EHospital.Diseases.BusinessLogic.Contracts;

namespace EHospital.Diseases.BusinessLogic.Services
{
    /// <summary>
    /// Represents business logic for handling DiseaseCategory controller's requests
    /// </summary>
    public class DiseaseCategoryService : IDiseaseCategoryService
    {
        private readonly IUniteOfWork _unitOfWork;

        /// <summary>
        /// Initializes new instance of DiseaseCategoryService class
        /// </summary>
        /// <param name="unitOfWork"> Concrete instance of UnitOfWork, injected in startup.cs </param>
        public DiseaseCategoryService(IUniteOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the collection of DiseaseCategories entries existing in the database
        /// </summary>
        /// <returns> The collection of DiseaseCategories objects sorted alphabetically</returns>
        public async Task<IEnumerable<DiseaseCategory>> GetDiseaseCategories()
        {
            var result = _unitOfWork.DiseaseCategories.GetAll();

            return await Task.FromResult(result.OrderBy(d => d.Name));
        }

        /// <summary>
        /// Looks for DiseaseCategory entry with requested Id
        /// </summary>
        /// <param name="id">Id of Category to look for</param>
        /// <returns>DiseaseCategory entry with specified Id or NULL if not found</returns>
        public async Task<DiseaseCategory> GetDiseaseCategoryById(int id)
        {
            var result = await _unitOfWork.DiseaseCategories.Get(id);

            return result;
        }

        /// <summary>
        /// Adds DiseaseCategory entry to the database
        /// Throws exception if object with the same name alrady exists in the database
        /// </summary>
        /// <param name="diseaseCategory">New DiseaseCategory object</param>
        /// <returns>DiseaseCategory object that has been added</returns>
        public async Task<DiseaseCategory> AddDiseaseCategoryAsync(DiseaseCategory diseaseCategory)
        {
            if (_unitOfWork.Diseases.GetAll().Any(dc => dc.Name == diseaseCategory.Name))
            {
                throw new ArgumentException("Category already exists.");
            }

            DiseaseCategory result = _unitOfWork.DiseaseCategories.Insert(diseaseCategory);
            await _unitOfWork.Save();
            return result;
        }
    }
}
