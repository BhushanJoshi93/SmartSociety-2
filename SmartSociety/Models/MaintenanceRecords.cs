using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class MaintenanceRecords
    {
        public int MaintenanceID { get; set; }
        public int UnitID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string Month { get; set; }
        string query = "";

        public int Insert()
        {
            query = "INSERT INTO MaintenanceRecords VALUES(@UnitID, @PaymentDate, @Status, @Month)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@PaymentDate", this.PaymentDate));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@Month", this.Month));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE MaintenanceRecords
                        SET UnitID=@UnitID, PaymentDate=@PaymentDate, Status=@Status, Month=@Month
                        WHERE MaintenanceID=@MaintenanceID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@PaymentDate", this.PaymentDate));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@Month", this.Month));
            lstParams.Add(new SqlParameter("@MaintenanceID", this.MaintenanceID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE MaintenanceRecords where MaintenanceID=@MaintenanceID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MaintenanceID", this.MaintenanceID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM MaintenanceRecords where MaintenanceID=@MaintenanceID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MaintenanceID", this.MaintenanceID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.MaintenanceID = Convert.ToInt32(dt.Rows[0]["MaintenanceID"]);
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"].ToString());
                this.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]);
                this.Month = dt.Rows[0]["Month"].ToString();
                this.Status = dt.Rows[0]["Status"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM MaintenanceRecords";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public bool SelectByMonth()
        {
            query = "SELECT * FROM MaintenanceRecords where Month=@Month";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Month", this.Month));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.MaintenanceID = Convert.ToInt32(dt.Rows[0]["MaintenanceID"]);
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"].ToString());
                this.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]);
                this.Month = dt.Rows[0]["Month"].ToString();
                this.Status = dt.Rows[0]["Status"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}