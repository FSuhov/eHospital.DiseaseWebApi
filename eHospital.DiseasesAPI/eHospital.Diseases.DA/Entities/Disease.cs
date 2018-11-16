using System.ComponentModel.DataAnnotations;

namespace eHospital.Diseases.DA.Entities
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }

        [Required(ErrorMessage = "Disease must have name!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid length of disease name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Disease must have category!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Disease must have description!")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
