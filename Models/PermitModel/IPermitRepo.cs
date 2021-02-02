using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.Models.PermitModel
{
   public interface IPermitRepo
    {
       bool DoesWvuEmployeeHavePermit(string wvuEmployeeID);

        Task AddPermit (Permit permit);

        List<Permit> ListAllPermits();
    }
}
