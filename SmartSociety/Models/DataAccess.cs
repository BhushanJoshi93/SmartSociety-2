﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartSociety.Models
{
    public class DataAccess
    {
        public static int ModifyData(string query, List<SqlParameter> lstParams)
        {
            SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\the_g\Desktop\JayCollege\JayProject\.NET Project\SmartSociety\SmartSociety\App_Data\SmartSocietyDB.mdf;Integrated Security=True";

            SqlCommand command1 = new SqlCommand();
            command1.CommandText = query;
            command1.Connection = connection1;
            for (int i = 0; i < lstParams.Count; i++)
            {
                if (lstParams[i].Value == null)
                {
                    lstParams[i].Value = DBNull.Value;
                }
                command1.Parameters.Add(lstParams[i]);
            }

            connection1.Open();

            int x = command1.ExecuteNonQuery();

            connection1.Close();

            return x;
        }

        public static DataTable SelectData(string query, List<SqlParameter> lstParams)
        {
            SqlConnection connection1 = new SqlConnection();
            connection1.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\the_g\Desktop\JayCollege\JayProject\.NET Project\SmartSociety\SmartSociety\App_Data\SmartSocietyDB.mdf;Integrated Security=True";

            SqlCommand command1 = new SqlCommand();
            command1.CommandText = query;
            command1.Connection = connection1;
            for (int i = 0; i < lstParams.Count; i++)
            {
                if (lstParams[i].Value == null)
                {
                    lstParams[i].Value = DBNull.Value;
                }
                command1.Parameters.Add(lstParams[i]);
            }

            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = command1;

            DataTable dt = new DataTable();

            connection1.Open();

            adapter1.Fill(dt);

            connection1.Close();

            return dt;

        }
    }
}