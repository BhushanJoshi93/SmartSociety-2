using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class ServiceProvider
    {
        public int ServiceProviderID { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public int ServiceTypeID { get; set; }
        public string Description { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Address { get; set; }
        public string Catalog { get; set; }
        public string Price { get; set; }

        public string query;
        public int Insert()
        {
            query = "INSERT INTO ServiceProvider VALUES(@Name, @PhotoPath, @ServiceTypeID, @Description, @Mobile1, @Mobile2, @Address, @Catalog, @Price)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@ServiceTypeID", this.ServiceTypeID));
            lstParams.Add(new SqlParameter("@Description", this.Description));
            lstParams.Add(new SqlParameter("@Mobile1", this.Mobile1));
            lstParams.Add(new SqlParameter("@Mobile2", this.Mobile2));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@Catalog", this.Catalog));
            lstParams.Add(new SqlParameter("@Price", this.Price));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE ServiceProvider
                        SET Name=@Name, PhotoPath=@PhotoPath, Description=@Description, Mobile1=@Mobile1, Mobile2=@Mobile2, Address=@Address, Catalog=@Catalog, Price=@Price
                        WHERE ServiceProviderID=@ServiceProviderID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceProviderID", this.ServiceProviderID));
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstParams.Add(new SqlParameter("@ServiceTypeID", this.ServiceTypeID));
            lstParams.Add(new SqlParameter("@Description", this.Description));
            lstParams.Add(new SqlParameter("@Mobile1", this.Mobile1));
            lstParams.Add(new SqlParameter("@Mobile2", this.Mobile2));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@Catalog", this.Catalog));
            lstParams.Add(new SqlParameter("@Price", this.Price));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE ServiceProvider where ServiceProviderID=@ServiceProviderID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceProviderID", this.ServiceProviderID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM ServiceProvider where ServiceProviderID=@ServiceProviderID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceProviderID", this.ServiceProviderID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.ServiceProviderID = Convert.ToInt32(dt.Rows[0]["ServiceProviderID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.ServiceTypeID = Convert.ToInt32(dt.Rows[0]["ServiceTypeID"]);
                this.Description = dt.Rows[0]["Description"].ToString();
                this.Mobile1 = dt.Rows[0]["Mobile1"].ToString();
                this.Mobile2 = dt.Rows[0]["Mobile2"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.Catalog = dt.Rows[0]["Catalog"].ToString();
                this.Price = dt.Rows[0]["Price"].ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM ServiceProvider";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
        public DataTable SelectJoin()
        {
            query = "SELECT SP.*, ST.STName FROM ServiceProvider SP, ServiceType ST WHERE SP.ServiceTypeID = ST.ServiceTypeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}