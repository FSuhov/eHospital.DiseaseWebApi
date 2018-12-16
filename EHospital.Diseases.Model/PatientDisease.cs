using System;
using System.ComponentModel.DataAnnotations;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represent an entry in PatientDisease table of Database
    /// </summary>
    public class PatientDisease : ISoftDeletable
    {
        /// <summary>
        /// Gets or Sets Unique integer identifier of entry
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets a Reference Id for Patient - Foreign Key
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or Sets a Reference Id for Disease - Foreign Key
        /// </summary>
        public int DiseaseId { get; set; }

        /// <summary>
        /// Gets or Sets a Reference Id for Doctor (User) - Foreign Key
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets start date of Disease
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or Sets end date of Disease
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or Sets Doctor's Note for this entry
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Marks this entry as deleted (if set to TRUE) so it will not be included in future responses
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
