using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHL_Api.Models
{
    public class PelaajaDTO
    {
        public int id { get; set; }
        public string nimi { get; set; }
        public int voitot { get; set; }
        public int häviöt { get; set; }
        public int jatkoaikahäviöt { get; set; }
        public int pisteet { get; set; }
        public int JoukkueId { get; set; }




    }
}