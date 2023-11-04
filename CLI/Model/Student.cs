using CLI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

enum Status
{
    B,
    S
}

namespace CLI.Model
{
    class Student : ISerializable
    {
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateTime DatumRodjenja { get; set; }

        // public Adresa AdresaStanovanja { get; set; }
        public string KontaktTelefon { get; set; }
        public string EmailAdresa { get; set; }
        public string BrojIndeksa { get; set; }
        public int TrenutnaGodinaStudija { get; set; }

        public Status Status { get; set; }
        public double ProsecnaOcena { get; set; }
        // public List<Ocena> SpisakPolozenihIspita { get; set; }
        public List<Predmet> SpisakNepolozenihIspita { get; set; }


        public string[] ToCSV()
        {
            string[] csvValues =
            {

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {

        }

    }
}
