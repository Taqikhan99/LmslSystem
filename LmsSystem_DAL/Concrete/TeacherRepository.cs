﻿using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

using System.Web;



namespace LmsSystem_DAL.Concrete
{
    public class TeacherRepository : ITeacherRepository
    {
        DbClass db = new DbClass();

        public List<Class> GetClassTimes(string username)
        {
            List<Class> classes = new List<Class>();

            DataTable dt = db.execQuery(
                $"select c.ClassId,co.CourseName,c.ClassDay, CONCAT(t.StartTime,' - ',t.EndTime) ClassTime,cr.RoomName " +
                $"from UserTb u inner join ClassTb c on u.LinkId=c.TeacherId " +
                $"inner join CourseTb co on c.CourseId=co.CourseId inner join TimeSlotTb t on c.SlotId=t.Id " +
                $"inner join ClassRoomTb cr on c.RoomId=cr.RoomId where RoleId=2 and UserName='{username}' "

                );
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    classes.Add(new Class
                    {
                        Id = Convert.ToInt32(dr["ClassId"]),
                        CourseName = dr["CourseName"].ToString(),
                        ClassDay = dr["ClassDay"].ToString(),
                        ClassTime = dr["ClassTime"].ToString(),
                        Room = dr["RoomName"].ToString()
                    });
                   
                }
            }
            return classes;

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

        public Teacher GetTeacherProfile(string username)
        {
            Teacher teacher = null;
            DataTable dt = db.execQuery(
                $"select Id,UserPicture, UserName,DepartName,FirstName,LastName,Password,Email,Phone,[Address] from UserTb u inner join " +
                $"TeacherTb t on u.LinkId=t.TeacherId inner join DepartmentTb d on u.DepartId=d.DepartmentId " +
                $"where RoleId = 2 and UserName='{username}'"
                );
            if(dt.Rows.Count> 0)
            {
                DataRow r = dt.Rows[0];
                teacher = new Teacher
                {
                    Id = Convert.ToInt32(r["Id"]),
                    Username = r["UserName"].ToString(),
                    DepartName = r["DepartName"].ToString(),
                    FirstName = r["FirstName"].ToString(),
                    LastName = r["LastName"].ToString(),
                    Email = r["Email"].ToString(),
                    Phone = r["Phone"].ToString(),
                    Address = r["Address"].ToString(),
                    Password = r["Password"].ToString(),
                    UserPicPath = r["UserPicture"].ToString()
                };
            }

            return teacher;
            
        }

        public string UpdateTeacherProfile(Teacher teacher)
        {
            Teacher teacher1 = new Teacher();
            string extension =Path.GetExtension(teacher.UserPic.FileName);

            //check if extension is jpg, jpeg or png
            if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
            {


                string filename = HttpContext.Current.User.Identity.Name + "_" + Path.GetFileName(teacher.UserPic.FileName) + Path.GetExtension(teacher.UserPic.FileName);

                teacher.UserPicPath = "~/Content/Images/profilepics/" + filename;


                if (teacher.UserPic != null)
                {
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/profilepics/"), filename);
                    //string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/profilepics/"), HttpContext.Current.User.Identity.Name + "_" + Path.GetFileName((teacher.UserPic.FileName))+ Path.GetExtension(teacher.UserPic.FileName));
                    teacher.UserPic.SaveAs(path);
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@Id", teacher.Id));
                    sqlParameters.Add(new SqlParameter("@username", teacher.Username));
                    sqlParameters.Add(new SqlParameter("@password", teacher.Password));
                    sqlParameters.Add(new SqlParameter("@address", teacher.Address));
                    sqlParameters.Add(new SqlParameter("@phone", teacher.Phone));
                    sqlParameters.Add(new SqlParameter("@imagepath", teacher.UserPicPath));

                    bool updated = db.execInsertProc("spUpdateTeacherProfile", sqlParameters);

                    if (updated)
                    {
                        return "Profile Updated Success!";
                    }
                   
                }
            }
            else
            {
                return "Image should be in correct format";
            }
            return "Some Error Occured!";


        }

    }
}
