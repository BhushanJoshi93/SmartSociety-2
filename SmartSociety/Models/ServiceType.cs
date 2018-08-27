using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class ServiceType
    {
        public int ServiceTypeID { get; set; }
        public string STName { get; set; }
        public string PhotoPath { get; set; }
        public string query;
        public int Insert()
        {
            query = "INSERT INTO ServiceType VALUES(@STName, @PhotoPath)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@STName", this.STName));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE ServiceType
                        SET STName=@STName, PhotoPath=@PhotoPath
                        WHERE ServiceTypeID=@ServiceTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@STName", this.STName));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@ServiceTypeID", this.ServiceTypeID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE ServiceType where ServiceTypeID=@ServiceTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceTypeID", this.ServiceTypeID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM ServiceType where ServiceTypeID=@ServiceTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceTypeID", this.ServiceTypeID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.ServiceTypeID = Convert.ToInt32(dt.Rows[0]["ServiceTypeID"]);
                this.STName = dt.Rows[0]["STName"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM ServiceType";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}