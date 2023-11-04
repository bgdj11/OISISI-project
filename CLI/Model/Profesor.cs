using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Serialization;

namespace CLI.Model
{
    class Profesor : ISerializable
    {
        public string ProfesorID { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateTime DatumRodjenja { get; set; }

        // public Adresa AdresaStanovanja { get; set; }
        public string KontaktTelefon { get; set; }
        public string EmailAdresa { get; set; }
        public string BrojLicneKarte { get; set; }
        public string Zvanje { get; set; }
        public int GodineStaza { get; set; }
        public List<Predmet> SpisakPredmeta { get; set; }

        public void FromCSV(string[] values)
        {

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {

        };
            return csvValues;
        }

    }
}
