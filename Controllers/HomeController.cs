using Cinema.Models;
using Cinema.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Controllers
{
    
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"] as string;
            user user =Session["user"] as user;
            if (user != null)
            {
                Session["userName"] = user.userName;
            }
            


            movieGallaryViewModel mgvm =  order(Request.Form["order"] as string);
            ViewBag.user = user;
            return View(mgvm);
        }

        private movieGallaryViewModel order(string orderby)
        {
            cinemaDB movie = new cinemaDB();
            movieGallaryViewModel mgvm = new movieGallaryViewModel();
            switch (orderby)
            {
                
                case "price decreace,order":
                    mgvm.movies = (from x in movie.moviegallaries select x).OrderBy(u => u.price).ToList<moviegallary>();
                    mgvm.movies.Reverse();
                    return mgvm;
                case "most popula,orderr":
                    mgvm.movies = (from x in movie.moviegallaries orderby x.rating select x).OrderBy(u => u.rating).ToList<moviegallary>();
                    return mgvm;
                case "catgory,order":
                    mgvm.movies = (from x in movie.moviegallaries orderby x.category select x).OrderBy(u => u.category).ToList<moviegallary>();
                    return mgvm;
                default:
                case "price increase,order":
                    mgvm.movies = (from x in movie.moviegallaries orderby x.price select x).OrderBy(u => u.price).ToList<moviegallary>();
                    return mgvm;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult movieInfo(moviegallary m)
        {
            cinemaDB cinemaDb = new cinemaDB();
            tickitViewModel t = new tickitViewModel
            {
                tikit = null,
                tikits = (from x in cinemaDb.tikits
                          where x.name.Equals(m.name) &
                           x.hall.Equals(m.hall) &
                          x.starttime.Equals(m.starttime)
                          select x).ToList<tikit>()
            };
            if (m.sale != null)
            {
                ViewBag.price = ((decimal)(m.price - m.price * (m.sale / 100)));
            }
            ViewBag.ticketView = m;
            List<feedback> feedbacks = (from x in cinemaDb.feedbacks where x.movie.Equals(m.name) select x).ToList<feedback>();
            ViewBag.feedbacks = feedbacks;
            ViewBag.user = Session["user"] as user;
          return View(t);
        }

        public ActionResult payment(creditCard cc)
        {
            if (ModelState.IsValid)
            {
                tikit t1 = TempData["ticket"] as tikit,
                    t2;
                /////////////////////////////////////////////////////
                cinemaDB cinemaDb = new cinemaDB();
                t2 = (from x in cinemaDb.tikits
                      where x.name.Equals(t1.name) &
                       x.hall.Equals(t1.hall) &
                      x.starttime.Equals(t1.starttime) &
                      x.seat==t1.seat
                      select x).First<tikit>();
                t2.username = Session["userName"] as string;
                cart c = (from x in cinemaDb.carts
                          where x.username.Equals(t2.username) &
                          x.movie.Equals(t1.name) &
                           x.hall.Equals(t1.hall) &
                          x.time.Equals(t1.starttime) &
                          x.seat == t1.seat
                          select x).First<cart>();
                cinemaDb.carts.Remove(c);

                cinemaDb.SaveChanges();

                TempData["Message"]  = "payment acceptd";

                return RedirectToAction("Index");
            }
             return View("buyTickit", cc);
        }
            public ActionResult payPage(tikit t)
        {
            cinemaDB cinemaDb = new cinemaDB();
            tickitViewModel tv = new tickitViewModel
            {
                tikit = null,
                tikits = (from x in cinemaDb.tikits
                          where x.name.Equals(t.name) &
                           x.hall.Equals(t.hall) &
                          x.starttime.Equals(t.starttime)
                          select x).ToList<tikit>()
            };
            ViewBag.seat = t.seat;
            ViewBag.ticketView = (from x in cinemaDb.moviegallaries
                                  where x.name.Equals(t.name) &
                                   x.hall.Equals(t.hall) &
                                  x.starttime.Equals(t.starttime)
                                  select x).First();
            ViewBag.user = Session["user"] as user;

            return View(tv);
        }
        public ActionResult buyTickit(tikit t)
        {
            
            TempData["ticket"] = t;
            return View();
            
        }
        public ActionResult goToCart()
        {
            user user = Session["user"] as user;

            if (user == null && Session["userName"] == null)
            {
                return RedirectToAction("unregisterdCart");
            }

            return RedirectToAction("cartPage");
        }
        public ActionResult cartPage()
        {
            user user = Session["user"] as user;
            string userName;
            if (user == null)
                userName = Session["userName"] as string;
            else
                userName = user.userName;
            cinemaDB cvdb = new cinemaDB();
            cartViewModel cvm = new cartViewModel {
                CartsView=(from x in cvdb.cartViews where x.userName.Equals(userName) select x).ToList<cartView>()
            };
            tempdbEntities3 tv = new  tempdbEntities3();
            ticketAndMovieModel tvm = new ticketAndMovieModel();
            tvm.ticketViews = (from x in tv.ticketViews where x.username.Equals(userName) select x).ToList<ticketView>();
            if (tvm.ticketViews.Count() > 0)
            {
                ViewBag.tickets = tvm.ticketViews;
            }
            ViewBag.user = userName;
            return View(cvm);
        }
        public ActionResult getUnregusterd()
        {

            Session["userName"] = Request.Form["name"] as string;
            if (Session["userName"] == null)
                return View("getUnregusterd");
            cinemaDB cinmaDb = new cinemaDB();
            String username = Session["userName"].ToString();
            List<user> users = (from x in cinmaDb.users where (x.userName.Equals(username)) select x).ToList<user>();
            List<admin> admins = (from x in cinmaDb.admins where (x.userName.Equals(username)) select x).ToList<admin>();
            ViewBag.erroressage = "";
            if (users.Count > 0 || admins.Count > 0)
            {
                ViewBag.erroressage = "user name alredy taken!";

                return View("getUnregusterd");
            }
            return RedirectToAction("addToCart", new
            {
                cv = Session["cart"] as cart
            }); 
        }
            public ActionResult addToCart(cart cv)
        {
            user user = Session["user"] as user;
            string userName ;
            if (user != null)
            {
                userName = user.userName;
            }
            else if(Session["userName"] != null)
            {
                userName = Session["userName"].ToString();
            }
            else
            {
                Session["cart"] = cv;
                return View("getUnregusterd");

            }
            if (cv == null)
            {
                cv = Session["cart"] as cart;
            }



            cinemaDB cvdb = new cinemaDB();
            
            cv.username = userName;
            List< cart> cc = (from x in cvdb.carts where (x.username.Equals(cv.username)) &&
                              x.hall.Equals(cv.hall)&&
                              x.movie.Equals(cv.movie)&&
                              x.time.Equals(cv.time)&&
                              x.seat.Equals(cv.seat)
                              select x).ToList<cart>();
            if (cc.Count() > 0)
            {
                TempData["Message"] = "movie already in the cart!";
                return RedirectToAction("Index");
            }
            cvdb.carts.Add(cv);
            cvdb.SaveChanges();
            
            return RedirectToAction("cartPage");
        }
        public ActionResult unregisterdCart()
        {
            return View();
        }

        public ActionResult GetunregisterdCart()
        {
            Session["userName"] = Request.Form["name"] as string;
            if (Session["userName"] == null)
                return View("unregisterdCart");
            cinemaDB cinmaDb = new cinemaDB();
            String username = Session["userName"].ToString();
            List<user> users = (from x in cinmaDb.users where (x.userName.Equals(username)) select x).ToList<user>();
            List<admin> admins = (from x in cinmaDb.admins where (x.userName.Equals(username)) select x).ToList<admin>();
            ViewBag.erroressage = "";
            if (users.Count > 0 || admins.Count > 0)
            {
                ViewBag.erroressage = "user name alredy taken!";

                return View("unregisterdCart");
            }
            return RedirectToAction("cartPage");
        }

        public ActionResult feedbackPage(string id)
        {
            ViewBag.username = Session["userName"] as string;
            ViewBag.movie = id;
            return View();
        }

        
            public ActionResult Feedback(feedback f)
        {
            tempdbEntities1 fb = new tempdbEntities1();
            List<feedback> fbs = (from x in fb.feedbacks where (x.movie.Equals(f.movie)) &&
                                  x.userame.Equals(f.userame)
                                  && x.rating.Equals(f.rating)
                                  && x.feedback1.Equals(f.feedback1)
                                  select x).ToList<feedback>();
            if (fbs.Count() == 0)
            {
                fb.feedbacks.Add(f);
                fb.SaveChanges();
                trigger(f.movie);
            }
            return RedirectToAction("Index");
        }


        private void trigger(string m)
        {
            tempdbEntities1 fb = new tempdbEntities1();
            cinemaDB cinemaDb = new cinemaDB();
            var avg = (from x in fb.feedbacks where (x.movie.Equals(m)) select x.rating).Average();
            movie movie = (from x in cinemaDb.movies where (x.name.Equals(m)) select x).First<movie>();
            movie.rating = (decimal?)avg;
            cinemaDb.SaveChanges();
        }

    }
}