using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devAkademi_HEnesGokdag.Models
{
    public class AuctionModel
    {
        public long id { get; set; }
        public string title { get; set; }

        public string description { get; set; }

        public double price { get; set; }

        public int has_promotion { get; set; }

        public int view_count { get; set; }

        public string city { get; set; }

        public string town { get; set; }

        public string c0 { get; set; }

        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string c6 { get; set; }
    }
}
