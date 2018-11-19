using System;
using System.Threading.Tasks;

namespace EHospital.Diseases.Model
{
    public interface IUniteOfWork : IDisposable
    {
        IRepository<Disease> Diseases { get; }

        IRepository<PatientInfo> Patients { get; }

        IRepository<UsersData> Users { get; }

        IRepository<DiseaseCategory> Categories { get; }

        IRepository<DiseaseCategory> DiseaseCategories { get; }

        IRepository<PatientDisease> PatientDiseases { get; }

        void CascadeDeletePatientDisease(int id);

        Task Save();
    }
}
