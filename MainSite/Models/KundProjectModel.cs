using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class KundProjectModel
    {
        public IEnumerable<Kunder> Kunders { get; set; }
        public IEnumerable<Projekt> Projekts { get; set; }
    }
}