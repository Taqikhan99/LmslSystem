using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Abstract
{
    public interface IUserRepository
    {
        bool AddUser(User user);

        bool AddProgram(Programs p);
        bool AddCourse(Course course);

        //bool AddClassRoom(ClassRoom room);

        //bool AddClass (Class c);
        bool UpdateUser(User user);
        //bool DeleteUser(User user);
        List<User> GetStudents();

        List<User> GetTeachers();
        List<Department> getDepartmentOptions();
        List<UsersByDepart> GetStudentsByDepart(int id);

        List<Course> GetAllCourses();
        List<Programs> GetAllPrograms(); 

        List<Roles> getRolesOptions();
    }
}
