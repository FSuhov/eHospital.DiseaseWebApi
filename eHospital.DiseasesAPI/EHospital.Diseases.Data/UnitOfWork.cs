using EHospital.Diseases.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EHospital.Diseases.Data
{
    public class UnitOfWork : IUniteOfWork
    {
        private DiseaseDBContext _context = new DiseaseDBContext();

        private Repository<Disease> _diseases;
        private Repository<PatientInfo> _patients;
        private Repository<DiseaseCategory> _categories;
        private Repository<UsersData> _users;
        private Repository<DiseaseCategory> _diseaseCategories;
        private Repository<PatientDisease> _patientDiseases;

        private bool disposed = false;

        public IRepository<Disease> Diseases
        {
            get
            {
                if (_diseases == null)
                {
                    _diseases = new Repository<Disease>(_context);
                }

                return _diseases;
            }
        }

        public IRepository<DiseaseCategory> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new Repository<DiseaseCategory>(_context);
                }

                return _categories;
            }
        }

        public IRepository<PatientInfo> Patients
        {
            get
            {
                if (_patients == null)
                {
                    _patients = new Repository<PatientInfo>(_context);
                }

                return _patients;
            }
        }

        public IRepository<UsersData> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new Repository<UsersData>(_context);
                }

                return _users;
            }
        }

        public IRepository<DiseaseCategory> DiseaseCategories
        {
            get
            {
                if (_diseaseCategories == null)
                {
                    _diseaseCategories = new Repository<DiseaseCategory>(_context);
                }

                return _diseaseCategories;
            }
        }

        public IRepository<PatientDisease> PatientDiseases
        {
            get
            {
                if (_patientDiseases == null)
                {
                    _patientDiseases = new Repository<PatientDisease>(_context);
                }

                return _patientDiseases;
            }
        }

        public void CascadeDeletePatientDisease(int id)
        {
            var procId = new SqlParameter("@Id", id);
            _context.Database.ExecuteSqlCommand("CascadeDeletePatientAllergy @Id", parameters: procId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
