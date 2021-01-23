using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.ViewModel
{
    public class ticketAndMovieModel
    {
        public ticketView ticketView { set; get; }
        public List<ticketView> ticketViews { set; get; }
    }
}