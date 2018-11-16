using eHospital.Diseases.DA.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eHospital.Diseases.DA.Contracts
{
    public interface IUniteOfWork : IDisposable
    {
        IRepository<Disease> Diseases { get; }

        IRepository<PatientInfo> Patients { get; }

        IRepository<UserData> Users { get; }

        IRepository<DiseaseCategory> Categories { get; }

        IRepository<DiseaseCategory> DiseaseCategories { get; }

        IRepository<PatientDisease> PatientDiseases { get; }

        void CascadeDeletePatientDisease(int id);

        Task Save();
    }
}
