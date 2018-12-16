using System.ComponentModel.DataAnnotations;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represent an entry in DiseasesCategory table of Database
    /// </summary>
    public class DiseaseCategory : ISoftDeletable
    {
        /// <summary>
        /// Gets or Sets Unique integer identifier of entry
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Category
        /// </summary>
        [Required(ErrorMessage = "Disease category must have name!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid length of disease category name!")]
        public string Name { get; set; }

        /// <summary>
        /// Marks this entry as deleted (if set to TRUE) so it will not be included in future responses
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
