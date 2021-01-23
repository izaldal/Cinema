using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cinema.Controllers
{
    public class authController : Controller
    {
        
       
        // GET: register
        public ActionResult userRegester()
        {
            return View();
        }
        
        public ActionResult adminRegester()
        {
            
                return View();
        }
        public ActionResult view()
        {
            return View();
        }

        public ActionResult loginPage()
        {
            cinemaDB cinmaDb = new cinemaDB();
            List<admin> admins = (from x in cinmaDb.admins select x).ToList<admin>();

            if (admins.Count == 0)
            {
                ViewBag.admin = true;
            }
            else
            {
                ViewBag.admin = false;

            }
            ViewBag.erroressage = "";
            return View();
        }

        public ActionResult showUser(user user)
        {

            return View(user);
        }
        [HttpPost]
        public ActionResult signUp(user user)
        {
            if (ModelState.IsValid)
            {
                cinemaDB cinmaDb = new cinemaDB();
                String username = Request.Form["userName"].ToString(), password = Request.Form["password"].ToString();
                List<user> users = (from x in cinmaDb.users where (x.userName.Equals(username)) select x).ToList<user>();
                List<admin> admins = (from x in cinmaDb.admins where (x.userName.Equals(username)) select x).ToList<admin>();
                ViewBag.erroressage = "";
                if (users.Count > 0 || admins.Count>0)
                {
                    ViewBag.erroressage = "user name alredy taken!";

                    return View("userRegester");
                }
                else
                {

                    cinmaDb.users.Add(user);
                    cinmaDb.SaveChanges();
                    return RedirectToAction("showUser", user);
                }
            }
            else {
                return View("userRegester", user);
            }
        }

        public ActionResult adminSignUp(admin user)
        {
            if (ModelState.IsValid)
            {
                cinemaDB cinmaDb = new cinemaDB();
                String username = Request.Form["userName"].ToString(), password = Request.Form["password"].ToString();
                List<user> users = (from x in cinmaDb.users where (x.userName.Equals(username)) select x).ToList<user>();
                List<admin> admins = (from x in cinmaDb.admins where (x.userName.Equals(username)) select x).ToList<admin>();
                ViewBag.erroressage = "";
                if (users.Count > 0 || admins.Count > 0)
                {
                    ViewBag.erroressage = "user name alredy taken!";

                    return View("userRegester");
                }
                else
                {

                    cinmaDb.admins.Add(user);
                    cinmaDb.SaveChanges();
                    return RedirectToAction("loginPage");
                }
            }
            else
            {
                return View("adminRegester", user);
            }
        }

        public ActionResult logIn()
        {
            String erroressage;

            cinemaDB cinmaDb = new cinemaDB();
            String username = Request.Form["userName"].ToString(), password = Request.Form["password"].ToString();
            List<admin> admins = (from x in cinmaDb.admins where (x.userName.Equals(username)) select x).ToList<admin>();

                if (admins.Count > 0)
            {
                if (admins[0].password.Equals(password))
                {
                    Session["admin"] = admins[0];
                    return RedirectToRoute("adminHome");
                }
                else
                    erroressage = "inveld password";
            }
            else
            {

                List <user> users = (from x in cinmaDb.users where (x.userName.Equals(username)) select x).ToList<user>();
                if (users.Count > 0)
                {
                    if (users[0].password.Equals(password))
                    {
                        Session["user"] = users[0];
                        return RedirectToRoute("home");
                    }
                    else
                        erroressage = "inveld password";
                }
                else
                    erroressage = "user name not exists!";

            }


            ViewBag.erroressage = erroressage;
            return View("loginPage");
        }
        
    }
}