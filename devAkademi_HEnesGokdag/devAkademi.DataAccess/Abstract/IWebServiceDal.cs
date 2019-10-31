using System;
using System.Collections.Generic;
using System.Text;

namespace devAkademi.DataAccess.Abstract
{
  public  interface IWebServiceDal
    {
        //interface tasarımı
         int AuctionSize();
        int AuctionsCount();
        Auction LoadJsonFindById(int id);
        List<Auction> LoadJsonsPagination(int offset, int size);
        List<Category> LoadJsonCategories();
        List<Auction> LoadAuctionsByCategory(string category);
        List<Auction> MostClickedAuctions();
        List<Auction> SearchAuction(string title);
    }
}
