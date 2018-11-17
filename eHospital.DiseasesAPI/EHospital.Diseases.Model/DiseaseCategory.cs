using System.ComponentModel.DataAnnotations;

namespace EHospital.Diseases.Model
{
    public class DiseaseCategory : ISoftDeletable
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Disease category must have name!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid length of disease category name!")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
