using ProjectBasedSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Interfaces.Residential
{
    public interface IBrgyResedential : IDisposable
    {
        IEnumerable<barangayResidential> GetResidentials();
        bool AddUpdate(barangayResidential brgyResident);

        IEnumerable<residentialImages> GetResidentialsImage();
        bool FileSave(residentialImages image); 


    }

}