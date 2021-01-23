using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.ViewModel
{
    public class movieGallaryViewModel
    {
        

        public moviegallary movie { get; set; }
        public List<moviegallary> movies { get; set; }
    }
}