using System;
using System.ComponentModel.DataAnnotations;

namespace EHospital.Diseases.Model
{
    public class PatientDisease : ISoftDeletable
    {
        [Key]
        public int PatientDiseaseId { get; set; }

        public int PatientId { get; set; }

        public int DiseaseId { get; set; }

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Note { get; set; }

        public bool IsDeleted { get; set; }
    }
}
