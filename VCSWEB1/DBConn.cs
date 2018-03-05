using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace VCSWEB1
{
    public class DBConn
    {

        public static DataTable ExecQuery(string command) {
            
            string CONSTR = WebConfigurationManager.AppSettings["ConStr"];
            using (SqlConnection conn = new SqlConnection(CONSTR)) {
                using (SqlDataAdapter sda = new SqlDataAdapter(command, CONSTR)) {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
        }

        public static int ExecNonQuery(string command) {
            string CONSTR = WebConfigurationManager.AppSettings["ConStr"];
            using (SqlConnection conn = new SqlConnection(CONSTR)) {
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.Connection.Open();
                int rows_affected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return rows_affected;
            }
        }

        public static DataTable ExecStoredProcedure(string storedprocedure, Dictionary<string, object> Params = null)
        {
            string CONSTR = WebConfigurationManager.AppSettings["ConStr"];
            using (SqlConnection conn = new SqlConnection(CONSTR))
            {
                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = storedprocedure;
                    if (Params != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in Params)
                            comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
        }
    }
}