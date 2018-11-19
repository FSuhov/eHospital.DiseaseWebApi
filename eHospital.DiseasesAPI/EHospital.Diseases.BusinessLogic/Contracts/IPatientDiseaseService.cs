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

        Task<PatientDisease> UpdatePatientDiseaseAsync(int patientDiseaseId, PatientDisease patientDisease);

        IEnumerable<PatientDiseaseInfo> GetPatientDiseaseInfos(int patientId);
    }
}
