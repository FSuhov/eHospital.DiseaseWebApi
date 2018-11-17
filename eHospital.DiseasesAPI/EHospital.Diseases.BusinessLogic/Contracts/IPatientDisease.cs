using EHospital.Diseases.Model;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.BusinessLogic.Contracts
{
    public interface IPatientDisease
    {
        IQueryable<Disease> GetDiseaseByPatient(int patientId);

        PatientDisease GetPatientDisease(int id);

        Task<PatientDisease> AddPatientDisease(PatientDisease patientDisease);

        Task<PatientDisease> UpdatePatientDisease(int id, PatientDisease patientDisease);
    }
}
