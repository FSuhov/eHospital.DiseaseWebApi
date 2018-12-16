using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHospital.Diseases.WebAPI.ViewModels
{
    /// <summary>
    /// Represents a simplified Disease view for using in the general lists of Diseases
    /// </summary>
    public class DiseaseView
    {
        /// <summary>
        /// Unique identifier of entry in Diseases table in Database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Disiease
        /// </summary>
        public string Name { get; set; }
    }
}
