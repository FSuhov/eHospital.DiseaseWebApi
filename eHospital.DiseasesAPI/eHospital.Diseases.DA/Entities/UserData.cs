using System;
using System.ComponentModel.DataAnnotations;

namespace eHospital.Diseases.DA.Entities
{
    public class UserData
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        public byte Gender { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public bool IsDeleted { get; set; }

    }
}
