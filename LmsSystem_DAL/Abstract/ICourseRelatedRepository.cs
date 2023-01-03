using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Abstract
{
    public interface ICourseRelatedRepository
    {
        bool AddProgram(Programs p);
        bool AddCourse(Course course);

        bool AddDepartment(Department d);

        bool AddClass(Class c);
        List<Department> getDepartmentOptions();

        List<Programs> getProgramsOptions(int id = 0);
        List<Course> getCourseOptions(int id = 0);
        List<Course> GetAllCourses();
        List<Programs> GetAllPrograms();

        List<TimeSlot> GetTimeSlots();
        List<Roles> getRolesOptions();

        List<Class> GetAllClasses();

        List<Department> GetDepartments();

        List<Classroom> GetClassrooms(string day, int timeid);
        List<Teacher> GetTeacherOptions(string day);

        bool DeleteCourse(int id);
        bool DeleteClass(int id);

        Class GetClassById(int id);

        bool UpdateClass(Class cl);
    }
}
