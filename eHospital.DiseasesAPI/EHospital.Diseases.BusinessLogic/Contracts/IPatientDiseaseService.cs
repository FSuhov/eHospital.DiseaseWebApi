using EHospital.Diseases.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IPatientDiseaseService
    {
        IQueryable<Disease> GetDiseaseByPatient(int patientId);

        PatientDisease GetPatientDisease(int patientDiseaseId);

        IQueryable<PatientDisease> GetPatientDiseasesByPatient(int patientId);

        Task<PatientDisease> AddPatientDiseaseAsync(PatientDisease patientDisease);        

        IEnumerable<PatientDiseaseInfo> GetPatientDiseasesInfos(int patientId);

        PatientDiseaseDetails GetPatientDiseaseDetailes(int patientDiseaseId);

        Task<PatientDisease> UpdatePatientDiseaseAsync(int id, PatientDiseaseDetails patientDiseaseDetails);

        Task DeletePatientDiseaseAsync(int id);
    }
}
