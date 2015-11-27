using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace NHL_Api.Models
{
    public class PelaajaDto
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public int Pelinumero { get; set; }
        public int Maalit { get; set; }
        public int Syötöt { get; set; }
        public int Pisteet { get; set; }
        public int Plusmiinus { get; set; }




    }
}