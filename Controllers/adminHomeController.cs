
using Cinema.Models;
using Cinema.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace Cinema.Controllers
{
    public class adminHomeController : Controller
    {
        private static admin Admin;
        public ActionResult deleteMovie(string id)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            cinemaDB cinmaDb = new cinemaDB();
            movie m = (from x in cinmaDb.movies where (x.name.Equals(id)) select x).First<movie>();
            List<tikit> t= (from x in cinmaDb.tikits where (x.name.Equals(id)) select x).ToList<tikit>();
            foreach(var tt in t){
                cinmaDb.tikits.Remove(tt);
                
            }
            cinmaDb.movies.Remove(m);
            cinmaDb.SaveChanges();
            return RedirectToAction("moveisManeging");
        }
        // GET: adminHome
        public ActionResult hallsManeging()
        {

            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            cinemaDB tempdb = new cinemaDB();
            hallViewModel hvm = new hallViewModel();
            hvm.hall = new hall();
            hvm.halls = (from x in tempdb.halls select x).ToList<hall>();

            return View(hvm);
        }
        public ActionResult addHallPage()
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            return View("addHall");
        }
        public ActionResult addHall(hall h)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            if (ModelState.IsValid)
            {
                cinemaDB cinmaDb = new cinemaDB();
                cinmaDb.halls.Add(h);
                cinmaDb.SaveChanges();
                return RedirectToAction("hallsManeging");
            }
            ViewBag.message = "error";

            return View("addHall",h);
        }
        
        public ActionResult deleteHall(string id)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            cinemaDB cinmaDb = new cinemaDB();
            hall h = (from x in cinmaDb.halls where (x.name.Equals(id)) select x).First<hall>();
            cinmaDb.halls.Remove(h);
            cinmaDb.SaveChanges();
            return RedirectToAction("hallsManeging");
        }
        public ActionResult editHall(string id)
        {

            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }

            cinemaDB cinmaDb = new cinemaDB();
            hall h = (from x in cinmaDb.halls where (x.name.Equals(id)) select x).First<hall>();
            return View("editHall",h);
        }
        public ActionResult edit(hall h)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            cinemaDB cinmaDb = new cinemaDB();
            hall hh = (from x in cinmaDb.halls where (x.name.Equals(h.name)) select x).First<hall>();
            hh.seatsNumber = h.seatsNumber;
          
            cinmaDb.SaveChanges();
            return RedirectToAction("hallsManeging");
        }
        public ActionResult addShowPage(string id)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            if (id != null)
            {
                ViewBag.moviename = id;

                cinemaDB cinmaDb = new cinemaDB();
               
                ViewBag.halls= (from x in cinmaDb.halls select x).ToList<hall>();
                
                return View("Addshow");
            }
            
            return View("Addshow");
        }
        public ActionResult AddShow(tikit t)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            cinemaDB cinmaDb = new cinemaDB();
            
            if (ModelState.IsValid)
            {
                
                

                hall h = (from x in cinmaDb.halls where x.name == t.hall select x).First<hall>();
                List<tikit> tikits = (from x in cinmaDb.tikits where x.hall.Equals(t.hall) & 
                                      ((x.starttime <= t.starttime & x.endtime <= t.endtime  & x.endtime >= t.starttime) |
                                      (x.starttime >= t.starttime &  x.endtime >= t.endtime & t.endtime >= x.starttime) |
                                      (x.starttime <= t.starttime & x.endtime >= t.endtime) |
                                      (x.starttime >= t.starttime & x.endtime <= t.endtime))  select x).ToList<tikit>();
                tikit tt ;
                if (tikits.Count == 0)
                {
                    for (int i = 1; i <= h.seatsNumber; i++)
                    {
                        tt = new tikit();
                        tt.name = t.name;
                        tt.hall = t.hall;
                        tt.starttime = t.starttime;
                        tt.endtime = t.endtime;
                        tt.seat = i;
                        tt.price = t.price;
                        tt.sale = t.sale;
                        cinmaDb.tikits.Add(tt);
                        cinmaDb.SaveChanges();
                    }

                    return RedirectToAction("moveisManeging");
                    
                }
                else
                {
                    ////////////////////////////////////////////////////////
                    ViewBag.moviename = t.name;
                    ViewBag.error = "you cant show two movies in the same time ";
                    ViewBag.halls = (from x in cinmaDb.halls select x).ToList<hall>();
                    return View("Addshow");

                }
                
            }
            
            return View(t);
        }
            public ActionResult showMovie(string id)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            if (id != null)
            {
                ViewBag.moviename = id;
                cinemaDB cinmaDb = new cinemaDB();
                movie m = (from x in cinmaDb.movies where (x.name.Equals(id)) select x).First<movie>();

                return View("showMovie",m);
            }
            else
            {
                throw new HttpException(404, "Your error message");//RedirectTo NoFoundPage
            }
        }

        /// <summary>
        /// //////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
           
            Admin = Session["admin"] as admin;
            ViewBag.user = Admin;
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            return View("adhome");
        }

        public ActionResult moveisManeging()
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            cinemaDB moviedal = new cinemaDB();
            movieViewModel mvm = new movieViewModel();
            mvm.movie = new movie();


            mvm.movies = (from x in moviedal.movies select x).ToList<movie>();
            
            ViewBag.addMovie = "~/Icons/Artboard_35-512.png";
            return View("moveisManeging", mvm);
        }


        public ActionResult AddMovie()
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");

            }
            return View();
        }

        
        public ActionResult Add(movie m)
        {
            if (idminLoggedIn())
            {
                return RedirectToRoute("loginPage");
            
            }
            if (ModelState.IsValid)
            {
                
                    if (m.ImageFile != null) {
                    
                    String filename = Path.GetFileNameWithoutExtension(m.ImageFile.FileName);
                    String extension = Path.GetExtension(m.ImageFile.FileName);
                    filename = m.name + extension;
                    m.image = "~/Images/" + filename;
                   
                    
                    filename = Path.Combine(Server.MapPath("~/Images"), filename);
                    m.ImageFile.SaveAs(filename);
                    ViewBag.erroressage = m.image;
                    cinemaDB moviedal = new cinemaDB();
                    
                    moviedal.movies.Add(m);
                    moviedal.SaveChanges();
                   
                    ModelState.Clear();

                    return RedirectToAction("moveisManeging");
                }
                ViewBag.erroressage = "empty file ";


               
            }
            else
            {
                ViewBag.erroressage = "validation error";
            }

            return View("addMovie", m);
                
        }
        private bool idminLoggedIn()
        {
          
         return Admin == null;
            
        }

    }
}