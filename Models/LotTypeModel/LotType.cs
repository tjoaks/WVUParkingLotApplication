using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models.LotTypeModel
{
    public class LotType
    {
        //attributes (instance variables): private
        //MVC requires Properties
        [Key]
        public int LotTypeID { get; set; }

        [Required]
        public string LotTypeName { get; set; }

        [NotMapped]
        public List<LotStatus> LotStatuses { get; set; }

        //create a constructor

        public LotType(string lotTypeName)
        {
            this.LotTypeName = lotTypeName;

        }

        public LotType() { }
    }
}

