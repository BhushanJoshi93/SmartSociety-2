using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class Notice
    {
        public int NoticeID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Attachment { get; set; }
        public DateTime NoticeDate { get; set; }
        public int MemberID { get; set; }

        public string query;
        public int Insert()
        {
            query = "INSERT INTO Notice VALUES(@Title, @Details, @Attachment, @NoticeDate, @MemberID)";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Title", this.Title));
            lstParams.Add(new SqlParameter("@Details", this.Details));
            lstParams.Add(new SqlParameter("@Attachment", this.Attachment));
            lstParams.Add(new SqlParameter("@NoticeDate", this.NoticeDate));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Update()
        {
            query = @"UPDATE Notice
                        SET Title=@Title, Details=@Details, Attachment=@Attachment, NoticeData=@NoticeDate, MemberID=@MemberID
                        WHERE NoticeID=@NoticeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@NoticeID", this.NoticeID));
            lstParams.Add(new SqlParameter("@Title", this.Title));
            lstParams.Add(new SqlParameter("@Details", this.Details));
            lstParams.Add(new SqlParameter("@Attachment", this.Attachment));
            lstParams.Add(new SqlParameter("@NoticeDate", this.NoticeDate));
            lstParams.Add(new SqlParameter("@MemberID", this.MemberID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public int Delete()
        {
            query = "DELETE Notice where NoticeID=@NoticeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@NoticeID", this.NoticeID));
            return DataAccess.ModifyData(query, lstParams);
        }
        public bool SelectByID()
        {
            query = "SELECT * FROM Notice where NoticeID=@NoticeID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@NoticeID", this.NoticeID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.NoticeID = Convert.ToInt32(dt.Rows[0]["NoticeID"]);
                this.Title = dt.Rows[0]["Title"].ToString();
                this.Details = dt.Rows[0]["Details"].ToString();
                this.Attachment = dt.Rows[0]["Attachment"].ToString();
                this.NoticeDate = Convert.ToDateTime(dt.Rows[0]["NoticeDate"]);
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
            query = "SELECT * FROM Notice";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstParams);
            return dt;
        }
    }
}