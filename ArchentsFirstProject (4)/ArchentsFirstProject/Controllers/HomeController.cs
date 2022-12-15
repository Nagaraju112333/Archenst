using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using ArchentsFirstProject.Models;

namespace ArchentsFirstProject.Controllers
{
    public class HomeController : Controller
    {
        ArchentsEntities6 db = new ArchentsEntities6();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {
                var result1 = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                var data = db.ShopingCartModels.Where(x => x.UserId == result1.RegisterId).ToList();
                Session["CartCount"] = data.Count;
            }
           
            return View();
        }
        public ActionResult Home( int s)
        {
            return View();
        }
        
       
    }
}