using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Concrete
{
    public class DbClass
    {
        string constr;
        SqlConnection con;
        public DbClass() {
            constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
            con = new SqlConnection(constr);
        }

        //procedure call for insert
        public bool execInsertProc(string procname,List<SqlParameter> sqlParameters)
        {
            try
            {
                int i = 0;


                using (SqlCommand cmd = new SqlCommand(procname, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        foreach (SqlParameter p in sqlParameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (i >= 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) { return false; }
            




        }

        public SqlConnection GetConnection()
        { 
            return con;
        }
        
        

    }
}
