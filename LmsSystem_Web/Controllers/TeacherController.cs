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
    public class TeacherController : Controller
    {
        private ITeacherRepository _teacherrepo;

        public TeacherController()
        {
            _teacherrepo = new TeacherRepository();
        }
        public ActionResult Index()
        {
            return View();
        }


        //controller for Teachers courses
        [Authorize(Roles ="Teacher")]
        public ActionResult MyCourses()
        {
            try
            {
                List<Course> courses = _teacherrepo.GetTeacherCourses(User.Identity.Name);
                return View(courses);
            }
            catch(Exception ex)
            {
                TempData["message"]=ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

        [Authorize(Roles = "Teacher")]

        public ActionResult GetClassSchedule() {

            try
            {
                List<Class> classes= _teacherrepo.GetClassTimes(User.Identity.Name);
                return View(classes);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherProfile()
        {
            try
            {
                Teacher teacher= _teacherrepo.GetTeacherProfile(User.Identity.Name);
                return View(teacher);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }

        }

    }
}