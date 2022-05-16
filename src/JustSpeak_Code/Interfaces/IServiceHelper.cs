using JustSpeak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustSpeak.Interfaces
{
   public interface IServiceHelper
    {
        List<EmpLegalNames> GetEmpLegalNames();
    }
}
