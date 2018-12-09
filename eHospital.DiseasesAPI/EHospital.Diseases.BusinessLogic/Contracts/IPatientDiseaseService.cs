using EHospital.Diseases.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IPatientDiseaseService
    {
        Task<IEnumerable<Disease>> GetDiseaseByPatient(int patientId);

        Task <PatientDisease> GetPatientDisease(int patientDiseaseId);

        Task <IEnumerable<PatientDisease>> GetPatientDiseasesByPatient(int patientId);

        Task<PatientDisease> AddPatientDiseaseAsync(PatientDisease patientDisease);        

        Task<IEnumerable<PatientDiseaseInfo>> GetPatientDiseasesInfos(int patientId);

        Task <PatientDiseaseDetails> GetPatientDiseaseDetailes(int patientDiseaseId);

        Task<PatientDisease> UpdatePatientDiseaseAsync(int id, PatientDiseaseDetails patientDiseaseDetails);

        Task DeletePatientDiseaseAsync(int id);
    }
}
