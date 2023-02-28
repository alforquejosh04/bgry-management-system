using ProjectBasedSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Interfaces.Nationality
{
    public interface INationality :IDisposable
    {
        IEnumerable<nationality> getNationality();
    }
}