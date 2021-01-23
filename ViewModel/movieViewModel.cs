using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.ViewModel
{
    public class movieViewModel
    {
        public movie movie { get; set; }
        public List<movie> movies { get; set; }
    }
}