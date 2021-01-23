using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cinema
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
               name: "register",
               url: "registerPage",
               defaults: new { controller = "auth", action = "userRegester", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "adminregisterPage",
             url: "adminregisterPage",
             defaults: new { controller = "auth", action = "adminRegester", id = UrlParameter.Optional }
         );
            routes.MapRoute(
                name: "loginPage",
                url: "loginPage",
                defaults: new { controller = "auth", action = "loginPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
                routes.MapRoute(
                name: "movie - info",
                url: "movieinfo",
                defaults: new { controller = "Home", action = "movieInfo", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "adminhome",
                url: "adminHome",
                defaults: new { controller = "adminHome", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "moveisManeging",
                url: "adminHome/movies",
                defaults: new { controller = "adminHome", action = "moveisManeging", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "addMoviePage",
                url: "adminHome/addMovie",
                defaults: new { controller = "adminHome", action = "addMovie", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ShowMoviePage",
                url: "adminHome/showMovie/{id}",
                defaults: new { controller = "adminHome", action = "showMovie", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "AddShowPage",
               url: "adminHome/addShowPage/{id}",
               defaults: new { controller = "adminHome", action = "addShowPage", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "AddShow",
               url: "adminHome/AddShow",
               defaults: new { controller = "adminHome", action = "AddShow", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "HallsPage",
              url: "adminHome/HallsPage",
              defaults: new { controller = "adminHome", action = "hallsManeging", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "addHallPage",
              url: "adminHome/AddHallPage",
              defaults: new { controller = "adminHome", action = "addHallPage", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "addHall",
              url: "adminHome/addHall",
              defaults: new { controller = "adminHome", action = "addHall", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "dalateMovie",
             url: "adminHome/deleteMovie",
             defaults: new { controller = "adminHome", action = "deleteMovie", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "addMovie",
               url: "adminHome/add",
               defaults: new { controller = "adminHome", action = "Add", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "loginSubmit",
               url: "logIn",
               defaults: new { controller = "auth", action = "login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "registerSubmit",
               url: "signUp",
               defaults: new { controller = "auth", action = "signUp", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
