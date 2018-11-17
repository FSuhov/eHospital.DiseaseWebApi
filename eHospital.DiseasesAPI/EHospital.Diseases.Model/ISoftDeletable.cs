using System;
using System.Collections.Generic;
using System.Text;

namespace EHospital.Diseases.Model
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
