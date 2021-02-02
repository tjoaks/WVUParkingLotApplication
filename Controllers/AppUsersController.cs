using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Models;
using DiscussionMVCAppOaks.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Controllers
{
    public class AppUsersController : Controller
    {
        private IApplicationUserRepo iApplicationUserRepo;

        public AppUsersController(IApplicationUserRepo applicationUserRepo)
        {
            iApplicationUserRepo = applicationUserRepo;
        }

        [HttpGet]
        public IActionResult SearchAppUsers()
        {
            SearchAppUsersViewModel viewModel = new SearchAppUsersViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchAppUsers(SearchAppUsersViewModel viewModel)
        {
            List<ApplicationUser> applicationUserList =
                iApplicationUserRepo.ListApplicationUsers();

            if (viewModel.appUserType != null)
            {
                applicationUserList =
                    applicationUserList.Where(a => a.GetType().Name == viewModel.appUserType).ToList();
            }

            viewModel.ApplicationUsersList = applicationUserList;
            return View();
        }
    }
}
