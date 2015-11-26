using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHL_Api.Models
{
    public class JoukkueDTO
    {
        public int JoukkueID { get; set; }
        public string nimi { get; set; }
        public int voitot { get; set; }
        public int häviöt { get; set; }
        public int jatkoaikahaviot { get; set; }
        public int pisteet { get; set; }


    }
}