using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Models.LotModel;
using DiscussionMVCAppOaks.Models.LotTypeModel;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models
{
    public class LotStatus
    {
        //attributes (instance variables): private
        //MVC requires Properties
        [Key]
        public int LotStatusID { get; set; }

        [Required]
        public string TypeOfDay { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        public double ParkingAmount { get; set; }

        //relational database connection
        public int LotID { get; set; }

        public int LotTypeID { get; set; }

        [ForeignKey("LotID")]
        public Lot Lot { get; set; }

        [ForeignKey("LotTypeID")]
        public LotType LotType { get; set; }

        public LotStatus() { }

        public LotStatus(string typeOfDay, DateTime startTime, DateTime endTime, double parkingAmount, int lotID, int lotTypeID)
        {
            this.TypeOfDay = typeOfDay;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.ParkingAmount = parkingAmount;
            this.LotID = lotID;
            this.LotTypeID = lotTypeID;



        }
    }


}
