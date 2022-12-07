using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using ArchentsFirstProject.Models;
using System.Web.Security;

namespace ArchentsFirstProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
            ArchentsEntities6 db=new ArchentsEntities6();   
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRegister([Bind(Exclude = "IsEmailVerified,ActivationCode")] Register user)
        {
            bool Status = false;
            string message = "";
            //b
            // Model Validation 
            if (ModelState.IsValid)
            {
                #region //Email is already Exist 
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion
                #region Generate Activation Code 
                user.ActivationCode = Guid.NewGuid();
                #endregion
                #region  Password Hashing 
                user.Password = Crypto.Hash(user.Password);
             //   user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); //
                #endregion
                user.IsEmailVerified = false;

                #region Save to Database
                using (ArchentsEntities6 dc = new ArchentsEntities6())
                {
                    dc.Registers.Add(user);
                    dc.SaveChanges();
                    //Send Email to User
                    SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id:" + user.Email;
                    Status = true;
                }
                #endregion
            }
            /* else
             {
                 message = "Invalid Request";
             }*/

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (ArchentsEntities6 dc = new ArchentsEntities6())
            {
                var v = dc.Registers.Where(a => a.Email == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailfor = "VerifyAccount")
        {
            var verifyUrl = "/Account/" + emailfor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("nagaraju.bodaarchents.com@outlook.com", emailID);
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Nagaraju@123"; // Replace with actual password
            string subject = "";
            string body = "";
            if (emailfor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailfor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }
            /*  string subject = "Your account is successfully created!";

              string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                  " successfully created. Please click on the below link to verify your account" +
                  " <br/><br/><a href='" + link + "'>" + link + "</a> ";*/
            MailMessage mc = new MailMessage("nagaraju.bodaarchents.com@outlook.com", emailID);
            mc.Subject = subject;
            mc.Body = body;
            mc.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
            smtp.Timeout = 1000000;
            smtp.EnableSsl = true;

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nc = new NetworkCredential("nagaraju.bodaarchents.com@outlook.com", "Nagaraju@123");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mc);
           
        }

        /*public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Account/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("nagaraju.bodaarchents.com@outlook.com", emailID);
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Nagaraju@123"; // Replace with actual password
            string subject = "";
            string body = "";
             subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                   " <br/><br/><a href='" + link + "'>" + link + "</a> ";
          
            *//*  string subject = "Your account is successfully created!";

              string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                  " successfully created. Please click on the below link to verify your account" +
                  " <br/><br/><a href='" + link + "'>" + link + "</a> ";*//*
            MailMessage mc = new MailMessage("nagaraju.bodaarchents.com@outlook.com", emailID);
            mc.Subject = subject;
            mc.Body = body;
            mc.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
            smtp.Timeout = 1000000;
            smtp.EnableSsl = true;

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nc = new NetworkCredential("nagaraju.bodaarchents.com@outlook.com", "Nagaraju@123");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mc);
           
        }*/
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;

            using (ArchentsEntities6 dc = new ArchentsEntities6())
            {
                var account = dc.Registers.Where(a => a.Email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                    account.ResetpasswordCode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {

                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (ArchentsEntities6 dc = new ArchentsEntities6())
            {
                var user = dc.Registers.Where(a => a.ResetpasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPassword model = new ResetPassword();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (ArchentsEntities6 dc = new ArchentsEntities6())
                {
                    var user = dc.Registers.Where(a => a.ResetpasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetpasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (ArchentsEntities6 db = new ArchentsEntities6())
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var a = db.Registers.FirstOrDefault(c => c.ActivationCode == new Guid(id));
                if (a != null)
                {
                    a.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.message = "Invalid Request";
                }
                ViewBag.Status = Status;
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login( UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (ArchentsEntities6 dc = new ArchentsEntities6())
            {
                var v = dc.Registers.Where(a => a.Email == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Employee");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }
    }
}