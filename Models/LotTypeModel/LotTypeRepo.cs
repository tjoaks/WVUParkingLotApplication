using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Data;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models.LotTypeModel
{
    public class LotTypeRepo : ILotTypeRepo
    {
        private ApplicationDbContext database;

        public LotTypeRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public List<LotType> ListAllLotTypes()
        {
            List<LotType> lotTypes =
                database.LotTypes.ToList();
            return lotTypes;
        }
    }
}
