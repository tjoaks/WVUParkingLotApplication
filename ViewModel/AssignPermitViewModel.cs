using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.ViewModel
{
    public class AssignPermitViewModel
    {
        public int LotID { get; set; }
        public string WvuEmployeeID { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}