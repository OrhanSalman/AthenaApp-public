using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Data;

namespace AthenaApp
{
    class DbConnector
    {
        public static SqlConnection ConnToDb()
        {
            try
            {
                SqlConnectionStringBuilder sqlbuilder = new SqlConnectionStringBuilder();
                sqlbuilder.DataSource = "ubi19.informatik.uni-siegen.de";
                sqlbuilder.UserID = "gruppe04-1";
                sqlbuilder.Password = "IvAg04!%§";
                sqlbuilder.InitialCatalog = "AthenaMain";

                SqlConnection sqlconn = new SqlConnection(sqlbuilder.ConnectionString);
                sqlconn.Open();

                if (sqlconn == null)
                {
                    Console.WriteLine("Datenbankverbindung fehlgeschlagen!");
                }
                else
                {
                    Console.Write("Datenbankverbindung hergestellt");
                }
                return sqlconn;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}