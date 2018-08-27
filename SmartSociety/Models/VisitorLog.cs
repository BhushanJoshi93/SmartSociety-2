using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SmartSociety.Models
{
    public class VisitorLog
    {
        public int VisitorLogID { get; set; }
        public string UnitName { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string PhotoPath { get; set; }
        public string cin { get; set; }
        public string cout { get; set; }
        public int UnitID { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public int WatchmanID { get; set; }
        public string Photo { get; set; }
        public string query;
        public int Insert()
        {
            query = "INSERT INTO VisitorLog VALUES(@Name, @Mobile, @PhotoPath, @UnitID, @CheckinTime, @CheckoutTime, @WatchmanID)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@CheckinTime", this.CheckinTime));
            lstParams.Add(new SqlParameter("@CheckoutTime", this.CheckoutTime));
            lstParams.Add(new SqlParameter("@WatchmanID", this.WatchmanID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE VisitorLog
                        SET Name=@Name, Mobile=@Mobile, PhotoPath=@PhotoPath, UnitID=@UnitID, CheckinTime=@CheckinTime, CheckoutTime=@CheckoutTime, WatchmanID=@WatchmanID
                        WHERE VisitorLogID=@VisitorLogID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@VisitorLogID", this.VisitorLogID));
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@CheckinTime", this.CheckinTime));
            lstParams.Add(new SqlParameter("@CheckoutTime", this.CheckoutTime));
            lstParams.Add(new SqlParameter("@WatchmanID", this.WatchmanID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE VisitorLog where VisitorLogID=@VisitorLogID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@VisitorLogID", this.VisitorLogID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM VisitorLog where VisitorLogID=@VisitorLogID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@VisitorLogID", this.VisitorLogID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.VisitorLogID = Convert.ToInt32(dt.Rows[0]["VisitorLogID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.CheckinTime = Convert.ToDateTime(dt.Rows[0]["CheckinTime"]);
                this.CheckoutTime = Convert.ToDateTime(dt.Rows[0]["CheckoutTime"]);
                this.WatchmanID = Convert.ToInt32(dt.Rows[0]["WatchmanID"]);

                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM VisitorLog";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public List<VisitorLog> SelectAllList()  //List<VisitorLog>
        {
            String query = "Select V.*, U.UnitName from VisitorLog V, Unit U WHERE V.UnitID=U.UnitID AND V.WatchmanID=@WatchmanID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@WatchmanID", this.WatchmanID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<VisitorLog> lstVLog = new List<VisitorLog>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VisitorLog vl = new VisitorLog();
                vl.VisitorLogID = Convert.ToInt32(dt.Rows[i]["VisitorLogID"]);
                vl.Name = dt.Rows[i]["Name"].ToString();
                vl.Mobile = dt.Rows[i]["Mobile"].ToString();
                vl.PhotoPath = dt.Rows[i]["PhotoPath"].ToString();
                vl.UnitName = dt.Rows[i]["UnitName"].ToString();
                vl.UnitID = Convert.ToInt32(dt.Rows[i]["UnitID"]);
                DateTime checkin = Convert.ToDateTime(dt.Rows[i]["CheckinTime"]);
                vl.cin = checkin.ToString("dd/MM/yy hh:mm");
                DateTime checkout = Convert.ToDateTime(dt.Rows[i]["CheckOutTime"]);
                vl.cout = checkout.ToString("dd/MM/yy hh:mm");
                vl.WatchmanID = Convert.ToInt32(dt.Rows[i]["WatchmanID"]);
                lstVLog.Add(vl);
            }
            return lstVLog;
        }

        public bool SelectByIDImage()
        {
            query = "SELECT * FROM VisitorLog where VisitorLogID=@VisitorLogID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@VisitorLogID", this.VisitorLogID));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            
            if (dt.Rows.Count > 0)
            {
                this.VisitorLogID = Convert.ToInt32(dt.Rows[0]["VisitorLogID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                DateTime checkin = Convert.ToDateTime(dt.Rows[0]["CheckinTime"]);
                this.cin = checkin.ToString("dd/MM/yy hh:mm:ss");
                DateTime checkout = Convert.ToDateTime(dt.Rows[0]["CheckOutTime"]);
                this.cout = checkout.ToString("dd/MM/yy hh:mm:ss");
                this.WatchmanID = Convert.ToInt32(dt.Rows[0]["WatchmanID"]);

                return true;
            }
            else
            {
                return false;
            }
        }
        public int UpdateCheckOut()
        {
            query = @"UPDATE VisitorLog
                        SET CheckoutTime=@CheckoutTime
                        WHERE VisitorLogID=@VisitorLogID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@CheckoutTime", this.CheckoutTime));
            lstParams.Add(new SqlParameter("@VisitorLogID", this.VisitorLogID));
            return DataAccess.ModifyData(query, lstParams);
        }
    }
}