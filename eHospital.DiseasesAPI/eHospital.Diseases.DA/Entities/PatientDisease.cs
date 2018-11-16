using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHospital.Diseases.DA.Entities
{
    public class PatientDisease
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
