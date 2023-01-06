using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Concrete
{
    public class TeacherRepository : ITeacherRepository
    {
        DbClass db = new DbClass();

        public List<Class> GetClassTimes()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetTeacherCourses(string username)
        {
            List<Course> courseList = new List<Course>();

            string query = $"Select co.CourseId,co.CourseName from UserTb u inner join ClassTb c on u.LinkId = c.TeacherId inner join courseTb co on " +
                $"c.CourseId=co.CourseId where RoleId =2 and UserName='{username}'";

            DataTable dt =db.execQuery(query);
            if (dt.Rows.Count > 0 )
            {
                foreach(DataRow dr in dt.Rows)
                {
                    courseList.Add(new Course
                    {
                        CourseId = Convert.ToInt32(dr["CourseId"]),
                        CourseName = dr["CourseName"].ToString(),
                    });
                }
            }
            return courseList;

            throw new NotImplementedException();
        }
    }
}
