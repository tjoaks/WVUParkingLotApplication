using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models
{
    public interface IApplicationUserRepo
    {
        List<ApplicationUser> ListApplicationUsers();

        List<WVUEmployee> ListWvuEmployees();
        string FindUserID();
        WVUEmployee FindWvuEmployee(string wvuEmployeeID);
    }
}
