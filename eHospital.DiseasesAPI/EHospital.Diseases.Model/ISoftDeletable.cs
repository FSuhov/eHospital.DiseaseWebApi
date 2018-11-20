using System;
using System.Collections.Generic;
using System.Text;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Contains property that gets or sets IsDeleted status of entry
    /// </summary>
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
