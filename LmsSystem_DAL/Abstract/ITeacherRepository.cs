using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Abstract
{
    public interface ITeacherRepository
    {
        List<Course> GetTeacherCourses(string username);

        List<Class> GetClassTimes(string username);

        Teacher GetTeacherProfile(string username);

        string UpdateTeacherProfile(Teacher teacher);
    }
}
