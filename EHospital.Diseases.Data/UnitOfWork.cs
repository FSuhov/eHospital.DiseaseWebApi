using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EHospital.Diseases.Model;
using Microsoft.EntityFrameworkCore;

namespace EHospital.Diseases.Data
{
    public class UnitOfWork : IUniteOfWork
    {
        private static DiseaseDBContext _context;

        private readonly Lazy<Repository<Disease>> _diseases 
                        = new Lazy<Repository<Disease>>(() => new Repository<Disease>(_context));
        private readonly Lazy<Repository<PatientInfo>> _patients
                        = new Lazy<Repository<PatientInfo>>(() => new Repository<PatientInfo>(_context));
        private readonly Lazy<Repository<DiseaseCategory>> _categories
                        = new Lazy<Repository<DiseaseCategory>>(() => new Repository<DiseaseCategory>(_context));
        private readonly Lazy<Repository<UsersData>> _users
                        = new Lazy<Repository<UsersData>>(() => new Repository<UsersData>(_context));
        private readonly Lazy<Repository<DiseaseCategory>> _diseaseCategories
                        = new Lazy<Repository<DiseaseCategory>>(() => new Repository<DiseaseCategory>(_context));
        private readonly Lazy<Repository<PatientDisease>> _patientDiseases
                        = new Lazy<Repository<PatientDisease>>(() => new Repository<PatientDisease>(_context));

        private bool _disposed = false;

        public UnitOfWork(DiseaseDBContext context)
        {
            _context = context;
        }

        public IRepository<Disease> Diseases
        {
            get => _diseases.Value;
        }

        public IRepository<DiseaseCategory> Categories
        {
            get => _categories.Value;
        }

        public IRepository<PatientInfo> Patients
        {
            get => _patients.Value;
        }

        public IRepository<UsersData> Users
        {
            get => _users.Value;
        }

        public IRepository<DiseaseCategory> DiseaseCategories
        {
            get => _diseaseCategories.Value;
        }

        public IRepository<PatientDisease> PatientDiseases
        {
            get => _patientDiseases.Value;
        }

        public void CascadeDeletePatientDisease(int id)
        {
            var procId = new SqlParameter("@Id", id);
            _context.Database.ExecuteSqlCommand("CascadeDeletePatientDisease @Id", parameters: procId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
