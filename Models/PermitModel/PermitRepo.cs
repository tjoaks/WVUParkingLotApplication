using DiscussionMVCAppOaks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.Models.PermitModel
{
    public class PermitRepo : IPermitRepo
    {
        private ApplicationDbContext database;

        public PermitRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public Task AddPermit(Permit permit)
        {
            throw new NotImplementedException();
        }

        public bool DoesWvuEmployeeHavePermit(string wvuEmployeeID)
        {
            bool hasPermit;
            Permit permit = database.Permits.Where(p => p.WvuEmployeeID == wvuEmployeeID).FirstOrDefault();

            if (permit == null)
            {
                hasPermit = false;
            }
            else
            {
                hasPermit = true;
            }

            return hasPermit;


        }

        public List<Permit> ListAllPermits()
        {
            throw new NotImplementedException();
        }
    }
}
