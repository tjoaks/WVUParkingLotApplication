using DiscussionMVCAppOaks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.Models.LotStatusModel
{
    public class LotStatusRepo : ILotStatusRepo
    {
        private ApplicationDbContext database;

        public LotStatusRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public double FindPermitAmount(int lotID, int lotTypeID)
        {
            LotStatus lotStatus = database.LotStatuses.Where(ls => ls.LotID == lotID && ls.LotTypeID == lotTypeID).FirstOrDefault();

            return lotStatus.ParkingAmount;
        }
    }
}
