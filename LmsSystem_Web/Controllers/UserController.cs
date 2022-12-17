﻿using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Concrete;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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


        //create user get req

        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            return View();
        }

        //create user Post req
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                bool success = _userRepo.AddUser(user);
                if (success)
                {
                    ViewBag.smessage = "New User Created!";
                    return RedirectToAction("Index");
                }
                else
                    TempData["emessage"] = "Something Wrong!";

            }

            return View();
        }





        [Authorize(Roles = "Admin")]
        //creating course get
        public ActionResult CreateCourse()
        {
            return View();
        }
        //creating course post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateCourse(Course c)
        {
            if (ModelState.IsValid)
            {
                bool success = _userRepo.AddCourse(c);
                if (success)
                {
                    ViewBag.smessage = "New Course Created!";
                    return RedirectToAction("Index");
                }
                

            }

            return View();
        }



        //method to get student list
        [Authorize(Roles = "Admin")]
        public ActionResult GetStudents() {

            List<User> users = _userRepo.GetStudents();

            return View(users);
        }

        //get departments
        public ActionResult GetDepartments()
        {
            List<Department> departs = _userRepo.getDepartmentOptions();
                
            return  Json(departs,JsonRequestBehavior.AllowGet);

        }

        //get roles
        public ActionResult GetRoles()
        {
            List<Roles> roles = _userRepo.getRolesOptions();

            return Json(roles, JsonRequestBehavior.AllowGet);

        }

        //get programs
        public ActionResult GetProgramOptions()
        {
            List<Programs> progs = _userRepo.getProgramsOptions();

            return Json(progs, JsonRequestBehavior.AllowGet);
        }

        //Course Related Work
        public ActionResult CourseRelated()
        {
            return View();
        }

        //get courses
        public ActionResult GetCourses()
        {
            //take all courses from userreo.getallcourses
            List<Course> courses =  _userRepo.GetAllCourses();
            return View(courses);
        }



        //add program
        public ActionResult CreateProgram()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateProgram(Programs p)
        {
            if (ModelState.IsValid)
            {
                bool success = _userRepo.AddProgram(p);
                if (success)
                {
                    ViewBag.smessage = "New Program Created!";
                    return RedirectToAction("Index");
                }


            }
            return View();
        }



        //get program
        
        public ActionResult GetPrograms()
        {
            List<Programs> progs = _userRepo.GetAllPrograms();
            return View(progs);
        }



        //Get and add classes
        public ActionResult GetClasses()
        {
            List<Class> classes = _userRepo.GetAllClasses();
            return View(classes);
        }

        [Authorize(Roles ="Admin")]
        
        public ActionResult CreateClass() {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateClass(Class c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = _userRepo.AddClass(c);
                    if (success)
                    {
                        ViewBag.smessage = "New Class Created!";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.smessage = ex.Message;
                return View();
            }
            
        }


    }
}