﻿using System;
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

        //make generic function to call get procedures
        public DataTable execGetProc(string procname,List<SqlParameter> parameters=null)
        {
            
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(procname,con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if(parameters != null)
                    {
                        foreach(SqlParameter p in parameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    
                    con.Open();
                    adapter.Fill(dt);
                    con.Close();


                }

                return dt;
            
            }


        //gene func to call a sql query withpout proc
        public DataTable execQuery(string query)
        {

            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                con.Open();
                adapter.Fill(dt);
                con.Close();


            }

            return dt;

        }

        //query that return true or false
        public bool execquery(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                int i = 0;
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }


            }
        }




        public SqlConnection GetConnection()
        { 
            return con;
        }
        
        

    }
}
