using devAkademi.DataAccess;
using devAkademi.DataAccess.Abstract;
using devAkademi_HEnesGokdag.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devAkademi_HEnesGokdag.ViewComponents
{
    public class CategoryListViewComponent :ViewComponent
    {
        private IWebServiceDal _read;
       
        public CategoryListViewComponent(IWebServiceDal read)
        {
            _read = read;
        }
        public IViewComponentResult Invoke()
        {
            var queryString = Request.Query["category"];
            string s = RouteData.Values["category"]?.ToString();
            return View(new CategoryListModel()
            {
                SelectedCategory = queryString.ToString(),
                Categories = _read.LoadJsonCategories()
            });
        }
    }
}
