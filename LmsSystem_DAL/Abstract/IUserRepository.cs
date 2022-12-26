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
        bool AddStudent(Student user);
        bool AddTeacher(Teacher user);
        bool AddProgram(Programs p);
        bool AddCourse(Course course);

        bool AddDepartment(Department d);

        bool AddClass(Class c);

        Student GetStudentById(int id);

        Teacher GetTeacherById(int id);
        Teacher GetTeacherDetails(int id);

        Student GetStudentDetails(int id);

        bool UpdateStudent(Student user);
        //bool UpdateTeacher(User user);
        bool DeleteUser(int id,int roleid);
        List<Student> GetStudents();

        User GetUserByIdRole(int id, int roleId);
        //User GetTeacherById(int id);

        List<Teacher> GetTeachers();
        List<Department> getDepartmentOptions();
        List<UsersByDepart> GetStudentsByDepart(int id);
        List<Programs> getProgramsOptions(int id = 0);
        List<Course> GetAllCourses();
        List<Programs> GetAllPrograms(); 

        List<Roles> getRolesOptions();

        List<Class> GetAllClasses();

        List<Department> GetDepartments();
    }
}
