using System;
using System.Collections.Generic;
using System.Configuration;
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

        public SqlConnection GetConnection()
        { 
            return con;
        }
        
        

    }
}
