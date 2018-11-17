using System.ComponentModel.DataAnnotations;

namespace EHospital.Diseases.Model
{
    public class Disease : ISoftDeletable
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
