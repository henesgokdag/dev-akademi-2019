using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using devAkademi_HEnesGokdag.Models;
using devAkademi.DataAccess;
using devAkademi.DataAccess.Abstract;

namespace devAkademi_HEnesGokdag.Controllers
{
    public class HomeController : Controller
    {
        private IWebServiceDal _read;
        public HomeController(IWebServiceDal read)
        {
            _read = read;
        }
         
        public IActionResult Index()
        {
            List<Auction> auctions = _read.LoadJsonsPagination(0, _read.AuctionSize());
            return View(new AuctionListModel()
            {
                Auctions = auctions
            });
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AuctionDetails(int id)
        {
            //id değerine göre ilan detayına ulaştığımız action
          Auction auction=  _read.LoadJsonFindById(id);
            if(auction==null)
            {
                return RedirectToAction("Index");
            }
            return View(new AuctionModel() {
                id=auction.id,
                title= auction.title,
                description = auction.description,
                price = auction.price,
                has_promotion = auction.has_promotion,
                view_count = auction.view_count,
                city= auction.city,
                town = auction.town,
                c0 = auction.c0,
                c1= auction.c1,
                c2 = auction.c2,
                c3 = auction.c3,
                c4 = auction.c4,
                c5 = auction.c5,
                c6 = auction.c6
                
            });
        }
        public IActionResult AuctionList(int id=0)
        {
            //sayfa bilgisi değerine göre ilan listesine ulaştığımız action
            int count = _read.AuctionsCount();
            int size = _read.AuctionSize();
            
            ViewBag.id = id;
            int pagesCount = count / size;
            int[] pages = new int[pagesCount+1];
            for (int i = 1; i <= pagesCount+1; i++)
            {
                pages[i-1] = i;
            }
            List<Auction> auctions=new List<Auction>();
            ViewBag.pages = pages;
            if(id!=0)
            {
                 auctions = _read.LoadJsonsPagination(id-1, _read.AuctionSize());
            }
            
            if (id == 0)
            {
                ViewBag.id = 1;
                 auctions = _read.LoadJsonsPagination(id, _read.AuctionSize());
            }
               
            return View(new AuctionListModel() {
                Auctions= auctions
            });
        }
        public IActionResult AuctionsByCategory(string category)
        {
            //gelen kategori bilgisine göre o kategoriyle ilişkili ilanların gösterildiği action
            List<Auction> auctions = _read.LoadAuctionsByCategory(category);
            ViewBag.count = auctions.Count;
            return View(new AuctionListModel()
            {
                Auctions = auctions,
                
                SelectedCategory = category
            });
        }
        public IActionResult AuctionsMostClicked()
        {
            //en çok bakılan 10 ilanın gösterildiği action
            List<Auction> auctions = _read.MostClickedAuctions();
            return View(new AuctionListModel()
            {
                Auctions = auctions
            });
        }
        [HttpPost]
        public IActionResult AuctionSearch(string searchedWord)
        {
            //gelen string bilgiye göre ilişkili title ve descriptionların bulunduğu ilanların gösterildiği action
            List<Auction> auctions = _read.SearchAuction(searchedWord);
            ViewBag.count = auctions.Count;
            return View(new AuctionListModel()
            {
                Auctions = auctions
            });
        }
    }
}
