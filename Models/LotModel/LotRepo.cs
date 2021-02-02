using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiscussionMVCAppOaks.Models.LotModel
{
    public class LotRepo : ILotRepo
    {
        private ApplicationDbContext database;

        //Dependency Injection 

        public LotRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public Task AddLot(Lot lot)
        {
            database.Lots.AddAsync(lot);
            return database.SaveChangesAsync();
        }

        public Task DeleteLot(Lot lot)
        {
            database.Lots.Remove(lot);
            return database.SaveChangesAsync();
        }

        public Task EditLot(Lot lot)
        {
            database.Lots.Update(lot);
            return database.SaveChangesAsync();
        }

        public Lot FindLot(int? lotID)
        {
            Lot lot = database.Lots.Find(lotID);
            return lot;
        }

        public string GetSpotsForAllLots()
        {

            var resultList = database.Lots.ToList();
            string jsondata = JsonConvert.SerializeObject(resultList);
            return jsondata;
        }

        public bool IsChosenLotAvailable(int chosenLotID)
        {
            Lot lot = FindLot(chosenLotID);
                bool isAvailable = false;

            if (lot.TotalSpots > lot.CurrentlyOccupiedSpots)
            {
                isAvailable = true;
            }
            return isAvailable;
        }

        public List<Lot> ListAllLots()
        {
            List<Lot> lotList = database.Lots
                .Include(l => l.LotStatuses)
                .ThenInclude(ls => ls.LotType)
                .ToList<Lot>();

            return lotList;
        }
    }
}

