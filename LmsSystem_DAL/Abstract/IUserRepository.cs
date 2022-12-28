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
        

        Student GetStudentById(int id);

        Teacher GetTeacherById(int id);
        Teacher GetTeacherDetails(int id);

        Student GetStudentDetails(int id);

        bool UpdateStudent(Student user);
        //bool UpdateTeacher(User user);
       
        List<Student> GetStudents();

        
        //User GetTeacherById(int id);

        List<Teacher> GetTeachers();
        List<UsersByDepart> GetStudentsByDepart(int id);

        bool DeleteTeacher(int id);
        bool DeleteStudent(int id);
    }
}
