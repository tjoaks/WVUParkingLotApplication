using DiscussionMVCAppOaks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.ViewModel
{
    public class SearchAppUsersViewModel
    {
        public string appUserType { get; set; }

        public List<ApplicationUser> ApplicationUsersList { get; set; }
    }
}
