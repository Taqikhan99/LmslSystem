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
        //private SqlConnection con;
        //string constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
        //void connection()
        //{
            
        //    con = new SqlConnection(constr);
        //}

        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddStudent(Student user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Firstname", user.FirstName));
            sqlParameters.Add(new SqlParameter("@Lastname", user.LastName));
            sqlParameters.Add(new SqlParameter("@Email", user.Email));
            sqlParameters.Add(new SqlParameter("@Phone", user.Phone));
            sqlParameters.Add(new SqlParameter("@Birthdate", user.Birthdate));
            sqlParameters.Add(new SqlParameter("@Semester", 1));
            sqlParameters.Add(new SqlParameter("@Username", user.Username));
            sqlParameters.Add(new SqlParameter("@Password", user.Password));
            sqlParameters.Add(new SqlParameter("@DepartId", user.DepartId));

            bool added = db.execInsertProc("spAddStudent", sqlParameters);

            return added;

        }

        //Add Teacher
        public bool AddTeacher(Teacher user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@FirstName", user.FirstName));
            sqlParameters.Add(new SqlParameter("@LastName", user.LastName));
            sqlParameters.Add(new SqlParameter("@Email", user.Email));
            sqlParameters.Add(new SqlParameter("@Phone", user.Phone));
            sqlParameters.Add(new SqlParameter("@Address", user.Address));
            sqlParameters.Add(new SqlParameter("@Salary", user.Salary));
            sqlParameters.Add(new SqlParameter("@Designation", user.Designation));
            sqlParameters.Add(new SqlParameter("@Username", user.Username));
            sqlParameters.Add(new SqlParameter("@DepartId", user.DepartId));
            sqlParameters.Add(new SqlParameter("@Password", user.Password));

            bool added = db.execInsertProc("spAddTeacher", sqlParameters);

            return added;

        }
        
        
               
        public List<Student> GetStudents()
        {
            
            List<Student> students= new List<Student>();

            DataTable dataTable = db.execGetProc("spGetStudents");

            foreach (DataRow dr in dataTable.Rows)
            {
                students.Add(new Student
                {
                    Id = Convert.ToInt32(dr["StudentId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Semester= Convert.ToInt32(dr["Semester"]),
                    Username= Convert.ToString(dr["UserName"]),
                    Joineddate = Convert.ToDateTime(dr["JoinedDate"])
                });
            }
            return students;

        }
        //Get Student by id
        public Student GetStudentById(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@stdId", id));

            Student std = new Student();
            DataTable dt = db.execGetProc("spGetStdById", sqlParameters);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                std.Id = Convert.ToInt32(r["StudentId"]);
                std.FirstName = r["FirstName"].ToString();
                std.LastName = r["LastName"].ToString();
                std.Email = r["Email"].ToString();
                std.Phone = r["Phone"].ToString();
                std.Birthdate = Convert.ToDateTime(r["BirthDate"]) ;
                std.Semester= Convert.ToInt32(r["Semester"]);
                std.Username = Convert.ToString(r["UserName"]);
                std.DepartId = Convert.ToInt32(r["DepartId"]);
                std.Password = r["Password"].ToString();
            }

            return std;
        }


        //Get Student Details and display on Details page
        public Student GetStudentDetails(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@stdId", id));

            Student std = new Student();
            DataTable dt = db.execGetProc("spGetStdDetails", sqlParameters);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                std.Id = Convert.ToInt32(r["StudentId"]);
                std.FirstName = r["FirstName"].ToString();
                std.LastName = r["LastName"].ToString();
                std.Email = r["Email"].ToString();
                std.Phone = r["Phone"].ToString();
                std.Birthdate = Convert.ToDateTime(r["BirthDate"]);
                std.Age = Convert.ToInt32(r["Age"]);
                std.Semester = Convert.ToInt32(r["Semester"]);
                std.Username = Convert.ToString(r["UserName"]);
                std.Departname = Convert.ToString(r["DepartName"]);
                std.Joineddate = Convert.ToDateTime(r["JoinedDate"]);
            }

            return std;

        }







        public Teacher GetTeacherDetails(int id)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@tId", id));

            Teacher t = new Teacher();
            DataTable dt = db.execGetProc("spGetTeacherById", sqlParameters);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                t.Id = Convert.ToInt32(r["TeacherId"]);
                t.FirstName = r["FirstName"].ToString();
                t.LastName = r["LastName"].ToString();
                t.Email = r["Email"].ToString();
                t.Phone = r["Phone"].ToString();
                t.Address = Convert.ToString(r["Address"]);
                t.Designation = Convert.ToString(r["Designation"]);
                t.Username = Convert.ToString(r["UserName"]);
                t.DepartName= Convert.ToString(r["DepartName"]);
                t.Password = r["Password"].ToString();
                t.Salary = Convert.ToInt32(r["Salary"]);
            }

            return t;


        }


        //update Student
        public bool UpdateStudent(Student s)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@UserId", s.Id));
            sqlParameters.Add(new SqlParameter("@FirstName", s.FirstName));
            sqlParameters.Add(new SqlParameter("@LastName", s.LastName));
            sqlParameters.Add(new SqlParameter("@Email", s.Email));
            sqlParameters.Add(new SqlParameter("@Phone", s.Phone));
            sqlParameters.Add(new SqlParameter("@Birthdate", s.Birthdate)); 
            sqlParameters.Add(new SqlParameter("@Semester", s.Semester));
            sqlParameters.Add(new SqlParameter("@Username", s.Username));
            sqlParameters.Add(new SqlParameter("@DepartId", s.DepartId));      
            sqlParameters.Add(new SqlParameter("@Password", s.Password));

            bool updated = db.execInsertProc("spUpdateStd", sqlParameters);

            return updated;

        }

        

        /// <summary>
        /// Get All Teachers
        /// </summary>
        /// <returns>list of teachers</returns>
        public List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            DataTable dataTable = db.execGetProc("spGetTeachers");

            foreach (DataRow dr in dataTable.Rows)
            {
                teachers.Add(new Teacher
                {
                    Id = Convert.ToInt32(dr["TeacherId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Email = Convert.ToString(dr["Email"]),
                    Designation = Convert.ToString(dr["Designation"]),
                    Username = Convert.ToString(dr["UserName"]),
                    Joineddate = Convert.ToDateTime(dr["JoinedDate"])
                    
                });
            }

            return teachers;
        }

        //Get Teacher by Id
        public Teacher GetTeacherById(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@tId", id));

            Teacher t = new Teacher();
            DataTable dt = db.execGetProc("spGetTeacherById", sqlParameters);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                t.Id = Convert.ToInt32(r["TeacherId"]);
                t.FirstName = r["FirstName"].ToString();
                t.LastName = r["LastName"].ToString();
                t.Email = r["Email"].ToString();
                t.Phone = r["Phone"].ToString();
                t.Address = Convert.ToString(r["Address"]);
                t.Designation = Convert.ToString(r["Designation"]);
                t.Username = Convert.ToString(r["UserName"]);
                t.DepartId = Convert.ToInt32(r["DepartId"]);
                t.Password = r["Password"].ToString();
                t.Salary = Convert.ToInt32(r["Salary"]);
            }

            return t;
        } 



        /// <summary>
        /// Get Students by depart
        /// </summary>
        /// <param name="depId"></param>
        /// <returns>List of students department wise</returns>
        public List<UsersByDepart> GetStudentsByDepart(int depId)
        {
            List<UsersByDepart> students = new List<UsersByDepart>();
            List<SqlParameter> sps = new List<SqlParameter>();
            
            sps.Add(new SqlParameter("departid", depId));
            DataTable dataTable = db.execGetProc("spGetStudentsByDep",sps);

            foreach (DataRow dr in dataTable.Rows)
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

        /// Get Teachers by department
        
        public List<UsersByDepart> GetTeachersByDepart(int depId)
        {
            List<UsersByDepart> teachers= new List<UsersByDepart>();
            List<SqlParameter> sps = new List<SqlParameter>();

            sps.Add(new SqlParameter("departid", depId));
            DataTable dataTable = db.execGetProc("spGetStudentsByDep", sps);

            foreach (DataRow dr in dataTable.Rows)
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


        

        public bool DeleteTeacher(int id)
        {
            bool deleted = false;
            //check first if teacher is teaching a course

            DataTable dt = db.execQuery($"Select * from ClassTb where TeacherId= {id}");

            if (dt.Rows.Count == 0)
            {
                deleted = db.execquery($"delete from UserTb where LinkId={id};delete from TeacherTb where TeacherId = {id};");
                return deleted;
            }

            return deleted;
        }

        public bool DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
