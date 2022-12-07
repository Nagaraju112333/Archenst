using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchentsFirstProject.Models
{
    public class OrderDetailsModel
    {
       public int OrderId { get; set; } 
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
    }
}