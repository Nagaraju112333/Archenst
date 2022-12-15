using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using ArchentsFirstProject.Models;
using Microsoft.Ajax.Utilities;
namespace ArchentsFirstProject.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        ArchentsEntities7 db=new ArchentsEntities7();   
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllCartDetails()
        {
            ViewBag.data1 = db.ShopingCartModels.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult WeeknedBoots(int id)
        {

          
            // var result=User.Identity.GetUserId();
          
            ViewBag.size=GetSizes(id);
            ViewBag.data = db.Products1.FirstOrDefault(x => x.Product_Id == id);
            return View();
        }
        [HttpPost]
        public ActionResult WeeknedBoots()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Review(Review rev)
        {
            db.Reviews.Add(rev);
            db.SaveChanges();
            return RedirectToAction("WeeknedBoots");
        }
        public List<size> GetSizes(int id)
        {
            var result=db.sizes.Where(x=>x.ProductId==id).ToList();
           
            return result;
        }
        [HttpGet]
        public ActionResult ProductSave()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProductSave(string ItemId)
        {
            ShopingCartModel list1 = new ShopingCartModel();
            Products1 productsss = db.Products1.Single(model => model.Product_Id.ToString() == ItemId);
            if (productsss != null)
            {

                list1.ItemId = Convert.ToInt32(ItemId);
                list1.ItemName = productsss.Product_Name;
                list1.Description = productsss.product_Description;
                list1.price = productsss.Price;
                list1.Quantity = 1;
                list1.UserId = Convert.ToInt32(TempData["ID"]);
                list1.TotalPrice = Convert.ToInt32(productsss.Price);
                list1.UnitPrice = productsss.Price;
                list1.Imagepath = productsss.Image1;

                listofshopingcart.Add(list1);

                db.ShopingCartModels.Add(list1);
                db.SaveChanges();
            }
            return View(listofshopingcart);
        }

        //  private List<ShopingCartModel> listofshopingcart = new List<ShopingCartModel>();


        /*  public ActionResult ProductPost(string ItemId)
          {
              var result = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().RegisterId;
              ViewBag.registerid = result;
              TempData["ID"] = result;
              var result1 = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
              List<ShopingCartModel> list = new List<ShopingCartModel>();
              ShopingCartModel list1=new ShopingCartModel();
              Products1 productsss = db.Products1.Single(model => model.Product_Id.ToString() == ItemId);
              if(Session["CartCounter"] != null)
              {
                  list = db.ShopingCartModels.Where(x => x.UserId == result1.RegisterId).ToList();
              }
              else
              {

                  list1.ItemId = Convert.ToInt32(ItemId);
                  list1.ItemName = productsss.Product_Name;
                  list1.Description = productsss.product_Description;
                  list1.price = productsss.Price;
                  list1.Quantity = 1;
                  list1.UserId = Convert.ToInt32(TempData["ID"]);
                  list1.TotalPrice = Convert.ToInt32(productsss.Price);
                  list1.UnitPrice = productsss.Price;
                  list1.Imagepath = productsss.Image1;

                  listofshopingcart.Add(list1);

                  db.ShopingCartModels.Add(list1);
                  db.SaveChanges();
              }
              Session["CartCounter"] = listofshopingcart.Count;
              return View();

          }*/
        public ActionResult Values(int value)
        {
            TempData["name"] = value;
            ViewBag.size = value;
            return View();
        }
        private List<ShopingCartModel> listofshopingcart = new List<ShopingCartModel>();

        public ActionResult ProductPost(string ItemId,int SizeOfProducts=0)
        {

            var sizedata = Convert.ToInt32(SizeOfProducts);
            var size1= ViewBag.size;
          
            if (User.Identity.IsAuthenticated)
            {
                
                var result = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().RegisterId;
                ViewBag.registerid = result;
                TempData["ID"] = result;
                var result1 = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                ShopingCartModel list1 = new ShopingCartModel();
                var productsss = db.Products1.FirstOrDefault(model => model.Product_Id.ToString() == ItemId);
                /* if (Session["CardCounter"] != null)
                 {
                     listofshopingcart = Session["Carditgem"] as List<ShopingCartModel>;
                 }*/
                /* if (result1 != null)
                 {
                     listofshopingcart = db.ShopingCartModels.Where(x => x.UserId == result1.RegisterId).ToList();
                 }*/
                var sameitem = db.ShopingCartModels.Where(x => x.ItemId.ToString() == ItemId && x.UserId == result1.RegisterId).ToList();
                if (sameitem.Count > 0)
                {
                    list1 = db.ShopingCartModels.FirstOrDefault(model => model.ItemId.ToString() == ItemId);
                    list1.Quantity = list1.Quantity + 1;
                    list1.TotalPrice = list1.Quantity * list1.UnitPrice;
                    db.SaveChanges();
                }
                else
                {
                    list1.ItemId = Convert.ToInt32(ItemId);
                    list1.ItemName = productsss.Product_Name;
                    list1.Description = productsss.product_Description;
                    list1.price = productsss.Price;
                    list1.Quantity = 1;
                    list1.UserId = Convert.ToInt32(TempData["ID"]);
                    list1.TotalPrice = Convert.ToInt32(productsss.Price);
                    list1.UnitPrice = productsss.Price;
                    list1.Imagepath = productsss.Image1;
                    list1.Size = sizedata;
                    listofshopingcart.Add(list1);
                    db.ShopingCartModels.Add(list1);
                    db.SaveChanges();
                }
                /*   Session["CardCounter"] = listofshopingcart.Count;
                   Session["Carditgem"] = listofshopingcart;*/
                //return Json(data: new { success = true, Counter = listofshopingcart.Count }, JsonRequestBehavior.AllowGet);

                return RedirectToAction("shopingCartDetails");
            }
            else
            {
                
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult RemoveCartItem(int id)
        {
            ViewBag.id = id;
            ShopingCartModel result = db.ShopingCartModels.FirstOrDefault(x => x.CartId == id);
            db.ShopingCartModels.Remove(result);
            db.SaveChanges();
            return Redirect("/Products/shopingCartDetails");
            //return View();

          //  return Redirect("");
        }
        public ActionResult AddCartDetails(Products1 p1)
        {
            return View();
        }
        public  ActionResult Dynamiccart(int id)
        {
            ViewBag.data = db.Products1.FirstOrDefault(x => x.Product_Id == id);
            return View();
        }
        /*  public ActionResult shopingCartDetails()
          {
              ViewBag.data11 = db.ShopingCartModels.ToList();
              return View();

          }*/
      /*  [HttpGet]
        public Register GetEmployeeDetails(string username, string password)
        {
            var details = db.Registers.FirstOrDefault(x => x.Email == username && x.Password == password);
            var empdetails = db.ShopingCartModels.FirstOrDefault(x => x.UserId == details.RegisterId);
            return details;
        }*/
        public ActionResult shopingCartDetails()
        {
            var result = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().RegisterId;
            ViewBag.registerid = result;
            var result1 = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            TempData["ID"] = result1;
                List<ShopingCartModel> resultdata = new List<ShopingCartModel>();
            
               resultdata = db.ShopingCartModels.Where(x => x.UserId == result1.RegisterId).ToList();
               TempData["name"]=resultdata.Count;

            /*if (Session["Carditgem"] ==null)
            {

            *//* list = db.ShopingCartModels.ToList();*//*
                 ViewBag.cartdata = db.ShopingCartModels.ToList();
            }
            else
            {
                list = Session["Carditgem"] as List<ShopingCartModel>;

            }
*/
            Session["CartCount"] = resultdata.Count;
            return View(resultdata);
        }
       /* [HttpGet]
        public ShopingCartModel AllProductDetaisl111(int id)
        {
            var result = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().RegisterId;
            ViewBag.registerid = result;
            var result111111 = db.ShopingCartModels.FirstOrDefault(x=>x.UserId==id);
         
            return result111111;
           
        }*/
      


        /* public ActionResult AddOrder()
         {
             int orderid = 0;

             listofshopingcart = Session["Carditgem"] as List<ShopingCartModel>;
             Order orderobj = new Order()
             {
                 Orderdate = DateTime.Now.ToString(),
                 OrderNumber=String.Format("{0:ddmmyyyyHHmmsss}",DateTime.Now),

             };
             db.Orders.Add(orderobj);
             db.SaveChanges();


             orderid =orderobj.OderId;
            foreach(var c in listofshopingcart)
             {
                 OrderDetail orederdetailsobj = new OrderDetail();
                 orederdetailsobj.Total=c.TotalPrice;
                 orederdetailsobj.productid=c.ItemId;
                 orederdetailsobj.Quantity = c.Quantity;
                 orederdetailsobj.UnitPrice= c.UnitPrice;
                 db.OrderDetails.Add(orederdetailsobj);
                 db.SaveChanges();
             }
             Session["Carditgem"] =null;
             Session["CardCounter"] = null;
             return RedirectToAction("WeeknedBoots");
         }*/

       public ActionResult Getalldata()
       {
            
            var result = db.Registers.Where(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().RegisterId;
            ViewBag.registerid = result;
           // id = ViewBag.registerid;
            List<ShopingCartModel> list = new List<ShopingCartModel>();
            if (Session["Carditgem"] == null)
            {
                var result11 = db.ShopingCartModels.ToList();
                ViewBag.daata = result11;
            }
            else
            {
                var result11 = db.ShopingCartModels.ToList();
                ViewBag.daata = result11;
                list = Session["Carditgem"] as List<ShopingCartModel>;
            }
            return View();
       }
    }
  
}