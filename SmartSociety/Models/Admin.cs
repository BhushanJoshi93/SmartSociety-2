using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PhotoPath { get; set; }
        public string Gender { get; set; }

        public string query;
        public int Insert()
        {
            query = @"INSERT 
                        INTO Admin 
                        VALUES(@Name, @Email, @Username, @Password, @Mobile, @CreatedDate, @PhotoPath, @Gender, @IsActive)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@CreatedDate", this.CreatedDate));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Gender", this.Gender));
            lstParams.Add(new SqlParameter("@IsActive", "Inactive"));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Admin 
                        SET Name=@Name, Email=@Email, Username=@Username, Password=@Password, Mobile=@Mobile, CreatedDate=@CreatedDate, PhotoPath=@PhotoPath, Gender=@Gender 
                        where AdminID=@AdminID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@CreatedDate", this.CreatedDate));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Gender", this.Gender));
            lstParams.Add(new SqlParameter("@AdminID", this.AdminID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = @"DELETE Admin 
                        where AdminID=@AdminID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AdminID", this.AdminID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = @"SELECT * FROM Admin 
                        where AdminID=@AdminID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@AdminID", this.AdminID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.AdminID = Convert.ToInt32(dt.Rows[0]["AdminID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Gender = dt.Rows[0]["Gender"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM Admin";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public bool Authenticate()
        {
            string query = "SELECT * FROM Admin WHERE Username = @Username AND Password = @Password AND IsActive = 'Active'";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.AdminID = Convert.ToInt32(dt.Rows[0]["AdminID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Gender = dt.Rows[0]["Gender"].ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}