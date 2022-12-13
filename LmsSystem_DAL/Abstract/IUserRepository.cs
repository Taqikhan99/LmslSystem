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
        bool UpdateUser(User user);
        //bool DeleteUser(User user);
        List<User> GetStudents();

        List<User> GetTeachers();

        List<UsersByDepart> GetStudentsByDepart(int id);
    }
}
