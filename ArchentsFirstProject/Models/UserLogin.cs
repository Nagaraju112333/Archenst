using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ArchentsFirstProject.Models
{
    public class UserLogin
    {
        [Display(Name = "EmailID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter EmailId")]
        public string EmailID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}