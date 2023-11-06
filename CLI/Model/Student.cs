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

        public int idStudent {  get; set; }
        public string prezime { get; set; }
        public string ime { get; set; }
        public DateOnly datumRodjenja { get; set; }

        public Adresa adresaStanovanja { get; set; }

        public int idAdrese { get; set; }
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
        DateOnly datumRodjenja,
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
            sb.AppendLine("ID studenta: " + idStudent);
            sb.AppendLine("Prezime: " + prezime);
            sb.AppendLine("Ime: " + ime);
            sb.AppendLine("Datum rođenja: " + datumRodjenja.ToString("dd.MM.yyyy"));
            sb.AppendLine("Adresa stanovanja: " + adresaStanovanja);
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
