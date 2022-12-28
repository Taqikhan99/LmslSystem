using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Entities;
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
    public class CourseRelatedRepository : ICourseRelatedRepository
    {
        DbClass db = new DbClass();
        //private SqlConnection con;
        //string constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();

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


        //Add new course
        public bool AddCourse(Course course)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@CourseName", course.CourseName));
            sqlParameters.Add(new SqlParameter("@ProgramId", course.ProgramId));
            bool added = db.execInsertProc("spAddCourse", sqlParameters);

            return added;

        }
        //ADD DEPARTMENT
        public bool AddDepartment(Department d)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Departname", d.DepartmentName));
            sqlParameters.Add(new SqlParameter("@StartDate", d.StartDate));

            bool added = db.execInsertProc("spAddDepart", sqlParameters);

            return added;
        }

        //ADD PROGRAM
        public bool AddProgram(Programs p)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@progName", p.ProgramName));
            sqlParameters.Add(new SqlParameter("@departId", p.DepartId));
            bool added = db.execInsertProc("spAddProgram", sqlParameters);

            return added;

        }

        //get classes
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

                });
            }

            return classes;
        }
        //get all courses
        public List<Course> GetAllCourses()
        {

            List<Course> courses = new List<Course>();

            DataTable dt = db.execGetProc("spGetCourses");

            foreach (DataRow dr in dt.Rows)
            {
                //add new course to data table for each row get
                courses.Add(new Course
                {
                    CourseId = Convert.ToInt32(dr["CourseId"]),
                    CourseName = dr["CourseName"].ToString(),
                    ProgramName = dr["ProgramName"].ToString()
                   
                });

            }
            return courses;
        }

        //Get all programs

        public List<Programs> GetAllPrograms()
        {

            List<Programs> programs = new List<Programs>();
            DataTable dt = db.execGetProc("spGetPrograms");

            foreach (DataRow dr in dt.Rows)
            {
                programs.Add(new Programs
                {
                    ProgramId = Convert.ToInt32(dr["ProgramId"]),
                    ProgramName = dr["ProgramName"].ToString(),
                    DepartId = Convert.ToInt32(dr["DepartId"])
                });
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

        //get department options
        public List<Department> getDepartmentOptions()
        {
            //make departmnt list
            List<Department> departments = new List<Department>();

            DataTable dt = db.execGetProc("spGetDepartmentOptions");

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(new Department
                {
                    DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                    DepartmentName = row["DepartName"].ToString()
                });
            }

            return departments;
        }
        //get departments
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

        //get program options
        public List<Programs> getProgramsOptions(int id)
        {
            List<Programs> programs = new List<Programs>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dt;
            if (id > 0)
            {
                parameters.Add(new SqlParameter("@depId", id));
                dt = db.execGetProc("spGetProgramOptions", parameters);
            }
            else
            {
                dt = db.execGetProc("spGetPrograms");
            }
            if (dt.Rows.Count > 0)
            {
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

        /// <summary>
        /// Get roles options
        /// </summary>
        /// <returns></returns>
        public List<Roles> getRolesOptions()
        {
            
            //make departmnt list
            List<Roles> roles = new List<Roles>();

            DataTable dt = db.execGetProc("spGetRoles");

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

        //get time slots
        public List<TimeSlot> GetTimeSlots()
        {
            List<TimeSlot> slots = new List<TimeSlot>();
            DataTable dt = db.execQuery("Select * from TimeSlotTb");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
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
        public List<Classroom> GetClassrooms(string day, int timeid)
        {
            List<Classroom> classrooms = new List<Classroom>();

            DataTable dt = db.execQuery($@"select cr.RoomId,cr.RoomName from ClassRoomTb cr 
                where cr.RoomId not in( Select c.RoomId from ClassTb c where ClassDay= '{day}' and SlotId = {timeid})");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
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
        //getting teacher options
        public List<Teacher> GetTeacherOptions(string day)
        {
            List<Teacher> teachers = new List<Teacher>();

            DataTable dt = db.execQuery($"select u.Id,t.FirstName+' '+t.LastName as TeacherName from UserTb u inner join TeacherTb t" +
                      $" on u.Id=t.TeacherId where u.Id not in( Select  TeacherId from ClassTb where ClassDay='{day}') ");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
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


        //delete course
        public bool DeleteCourse(int id)
        {
            bool deleted = db.execquery("delete from CourseTb where CourseId = " + id);

            return deleted;
        }


        //delete class
        public bool DeleteClass(int id)
        {
            bool deleted = db.execquery("delete from ClassTb where ClassId = " + id);

            return deleted;
        }
    }

    

}
