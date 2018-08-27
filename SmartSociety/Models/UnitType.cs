using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class UnitType
    {
        public int UnitTypeID { get; set; }
        public string UnitTypeName { get; set; }
        public string query;
        public int Insert()
        {
            query = "INSERT INTO UnitType VALUES(@UnitTypeName)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitTypeName", this.UnitTypeName));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE UnitType
                        SET UnitTypeName=@UnitTypeName
                        WHERE UnitTypeID=@UnitTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitTypeName", this.UnitTypeName));
            lstParams.Add(new SqlParameter("@UnitTypeID", this.UnitTypeID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE UnitType where UnitTypeID=@UnitTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitTypeID", this.UnitTypeID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM UnitType where UnitTypeID=@UnitTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitTypeID", this.UnitTypeID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.UnitTypeID = Convert.ToInt32(dt.Rows[0]["UnitTypeID"]);
                this.UnitTypeName = dt.Rows[0]["UnitTypeName"].ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM UnitType";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}