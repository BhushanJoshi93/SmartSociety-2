using SmartSociety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartSociety.Controllers
{
    public class WatchmanAPIController : ApiController
    {

        
        
        public Wing GetWing(int ID)
        {
            Wing w = new Wing();
            w.WingID = ID;
            w.SelectByID();
            return w;
        }
        public Watchman ValidateWatchman(string Username, string Password)
        {
            Watchman wm = new Watchman();
            wm.Username = Username;
            wm.Password = Password;
            if (wm.Validate())
            {
                return wm;
            }
            else
            {
                return null;
            }
        }
    }
}