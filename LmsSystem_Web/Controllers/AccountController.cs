using LmsSystem_DAL.Concrete;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LmsSystem_Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        DbClass Db = new DbClass();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserVerify user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    SqlConnection con = Db.GetConnection();
                    SqlCommand cmd = new SqlCommand("spVerifyUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userEmail", user.Email);
                    cmd.Parameters.AddWithValue("@userPassword", user.Password);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    con.Open();
                    adapter.Fill(dt);
                    con.Close();

                    bool loginSuccess = dt.Rows.Count > 0;

                    if (loginSuccess)
                    {
                        //DataRow row = dt.Rows[0];
                        //string userEmail = row["Email"].ToString();
                        //string userRole = row["RoleId"].ToString();

                        //saving in session
                        //setting auth cookie
                        FormsAuthentication.SetAuthCookie(user.Email, false);



                        return RedirectToAction("Index", "User");

                    }


                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View();
            }
            //    ModelState.AddModelError("", "Invalid email or password!");
            //    return View();
            //}
        }

            //Logout
            public ActionResult Logout()
            {
                //logout using form authentication

                FormsAuthentication.SignOut();

                return RedirectToAction("Login");
            }

        }
    } 