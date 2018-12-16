using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.WebAPI.ViewModels
{
    public class PatientDiseaseView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Doctor { get; set; }
    }
}
