using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Concrete;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LmsSystem_Web.Controllers
{

    
    public class UserController : Controller
    {
        // GET: User
        private IUserRepository _userRepo;

        public UserController()
        {
            this._userRepo = new UserRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateCourse()
        {
            return View();
        }

        //method to get student list
        [Authorize(Roles = "Admin")]
        public ActionResult GetStudents() {

            List<User> users = _userRepo.GetStudents();

            return View(users);
        }


        



       
    }
}