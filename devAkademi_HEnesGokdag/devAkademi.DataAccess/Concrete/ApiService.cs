using devAkademi.DataAccess.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace devAkademi.DataAccess.Concrete
{
  public  class ApiService : IWebServiceDal
    {

        
        public int AuctionSize()
        {
            return 50;
        }
        //toplam ilan sayısını dönen fonksiyon
        public int AuctionsCount()
        {
            int count = 0;
            List<Auction> auctions = new List<Auction>();
            for (int i = 0; i < 40; i++)
            {

                List<Auction> auctionsTemp = new List<Auction>();
                string url = "https://devakademi.sahibinden.com/api/auctions?offset=" + i + "&size=50";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
                webRequest.Method = "Get";
                HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                    auctions = JsonConvert.DeserializeObject<List<Auction>>(jsonString);
                    foreach (var auction in auctionsTemp)
                    {

                        auctions.Add(auction);

                    }
                    count += auctions.Count;
                }
            }
            return count;
        }
        //detay sayfası için idye göre tek bir ilan dönen fonksiyon
        public Auction LoadJsonFindById(int id)
        {
            string url = "https://devakademi.sahibinden.com/api/auction?id=" + id;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webRequest.Method = "Get";
            HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

            string jsonString;

            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            Auction auction = JsonConvert.DeserializeObject<Auction>(jsonString);
            return auction;

        }
        //pagination için kullanılan fonksiyon
        public List<Auction> LoadJsonsPagination(int offset, int size)
        {
            List<Auction> auctions = new List<Auction>();
            string url = "https://devakademi.sahibinden.com/api/auctions?offset=" + offset * size + "&size=" + size;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
            webRequest.Method = "Get";
            HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
                auctions = JsonConvert.DeserializeObject<List<Auction>>(jsonString);
            }
            return auctions;
        }
        //ilan verilerinin içinde kaç farklı c0(kategori) olduğunu bulan ve bunları Category'ye kaydeden fonksiyon
        public List<Category> LoadJsonCategories()
        {
            List<Category> categories = new List<Category>();
            List<string> categoryNames = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                List<Auction> auctions = new List<Auction>();
                string url = "https://devakademi.sahibinden.com/api/auctions?offset=" + i + "&size=50";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
                webRequest.Method = "Get";
                HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                    auctions = JsonConvert.DeserializeObject<List<Auction>>(jsonString);
                    foreach (var auction in auctions)
                    {
                        if (!categoryNames.Contains(auction.c0))
                        {

                            categoryNames.Add(auction.c0);
                        }
                    }

                }
            }
            categoryNames.Sort();
            foreach (var item in categoryNames)
            {
                Category category = new Category();
                category.CategoryName = item;
                category.Selected = false;
                categories.Add(category);
            }
            return categories;
        }
        //gelen kategori adına göre bu kategoriyle ilişkili ilanları getiren fonksiyon
        public List<Auction> LoadAuctionsByCategory(string category)
        {
            int size = 50;
            List<Auction> auctionsByCategory = new List<Auction>();
            for (int i = 0; i < 40; i++)
            {
                List<Auction> auctions = new List<Auction>();
                string url = "https://devakademi.sahibinden.com/api/auctions?offset=" + i * size + "&size=50";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
                webRequest.Method = "Get";
                HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                    auctions = JsonConvert.DeserializeObject<List<Auction>>(jsonString);
                    foreach (var auction in auctions)
                    {
                        if (auction.c0 == category)
                        {
                            auctionsByCategory.Add(auction);
                        }
                    }

                }
            }
            return auctionsByCategory;
        }
        //en çok tıklanan 10 ilanı döndüren fonk
        public List<Auction> MostClickedAuctions()
        {
            int size = 50;
            List<Auction> mostClickedAuctions = new List<Auction>();
            for (int i = 0; i < 40; i++)
            {
                List<Auction> auctions = new List<Auction>();
                string url = "https://devakademi.sahibinden.com/api/auctions?offset=" + i * size + "&size=50";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
                webRequest.Method = "Get";
                HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                    auctions = JsonConvert.DeserializeObject<List<Auction>>(jsonString);
                    foreach (var auction in auctions)
                    {

                        mostClickedAuctions.Add(auction);


                    }

                }
            }
            mostClickedAuctions = mostClickedAuctions.OrderByDescending(t => t.view_count).Take(10).ToList();

            return mostClickedAuctions;
        }
        //gelen string bilgisini title ve description'da  arayıp bağlantılı ilanları döndüren fonk.
        public List<Auction> SearchAuction(string searchedWord)
        {
            int size = 50;
            List<Auction> searchedAuctions = new List<Auction>();
            for (int i = 0; i < 40; i++)
            {
                List<Auction> auctions = new List<Auction>();
                string url = "https://devakademi.sahibinden.com/api/auctions?offset=" + i * size + "&size=50";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(url));
                webRequest.Method = "Get";
                HttpWebResponse WebResp = (HttpWebResponse)webRequest.GetResponse();

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                    auctions = JsonConvert.DeserializeObject<List<Auction>>(jsonString);
                    foreach (var auction in auctions)
                    {

                        if(auction.title.Contains(searchedWord))
                        {
                            searchedAuctions.Add(auction);
                        }
                        else if(auction.description.Contains(searchedWord))
                        {
                            searchedAuctions.Add(auction);
                        }

                    }

                }
            }
           

            return searchedAuctions;
        }
    }
}
