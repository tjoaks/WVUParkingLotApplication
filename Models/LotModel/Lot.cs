using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models.LotModel
{
    public class Lot
    {

        [Key]
        public int LotID { get; set; }
        [Required]
        public string LotNumber { get; set; }
        [Required]
        public string LotName { get; set; }
        [Required]
        public string LotAddress { get; set; }
        [Required]
        public int CurrentlyOccupiedSpots { get; set; }
        [Required]
        public int TotalSpots { get; set; }
        public List<LotStatus> LotStatuses { get; set; }

        public Lot(string lotNumber, string lotName, string lotAddress, int totalSpots)
        {
            this.LotNumber = lotNumber;
            this.LotName = lotName;
            this.LotAddress = lotAddress;
            this.TotalSpots = totalSpots;
            this.CurrentlyOccupiedSpots = 0;
        }
        public Lot() { }
    }
}
