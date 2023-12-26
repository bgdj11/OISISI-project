using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    public class Profesor : ISerializable
    {
        public int IdProfesor { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumRodjenja { get; set; }

        public Adresa AdresaStanovanja { get; set; }

        public int IdAdrese { get; set; }
        public string KontaktTelefon { get; set; }
        public string EmailAdresa { get; set; }
        public string BrojLicneKarte { get; set; }
        public string Zvanje { get; set; }
        public int GodineStaza { get; set; }
        public List<Predmet> SpisakPredmeta { get; set; }

        public int IdKatedre { get; set; }

        public Profesor()
        {
            AdresaStanovanja = new Adresa();
            SpisakPredmeta = new List<Predmet>();
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
            this.Prezime = prezime;
            this.Ime = ime;
            this.DatumRodjenja = datumRodjenja;
            this.IdAdrese = id;
            this.KontaktTelefon = kontaktTelefon;
            this.EmailAdresa = emailAdresa;
            this.BrojLicneKarte = brojLicneKarte;
            this.Zvanje = zvanje;
            this.GodineStaza = godineStaza;
            SpisakPredmeta = new List<Predmet>();
            AdresaStanovanja = adresa;
            this.IdKatedre = IdKatedre;
        }

        public Profesor(
            string prezime,
            string ime, 
            string adresa,
            string datumRodj,
            string kontaktTel,
            string email,
            string brojLk,
            string zvanje, 
            int staz,
            int idkatedre
            ) 
        {
            this.Prezime = prezime;
            this.Ime = ime;
            this.AdresaStanovanja = makeAdresa(adresa);
            this.DatumRodjenja = DateOnly.Parse(datumRodj);
            this.KontaktTelefon = kontaktTel;
            this.EmailAdresa = email;
            this.BrojLicneKarte = brojLk;
            this.Zvanje = zvanje;
            this.GodineStaza = staz;
            this.IdKatedre = idkatedre;
        }


        private Adresa makeAdresa(string input)
        {
            var delovi = input.Split(',');
            if (delovi.Length != 4)
            {
                throw new FormatException("String ne sadrži ispravan broj polja.");
            }

            return new Adresa
            (delovi[0].Trim(),
                int.Parse(delovi[1].Trim()),
                delovi[2].Trim(),
                delovi[3].Trim()
            );
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdProfesor.ToString(),
                Prezime,
                Ime,
                DatumRodjenja.ToString(),
                IdAdrese.ToString(),
                KontaktTelefon,
                EmailAdresa,
                BrojLicneKarte,
                Zvanje,
                GodineStaza.ToString(),
                IdKatedre.ToString()
        };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            IdProfesor = int.Parse(values[0]);
            Prezime = values[1];
            Ime = values[2];
            DatumRodjenja = DateOnly.Parse(values[3]);
            IdAdrese = int.Parse(values[4]);
            KontaktTelefon = values[5];
            EmailAdresa = values[6];   
            BrojLicneKarte = values[7];
            Zvanje = values[8];
            GodineStaza = int.Parse(values[9]);
            IdKatedre = int.Parse(values[10]);

        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID profesora: " + IdProfesor);
            sb.AppendLine("Prezime: " + Prezime);
            sb.AppendLine("Ime: " + Ime);
            sb.AppendLine("Datum rođenja: " + DatumRodjenja.ToString("dd.MM.yyyy"));
            sb.AppendLine("Adresa stanovanja: " + AdresaStanovanja);
            sb.AppendLine("Kontakt telefon: " + KontaktTelefon);
            sb.AppendLine("Email adresa: " + EmailAdresa);
            sb.AppendLine("Broj lične karte: " + BrojLicneKarte);
            sb.AppendLine("Zvanje: " + Zvanje);
            sb.AppendLine("Godine staža: " + GodineStaza);

            sb.Append("\n Predmeti na kojima predaje: \n");

            foreach (Predmet p in SpisakPredmeta)
            {
                sb.Append(p.ToString() + "\n");
            }

            return sb.ToString();
        }
    }
}
