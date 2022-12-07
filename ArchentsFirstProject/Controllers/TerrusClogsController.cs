using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchentsFirstProject.Models;
namespace ArchentsFirstProject.Controllers
{
    public class TerrusClogsController : Controller
    {
        private ArchentsEntities6 db=new ArchentsEntities6();
        // GET: TerrusClogs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TerrusClogs()
        {
            ViewBag.data= db.Products1.Where(x => x.CategoryID == 801).ToList();
            return View();
        }
    }
}