using eHospital.Diseases.DA.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace eHospital.Diseases.Domain.Contracts
{
    public interface IPatientDisease
    {
        IQueryable<Disease> GetDiseaseByPatient(int patientId);

        PatientDisease GetPatientDisease(int id);

        Task<PatientDisease> AddPatientDisease(PatientDisease patientDisease);

        Task<PatientDisease> UpdatePatientDisease(int id, PatientDisease patientDisease);
    }
}
