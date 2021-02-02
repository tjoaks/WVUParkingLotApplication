using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models.PermitModel
{
    public class Permit
    {
        [Key]
        public int PermitID { get; set; }

        [Required]
        public double PermitAmount { get; set; }

        [Required]
        public DateTime PermitStartDate { get; set; }

        [Required]
        public DateTime PermitEndDate { get; set; }

        [Required]
        public string WvuEmployeeID { get; set; }

        [ForeignKey("WvuEmployeeID")]
        public WVUEmployee WVUEmployee { get; set; }

        public string ParkingEmployeeWhoAssignedPermitID { get; set; }

        [NotMapped]
        public WVUEmployee ParkingEmployeeWhoAssignedPermit { get; set; }

        public DateTime DateTimeWhenParkingPermitWasAssigned { get; set; }

        public Permit(double permitAmount, DateTime permitStartDate,
            DateTime permitEndDate, string wvuEmployeeID, string parkingEmployeeID)
        {
            this.PermitAmount = permitAmount;
            this.PermitStartDate = permitStartDate;
            this.PermitEndDate = permitEndDate;
            this.WvuEmployeeID = wvuEmployeeID;
            this.DateTimeWhenParkingPermitWasAssigned = DateTime.Now;
            this.ParkingEmployeeWhoAssignedPermitID = parkingEmployeeID;
        }
        public Permit() { }
    }
}

