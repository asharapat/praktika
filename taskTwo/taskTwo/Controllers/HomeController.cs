using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taskTwo.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace taskTwo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();         
        }
        public ActionResult Data()
        {   
            TestdbEntities entities = new TestdbEntities();
            //var newc = (from c1 in entities.Costs
            //            join c2 in entities.regions on c1.regionId equals c2.regionID
            //            join c3 in entities.products on c1.productId equals c3.productId

            //            select new DB()
            //            {
            //                costAmount1 = c1.costAmount,
            //                costMonth1 = c1.Month,
            //                costRegion = c2.region1,
            //                costProduct = c3.productName

            //            }
            //            ).ToList();
            var newd = (from t1 in entities.Supplies
                        from c1 in entities.Costs
                        where c1.regionId == t1.regionId 
                        where c1.Month == t1.Month
                        where c1.productId == t1.productId
                        join c2 in entities.regions on c1.regionId equals c2.regionID 
                        join c3 in entities.products on c1.productId equals c3.productId 
                       join t2 in entities.regions on t1.regionId equals t2.regionID
                       join t3 in entities.products on t1.productId equals t3.productId
                       join t4 in entities.Categories on t1.categoryId equals t4.categoryId
                       orderby t2.region1,t3.productName, t1.Month
                       select new DB()
                       {
                           supplyID = t1.SupplyId,
                           supplyAmount = t1.SupllyAmount,
                           supplyMonth = t1.Month,
                           supplyRegion = t2.region1,
                           supplyPName = t3.productName,
                           supplyCNumber = t4.categoryNumber,
                           costAmount1 = c1.costAmount,
                           costMonth1 = c1.Month   
                           //supplyRegionId = t1.regionId,
                           //supplyProductId = t1.productId,
                           //supplyCategoriesId = t1.categoryId
                           
                       }).ToList();
            ViewBag.Regions = entities.regions.ToList();
            ViewBag.Products = entities.products.ToList();
            ViewBag.Categories = entities.Categories.ToList();
            return View(newd);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}