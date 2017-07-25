using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HackFest.Utilitys
{

    public class AccessEdiDb
    {
        static String conStrEdiDb = @"Server=tcp:hackfestsaison.database.windows.net,1433;Initial Catalog=hackfest;Persist Security Info=False;User ID=hackfest;Password=Sisco8181;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        static public string AuthVendorId(string[] args)
        {
            string returnJson = null;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = conStrEdiDb;
                cn.Open();

                SqlCommand cmd = new SqlCommand("dbo.authVendorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("argRetailId", SqlDbType.NVarChar));
                cmd.Parameters["argRetailId"].Direction = ParameterDirection.Input;
                cmd.Parameters["argRetailId"].Value = args[0];
                cmd.Parameters.Add(new SqlParameter("argVendorId", SqlDbType.NVarChar));
                cmd.Parameters["argVendorId"].Direction = ParameterDirection.Input;
                cmd.Parameters["argVendorId"].Value = args[1];

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine("結果セット:" + dr[0]);
                    returnJson = dr[0].ToString();
                }
                dr.Close();
            }
            return returnJson;
        }
    }
}
