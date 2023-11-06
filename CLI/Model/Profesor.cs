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
        public int idProfesor { get; set; }
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
        string idProfesor,
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
            idProfesor = idProfesor;
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


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID profesora: " + idProfesor);
            sb.AppendLine("Prezime: " + prezime);
            sb.AppendLine("Ime: " + ime);
            sb.AppendLine("Datum rođenja: " + datumRodjenja.ToString("dd.MM.yyyy"));
            sb.AppendLine("Adresa stanovanja: " + AdresaStanovanja);
            sb.AppendLine("Kontakt telefon: " + kontaktTelefon);
            sb.AppendLine("Email adresa: " + emailAdresa);
            sb.AppendLine("Broj lične karte: " + brojLicneKarte);
            sb.AppendLine("Zvanje: " + zvanje);
            sb.AppendLine("Godine staža: " + godineStaza);

            sb.AppendLine("Spisak predmeta:");
            foreach (Predmet predmet in spisakPredmeta)
            {
                sb.AppendLine(predmet.ToString());
            }

            return sb.ToString();
        }
    }
}
