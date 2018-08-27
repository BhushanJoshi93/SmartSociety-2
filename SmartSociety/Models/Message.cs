using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public DateTime CreateTime { get; set; }
        public int MemberID { get; set; }

        public string query;
        public int Insert()
        {
            query = "INSERT INTO Message VALUES(@MessageText, @CreateTime, @MemberID)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MessageText", this.MessageText));
            lstParams.Add(new SqlParameter("@CreateTime", this.CreateTime));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Message 
                        SET MessageText=@MessageText, CreateTime=@CreateTime, MemberID=@MemberID
                        where MessageID=@MessageID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MessageText", this.MessageText));
            lstParams.Add(new SqlParameter("@CreateTime", this.CreateTime));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            lstParams.Add(new SqlParameter("@MessageID", this.MessageID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE Message where MessageID=@MessageID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MessageID", this.MessageID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM Message where MessageID=@MessageID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MessageID", this.MessageID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.MessageID = Convert.ToInt32(dt.Rows[0]["MessageID"]);
                this.MessageText = dt.Rows[0]["MessageText"].ToString();
                this.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                this.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM Message";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}