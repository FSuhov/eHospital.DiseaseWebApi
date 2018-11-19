using EHospital.Diseases.BusinessLogic.Contracts;
using EHospital.Diseases.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EHospital.Diseases.BusinessLogic.Services
{
    public class PatientDiseaseService : IPatientDiseaseService
    {
        private readonly IUniteOfWork _unitOfWork;

        public PatientDiseaseService(IUniteOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Disease> GetDiseaseByPatient(int patientId)
        {
            var diseasesOfPatient = from diseases in _unitOfWork.PatientDiseases.GetAll()
                                    where diseases.PatientId == patientId
                                    join disease in _unitOfWork.Diseases.GetAll()
                                    on diseases.DiseaseId equals disease.DiseaseId
                                    select disease;

            return diseasesOfPatient.OrderBy(d => d.Name);
        }

        public IQueryable<PatientDisease> GetPatientDiseasesByPatient(int patientId)
        {
            var patientDiseases = _unitOfWork.PatientDiseases.GetAll().Where(d => d.PatientId == patientId);

            return patientDiseases;
        }

        public PatientDisease GetPatientDisease(int patientDiseaseId)
        {
            var patientDisease = _unitOfWork.PatientDiseases.Get(patientDiseaseId);

            return patientDisease;
        }

        public async Task<PatientDisease> AddPatientDiseaseAsync(PatientDisease patientDisease)
        {
            PatientDisease patientDiseaseInserted = _unitOfWork.PatientDiseases.Insert(patientDisease);
            await _unitOfWork.Save();

            return patientDiseaseInserted;
        }

        public async Task<PatientDisease> UpdatePatientDiseaseAsync(int patientDiseaseId, PatientDisease patientDisease)
        {
            var patientDiseaseUpdated = _unitOfWork.PatientDiseases.Get(patientDiseaseId);
            if (patientDiseaseUpdated == null)
            {
                throw new ArgumentNullException("No such record");
            }

            _unitOfWork.PatientDiseases.Update(patientDiseaseUpdated);
            await _unitOfWork.Save();
            return patientDiseaseUpdated;
        }

        public IEnumerable<PatientDiseaseInfo> GetPatientDiseaseInfos(int patientId)
        {

            var infos = from pd in _unitOfWork.PatientDiseases.GetAll()
                        where pd.PatientId == patientId
                        join dis in _unitOfWork.Diseases.GetAll()
                        on pd.DiseaseId equals dis.DiseaseId
                        join cat in _unitOfWork.Categories.GetAll()
                        on dis.CategoryId equals cat.CategoryId
                        join user in _unitOfWork.Users.GetAll()
                        on pd.UserId equals user.UserId
                        select new PatientDiseaseInfo
                        {
                            Id = pd.PatientDiseaseId,
                            Name = dis.Name,
                            StartDate = pd.StartDate,
                            IsCurrent = (pd.EndDate == null),
                            CategoryName = cat.Name,
                            Doctor = user.LastName
                        };

            return infos;
        } 


        public DiseaseCategory GetCategory(int diseaseId)
        {
            var category = (from categories in _unitOfWork.Categories.GetAll()
                            join diseases in _unitOfWork.Diseases.GetAll()
                            on categories.CategoryId equals diseases.CategoryId
                            select categories).First();
            return category;
        }

        public Disease GetDisease(int diseaseId)
        {
            return _unitOfWork.Diseases.Get(diseaseId);
        }

        public UsersData GetDoctor(int userId)
        {
            return _unitOfWork.Users.Get(userId);
        }
    }
}
