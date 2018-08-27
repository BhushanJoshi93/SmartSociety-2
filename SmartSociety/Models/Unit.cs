using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Unit
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public int WingID { get; set; }
        public int UnitTypeID { get; set; }
        public string Status { get; set; }
        public string OwnerName { get; set; }
        public string DocumentPath { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string query;
        public int Insert()
        {
            query = "INSERT INTO Unit VALUES(@UnitName, @WingID, @UnitTypeID, @Status, @OwnerName, @DocumentPath, @Mobile, @Phone)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitName", this.UnitName));
            lstParams.Add(new SqlParameter("@WingID", this.WingID));
            lstParams.Add(new SqlParameter("@UnitTypeID", this.UnitTypeID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@OwnerName", this.OwnerName));
            lstParams.Add(new SqlParameter("@DocumentPath", this.DocumentPath));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Phone", this.Phone));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Unit
                        SET UnitName=@UnitName, WingID=@WingID, UnitTypeID=@UnitTypeID, Status=@Status, OwnerName=@OwnerName, DocumentPath=@DocumentPath, Mobile=@Mobile, Phone=@Phone
                        WHERE UnitID=@UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitName", this.UnitName));
            lstParams.Add(new SqlParameter("@WingID", this.WingID));
            lstParams.Add(new SqlParameter("@UnitTypeID", this.UnitTypeID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@OwnerName", this.OwnerName));
            lstParams.Add(new SqlParameter("@DocumentPath", this.DocumentPath));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Phone", this.Phone));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE Unit where UnitID=@UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM Unit where UnitID=@UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.UnitName = dt.Rows[0]["UnitName"].ToString();
                this.WingID = Convert.ToInt32(dt.Rows[0]["WingID"]);
                this.UnitTypeID = Convert.ToInt32(dt.Rows[0]["UnitTypeID"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.OwnerName = dt.Rows[0]["OwnerName"].ToString();
                this.DocumentPath = dt.Rows[0]["DocumentPath"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Phone = dt.Rows[0]["Phone"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM Unit";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public DataTable SelectJoin()
        {
            query = "SELECT U.*, UT.UnitTypeName, W.WingName FROM Unit U, UnitType UT, Wing W WHERE U.UnitTypeID = UT.UnitTypeID AND U.WingID = W.WingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public DataTable SelectJoinByUnitID()
        {
            query = "SELECT U.*, UT.UnitTypeName, W.WingName FROM Unit U, UnitType UT, Wing W WHERE U.UnitID=@UnitID AND U.UnitTypeID = UT.UnitTypeID AND U.WingID = W.WingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public DataTable SelectByUnitID()
        {
            query = "SELECT * FROM Unit WHERE UnitID=@UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public DataTable SelectMaintenanceRecords()
        {
            query = "SELECT U.*, UT.UnitTypeName, W.WingName, M.* FROM Unit U, UnitType UT, Wing W, MaintenanceRecords M WHERE U.UnitTypeID = UT.UnitTypeID AND U.WingID = W.WingID AND U.UnitID=M.UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public List<Unit> SelectAllUnitName()  //List<VisitorLog>
        {
            String query = "Select * from Unit where WingID=@WingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WingID", this.WingID));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            List<Unit> lstUnitName = new List<Unit>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Unit u = new Unit();
                u.UnitID = Convert.ToInt32(dt.Rows[i]["UnitID"]);
                u.UnitName = dt.Rows[i]["UnitName"].ToString();
                lstUnitName.Add(u);
            }
            return lstUnitName;
        }
        public Boolean SelectByWingIDUnitName()  //List<VisitorLog>
        {
            String query = "Select * from Unit where WingID=@WingID AND UnitName=@UnitName";
            List<SqlParameter> lstparams = new List<SqlParameter>();
            lstparams.Add(new SqlParameter("@WingID", this.WingID));
            lstparams.Add(new SqlParameter("@UnitName", this.UnitName));
            DataTable dt = DataAccess.SelectData(query, lstparams);
            List<Wing> lstWingName = new List<Wing>();
            if (dt.Rows.Count > 0)
            {
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}