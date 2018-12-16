using System;
using System.Collections.Generic;
using System.Text;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represents a extended User-Friendly View of specific PatientDisease entry of particular Patient
    /// with reference ID's replaced by Names (text values)
    /// </summary>
    public class PatientDiseaseDetails
    {
        /// <summary>
        /// Gets or Sets Unique integer identifier of entry
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Disease
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Name of Category
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or Sets Description of Disease
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets start date of Disease
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or Sets end date of Disease
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or Sets Doctor's Note for this entry
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or Sets Doctor's LastName for this entry
        /// </summary>
        public string Doctor { get; set; }
    }
}
