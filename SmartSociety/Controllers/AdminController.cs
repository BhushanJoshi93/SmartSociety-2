using SmartSociety.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SmartSociety.Controllers
{
    public class AdminController : Controller
    {
        // GET: Login
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TempTest()
        {
            return View();
        }

        public ActionResult WingDetail(int ID)
        {
            Wing w = new Wing();
            w.WingID = ID;
            w.SelectByID();
            ViewBag.WingUpdate = w;
            DataTable dt = w.SelectAll();
            return View(dt);
        }
        [HttpPost]
        public ActionResult WingDetail(FormCollection collection)
        {
            Wing w = new Wing();
            w.WingID = Convert.ToInt32(collection["WingID"]);

            if (w.WingID > 0)
            {
                w.SelectByID();
                w.WingID = Convert.ToInt32(collection["WingID"]);
                w.WingName = collection["WingName"];
                w.MaxUnit = Convert.ToInt32(collection["MaxUnit"]);
                w.Update();
                return RedirectToAction("WingList");
            }
            else
            {
                w.WingName = collection["WingName"];
                w.MaxUnit = Convert.ToInt32(collection["MaxUnit"]);
                w.Insert();
                return RedirectToAction("WingList");
            }
        }
        public ActionResult WingDelete(int ID)
        {
            Wing w = new Wing();
            w.WingID = ID;
            w.Delete();
            return RedirectToAction("WingList");
        }
        public ActionResult WingList()
        {
            return RedirectToAction("WingDetail/0");
        }
        public ActionResult UnitTypeDetail(int ID)
        {
            UnitType ut = new UnitType();
            ut.UnitTypeID = ID;
            ut.SelectByID();
            ViewBag.UnitTypeUpdate = ut;
            DataTable dt = ut.SelectAll();
            return View(dt);
        }
        [HttpPost]
        public ActionResult UnitTypeDetail(FormCollection collection)
        {
            UnitType ut = new UnitType();
            ut.UnitTypeID = Convert.ToInt32(collection["UnitTypeID"]);

            if (ut.UnitTypeID > 0)
            {
                ut.SelectByID();
                ut.UnitTypeID = Convert.ToInt32(collection["UnitTypeID"]);
                ut.UnitTypeName = collection["UnitTypeName"];
                ut.Update();
                return RedirectToAction("UnitTypeList");
            }
            else
            {
                ut.UnitTypeName = collection["UnitTypeName"];
                ut.Insert();
                return RedirectToAction("UnitTypeList");
            }
        }
        public ActionResult UnitTypeDelete(int ID)
        {
            UnitType ut = new UnitType();
            ut.UnitTypeID = ID;
            ut.Delete();
            return RedirectToAction("UnitTypeList");
        }
        public ActionResult UnitTypeList()
        {
            return RedirectToAction("UnitTypeDetail/0");
        }
        public ActionResult ServiceTypeDetail(int ID)
        {
            ServiceType st = new ServiceType();
            st.ServiceTypeID = ID;
            st.SelectByID();
            ViewBag.ServiceTypeUpdate = st;
            DataTable dt = st.SelectAll();
            return View(dt);
        }
        [HttpPost]
        public ActionResult ServiceTypeDetail(FormCollection collection)
        {
            ServiceType st = new ServiceType();
            st.ServiceTypeID = Convert.ToInt32(collection["ServiceTypeID"]);

            if (st.ServiceTypeID > 0)
            {
                st.SelectByID();
                st.ServiceTypeID = Convert.ToInt32(collection["ServiceTypeID"]);
                st.STName = collection["STName"];
                if (Request.Files["Photo"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    st.PhotoPath = path;
                }
                else
                {
                    st.PhotoPath = "";
                }
                st.Update();
                return RedirectToAction("ServiceTypeList");
            }
            else
            {
                st.STName = collection["STName"];
                if (Request.Files["Photo"] != null)
                {
                    string path = "~/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    st.PhotoPath = path;
                }
                st.Insert();
                return RedirectToAction("ServiceTypeList");
            }
        }
        public ActionResult ServiceTypeList()
        {
            return RedirectToAction("ServiceTypeDetail/0");
            //ServiceType st = new ServiceType();
            //DataTable dt = st.SelectAll();
            //return View(dt);
        }
        public ActionResult ServiceTypeDelete(int ID)
        {
            ServiceType st = new ServiceType();
            st.ServiceTypeID = ID;
            st.Delete();
            return RedirectToAction("ServiceTypeList");
        }
        public ActionResult UnitDetail(int ID)
        {
            Unit u = new Unit();
            Wing w = new Wing();
            UnitType ut = new UnitType();
            Member m = new Member();
            u.UnitID = ID;
            u.SelectByID();
            ViewBag.WingData = w.SelectAll();
            ViewBag.UnitTypeData = ut.SelectAll();
            ViewBag.MemberData = m.SelectAll();
            return View(u);
        }
        [HttpPost]
        public ActionResult UnitDetail(FormCollection collection)
        {
            Unit u = new Unit();
            u.UnitID = Convert.ToInt32(collection["UnitID"]);

            if (u.UnitID > 0)
            {
                u.SelectByID();
                u.UnitID = Convert.ToInt32(collection["UnitID"]);
                u.UnitName = collection["UnitName"];
                u.WingID = Convert.ToInt32(collection["WingID"]);
                u.UnitTypeID = Convert.ToInt32(collection["UnitTypeID"]);
                u.Status = (collection["Status"].ToString() == "True"? "Active" : "Inactive");
                u.OwnerName = collection["OwnerName"];
                if (Request.Files["DocumentPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["DocumentPath"].FileName;
                    Request.Files["DocumentPath"].SaveAs(Server.MapPath(path));
                    u.DocumentPath = path;
                }
                else
                {
                    u.DocumentPath = "";
                }
                u.Mobile = collection["Mobile"];
                u.Phone = collection["Phone"];
                u.Update();
                return RedirectToAction("UnitSearch");
            }
            else
            {
                string status = collection["Status"].ToString();
                u.UnitName = collection["UnitName"];
                u.WingID = Convert.ToInt32(collection["WingID"]);
                u.UnitTypeID = Convert.ToInt32(collection["UnitTypeID"]);
                u.Status = (status.Equals("True") ? "Active":"Inactive");
                u.OwnerName = collection["OwnerName"];
                if (Request.Files["DocumentPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["DocumentPath"].FileName;
                    Request.Files["DocumentPath"].SaveAs(Server.MapPath(path));
                    u.DocumentPath = path;
                }
                else
                {
                    u.DocumentPath = "";
                }
                u.Mobile = collection["Mobile"];
                u.Phone = collection["Phone"];
                u.Insert();
                return RedirectToAction("UnitSearch");
            }
        }
        public ActionResult UnitSearch()
        {
            Unit u = new Unit();
            DataTable dt = u.SelectJoin();
            return View(dt);
        }
        public ActionResult UnitDelete(int ID)
        {
            Unit u = new Unit();
            u.UnitID = ID;
            u.Delete();
            return RedirectToAction("UnitSearch");
        }
        public ActionResult MemberDetail(int ID)
        {
            Member m = new Member();
            Unit u = new Unit();
            ViewBag.UnitData = u.SelectAll();
            m.MemberID = ID;
            m.SelectByID();
            return View(m);
        }
        [HttpPost]
        public ActionResult MemberDetail(FormCollection collection)
        {
            Member m = new Member();
            m.MemberID = Convert.ToInt32(collection["MemberID"]);

            if (m.MemberID > 0)
            {
                m.SelectByID();
                m.MemberID = Convert.ToInt32(collection["MemberID"]);
                m.Name = collection["Name"];
                m.Mobile = collection["Mobile"];
                m.Email = collection["Email"];
                m.Username = collection["Password"];
                m.CreatedDate = DateTime.Today;
                m.UnitID = Convert.ToInt32(collection["UnitID"]);
                m.Status = collection["Status"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    m.PhotoPath = path;
                }
                else
                {
                    m.PhotoPath = "";
                }
                m.Gender = collection["Gender"];
                m.MemberType = collection["MemberType"];
                if (m.Update() != 0)
                {
                    Mail mobj = new Mail();
                    mobj.EmailTo = m.Email;
                    mobj.Subject = "Registered Successfully on Smart Society";
                    mobj.Body = "Dear " + m.Name + ",\n" + "Username: " + m.Username + "\n" + "Password: " + m.Password;
                    mobj.sendMail();
                }
                return RedirectToAction("MemberSearch");
            }
            else
            {
                m.Name = collection["Name"];
                m.Mobile = collection["Mobile"];
                m.Email = collection["Email"];
                m.Username = collection["Password"];
                m.CreatedDate = DateTime.Today;
                m.UnitID = Convert.ToInt32(collection["UnitID"]);
                m.Status = collection["Status"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    m.PhotoPath = path;
                }
                else
                {
                    m.PhotoPath = "";
                }
                m.Gender = collection["Gender"];
                m.MemberType = collection["MemberType"];
                if (m.Insert() != 0)
                {
                    Mail mobj = new Mail();
                    mobj.EmailTo = m.Email;
                    mobj.Subject = "Registered Successfully on Smart Society";
                    mobj.Body = "Dear " + m.Name + ",\n" + "Username: " + m.Username + "\n" + "Password: " + m.Password;
                    mobj.sendMail();
                }
                return RedirectToAction("MemberSearch");
            }
        }
        public ActionResult AddMember(int ID)
        {
            Unit u = new Unit();
            Member m = new Member();
            u.UnitID = ID;
            m.UnitID = ID;
            DataTable dtu = u.SelectJoinByUnitID();
            DataTable dtm = m.SelectByUnitID();
            ViewBag.MemberData = dtm;
            return View(dtu);
        }
        public ActionResult UnitMemberDetail(int uID, int mID)
        {
            Unit u = new Unit();
            Member m = new Member();
            u.UnitID = uID;
            m.MemberID = mID;
            ViewBag.UnitData = u.SelectByUnitID();
            m.SelectByID();
            return View(m);
        }
        [HttpPost]
        public ActionResult UnitMemberDetail(FormCollection collection)
        {
            Member m = new Member();
            m.MemberID = Convert.ToInt32(collection["MemberID"]);

            if (m.MemberID > 0)
            {
                m.SelectByID();
                m.MemberID = Convert.ToInt32(collection["MemberID"]);
                m.Name = collection["Name"];
                m.Mobile = collection["Mobile"];
                m.Email = collection["Email"];
                m.Username = collection["Username"];
                m.Password = collection["Password"];
                m.CreatedDate = DateTime.Today;
                m.UnitID = Convert.ToInt32(collection["UnitID"]);
                m.Status = collection["Status"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    m.PhotoPath = path;
                }
                else
                {
                    m.PhotoPath = "";
                }
                m.Gender = collection["Gender"];
                m.MemberType = collection["MemberType"];
                if(m.Update() != 0)
                {
                    Mail mobj = new Mail();
                    mobj.EmailTo = m.Email;
                    mobj.Subject = "Registered Successfully on Smart Society";
                    mobj.Body = "Dear " + m.Name + ",\n" + "Username: " + m.Username + "\n" + "Password: " + m.Password;
                    mobj.sendMail();
                }
                return RedirectToAction("MemberSearch");
            }
            else
            {
                m.Name = collection["Name"];
                m.Mobile = collection["Mobile"];
                m.Email = collection["Email"];
                m.Username = collection["Username"];
                m.Password = collection["Password"];
                m.CreatedDate = DateTime.Today;
                m.UnitID = Convert.ToInt32(collection["UnitID"]);
                m.Status = collection["Status"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    m.PhotoPath = path;
                }
                else
                {
                    m.PhotoPath = "";
                }
                m.Gender = collection["Gender"];
                m.MemberType = collection["MemberType"];
                if(m.Insert() != 0)
                {
                    Mail mobj = new Mail();
                    mobj.EmailTo = m.Email;
                    mobj.Subject = "Registered Successfully on Smart Society";
                    mobj.Body = "Dear "+m.Name+",\n"+"Username: "+m.Username+"\n"+"Password: "+m.Password;
                    mobj.sendMail();
                }
                return RedirectToAction("MemberSearch");
            }
        }

        public ActionResult MemberSearch()
        {
            Member m = new Member();
            DataTable dt = m.SelectAllJoinUnit();
            return View(dt);
        }
        public ActionResult MemberDelete(int ID)
        {
            Member m = new Member();
            m.MemberID = ID;
            m.Delete();
            return RedirectToAction("MemberSearch");
        }
        public ActionResult ServiceProviderDetail(int ID)
        {
            ServiceProvider sp = new ServiceProvider();
            sp.ServiceProviderID = ID;
            sp.SelectByID();
            ServiceType st = new ServiceType();
            DataTable dt = st.SelectAll();
            ViewBag.ServiceTypeDetail = dt;
            return View(sp);
        }
        [HttpPost]
        public ActionResult ServiceProviderDetail(FormCollection collection)
        {
            ServiceProvider sp = new ServiceProvider();
            sp.ServiceProviderID = Convert.ToInt32(collection["ServiceProviderID"]);

            if (sp.ServiceProviderID > 0)
            {
                sp.SelectByID();
                sp.ServiceProviderID = Convert.ToInt32(collection["ServiceProviderID"]);
                sp.Name = collection["Name"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    sp.PhotoPath = path;
                }
                else
                {
                    sp.PhotoPath = "";
                }
                sp.ServiceTypeID = Convert.ToInt32(collection["ServiceTypeID"]);
                sp.Description = collection["Description"];
                sp.Mobile1 = collection["Mobile1"];
                sp.Mobile2 = collection["Mobile2"];
                sp.Address = collection["Address"];
                if (Request.Files["Catalog"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Catalog"].FileName;
                    Request.Files["Catalog"].SaveAs(Server.MapPath(path));
                    sp.Catalog = path;
                }
                else
                {
                    sp.Catalog = "";
                }
                sp.Price = collection["Price"];
                sp.Update();
                return RedirectToAction("ServiceProviderSearch");
            }
            else
            {
                sp.Name = collection["Name"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    sp.PhotoPath = path;
                }
                else
                {
                    sp.PhotoPath = "";
                }
                sp.ServiceTypeID = Convert.ToInt32(collection["ServiceTypeID"]);
                sp.Description = collection["Description"];
                sp.Mobile1 = collection["Mobile1"];
                sp.Mobile2 = collection["Mobile2"];
                sp.Address = collection["Address"];
                if (Request.Files["Catalog"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Catalog"].FileName;
                    Request.Files["Catalog"].SaveAs(Server.MapPath(path));
                    sp.Catalog = path;
                }
                else
                {
                    sp.Catalog = "";
                }
                sp.Price = collection["Price"];
                sp.Insert();
                return RedirectToAction("ServiceProviderSearch");
            }
        }
        public ActionResult ServiceProviderSearch()
        {
            ServiceProvider sp = new ServiceProvider();
            DataTable dt = sp.SelectJoin();
            return View(dt);
        }
        public ActionResult ServiceProviderDelete(int ID)
        {
            ServiceProvider sp = new ServiceProvider();
            sp.ServiceProviderID = ID;
            sp.Delete();
            return RedirectToAction("ServiceProviderSearch");
        }
        public ActionResult WatchmanDetail(int ID)
        {
            Watchman wm = new Watchman();
            wm.WatchmanID = ID;
            wm.SelectByID();
            ViewBag.WatchmanUpdate = wm;
            DataTable dt = wm.SelectAll();
            return View(dt);
        }
        [HttpPost]
        public ActionResult WatchmanDetail(FormCollection collection)
        {
            Watchman w = new Watchman();
            w.WatchmanID = Convert.ToInt32(collection["WatchmanID"]);

            if (w.WatchmanID > 0)
            {
                w.SelectByID();
                w.WatchmanID = Convert.ToInt32(collection["WatchmanID"]);
                w.Name = collection["Name"];
                w.Mobile = collection["Mobile"];
                w.Username = collection["Username"];
                w.Password = collection["Password"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    w.PhotoPath = path;
                }
                else
                {
                    w.PhotoPath = "";
                }
                w.Update();
                return RedirectToAction("WatchmanList");
            }
            else
            {
                w.Name = collection["Name"];
                w.Mobile = collection["Mobile"];
                w.Username = collection["Username"];
                w.Password = collection["Password"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    w.PhotoPath = path;
                }
                else
                {
                    w.PhotoPath = "";
                }
                w.Insert();
                return RedirectToAction("WatchmanList");
            }
        }
        public ActionResult WatchmanList()
        {
            return RedirectToAction("WatchmanDetail/0");
        }
        public ActionResult WatchmanDelete(int ID)
        {
            Watchman w = new Watchman();
            w.WatchmanID = ID;
            w.Delete();
            return RedirectToAction("WatchmanList");
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            Admin A = new Admin();
            A.AdminID = Convert.ToInt32(collection["AdminID"]);
            if (A.AdminID > 0)
            {
                A.SelectByID();
                A.AdminID = Convert.ToInt32(collection["AdminID"]);
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
                A.Update();
                return RedirectToAction("AdminProfile");
            }
            else
            {
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
                    return RedirectToAction("Login", "Access");
                }
                else
                {
                    return RedirectToAction("Login#toregister", "Access");
                }
            }
        }
        public ActionResult AdminProfile()
        {
            Admin A = new Admin();
            Admin CurrentAdmin = (Admin)Session["Admin"];
            A.AdminID = CurrentAdmin.AdminID;
            A.SelectByID();
            return View(A);
        }
        
    }
}