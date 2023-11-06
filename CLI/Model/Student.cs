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

        public int idStudent {  get; set; }
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID studenta: " + idStudent);
            sb.AppendLine("Prezime: " + prezime);
            sb.AppendLine("Ime: " + ime);
            sb.AppendLine("Datum rođenja: " + datumRodjenja.ToString("dd.MM.yyyy"));
            sb.AppendLine("Adresa stanovanja: " + AdresaStanovanja);
            sb.AppendLine("Kontakt telefon: " + kontaktTelefon);
            sb.AppendLine("Email adresa: " + emailAdresa);
            sb.AppendLine("Broj indeksa: " + brojIndeksa);
            sb.AppendLine("Trenutna godina studija: " + trenutnaGodinaStudija);
            sb.AppendLine("Status: " + status);
            sb.AppendLine("Prosečna ocena: " + prosecnaOcena);

            sb.AppendLine("Spisak položenih ispita:");
            foreach (OcenaNaUpisu ocena in spisakPolozenihIspita)
            {
                sb.AppendLine(ocena.ToString());
            }

            sb.AppendLine("Spisak nepoloženih ispita:");
            foreach (Predmet predmet in spisakNepolozenihIspita)
            {
                sb.AppendLine(predmet.ToString());
            }

            return sb.ToString();
        }

    }
}
