using System.ComponentModel.DataAnnotations;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represent an entry in Diseases table of Database
    /// </summary>
    public class Disease : ISoftDeletable
    {
        /// <summary>
        /// Gets or Sets Unique integer identifier of entry
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Disease
        /// </summary>
        [Required(ErrorMessage = "Disease must have name!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid length of disease name!")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Reference Id for Category - Foreign Key
        /// </summary>
        [Required(ErrorMessage = "Disease must have category!")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or Sets Description of Disease
        /// </summary>
        [Required(ErrorMessage = "Disease must have description!")]
        public string Description { get; set; }

        /// <summary>
        /// Marks this entry as deleted (if set to TRUE) so it will not be included in future responses
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
