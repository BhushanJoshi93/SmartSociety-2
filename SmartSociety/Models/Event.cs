using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
        public string Details { get; set; }
        public string Venue { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EventType { get; set; }
        public int UnitID { get; set; }

        public string query;
        public int Insert()
        {
            query = "INSERT INTO Event VALUES(@Title, @PhotoPath, @Details, @Venue, @EventDate, @CreatedDate, @EventType, @UnitID)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Title", this.Title));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Details", this.Details));
            lstParams.Add(new SqlParameter("@Venue", this.Venue));
            lstParams.Add(new SqlParameter("@EventDate", this.EventDate));
            lstParams.Add(new SqlParameter("@CreatedDate", this.CreatedDate));
            lstParams.Add(new SqlParameter("@EventType", this.EventType));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = "UPDATE Event SET Title=@Title, PhotoPath=@PhotoPath, Details=@Details, Venue=@Venue, EventDate=@EventDate, CreatedDate=@CreatedDate, EventType=@EventType, UnitID=@UnitID where EventID=@EventID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Title", this.Title));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Details", this.Details));
            lstParams.Add(new SqlParameter("@Venue", this.Venue));
            lstParams.Add(new SqlParameter("@EventDate", this.EventDate));
            lstParams.Add(new SqlParameter("@CreatedDate", this.CreatedDate));
            lstParams.Add(new SqlParameter("@EventType", this.EventType));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@EventID", this.EventID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE Event where EventID=@EventID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventID", this.EventID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM Event where EventID=@EventID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventID", this.EventID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.EventID = Convert.ToInt32(dt.Rows[0]["EventID"]);
                this.Title = dt.Rows[0]["Title"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Details = dt.Rows[0]["Details"].ToString();
                this.Venue = dt.Rows[0]["Venue"].ToString();
                this.EventDate = Convert.ToDateTime(dt.Rows[0]["EventDate"]);
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                this.EventType = dt.Rows[0]["EventType"].ToString();
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.EventID = Convert.ToInt32(dt.Rows[0]["EventID"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM Event";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public bool SelectByIDJoin()
        {
            query = "SELECT E.* U.UnitName FROM Event E, Unit U where EventID=@EventID AND UnitID=@UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventID", this.EventID));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.EventID = Convert.ToInt32(dt.Rows[0]["EventID"]);
                this.Title = dt.Rows[0]["Title"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Details = dt.Rows[0]["Details"].ToString();
                this.Venue = dt.Rows[0]["Venue"].ToString();
                this.EventDate = Convert.ToDateTime(dt.Rows[0]["EventDate"]);
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                this.EventType = dt.Rows[0]["EventType"].ToString();
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAllJoin()
        {
            query = "SELECT E.*, U.UnitName FROM Event E, Unit U where E.UnitID=U.UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}