using Packt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeFinder.Models
{
    public class HomeIndexViewModel
    {

        public int VisitorCount { get; set; }

        public IList<Cafe> Cafes { get; set; }


    }
}
