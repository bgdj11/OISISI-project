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

        public Adresa AdresaStanovanja { get; set; }
        public string kontaktTelefon { get; set; }
        public string emailAdresa { get; set; }
        public string brojIndeksa { get; set; }
        public int trenutnaGodinaStudija { get; set; }

        public Status status { get; set; }
        public double prosecnaOcena { get; set; }
        public List<OcenaNaUpisu> spisakPolozenihIspita { get; set; }
        public List<Predmet> spisakNepolozenihIspita { get; set; }

        public Student(
        string prezime,
        string ime,
        DateTime datumRodjenja,
        Adresa adresaStanovanja,
        string kontaktTelefon,
        string emailAdresa,
        string brojIndeksa,
        int trenutnaGodinaStudija,
        Status status,
        double prosecnaOcena)
        {
            prezime = prezime;
            ime = ime;
            datumRodjenja = datumRodjenja;
            adresaStanovanja = adresaStanovanja;
            kontaktTelefon = kontaktTelefon;
            emailAdresa = emailAdresa;
            brojIndeksa = brojIndeksa;
            trenutnaGodinaStudija = trenutnaGodinaStudija;
            status = status;
            prosecnaOcena = prosecnaOcena;
            spisakPolozenihIspita = new List<OcenaNaUpisu>();
            spisakNepolozenihIspita = new List<Predmet>();
        }

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
