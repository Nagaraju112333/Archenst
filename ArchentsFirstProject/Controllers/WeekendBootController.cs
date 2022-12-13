using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using ArchentsFirstProject.Models;
namespace ArchentsFirstProject.Controllers
{
    public class WeekendBootController : Controller
    {
        // GET: WeekendBoot
        public ActionResult Index()
        {
            return View();
        }
      
        ArchentsEntities5 db = new ArchentsEntities5();
        [HttpGet]
        public ActionResult WeekendBootMainPage()
        {
            ViewBag.data = db.Products1.Where(x => x.CategoryID == 800).ToList();
            return View();
        }
   
        [HttpPost]
        public   JsonResult WeekendBootMainPage(int Itemid)
        {
            List<Products1> list = new List<Products1>();
            Products1 list1 = new Products1();
          if (Session["CardCounter"] != null)
            {
                list= Session["CardCounter"] as List<Products1>;
            }
                var result = db.Products1.SingleOrDefault(x => x.Product_Id == Itemid);
            if (list.Any(x => x.Product_Id== Itemid))
            {
                list1 = list.Single(x => x.Product_Id == Itemid);   
            }
            else
            {
                list1.Product_Id = Itemid;
                list1.Product_Name = result.Product_Name;
                list1.Price= result.Price;
               list.Add(list1);
            }
            Session["CardCounter"] = list.Count;
            Session["CardItem"] = list;
            return Json(data:new { success=true,Counter=list.Count}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Weekenboot()
        {
            return View();
        }

      
    }
}