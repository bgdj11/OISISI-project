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
        public string profesorID { get; set; }
        public string prezime { get; set; }
        public string ime { get; set; }
        public DateTime datumRodjenja { get; set; }

        public Adresa AdresaStanovanja { get; set; }
        public string kontaktTelefon { get; set; }
        public string emailAdresa { get; set; }
        public string brojLicneKarte { get; set; }
        public string zvanje { get; set; }
        public int godineStaza { get; set; }
        public List<Predmet> spisakPredmeta { get; set; }

        public Profesor(
        string profesorID,
        string prezime,
        string ime,
        DateTime datumRodjenja,
        Adresa adresaStanovanja,
        string kontaktTelefon,
        string emailAdresa,
        string brojLicneKarte,
        string zvanje,
        int godineStaza)
        {
            profesorID = profesorID;
            prezime = prezime;
            ime = ime;
            datumRodjenja = datumRodjenja;
            adresaStanovanja = adresaStanovanja;
            kontaktTelefon = kontaktTelefon;
            emailAdresa = emailAdresa;
            brojLicneKarte = brojLicneKarte;
            zvanje = zvanje;
            godineStaza = godineStaza;
            spisakPredmeta = new List<Predmet>();
        }


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
