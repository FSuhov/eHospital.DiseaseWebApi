using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EHospital.Diseases.BusinessLogic.Contracts;
using EHospital.Diseases.Model;

namespace EHospital.Diseases.BusinessLogic.Services
{
    /// <summary>
    /// Represents business logic for handling PatientDisease controller's requests
    /// </summary>
    public class PatientDiseaseService : IPatientDiseaseService
    {
        private readonly IUniteOfWork _unitOfWork;

        /// <summary>
        /// Initilializes new instance of DiseaseCategoryService class
        /// </summary>
        /// <param name="unitOfWork"> Concrete instance of UnitOfWork, injected in startup.cs </param>
        public PatientDiseaseService(IUniteOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the collection of Disease entries related to Patient with specified Id.    
        /// </summary>
        /// <returns> The collection of Diseases objects sorted alphabetically</returns>
        public async Task<IEnumerable<Disease>> GetDiseaseByPatient(int patientId)
        {
            var diseasesOfPatient = await Task.FromResult
                                    (from diseases in _unitOfWork.PatientDiseases.GetAll()
                                    where diseases.PatientId == patientId
                                    join disease in _unitOfWork.Diseases.GetAll()
                                    on diseases.DiseaseId equals disease.Id
                                    select disease);

            return diseasesOfPatient.OrderBy(d => d.Name);
        }

        /// <summary>
        /// Gets the collection of PatientDisease entries related to Patient with specified Id.    
        /// </summary>
        /// <returns> The collection of PatientDisease objects</returns>
        public async Task<IEnumerable<PatientDisease>> GetPatientDiseasesByPatient(int patientId)
        {
            var patientDiseases = await Task.FromResult(_unitOfWork.PatientDiseases.GetAll().Where(d => d.PatientId == patientId));

            return patientDiseases;
        }

        /// <summary>
        /// Gets PatientDisease entry with specified Id
        /// </summary>
        /// <param name="patientDiseaseId">Id of PatientDisease entry to look for</param>
        /// <returns>PatientDisease object or NULL if not found</returns>
        public async Task<PatientDisease> GetPatientDisease(int patientDiseaseId)
        {
            var result = await _unitOfWork.PatientDiseases.Get(patientDiseaseId);

            if (result == null)
            {
                throw new ArgumentNullException("No PatientDisease with such id.");
            }

            return result;
        }

        /// <summary>
        /// Adds new PatientDisease entry to Database
        /// </summary>
        /// <param name="patientDisease">PatientDisease object to be added</param>
        /// <returns>Added PatientDisease object</returns>
        public async Task<PatientDisease> AddPatientDiseaseAsync(PatientDisease patientDisease)
        {
            PatientDisease patientDiseaseInserted = _unitOfWork.PatientDiseases.Insert(patientDisease);
            await _unitOfWork.Save();

            return patientDiseaseInserted;
        }

        /// <summary>
        /// Updates existing PatientDisease entry in Database.
        /// Throws exception in case entry to be updated not found.
        /// </summary>
        /// <param name="patientDiseaseId">Id of PatientDisease entry to be updated</param>
        /// <param name="patientDisease">A sample PatientDisease object to be copied from</param>
        /// <returns></returns>
        public async Task<PatientDisease> UpdatePatientDiseaseAsync(int patientDiseaseId, PatientDisease patientDisease)
        {
            var patientDiseaseUpdated = await _unitOfWork.PatientDiseases.Get(patientDiseaseId);
            if (patientDiseaseUpdated == null)
            {
                throw new ArgumentException("No such record");
            }

            _unitOfWork.PatientDiseases.Update(patientDiseaseUpdated);
            await _unitOfWork.Save();
            return patientDiseaseUpdated;
        }

        /// <summary>
        /// Collects data from 4 tables in DB and combines it to 
        /// the list of diseases of selected patient with detailed information
        /// </summary>
        /// <param name="patientId">Id of patient to look for</param>
        /// <returns>Collection of objects in PatientDiseaseInfo view</returns>
        public async Task<IEnumerable<PatientDiseaseInfo>> GetPatientDiseasesInfos(int patientId)
        {
            var infos = await Task.FromResult
                        (from pd in _unitOfWork.PatientDiseases.GetAll()
                        where pd.PatientId == patientId
                        join dis in _unitOfWork.Diseases.GetAll()
                        on pd.DiseaseId equals dis.Id
                        join cat in _unitOfWork.Categories.GetAll()
                        on dis.CategoryId equals cat.Id
                        join user in _unitOfWork.Users.GetAll()
                        on pd.UserId equals user.Id
                        select new PatientDiseaseInfo
                        {
                            Id = pd.Id,
                            Name = dis.Name,
                            StartDate = pd.StartDate,
                            IsCurrent = (pd.EndDate == null),
                            CategoryName = cat.Name,
                            Doctor = user.LastName
                        });

            return infos;
        } 

        /// <summary>
        /// Looks for PatientDisease instance with specified ID,
        /// selects related data for that instance from other tables 
        /// </summary>
        /// <param name="patientDiseaseId">Id of PatiendDisease instance to look for </param>
        /// <returns> Readable Representation of PatientDisease with reference's Ids replaced with text Names </returns>
        public async Task<PatientDiseaseDetails> GetPatientDiseaseDetailes (int patientDiseaseId)
        {
            var diseaseDetails =  await Task.FromResult
                                  (from pd in _unitOfWork.PatientDiseases.GetAll()
                                  where pd.Id == patientDiseaseId
                                  from dis in _unitOfWork.Diseases.GetAll()
                                  where dis.Id == pd.DiseaseId
                                  from cat in _unitOfWork.DiseaseCategories.GetAll()
                                  where cat.Id == dis.CategoryId
                                  from user in _unitOfWork.Users.GetAll()
                                  where user.Id == pd.UserId
                                  select new PatientDiseaseDetails
                                  {
                                      Id = pd.Id,
                                      Name = dis.Name,
                                      CategoryName = cat.Name,
                                      Description = dis.Description,
                                      StartDate = pd.StartDate,
                                      EndDate = pd.EndDate,
                                      Notes = pd.Note,
                                      Doctor = user.LastName
                                  });

            return diseaseDetails.FirstOrDefault();
        }

        /// <summary>
        /// Updates some of PatientDisease instance properties: EndDate, Note, UserId (doctor)
        /// </summary>
        /// <param name="id">Id of PatientDisease instance to be updated </param>
        /// <param name="patientDiseaseDetails">Readable representation of PatientDisease </param>
        /// <returns>Updates instance of PatientDisease </returns>
        public async Task<PatientDisease> UpdatePatientDiseaseAsync(int id, PatientDiseaseDetails patientDiseaseDetails)
        {
            var patientDiseaseToUpdate = await _unitOfWork.PatientDiseases.Get(id);
            patientDiseaseToUpdate.EndDate = patientDiseaseDetails.EndDate;
            patientDiseaseToUpdate.Note = patientDiseaseDetails.Notes;
            patientDiseaseToUpdate.UserId = _unitOfWork.Users.GetAll().First(u => u.LastName == patientDiseaseDetails.Doctor).Id;

            await _unitOfWork.Save();
            return patientDiseaseToUpdate;
        }

        /// <summary>
        /// Sets PatientDisease instance's IsDeleted property to TRUE
        /// Throws ArgumentNullException if no instance with such ID found
        /// </summary>
        /// <param name="id">Id of PatientDisease instance to be deleted</param>        
        public async Task DeletePatientDiseaseAsync(int id)
        {
            var patientDiseaseToDelete = await _unitOfWork.PatientDiseases.Get(id);

            if (patientDiseaseToDelete == null)
            {
                throw new ArgumentException("No entry with such ID.");
            }

            _unitOfWork.PatientDiseases.Delete(patientDiseaseToDelete);
            await _unitOfWork.Save();
        }
    }
}
