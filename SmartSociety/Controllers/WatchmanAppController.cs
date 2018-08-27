using SmartSociety.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSociety.Controllers
{
    public class WatchmanAppController : Controller
    {
        // GET: WatchmanApp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authentication(FormCollection collection)
        {
            Watchman w = new Watchman();
            w.Username = collection["un"];//Request.QueryString["un"];
            w.Password = collection["pw"];//Request.QueryString["pw"];
            if(w.Validate())
            {
                if (w.PhotoPath != null)
                {
                    //vl.Photo = ie.ImageToBase64(vl.PhotoPath);
                    byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(w.PhotoPath));

                    w.Photo = Convert.ToBase64String(imageArray);
                }
                else
                {
                    w.Photo = "";
                }
                return Json(w);
            }
            else
            {
                return Content("Error");
            }
        }

        public ActionResult VisitorLogView(int ID)
        {
            try
            {
                VisitorLog v = new VisitorLog();
                v.WatchmanID = ID;
                return Json(v.SelectAllList(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("Fail");
            }
        }

        [HttpPost]
        public ActionResult GetAllUnitName(FormCollection collection)
        {
            try
            {
                Wing w = new Wing();
                Unit u = new Unit();
                w.WingName = collection["WingName"].ToString();
                w.SelectByWingID();
                u.WingID = w.WingID;
                return Json(u.SelectAllUnitName(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("Fail");
            }

        }
        public ActionResult GetAllWingName()
        {
            try
            {
                Wing w = new Wing();
                return Json(w.SelectAllWingName(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("Fail");
            }
        }
        [HttpPost]
        public ActionResult VisitorDetail(FormCollection collection)
        {
            Wing w = new Wing();
            Unit u = new Unit();
            w.WingName = collection["WingName"];
            w.SelectByWingID();
            u.WingID = w.WingID;
            u.UnitName = collection["UnitName"];
            u.SelectByWingIDUnitName();
            VisitorLog vl = new VisitorLog();
            ImageDecoding id = new ImageDecoding();
            String photo = collection["Photo"];
            vl.Name = collection["Name"];
            vl.Mobile = collection["Mobile"];
            vl.CheckinTime = DateTime.Now;
            vl.CheckoutTime = vl.CheckinTime;
            vl.WatchmanID = Convert.ToInt32(collection["WatchmanID"]);
            Image fp = id.Base64ToImage(photo);
            vl.UnitID = u.UnitID;//Convert.ToInt32(collection["UnitID"]);
            //vl.PhotoPath;
            if (fp != null)//Request.Files["Photo"] != null)
            {
                string path = "/uploads/visitorlog/" + DateTime.Now.Ticks.ToString() + "_log" + ".jpeg";
                //Request.Files["Photo"].SaveAs(Server.MapPath(path));
                string newFile = Guid.NewGuid().ToString();// + fileExtensionApplication;
                string filePath = Path.Combine(Server.MapPath("~/Assets/") + Request.QueryString["id"] + "/", newFile);
                
                fp.Save(Server.MapPath(path), ImageFormat.Png);
                vl.PhotoPath = path;
            }
            else
            {
                vl.PhotoPath = "";
            }
            if (vl.Insert() == 1)
            {
                return Json("Inserted", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("Error");
            }
        }
        public ActionResult VisitorView(int ID)
        {
            VisitorLog vl = new VisitorLog();
            vl.VisitorLogID = ID;
            if (vl.SelectByIDImage())
            {
                if (vl.PhotoPath != null)
                {
                    //vl.Photo = ie.ImageToBase64(vl.PhotoPath);
                    byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(vl.PhotoPath));

                    vl.Photo = Convert.ToBase64String(imageArray);
                }
                return Json(vl, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckOut(int ID)
        {
            VisitorLog vl = new VisitorLog();
            vl.VisitorLogID = ID;
            vl.CheckoutTime = DateTime.Now;
            if (vl.UpdateCheckOut() > 0)
            {
                return Content("Updated");
            }
            else
            {
                return Content("Error");
            }
        }
    }
}