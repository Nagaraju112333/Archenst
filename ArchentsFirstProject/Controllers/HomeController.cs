using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
namespace ArchentsFirstProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Home( int s)
        {
            return View();
        }
        
       
    }
}