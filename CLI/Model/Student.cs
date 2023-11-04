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
        public string prezime { get; set; }
        public string ime { get; set; }
        public DateTime datumRodjenja { get; set; }

        // public Adresa AdresaStanovanja { get; set; }
        public string kontaktTelefon { get; set; }
        public string emailAdresa { get; set; }
        public string brojIndeksa { get; set; }
        public int trenutnaGodinaStudija { get; set; }

        public Status status { get; set; }
        public double prosecnaOcena { get; set; }
        // public List<Ocena> SpisakPolozenihIspita { get; set; }
        public List<Predmet> spisakNepolozenihIspita { get; set; }


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
