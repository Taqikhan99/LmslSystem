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
        bool AddStudent(User user);
        bool AddTeacher(User user);
        bool AddProgram(Programs p);
        bool AddCourse(Course course);

        bool AddClass(Class c);

        UserDetails GetUserDetails(int id,int roleid);

        bool UpdateUser(User user);
        //bool UpdateTeacher(User user);
        bool DeleteUser(int id,int roleid);
        List<User> GetStudents();

        User GetUserByIdRole(int id, int roleId);
        //User GetTeacherById(int id);

        List<User> GetTeachers();
        List<Department> getDepartmentOptions();
        List<UsersByDepart> GetStudentsByDepart(int id);
        List<Programs> getProgramsOptions();
        List<Course> GetAllCourses();
        List<Programs> GetAllPrograms(); 

        List<Roles> getRolesOptions();

        List<Class> GetAllClasses();
    }
}
