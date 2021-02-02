using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.Models.LotStatusModel
{
    public interface ILotStatusRepo
    {
        double FindPermitAmount(int lotID, int lotTypeID);
    }
}