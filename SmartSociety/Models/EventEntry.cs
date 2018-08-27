using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class EventEntry
    {
        public int EventEntryID { get; set; }
        public int EventID { get; set; }
        public int UnitID { get; set; }
        public int MemberID { get; set; }
        public int EntryCount { get; set; }
        public DateTime EntryDate { get; set; }

        public string query;
        public int Insert()
        {
            query = "INSERT INTO EventEntry VALUES(@EventID, @UnitID, @MemberID, @EntryCount, @EntryDate)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventID", this.EventID));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            lstParams.Add(new SqlParameter("@EntryCount", this.EntryCount));
            lstParams.Add(new SqlParameter("@EntryDate", this.EntryDate));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = "UPDATE EventEntry SET EventID=@EventID, UnitID=@UnitID, MemberID=@MemberID, EntryCount=@EntryCount, EntryDate=@EntryDate where EventEntryID=@EventEntryID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventID", this.EventID));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            lstParams.Add(new SqlParameter("@EntryCount", this.EntryCount));
            lstParams.Add(new SqlParameter("@EntryDate", this.EntryDate));
            lstParams.Add(new SqlParameter("@EventEntryID", this.EventEntryID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE EventEntry where EventEntryID=@EventEntryID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventEntryID", this.EventEntryID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM EventEntry where EventEntryID=@EventEntryID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@EventEntryID", this.EventEntryID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.EventEntryID = Convert.ToInt32(dt.Rows[0]["EventEntryID"]);
                this.EventID = Convert.ToInt32(dt.Rows[0]["EventID"]);
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                this.EntryCount = Convert.ToInt32(dt.Rows[0]["EntryCount"]);
                this.EntryDate = Convert.ToDateTime(dt.Rows[0]["EntryDate"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM EventEntry";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}