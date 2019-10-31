using devAkademi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devAkademi_HEnesGokdag.Models
{
    public class AuctionListModel
    {
      public  List<Auction> Auctions { get; set; }
      public string SelectedCategory { get; set; }
    }
}
