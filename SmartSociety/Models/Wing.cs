using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Wing
    {
        public int WingID { get; set; }
        public string WingName { get; set; }
        public int MaxUnit { get; set; }
        public string query;
        public int Insert()
        {
            query = "INSERT INTO Wing VALUES(@WingName, @MaxUnit)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WingName", this.WingName));
            lstParams.Add(new SqlParameter("@MaxUnit", this.MaxUnit));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Wing
                        SET WingName=@WingName, MaxUnit=@MaxUnit
                        WHERE WingID=@WingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WingName", this.WingName));
            lstParams.Add(new SqlParameter("@MaxUnit", this.MaxUnit));
            lstParams.Add(new SqlParameter("@WingID", this.WingID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE Wing where WingID=@WingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WingID", this.WingID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM Wing where WingID=@WingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WingID", this.WingID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.WingID = Convert.ToInt32(dt.Rows[0]["WingID"]);
                this.WingName = dt.Rows[0]["WingName"].ToString();
                this.MaxUnit = Convert.ToInt32(dt.Rows[0]["MaxUnit"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM Wing";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public List<Wing> SelectAllWingName()  //List<VisitorLog>
        {
            String query = "Select * from Wing";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstparams);
            List<Wing> lstWingName = new List<Wing>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Wing w = new Wing();
                w.WingID = Convert.ToInt32(dt.Rows[i]["WingID"]);
                w.WingName = dt.Rows[i]["WingName"].ToString();
                lstWingName.Add(w);
            }
            return lstWingName;
        }
        public Boolean SelectByWingID()  //List<VisitorLog>
        {
            String query = "Select * from Wing where WingName=@WingName";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@WingName", this.WingName));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            List<Wing> lstWingName = new List<Wing>();
            if (dt.Rows.Count > 0)
            {
                this.WingID = Convert.ToInt32(dt.Rows[0]["WingID"]);
                this.WingName = dt.Rows[0]["WingName"].ToString();
                this.MaxUnit = Convert.ToInt32(dt.Rows[0]["MaxUnit"]);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}