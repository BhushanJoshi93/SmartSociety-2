using System.Web.Mvc;
using SmartSociety.Models;
using System.Text;
using System.Security.Cryptography;
using System;

namespace SmartSociety.Controllers
{
    public class AccessController : Controller
    {
        public ActionResult Login()
        {
            //Session["Admin"] = null;
            Session.Clear();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Validate(FormCollection collection)
        {
            Admin A = new Admin();
            A.Username = collection["Username"];

            byte[] data = Encoding.ASCII.GetBytes(collection["Password"]);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            A.Password = Encoding.ASCII.GetString(md5data);

            if (A.Authenticate())
            {
                Session["Admin"] = A;
                return RedirectToAction("Test", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The user name or password is incorrect");

                return RedirectToAction("Login", "Access");
            }
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            Admin A = new Admin();

            A.Name = collection["Name"];
            A.Email = collection["Email"];
            A.Username = collection["Username"];

            byte[] data = Encoding.ASCII.GetBytes(collection["Password"]);
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(data);
            A.Password = Encoding.ASCII.GetString(md5data);

            A.Mobile = collection["Mobile"];
            A.CreatedDate = DateTime.Today;
            if (Request.Files["PhotoPath"] != null)
            {
                string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                A.PhotoPath = path;
            }
            else
            {
                A.PhotoPath = "";
            }
            A.Gender = collection["Gender"];

            if (A.Insert() != 0)
            {
                Mail m = new Mail();
                m.EmailTo = collection["Email"];
                m.Subject = "Registered On SmartSociety Successfully";
                m.Body = "Thank you " + collection["Name"] + " for regitering on Smartsociety.\n" + "Your username is " + collection["Username"];
                m.sendMail();
                return RedirectToAction("Login", "Access");
            }
            else
            {
                return RedirectToAction("Login#toregister", "Access");
            }
        }
    }
}