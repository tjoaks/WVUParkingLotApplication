using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models.LotModel
{
    public interface ILotRepo
    {
        string GetSpotsForAllLots();
        List<Lot> ListAllLots();
        Task AddLot(Lot lot);
        Task EditLot(Lot lot);
        Lot FindLot(int? lotID);
        Task DeleteLot(Lot lot);

        bool IsChosenLotAvailable(int chosenLotID);

    }
}
