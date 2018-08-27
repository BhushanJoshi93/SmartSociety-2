using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Watchman
    {
        public int WatchmanID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string PhotoPath { get; set; }
        public string Photo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string query;
        public int Insert()
        {
            query = "INSERT INTO Watchman VALUES(@Name, @Mobile, @PhotoPath, @Username, @Password)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Watchman
                        SET Name=@Name, Mobile=@Mobile, PhotoPath=@PhotoPath, Username=@Username, Password=@Password
                        WHERE WatchmanID=@WatchmanID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@WatchmanID", this.WatchmanID));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE Watchman where WatchmanID=@WatchmanID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WatchmanID", this.WatchmanID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM Watchman where WatchmanID=@WatchmanID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@WatchmanID", this.WatchmanID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.WatchmanID = Convert.ToInt32(dt.Rows[0]["WatchmanID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
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
            query = "SELECT * FROM Watchman";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public bool Validate()
        {
            query = "SELECT * FROM Watchman where Username=@Username AND Password=@Password";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.WatchmanID = Convert.ToInt32(dt.Rows[0]["WatchmanID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}