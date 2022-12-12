using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LmsSystem_DAL.Concrete
{
    public class UserRepository : IUserRepository
    {

        private SqlConnection con;

        void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
            con = new SqlConnection(constr);
        }

        public bool AddUser(User user)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spAddUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
            cmd.Parameters.AddWithValue("@DepartId", user.DepartId);
            cmd.Parameters.AddWithValue("@JoinedDate", user.JoinedDate);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            //open conn
            con.Open();
            int i = cmd.ExecuteNonQuery();
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

        //public bool DeleteUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        public void GetStudents()
        {
            connection();
            List<User> students= new List<User>();  
            SqlCommand cmd = new SqlCommand("spGetStudents",con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            //add to list
            foreach (DataRow dr in dt.Rows)
            {
                students.Add(new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    RoleId = Convert.ToInt32(dr["RoleId"]),
                    Password = Convert.ToString(dr["Password"]),
                    DepartId = Convert.ToInt32(dr["DepartId"]),
                    JoinedDate= Convert.ToDateTime(dr["JoinedDate"]),
                    Phone= Convert.ToString(dr["Phone"])
                });
            }


        }

        public void GetTeachers()
        {

        }

        public void GetStudentsByDepart(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
