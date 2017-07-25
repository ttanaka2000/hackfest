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

        static public string ExecProcedures(string[] args)
        {
            string returnJson = null;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = conStrEdiDb;
                cn.Open();

                SqlCommand cmd = new SqlCommand(args[0], cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("argRetailId", SqlDbType.NVarChar));
                cmd.Parameters["argRetailId"].Direction = ParameterDirection.Input;
                cmd.Parameters["argRetailId"].Value = args[1];
                cmd.Parameters.Add(new SqlParameter("argVendorId", SqlDbType.NVarChar));
                cmd.Parameters["argVendorId"].Direction = ParameterDirection.Input;
                cmd.Parameters["argVendorId"].Value = args[2];

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


public class AuthVendorId
{
    private string retailId
    {
        set { this.retailId = value; }
        get { return this.retailId; }
    }

    private string vendorId
    {
        set { this.vendorId = value; }
        get { return this.retailId; }
    }

    private string result
    {
        get { return this.result; }
    }

    AuthVendorId(string[] arg)
    {
        this.retailId = arg[0];
        this.vendorId = arg[1];
    }

    void Main()
    {
        string[] inputarray = new string[3];
        inputarray[0] = "dbo.authVendorId";
        inputarray[1] = this.retailId;
        inputarray[2] = this.vendorId;

        string responseJson = AccessEdiDb.ExecProcedures(inputarray);





    }








}
}