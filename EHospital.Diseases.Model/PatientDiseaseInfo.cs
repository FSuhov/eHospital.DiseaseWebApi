using System;
using System.Collections.Generic;
using System.Text;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Represents a detailed View of PatientDisease of selected patient
    /// with reference ID's replaced by Names (text values).
    /// To be used in PatientDisease User Story 2.
    /// </summary>
    public class PatientDiseaseInfo
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
        /// Gets or Sets start date of Disease
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets Status of Disease, or Sets it evaluating the existing EndDate of PatientDisease entry
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Gets or Sets Doctor's LastName for this entry
        /// </summary>
        public string Doctor { get; set; }
    }
}
