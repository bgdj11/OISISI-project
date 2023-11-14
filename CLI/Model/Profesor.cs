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
        public DateOnly datumRodjenja { get; set; }

        public Adresa adresaStanovanja { get; set; }

        public int idAdrese { get; set; }
        public string kontaktTelefon { get; set; }
        public string emailAdresa { get; set; }
        public string brojLicneKarte { get; set; }
        public string zvanje { get; set; }
        public int godineStaza { get; set; }
        public List<Predmet> spisakPredmeta { get; set; }

        public int IdKatedre { get; set; }

        public Profesor()
        {
            adresaStanovanja = new Adresa();
            spisakPredmeta = new List<Predmet>();
        }

        public Profesor(
        string prezime,
        string ime,
        DateOnly datumRodjenja,
        int id,
        string kontaktTelefon,
        string emailAdresa,
        string brojLicneKarte,
        string zvanje,
        int godineStaza,
        int IdKatedre,
        Adresa adresa)
        {
            this.prezime = prezime;
            this.ime = ime;
            this.datumRodjenja = datumRodjenja;
            this.idAdrese = id;
            this.kontaktTelefon = kontaktTelefon;
            this.emailAdresa = emailAdresa;
            this.brojLicneKarte = brojLicneKarte;
            this.zvanje = zvanje;
            this.godineStaza = godineStaza;
            spisakPredmeta = new List<Predmet>();
            adresaStanovanja = adresa;
            this.IdKatedre = IdKatedre;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idProfesor.ToString(),
                prezime,
                ime,
                datumRodjenja.ToString(),
                adresaStanovanja.idAdrese.ToString(),
                kontaktTelefon,
                emailAdresa,
                brojLicneKarte,
                zvanje,
                godineStaza.ToString(),
                IdKatedre.ToString()
        };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            idProfesor = int.Parse(values[0]);
            prezime = values[1];
            ime = values[2];
            datumRodjenja = DateOnly.Parse(values[3]);
            idAdrese = int.Parse(values[4]);
            kontaktTelefon = values[5];
            emailAdresa = values[6];   
            brojLicneKarte = values[7];
            zvanje = values[8];
            godineStaza = int.Parse(values[9]);
            IdKatedre = int.Parse(values[10]);

        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID profesora: " + idProfesor);
            sb.AppendLine("Prezime: " + prezime);
            sb.AppendLine("Ime: " + ime);
            sb.AppendLine("Datum rođenja: " + datumRodjenja.ToString("dd.MM.yyyy"));
            sb.AppendLine("Adresa stanovanja: " + adresaStanovanja);
            sb.AppendLine("Kontakt telefon: " + kontaktTelefon);
            sb.AppendLine("Email adresa: " + emailAdresa);
            sb.AppendLine("Broj lične karte: " + brojLicneKarte);
            sb.AppendLine("Zvanje: " + zvanje);
            sb.AppendLine("Godine staža: " + godineStaza);

            sb.Append("\n Predmeti na kojima predaje: \n");

            foreach (Predmet p in spisakPredmeta)
            {
                sb.Append(p.ToString() + "\n");
            }

            return sb.ToString();
        }
    }
}
