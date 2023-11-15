using CLI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
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

        public int idStudent { get; set; }
        public string prezime { get; set; }
        public string ime { get; set; }
        public DateOnly datumRodjenja { get; set; }

        public int idIndeksa {  get; set; }

        public Indeks indeks { get; set; }

        public Adresa adresaStanovanja { get; set; }

        public int idAdrese { get; set; }
        public string kontaktTelefon { get; set; }
        public string emailAdresa { get; set; }
        public string brojIndeksa { get; set; }
        public int trenutnaGodinaStudija { get; set; }

        public Status status { get; set; }
        public double prosecnaOcena { get; set; }
        public List<OcenaNaUpisu> PolozeniIspiti { get; set; }

        public List<Predmet> NepolozeniIspiti { get; set; }



        public Student()
        {
            adresaStanovanja = new Adresa();
            indeks = new Indeks();
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
            this.prezime = prezime;
            this.ime = ime;
            this.datumRodjenja = datumRodjenja;
            adresaStanovanja = a;
            this.kontaktTelefon = kontaktTelefon;
            this.emailAdresa = emailAdresa;
            this.trenutnaGodinaStudija = trenutnaGodinaStudija;
            this.status = status;
            this.prosecnaOcena = prosecnaOcena;
            idAdrese = adresa;
            idIndeksa = idInd;
            indeks = ind;
            PolozeniIspiti = new List<OcenaNaUpisu>();
            NepolozeniIspiti = new List<Predmet>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idStudent.ToString(),
                prezime,
                ime,
                datumRodjenja.ToString(),
                adresaStanovanja.idAdrese.ToString(),
                kontaktTelefon,
                emailAdresa,
                brojIndeksa,
                trenutnaGodinaStudija.ToString(),
                Enum.GetName(typeof(Status),status),
                prosecnaOcena.ToString(),
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idStudent = int.Parse(values[0]);
            prezime = values[1];
            ime = values[2];
            datumRodjenja = DateOnly.Parse(values[3]);
            idAdrese = int.Parse(values[4]);
            kontaktTelefon = values[5];
            emailAdresa = values[6];
            brojIndeksa = values[7];
            trenutnaGodinaStudija = int.Parse(values[8]);
            status = (Status)Enum.Parse(typeof(Status), values[9]);
            prosecnaOcena = double.Parse(values[10]);  
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int maxLabelLength = 30;
            string format = "{0,-" + maxLabelLength + "}: {1}";

            sb.AppendLine(string.Format(format, "ID studenta", idStudent));
            sb.AppendLine(string.Format(format, "Prezime", prezime));
            sb.AppendLine(string.Format(format, "Ime", ime));
            sb.AppendLine(string.Format(format, "Datum rođenja", datumRodjenja.ToString("dd.MM.yyyy")));
            sb.AppendLine(string.Format(format, "Adresa stanovanja", adresaStanovanja));
            sb.AppendLine(string.Format(format, "Kontakt telefon", kontaktTelefon));
            sb.AppendLine(string.Format(format, "Email adresa", emailAdresa));
            sb.AppendLine(string.Format(format, "Broj indeksa", brojIndeksa));
            sb.AppendLine(string.Format(format, "Trenutna godina studija", trenutnaGodinaStudija));
            sb.AppendLine(string.Format(format, "Status", status));
            sb.AppendLine(string.Format(format, "Prosečna ocena", prosecnaOcena));

            return sb.ToString();
        }

    }
}
