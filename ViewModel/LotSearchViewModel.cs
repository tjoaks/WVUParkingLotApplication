using DiscussionMVCAppOaks.Models.LotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;

namespace DiscussionMVCAppOaks.ViewModel
{
    public class LotSearchViewModel
    {
        public bool IsLotCurrentlyAvailable { get; set; }
        public int? LotTypeID { get; set; }
        public string TypeOfDay { get; set; }

        public int InputPageSize { get; set; }
      //  public List<Lot> LotSearchResult { get; set; }

        public PagedResult<Lot> LotSearchResult { get; set; }

        public LotSearchViewModel()
        {
            this.LotSearchResult = new PagedResult<Lot>();
        }

    }
}