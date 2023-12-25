using CLI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

public enum Status
{
    B,
    S
}

namespace CLI.Model
{
    public class Student : ISerializable
    {

        public int IdStudent { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumRodjenja { get; set; }

        public int IdIndeksa {  get; set; }

        public Indeks Indeks { get; set; }

        public Adresa AdresaStanovanja { get; set; }

        public int IdAdrese { get; set; }
        public string KontaktTelefon { get; set; }
        public string EmailAdresa { get; set; }
        public int TrenutnaGodinaStudija { get; set; }

        public Status Status { get; set; }
        public double ProsecnaOcena { get; set; }
        public List<OcenaNaUpisu> PolozeniIspiti { get; set; }

        public List<Predmet> NepolozeniIspiti { get; set; }



        public Student()
        {
            AdresaStanovanja = new Adresa();
            Indeks = new Indeks();
            PolozeniIspiti = new List<OcenaNaUpisu>();
            NepolozeniIspiti = new List<Predmet>();
        }

        public Student(
        string prezime,
        string ime,
        DateOnly datumRodjenja,
        int adresa,
        string kontaktTelefon,
        string emailAdresa,
        int trenutnaGodinaStudija,
        Status status,
        double prosecnaOcena,
        Adresa a,
        int idInd,
        Indeks ind)
        {
            this.Prezime = prezime;
            this.Ime = ime;
            this.DatumRodjenja = datumRodjenja;
            AdresaStanovanja = a;
            this.KontaktTelefon = kontaktTelefon;
            this.EmailAdresa = emailAdresa;
            this.TrenutnaGodinaStudija = trenutnaGodinaStudija;
            this.Status = status;
            this.ProsecnaOcena = prosecnaOcena;
            IdAdrese = adresa;
            IdIndeksa = idInd;
            Indeks = ind;
            PolozeniIspiti = new List<OcenaNaUpisu>();
            NepolozeniIspiti = new List<Predmet>();
        }

        public Student(
            string prezime, 
            string ime,
            DateOnly datumRodjenja,
            string adresa,
            string kontakttel,
            string email,
            string indeks, 
            int godinastudija,
            string status,
            double prosecnaocena
        )
        {
            this.Prezime = prezime;
            this.Ime = ime;
            this.DatumRodjenja = datumRodjenja;
            this.AdresaStanovanja = makeAdresa(adresa);
            this.KontaktTelefon = kontakttel;
            this.EmailAdresa = email;
            this.Indeks = makeIndex(indeks);
            this.TrenutnaGodinaStudija = godinastudija;
            this.Status = MakeStatus(status);
            this.ProsecnaOcena = prosecnaocena;
            PolozeniIspiti = new List<OcenaNaUpisu>();
            NepolozeniIspiti = new List<Predmet>();
            //this.IdAdrese = this.AdresaStanovanja.idAdrese;
            //this.IdIndeksa = this.Indeks.idIndeksa;

        }

        public Status MakeStatus(string stat)
        {
            if (stat.Equals("Samofinansiranje"))
                return Status.S;
            else
                return Status.B;
        }

        private Adresa makeAdresa(string input)
        {
            var delovi = input.Split(',');
            if (delovi.Length != 4)
            {
                throw new FormatException("String ne sadrži ispravan broj polja.");
            }

            return new Adresa
            (   delovi[0].Trim(),
                int.Parse(delovi[1].Trim()),
                delovi[2].Trim(),
                delovi[3].Trim()
            );
        }

        private Indeks makeIndex(string input)
        {
            string[] parts = input.Split('/');

            string oznaka = input.Substring(0, 2);
            int upis = int.Parse(parts[0].Substring(3));
            int godina = int.Parse(parts[1]);

            Indeks indeks = new Indeks(oznaka, upis, godina);

            return indeks;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdStudent.ToString(),
                Prezime,
                Ime,
                DatumRodjenja.ToString(),
                IdAdrese.ToString(),
                KontaktTelefon,
                EmailAdresa,
                IdIndeksa.ToString(),
                TrenutnaGodinaStudija.ToString(),
                Enum.GetName(typeof(Status),Status),
                ProsecnaOcena.ToString(),
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdStudent = int.Parse(values[0]);
            Prezime = values[1];
            Ime = values[2];
            DatumRodjenja = DateOnly.Parse(values[3]);
            IdAdrese = int.Parse(values[4]);
            KontaktTelefon = values[5];
            EmailAdresa = values[6];
            IdIndeksa = int.Parse(values[7]);
            TrenutnaGodinaStudija = int.Parse(values[8]);
            Status = (Status)Enum.Parse(typeof(Status), values[9]);
            ProsecnaOcena = double.Parse(values[10]);  
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int maxLabelLength = 30;
            string format = "{0,-" + maxLabelLength + "}: {1}";

            sb.AppendLine(string.Format(format, "ID studenta", IdStudent));
            sb.AppendLine(string.Format(format, "Prezime", Prezime));
            sb.AppendLine(string.Format(format, "Ime", Ime));
            sb.AppendLine(string.Format(format, "Datum rođenja", DatumRodjenja.ToString("dd.MM.yyyy")));
            sb.AppendLine(string.Format(format, "Adresa stanovanja", AdresaStanovanja.ToString()));
            sb.AppendLine(string.Format(format, "Kontakt telefon", KontaktTelefon));
            sb.AppendLine(string.Format(format, "Email adresa", EmailAdresa));
            sb.AppendLine(string.Format(format, "Broj indeksa", Indeks.ToString()));
            sb.AppendLine(string.Format(format, "Trenutna godina studija", TrenutnaGodinaStudija));
            sb.AppendLine(string.Format(format, "Status", Status));
            sb.AppendLine(string.Format(format, "Prosečna ocena", ProsecnaOcena));

            return sb.ToString();
        }

    }
}
