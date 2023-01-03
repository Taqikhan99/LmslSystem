using LmsSystem_DAL.Abstract;
using LmsSystem_DAL.Concrete;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace LmsSystem_Web.Controllers
{

    
    public class UserController : Controller
    {
        // GET: User
        private IUserRepository _userRepo;
        private ICourseRelatedRepository _courseRelatedRepo;

        public UserController()
        {
            this._userRepo = new UserRepository();
            this._courseRelatedRepo= new CourseRelatedRepository();
        }
        public ActionResult Index()
        {
            ViewBag.smessage = TempData["message"];
            return View();
        }

        //Student Related tasks
        //================================================
        //create Student

        [Authorize(Roles = "Admin")]
        public ActionResult CreateStudent()
        {
            return View();
        }

        //create user Post req
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool success = _userRepo.AddStudent(student);

                    if (success)
                    {
                        TempData["message"] = "New Student Added";
                        return RedirectToAction("Index");
                    }
                    else
                        TempData["emessage"] = "Something Wrong!";


                }
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return View();
        }


        //method to get student list
        [Authorize(Roles = "Admin")]
        public ActionResult GetStudents()
        {

            List<Student> users = _userRepo.GetStudents();
            ViewBag.smessage = TempData["message"];

            return View(users);
        }

        

        [HttpGet]
        public ActionResult EditStudent(int Id)
        {
            try
            {
                Student std = _userRepo.GetStudentById(Id);

                return View(std);
            }
            catch(Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("ErrorPage","Account");
            }
        }

        [HttpPost]
        public ActionResult EditStudent(Student user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = _userRepo.UpdateStudent(user);
                    if (success)
                    {
                        TempData["message"] = "Student Record Updated";
                        return RedirectToAction("GetStudents");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");

            }

        }
        ////get student details
        public ActionResult StudentDetails(int id)
        {
            try
            {
                Student std = _userRepo.GetStudentDetails(id);

                return View(std);
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult CreateTeacher()
        {
            return View();
        }

        //create user Post req
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateTeacher(Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool success = _userRepo.AddTeacher(teacher);

                    if (success)
                    {
                        TempData["message"] = "New Teacher Added";
                        return RedirectToAction("Index");
                    }
                    else
                        TempData["emessage"] = "Something Wrong!";


                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return View();
        }


        //edit teacher
        [Authorize(Roles ="Admin")]
        public ActionResult EditTeacher(int id)
        {
            try
            {
                Teacher teacher = _userRepo.GetTeacherById(id);

                return View(teacher);
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("ErrorPage", "Account");
            }

            
        }

        // Edit Teacher Post
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult EditTeacher(Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }
                return View();

            }
            catch(Exception ex)
            {
                TempData["message"]= ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }

        }


        //Get TeacherDetail

        [Authorize(Roles ="Admin")]
        public ActionResult GetTeacherDetails(int id)
        {
            try
            {
                Teacher teacher = _userRepo.GetTeacherDetails(id);

                return View(teacher);
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

        //delete teacger
        public ActionResult DeleteTeacher(int id)
        {
            try
            {
                // TODO: Add delete logic here

                bool stdDeleted = _userRepo.DeleteTeacher(id);
                if (stdDeleted)
                {
                        TempData["message"] = "Teacher Deleted Success!";
                        return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "The Teacher is Teaching a course. please drop course first!.";
                    return RedirectToAction("GetTeachers");
                }

                

            }
            catch(Exception ex)
            {
                TempData["message"]=ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }

        }
        //================================================
        //================================================

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
                bool success = _courseRelatedRepo.AddCourse(c);
                if (success)
                {

                    TempData["message"] = "New Course Added";
                    return RedirectToAction("Index");
                }
                

            }

            return View();
        }

       


        [Authorize(Roles = "Admin")]
        public ActionResult GetTeachers()
        {

            List<Teacher> users = _userRepo.GetTeachers();

            ViewBag.smessage = TempData["message"];
            return View(users);
        }




        //Course Related Work
        [Authorize(Roles = "Admin")]
        public ActionResult CourseRelated()
        {
            return View();
        }

        //get courses
        [Authorize(Roles = "Admin")]
        public ActionResult GetCourses()
        {
            //take all courses from userreo.getallcourses
            List<Course> courses =  _courseRelatedRepo.GetAllCourses();
            return View(courses);
        }



        //add program
        public ActionResult CreateProgram()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateProgram(Programs p)
        {
            if (ModelState.IsValid)
            {
                bool success = _courseRelatedRepo.AddProgram(p);
                if (success)
                {
                    TempData["message"] = "New Program Added";
                    return RedirectToAction("Index");
                }


            }
            return View();
        }



        //get program
        [Authorize(Roles = "Admin")]
        public ActionResult GetPrograms()
        {
            List<Programs> progs = _courseRelatedRepo.GetAllPrograms();
            return View(progs);
        }



        //Get and add classes
        public ActionResult GetClasses()
        {
            try
            {
                List<Class> classes = _courseRelatedRepo.GetAllClasses();
                return View(classes);
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
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
                    bool success = _courseRelatedRepo.AddClass(c);
                    if (success)
                    {
                        TempData["message"] = "New Class Added";
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

        //Get and create Teachers
        //public ActionResult CreateTeacher()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateTeacher(Teacher t)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            bool success = _userRepo.AddTeacher(u);
        //            if (success)
        //            {
        //                TempData["message"] = "New Teacher Added";
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.smessage = ex.Message;
        //        return View();
        //    }

            
        //}



        //Json return methods
        //get departments
        public ActionResult GetDepartmentsOptions()
        {
            List<Department> departs = _courseRelatedRepo.getDepartmentOptions();

            return Json(departs, JsonRequestBehavior.AllowGet);

        }

        //get roles
        public ActionResult GetRolesOptions()
        {
            try
            {
                List<Roles> roles = _courseRelatedRepo.getRolesOptions();

                return Json(roles, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

        //get course options
        public ActionResult GetCourseOptions(int id)
        {
            try
            {
                List<Course> courses = _courseRelatedRepo.getCourseOptions(id);

                return Json(courses, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }


        //get programs
        public ActionResult GetProgramOptions(int id)
        {
            List<Programs> progs = _courseRelatedRepo.getProgramsOptions(id);

            return Json(progs, JsonRequestBehavior.AllowGet);
        }

        //get time slots
        public ActionResult GetTimeSlots()
        {
            List<TimeSlot> slots=_courseRelatedRepo.GetTimeSlots();
            return Json(slots, JsonRequestBehavior.AllowGet);

        }

        //get rooms based on available time slot
        public ActionResult GetClassRoomOptions(string day,int slotid)
        {
            try
            {
                List<Classroom> classrooms = _courseRelatedRepo.GetClassrooms(day,slotid);
                return Json(classrooms, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

        public ActionResult GetTeacherOptions(string day)
        {
            try
            {
                List<Teacher> t = _courseRelatedRepo.GetTeacherOptions(day);
                return Json(t, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }
        


        //==========================================

        // Get Departments
        public ActionResult GetDepartments()
        {
            List<Department> departments = _courseRelatedRepo.GetDepartments();
            return View(departments);
        }

        //create department
        public ActionResult CreateDepartment()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = _courseRelatedRepo.AddDepartment(department);
                    if (success)
                    {
                        TempData["message"] = "New Department Added";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        //Edit class
        public ActionResult EditClass(int id)
        {
            try
            {
                Class cl = _courseRelatedRepo.GetClassById(id);

                return View(cl);
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return RedirectToAction("ErrorPage", "Account");
            }

        }

        [HttpPost]
        //Edit class
        public ActionResult EditClass(Class cl)
         {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = _courseRelatedRepo.UpdateClass(cl);
                    if (success)
                    {
                        TempData["message"] = "Class Record Updated";
                        return RedirectToAction("Index");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");

            }

        }



        //delete class
        public ActionResult DeleteClass(int id)
        {
            try
            {
                bool deleted = _courseRelatedRepo.DeleteClass(id);
                if (deleted)
                {
                    TempData["message"] = "Course Deleted Success!";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("GetClasses");
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }
        //delete course
        
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                bool deleted = _courseRelatedRepo.DeleteCourse(id);
                if (deleted)
                {
                    TempData["message"] = "Course Deleted Success!";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("GetCourses");
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

    }
}