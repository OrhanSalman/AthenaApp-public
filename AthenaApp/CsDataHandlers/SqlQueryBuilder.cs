using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace AthenaApp.CsDataHandlers
{
    class SqlQueryBuilder
    {


        private String sqlQuery { get; set; }



        private void BuildQuery(SqlConnection sqlconn, String database, String table, String method, List<String> columns, List<String> values)
        {

            

            sqlQuery += "USE [" + database + "]" + method + " [" + table + "]";


                sqlQuery += " (" + columns[0];
                for (int i = 1; i < columns.Count; i++)
                {
                    sqlQuery += ", " + columns[i];

                    if (i == columns.Count)
                    {
                        sqlQuery += ")";
                    }
                }



                sqlQuery += " (" + values[0];
                for (int i = 1; i < values.Count; i++)
                {
                    sqlQuery += ", " + values[i];

                    if (i == values.Count)
                    {
                        sqlQuery += ")";

                    }
                }



            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlconn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error in executing SQL-Command:\n " + e.ToString());
                }
                finally
                {
                    sqlconn.Close();
                }

            }
        }
    }
}
