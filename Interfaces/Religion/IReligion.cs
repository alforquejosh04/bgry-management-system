using ProjectBasedSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Interfaces.Religion
{
    public interface IReligion : IDisposable
    {
        IEnumerable<religion> GetReligion();
    }
}