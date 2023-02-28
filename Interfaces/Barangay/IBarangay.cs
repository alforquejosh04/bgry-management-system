using ProjectBasedSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Interfaces.Barangay
{
    public interface IBarangay : IDisposable
    {
        IEnumerable<barangay> GetBarangay();
    }
}