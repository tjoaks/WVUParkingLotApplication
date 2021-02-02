using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMVCAppOaks.Models
{
    public class ApplicationUserRepo : IApplicationUserRepo
    {
        private ApplicationDbContext database;
        private IHttpContextAccessor httpContextAccessor;

        public ApplicationUserRepo(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            this.database = dbContext;
        }

        public string FindUserID()
        {
            string userID = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userID;
        }

        public WVUEmployee FindWvuEmployee(string wvuEmployeeID)
        {
            WVUEmployee employee = database.WVUEmployees.Include(w => w.Department).Where(w => w.Id == wvuEmployeeID).FirstOrDefault();
            return employee;
        }

        public List<ApplicationUser> ListApplicationUsers()
        {
            List<ApplicationUser> applicationUser =
                database.ApplicationUsers.ToList<ApplicationUser>();
            return applicationUser;
        }

        public List<WVUEmployee> ListWvuEmployees()
        {
            List<WVUEmployee> wvuEmployees = database.WVUEmployees.Include(w => w.Department).ToList<WVUEmployee>();
            return wvuEmployees;
        }

    }
}
