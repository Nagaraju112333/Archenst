using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchentsFirstProject.Models
{
    public class Address1
    {
        public User_Shiping_Address shiping_address { get; set; }
        public OrderDetail order_detail { get; set; }
        public string razorpayKey { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string orderId { get; set; }
        
        public int amount { get; set; }
      
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
     
        public int AdderssId { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Apartment { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Pincode { get; set; }
        public string Phone_Number { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}