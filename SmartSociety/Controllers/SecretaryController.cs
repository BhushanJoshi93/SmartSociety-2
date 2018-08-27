using SmartSociety.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSociety.Controllers
{
    public class SecretaryController : Controller
    {
        // GET: Secretary
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (Session["Admin"] == null)
        //    {
        //        filterContext.Result = RedirectToAction("Login", "Access");
        //    }
        //    else
        //    {
        //        base.OnActionExecuting(filterContext);
        //    }
        //}
        public ActionResult TempTest()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NoticeDetail(int ID)
        {
            Notice n = new Notice();
            n.NoticeID = ID;
            n.SelectByID();
            return View(n);
        }
        [HttpPost]
        public ActionResult NoticeDetail(FormCollection collection)
        {
            Notice n = new Notice();
            n.NoticeID = Convert.ToInt32(collection["NoticeID"]);

            if (n.NoticeID > 0)
            {
                n.SelectByID();
                n.NoticeID = Convert.ToInt32(collection["NoticeID"]);
                n.Title = collection["Title"];
                n.Details = collection["Details"];
                n.NoticeDate = DateTime.Now;
                if (Request.Files["Attachment"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Attachment"].FileName;
                    Request.Files["Attachment"].SaveAs(Server.MapPath(path));
                    n.Attachment = path;
                }
                else
                {
                    n.Attachment = "";
                }
                n.Update();
                return RedirectToAction("NoticeList");
            }
            else
            {
                n.Title = collection["Title"];
                n.Details = collection["Details"];
                n.NoticeDate = DateTime.Now;
                if (Request.Files["Attachment"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Attachment"].FileName;
                    Request.Files["Attachment"].SaveAs(Server.MapPath(path));
                    n.Attachment = path;
                }
                else
                {
                    n.Attachment = "";
                }
                n.Insert();
                return RedirectToAction("NoticeList");
            }
        }
        public ActionResult NoticeList()
        {
            Notice n = new Notice();
            DataTable dt = n.SelectAll();
            return View(dt);
        }
        public ActionResult EventDetail(int ID)
        {
            Event e = new Event();
            Unit u = new Unit();
            DataTable dtu = u.SelectAll();
            ViewBag.dtu = dtu;
            e.EventID = ID;
            e.SelectByID();
            return View(e);
        }
        [HttpPost]
        public ActionResult EventDetail(FormCollection collection)
        {
            Event e = new Event();
            e.EventID = Convert.ToInt32(collection["EventID"]);

            if (e.EventID > 0)
            {
                e.SelectByID();
                e.EventID = Convert.ToInt32(collection["EventID"]);
                e.Title = collection["Title"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    e.PhotoPath = path;
                }
                else
                {
                    e.PhotoPath = "";
                }
                e.Details = collection["Details"];
                e.EventDate = Convert.ToDateTime(collection["EventDate"]);
                e.CreatedDate = DateTime.Now;
                e.EventType = collection["EventType"];
                e.UnitID = Convert.ToInt32(collection["UID"]);
                e.Update();
                return RedirectToAction("EventSearch");
            }
            else
            {
                e.Title = collection["Title"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    e.PhotoPath = path;
                }
                else
                {
                    e.PhotoPath = "";
                }
                e.Details = collection["Details"];
                e.EventDate = Convert.ToDateTime(collection["EventDate"]);
                e.CreatedDate = DateTime.Now;
                e.EventType = collection["EventType"];
                e.UnitID = Convert.ToInt32(collection["UID"]);
                e.Insert();
                return RedirectToAction("EventSearch");
            }
        }
        public ActionResult EventSearch()
        {
            Event e = new Event();
            DataTable dt = e.SelectAllJoin();
            return View(dt);
        }
        public ActionResult VisitorSearch()
        {
            return View();
        }
        public ActionResult VisitorView()
        {
            return View();
        }
        public ActionResult MaintenancePaid()
        {
            Unit u = new Unit();
            DataTable dt = u.SelectJoin();
            return View(dt);
        }
        public ActionResult MaintenancePaidStore(int ID, string stat)
        {
            MaintenanceRecords mr = new MaintenanceRecords();
            mr.UnitID = ID;
            mr.PaymentDate = DateTime.Now;
            mr.Status = stat;
            mr.Month = DateTime.Now.ToString("MMMM");
            mr.Insert();
            return View();
        }
    }
}