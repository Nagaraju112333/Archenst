using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchentsFirstProject.Models;

namespace ArchentsFirstProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        ArchentsEntities5 db=new ArchentsEntities5();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShipingAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShipingAddress(Products1 pro)
        {
            return View();
        }
            [HttpPost]
        public ActionResult Paymentmethod(relationship _requestData)
        {
            
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_vo9CKHAWRXQ63z", "pZo9bDH7FTzrnRqedutDHIsU");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", _requestData.amount * 100);  
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); 
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            User_Shiping_Address shipingAddress = new User_Shiping_Address();
            string orderId = orderResponse["id"].ToString();
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.orderId = orderResponse.Attributes["id"];
            _requestData.razorpayKey = "rzp_test_vo9CKHAWRXQ63z";
            orderDetail.Amount = Convert.ToInt32(_requestData.amount * 100);
            _requestData.currency = "INR";
           /* shipingAddress.Firstname = _requestData.Firstname;
            shipingAddress.LastName = _requestData.LastName;
            shipingAddress.Country = _requestData.Country;
            shipingAddress.Apartment = _requestData.Apartment;
            shipingAddress.city = _requestData.city;
            shipingAddress.Email = _requestData.Email;
            shipingAddress.Phone_Number = _requestData.Phone_Number;
            shipingAddress.Pincode = _requestData.Pincode;
            shipingAddress.Address = _requestData.Address;
            shipingAddress.state = _requestData.state;
            _requestData.description = "Testing description";
            db.User_Shiping_Address.Add(shipingAddress);
            db.SaveChanges();
            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();*/
            // Return on PaymentPage with Order data
            return View("PaymentPage");
        }
        public ActionResult PaymentPage()
        {
            return View();
        }
    }
}