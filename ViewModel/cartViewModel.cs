using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.ViewModel
{
    public class cartViewModel
    {
        public cartView CartView { set; get; }
        public List<cartView> CartsView { set; get; }
    }
}