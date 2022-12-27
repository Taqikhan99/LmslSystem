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

        //Add new course
        public bool AddCourse(Course course) 
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@CourseName", course.CourseName));
            sqlParameters.Add(new SqlParameter("@ProgramId", course.ProgramId));
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

            SqlCommand cmd = new SqlCommand("spGetDepartmentOptions", con);

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

        public List<Programs> getProgramsOptions(int id =0)
        {
            List<Programs> programs = new List<Programs>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dt;
            if (id > 0)
            {   parameters.Add(new SqlParameter("@depId", id));
                dt = db.execGetProc("spGetProgramOptions", parameters);
            }
            else
            {
                dt = db.execGetProc("spGetPrograms");
            }
            if (dt.Rows.Count > 0) {
                foreach (DataRow r in dt.Rows)
                {
                    programs.Add(new Programs
                    {
                        ProgramId = Convert.ToInt32(r["ProgramId"]),
                        ProgramName = r["ProgramName"].ToString(),
                        DepartId = Convert.ToInt32(r["DepartId"])
                    });

                }
            }


            return programs;
        }


        //get course options
        public List<Course> getCourseOptions(int id = 0)
        {
            List<Course> courses = new List<Course>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            
            
            parameters.Add(new SqlParameter("@progId", id));
            DataTable dt = db.execGetProc("spGetCourseOptions", parameters);
            
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    courses.Add(new Course
                    {
                        ProgramId = Convert.ToInt32(r["ProgramId"]),
                        CourseName = r["CourseName"].ToString(),
                        CourseId = Convert.ToInt32(r["CourseId"])
                    });

                }
            }


            return courses;
        }

        //get class room based on day and time slot



        //get time slots
        public List<TimeSlot> GetTimeSlots()
        {
            List<TimeSlot> slots = new List<TimeSlot>();
            DataTable dt = db.execQuery("Select * from TimeSlotTb");

            if (dt.Rows.Count > 0)
            {
                foreach(DataRow r in dt.Rows)
                {
                    slots.Add(new TimeSlot
                    {
                        Id = Convert.ToInt32(r["Id"]),
                        StartTime = r["StartTime"].ToString(),
                        EndTime = r["EndTime"].ToString()
                    });
                }
            }
            return slots;
        }


        //get classroom options based on condition where day and time slots do not match.
        public List<Classroom> GetClassrooms(string day,int timeid)
        {
            List<Classroom> classrooms = new List<Classroom>();

            DataTable dt = db.execQuery($@"select cr.RoomId,cr.RoomName from ClassRoomTb cr 
                where cr.RoomId not in( Select c.RoomId from ClassTb c where ClassDay= '{day}' and SlotId = {timeid})");

            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    classrooms.Add(new Classroom
                    {
                        Id = Convert.ToInt32(dr["RoomId"]),
                        RoomName = dr["RoomName"].ToString(),
                    });
                }
            }

            return classrooms;
        }

        public List<Teacher> GetTeacherOptions(string day)
        {
            List<Teacher> teachers= new List<Teacher>();

            DataTable dt = db.execQuery($"select u.Id,t.FirstName+' '+t.LastName as TeacherName from UserTb u inner join TeacherTb t" +
                      $" on u.Id=t.TeacherId where u.Id not in( Select  TeacherId from ClassTb where ClassDay='{day}') ");
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    teachers.Add(new Teacher
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["TeacherName"].ToString()
                    });
                }
            }
            return teachers;
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



        public User GetUserByIdRole(int id,int roleId) {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@uid", id));
            sqlParameters.Add(new SqlParameter("@roleid", roleId));

            User user = new User();

            DataTable dt = db.execGetProc("spGetUserByIdnRole", sqlParameters);

            if(dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                user.Id = Convert.ToInt32(r["Id"]);
                user.FirstName = r["FirstName"].ToString();
                user.LastName = r["LastName"].ToString();
                user.Email = r["Email"].ToString();
                user.Phone = r["Phone"].ToString() ;
                user.JoinedDate = Convert.ToDateTime(r["JoinedDate"]);
                user.DepartmentId= Convert.ToInt32(r["DepartId"]);
                user.Password= r["Password"].ToString();
            }
            return user;


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

        
        //delete student
        public bool DeleteUser(int id, int roleid)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@stdId",id));
            sqlParameters.Add(new SqlParameter("@roleId", roleid));
            bool deleted = db.execInsertProc("spDeleteUser ", sqlParameters);
            return deleted;
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


        

        

        public bool AddProgram(Programs p)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@progName", p.ProgramName));
            sqlParameters.Add(new SqlParameter("@departId", p.DepartId));
            bool added = db.execInsertProc("spAddProgram", sqlParameters);

            return added;
            
        }

        public List<Programs> GetAllPrograms()
        {
            
            List<Programs> programs = new List<Programs>();
            DataTable dt = db.execGetProc("spGetPrograms"); 

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

        public bool AddClass(Class cl)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@teacherId", cl.TeacherId));
            sqlParameters.Add(new SqlParameter("@courseId", cl.CourseId));
            sqlParameters.Add(new SqlParameter("@classDay", cl.ClassDay));
            sqlParameters.Add(new SqlParameter("@roomId", cl.ClassRoomId));
            sqlParameters.Add(new SqlParameter("@slotId", cl.SlotId));

            bool added = db.execInsertProc("spAddClass", sqlParameters);

            return added;
        }

        public List<Class> GetAllClasses()
        {
            List<Class> classes = new List<Class>();
            DataTable dt = db.execGetProc("spGetClass");

            foreach (DataRow dr in dt.Rows)
            {
                classes.Add(new Class
                {
                    ClassId = Convert.ToInt32(dr["ClassId"]),
                    TeacherName = dr["TeacherName"].ToString(),
                    CourseName = dr["CourseName"].ToString(),
                    ClassDay = dr["ClassDay"].ToString(),
                    Room = dr["RoomName"].ToString(),
                    ClassTime = dr["Classtime"].ToString()

                }) ;
            }

            return classes;



            throw new NotImplementedException();
        }


        ///=================================///
        ///Department Related
        /// ===
        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            DataTable dt = db.execGetProc("spGetDepartments");

            foreach (DataRow dr in dt.Rows)
            {
                departments.Add(new Department
                {
                    DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                    DepartmentName = dr["DepartName"].ToString(),
                    StartYear = dr["StartYear"].ToString(),
                    StudentsEnrolled = Convert.ToInt32(dr["StudentsEnrolled"])

                });
            }

            return departments;
        }

        public bool AddDepartment(Department d)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Departname", d.DepartmentName));
            sqlParameters.Add(new SqlParameter("@StartDate", d.StartDate));

            bool added = db.execInsertProc("spAddDepart", sqlParameters);

            return added;
        }


    }
}


