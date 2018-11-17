using EHospital.Diseases.Model;
using Microsoft.EntityFrameworkCore;


namespace EHospital.Diseases.Data
{
    public class DiseaseDBContext : DbContext
    {
        public DiseaseDBContext()
        {
        }

        public DiseaseDBContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Disease> Diseases { get; set; }        

        public virtual DbSet<DiseaseCategory> DiseaseCategories{ get; set; }

        public virtual DbSet<PatientDisease> PatientDiseases { get; set; }

        public virtual DbSet<PatientInfo> Patients { get; set; }

        public virtual DbSet<UserData> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-PU90CNF;Database=EHospitalDB;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}
