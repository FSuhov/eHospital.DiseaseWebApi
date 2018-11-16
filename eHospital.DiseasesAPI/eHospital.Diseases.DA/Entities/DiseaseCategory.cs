using System.ComponentModel.DataAnnotations;

namespace eHospital.Diseases.DA.Entities
{
    public class DiseaseCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Disease category must have name!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid length of disease category name!")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
