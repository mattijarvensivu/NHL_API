using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHL_Api.Models
{
    public class JoukkueDto
    {
        public int Joukkueid { get; set; }
        public string Lyhenne { get; set; }
        public string Nimi { get; set; }
        public int Voitot { get; set; }
        public int Häviöt { get; set; }
        public int Jatkoaikahaviot { get; set; }
        public int Pisteet { get; set; }


    }
}