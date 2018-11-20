using System;
using System.ComponentModel.DataAnnotations;


namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represents a model of User as stored in Database
    /// </summary>
    public class UsersData : ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a Unique number to identify the book and store in the Database
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets User's Name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets User's Surname 
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets User's Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets User's City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets User's street address
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Gets or sets User's date of birth
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets User's contact phone number
        /// </summary>
        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets User's gender
        /// </summary>
        public byte Gender { get; set; }

        /// <summary>
        /// Gets or sets User's Email address
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Marks this entry as deleted (if set to TRUE) so it will not be included in future responses
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
