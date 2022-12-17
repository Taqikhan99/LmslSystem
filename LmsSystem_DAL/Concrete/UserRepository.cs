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
        DbClass db = new DbClass();
        private SqlConnection con;
        string constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
        void connection()
        {
            
            con = new SqlConnection(constr);
        }

        

        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@FirstName", user.FirstName));
            sqlParameters.Add(new SqlParameter("@LastName", user.LastName));
            sqlParameters.Add(new SqlParameter("@Email", user.Email));
            sqlParameters.Add(new SqlParameter("@Phone", user.Phone));
            sqlParameters.Add(new SqlParameter("@RoleId", user.RoleId));
            sqlParameters.Add(new SqlParameter("@DepartmentId", user.DepartmentId));
            sqlParameters.Add(new SqlParameter("@Password", user.Password));

            bool added = db.execInsertProc("spAddUser", sqlParameters);

            return added;

        }

        //Add new course
        public bool AddCourse(Course course)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@CourseName", course.CourseName));
            sqlParameters.Add(new SqlParameter("@CourseName", course.ProgramId));
            bool added = db.execInsertProc("spAddCourse", sqlParameters);

            return added;

        }

        /// <summary>
        /// Get Departments to populate the select box while creating nnew user
        /// </summary>
        /// <returns></returns>
        /// 
        public List<Department> getDepartmentOptions()
        {
            connection();
            //make departmnt list
            List<Department> departments = new List<Department>();

            SqlCommand cmd = new SqlCommand("spGetDepartments", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach(DataRow row in dt.Rows)
            {
                departments.Add(new Department
                {
                    DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                    DepartmentName = row["DepartName"].ToString()
                }); 
            }

            return departments;


        }

        /// <summary>
        /// Get roles options
        /// </summary>
        /// <returns></returns>
        public List<Roles> getRolesOptions()
        {
            connection();
            //make departmnt list
            List<Roles> roles = new List<Roles>();

            SqlCommand cmd = new SqlCommand("spGetRoles", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                roles.Add(new Roles
                {
                    RoleId = Convert.ToInt32(row["RoleId"]),
                    RoleName = row["RoleName"].ToString()
                });
            }

            return roles;


        }

        public List<Programs> getProgramsOptions()
        {
            connection();
            //make departmnt list
            List<Programs> programs = new List<Programs>();

            SqlCommand cmd = new SqlCommand("spGetPrograms", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                programs.Add(new Programs
                {
                    ProgramId = Convert.ToInt32(row["ProgramId"]),
                    ProgramName = row["ProgramName"].ToString(),
                    DepartId= Convert.ToInt32(row["DepartId"])
                });
            }

            return programs;
        }

        //public bool DeleteUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        public List<User> GetStudents()
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
                    DepartmentId = Convert.ToInt32(dr["DepartId"]),
                    JoinedDate= Convert.ToDateTime(dr["JoinedDate"]),
                    Phone= Convert.ToString(dr["Phone"])
                });
            }

            return students;


        }
        /// <summary>
        /// Get All Teachers
        /// </summary>
        /// <returns>list of teachers</returns>
        public List<User> GetTeachers()
        {
            connection();
            List<User> teachers = new List<User>();
            SqlCommand cmd = new SqlCommand("spGetTeachers", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            //add to list
            foreach (DataRow dr in dt.Rows)
            {
                teachers.Add(new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    RoleId = Convert.ToInt32(dr["RoleId"]),
                    DepartmentId = Convert.ToInt32(dr["DepartId"]),
                    JoinedDate = Convert.ToDateTime(dr["JoinedDate"]),
                    Phone = Convert.ToString(dr["Phone"])
                });
            }

            return teachers;
        }

        /// <summary>
        /// Get Students by depart
        /// </summary>
        /// <param name="depId"></param>
        /// <returns>List of students department wise</returns>
        public List<UsersByDepart> GetStudentsByDepart(int depId)
        {
            connection();
            List<UsersByDepart> students = new List<UsersByDepart>();
            SqlCommand cmd = new SqlCommand("spGetStudentsByDep", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@departid", depId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            //add to list
            foreach (DataRow dr in dt.Rows)
            {
                students.Add(new UsersByDepart
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    UserName = Convert.ToString(dr["UserName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    DepartName = Convert.ToString(dr["DepartName"])
                    
                });
            }

            return students;

        }

        /// <summary>
        /// Get Teachers by department
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public List<UsersByDepart> GetTeachersByDepart(int depId)
        {
            connection();
            List<UsersByDepart> teachers = new List<UsersByDepart>();
            SqlCommand cmd = new SqlCommand("spGetTeachersByDep", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@departid", depId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();
            //add to list
            foreach (DataRow dr in dt.Rows)
            {
                teachers.Add(new UsersByDepart
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    UserName = Convert.ToString(dr["UserName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    DepartName = Convert.ToString(dr["DepartName"])

                });
            }

            return teachers;

        }

        //get all courses

        public List<Course> GetAllCourses()
        {
            connection();
            List<Course> courses = new List<Course>();
            SqlCommand cmd = new SqlCommand("spGetCourses", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                //add new course to data table for each row get
                courses.Add(new Course
                {
                    CourseId = Convert.ToInt32(dr["CourseId"]),
                    CourseName = dr["CourseName"].ToString()
                }) ;

            }

            return courses;
        }

       

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool AddProgram(Programs p)
        {
            connection();
            SqlCommand cmd = new SqlCommand("spAddProgram", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@progName", p.ProgramName);
            cmd.Parameters.AddWithValue("@departId", p.DepartId);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;

            
        }

        public List<Programs> GetAllPrograms()
        {
            connection();
            List<Programs> programs = new List<Programs>();
            SqlCommand cmd = new SqlCommand("spGetPrograms", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close(); 

            foreach(DataRow dr in dt.Rows)
            {
                programs.Add(new Programs
                {
                    ProgramId = Convert.ToInt32( dr["ProgramId"]),
                    ProgramName = dr["ProgramName"].ToString(),
                    DepartId = Convert.ToInt32(dr["DepartId"])
                });
            }

            return programs;

        }
        //add and get classes

        public bool AddClass(Class c)
        {
            throw new NotImplementedException();
        }

        public List<Class> GetAllClasses()
        {
            connection();
            List<Class> classes = new List<Class>();
            SqlCommand cmd = new SqlCommand("spGetClass", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                classes.Add(new Class
                {
                    ClassId = Convert.ToInt32(dr["ClassId "]),
                    UserId = Convert.ToInt32(dr["UserId"]),
                    CourseId = Convert.ToInt32(dr["CourseId"]),
                    ClassDay = dr["ClassDay"].ToString(),
                    ClassTime = Convert.ToDateTime( dr["ClassTime"])
                });
            }

            return classes;



            throw new NotImplementedException();
        }
    }
}
