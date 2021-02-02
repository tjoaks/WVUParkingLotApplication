using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.ViewModel
{
    public class DistanceMatrixViewModel
    {
        public string WvuEmployeeID { get; set; }
        public int LotID { get; set; }
        public string LotNumber { get; set; }
        public string LotName { get; set; }
        public string LotAddress { get; set; }
        public double DistanceInMiles { get; set; }
        public int DurationInMinutes { get; set; }
        public int AvailableSpots { get; set; }
        public List<DistanceMatrixViewModel> DistanceMatrix { get; set; }
    }
}
