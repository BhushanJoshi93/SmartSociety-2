using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UnitID { get; set; }
        public string Status { get; set; }
        public string PhotoPath { get; set; }
        public string Gender { get; set; }
        public string MemberType { get; set; }

        public string query;
        public int Insert()
        {
            query = @"INSERT 
                        INTO Member 
                        VALUES(@Name, @Mobile, @Email, @Username, @Password, @CreatedDate, @UnitID, @Status, @PhotoPath, @Gender, @Membertype)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@CreatedDate", this.CreatedDate));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Gender", this.Gender));
            lstParams.Add(new SqlParameter("@Membertype", this.MemberType));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Member 
                        SET Name=@Name, Mobile=@Mobile, Email=@Email, Username=@Username, Password=@Password, CreatedDate=@CreatedDate, UnitID=@UnitID, Status=@Status, PhotoPath=@PhotoPath, Gender=@Gender, Membertype=@Membertype 
                        where MemberID=@MemberID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@CreatedDate", this.CreatedDate));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@Gender", this.Gender));
            lstParams.Add(new SqlParameter("@Membertype", this.MemberType));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = @"DELETE Member 
                        where MemberID=@MemberID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = @"SELECT * FROM Member 
                        where MemberID=@MemberID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Gender = dt.Rows[0]["Gender"].ToString();
                this.MemberType = dt.Rows[0]["Membertype"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM Member";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public bool Authenticate()
        {
            string query = "SELECT * FROM Member WHERE Username = @Username AND Password = @Password";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Gender = dt.Rows[0]["Gender"].ToString();
                this.MemberType = dt.Rows[0]["Membertype"].ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectByUnitID()
        {
            query = "SELECT M.*, U.UnitName FROM Member M, Unit U WHERE M.UnitID = @UnitID AND U.UnitID=M.UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public DataTable SelectAllJoinUnit()
        {
            query = "SELECT M.*, U.UnitName FROM Member M, Unit U WHERE U.UnitID=M.UnitID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}