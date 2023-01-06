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

    }
}