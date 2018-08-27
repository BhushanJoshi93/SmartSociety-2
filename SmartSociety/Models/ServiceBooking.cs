using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class ServiceBooking
    {
        public int ServiceBookingID { get; set; }
        public int UnitID { get; set; }
        public int ServiceProviderID { get; set; }
        public DateTime BookingDate { get; set; }
        public int MemberID { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string Review { get; set; }
        public string Rating { get; set; }
        public DateTime ReviewTime { get; set; }

        public string query;
        public int Insert()
        {
            query = "INSERT INTO ServiceBooking VALUES(@UnitID, @ServiceProviderID, @BookingDate, @MemberID, @Status, @Price, @Review, @Rating, @ReviewTime)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@ServiceProviderID", this.ServiceProviderID));
            lstParams.Add(new SqlParameter("@BookingDate", this.BookingDate));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@Price", this.Price));
            lstParams.Add(new SqlParameter("@Review", this.Review));
            lstParams.Add(new SqlParameter("@Rating", this.Rating));
            lstParams.Add(new SqlParameter("@ReviewTime", this.ReviewTime));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE ServiceBooking
                        SET UnitID=@UnitID, ServiceProviderID=@ServiceProviderID, BookingDate=@BookingDate, MemberID=@MemberID, Status=@Status, Price=@Price, Review=@Review, Rating=@Rating, ReviewTime=@ReviewTime
                        WHERE ServiceBookingID=@ServiceBookingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceBookingID", this.ServiceBookingID));
            lstParams.Add(new SqlParameter("@UnitID", this.UnitID));
            lstParams.Add(new SqlParameter("@ServiceProviderID", this.ServiceProviderID));
            lstParams.Add(new SqlParameter("@BookingDate", this.BookingDate));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@Price", this.Price));
            lstParams.Add(new SqlParameter("@Review", this.Review));
            lstParams.Add(new SqlParameter("@Rating", this.Rating));
            lstParams.Add(new SqlParameter("@ReviewTime", this.ReviewTime));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE ServiceBooking where ServiceBookingID=@ServiceBookingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceBookingID", this.ServiceBookingID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM ServiceBooking where ServiceBookingID=@ServiceBookingID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@ServiceBookingID", this.ServiceBookingID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.ServiceBookingID = Convert.ToInt32(dt.Rows[0]["ServiceBookingID"]);
                this.UnitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                this.ServiceProviderID = Convert.ToInt32(dt.Rows[0]["ServiceProviderID"]);
                this.BookingDate = Convert.ToDateTime(dt.Rows[0]["BookingDate"]);
                this.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.Price = dt.Rows[0]["Price"].ToString();
                this.Review = dt.Rows[0]["Review"].ToString();
                this.Rating = dt.Rows[0]["Rating"].ToString();
                this.ReviewTime = Convert.ToDateTime(dt.Rows[0]["ReviewTime"]);
                
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            query = "SELECT * FROM ServiceBooking";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}