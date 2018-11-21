using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represents a model of Patient as stored in Database
    /// </summary>
    public class PatientInfo : ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a Unique number to identify the book and store in the Database
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Patient Name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Patient Surname
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Patient's Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets Patient's City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets Patient's Street Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets Patient's Date of Birth
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or Sets Patient's Contact Phone Number
        /// </summary>
        [MaxLength(12)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets Patient's Gender
        /// </summary>
        public byte Gender { get; set; }

        /// <summary>
        /// Gets or Sets Patient's Email
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets Reference Id of Image of Patient
        /// </summary>
        [ForeignKey("Image")]
        public int ImageId { get; set; }

        /// <summary>
        /// Marks this entry as deleted (if set to TRUE) so it will not be included in future responses
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
