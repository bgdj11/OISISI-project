using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

public enum Semestar
{
    Letnji,
    Zimski
}

namespace CLI.Model
{
    public class Predmet : ISerializable
    {
        public int IdPredmet { get; set; }
        public string SifraPredmeta { get; set; }
        public string NazivPredmeta { get; set; }
        public Semestar Semestar { get; set; }
        public int GodinaStudija { get; set; }
        public Profesor Profesor { get; set; }

        public int  IdProfesora { get; set; }
        public int BrojESPB { get; set; }
        public List<Student> SpisakPolozenihStudenata { get; set; }
        public List<Student> SpisakNepolozenihStudenata { get; set; }

        public Predmet()
        {
            SpisakNepolozenihStudenata = new List<Student>();
            SpisakPolozenihStudenata = new List<Student>();
        }

        public Predmet(string sifra, string naziv, Semestar sem, int godina, int idProfesora, int espb)
        {
            SifraPredmeta = sifra;
            NazivPredmeta = naziv;
            Semestar = sem;
            GodinaStudija = godina;
            this.IdProfesora = idProfesora;
            BrojESPB = espb;
            SpisakPolozenihStudenata = new List<Student>();
            SpisakNepolozenihStudenata = new List<Student>();
        }

        public Predmet(string sifra, string naziv, string sem, int godina, int idProf, int espb)
        {
            SifraPredmeta = sifra;
            NazivPredmeta = naziv;
            Semestar = MakeSemestar(sem);
            GodinaStudija = godina;
            IdProfesora = idProf;
            BrojESPB = espb;
            SpisakPolozenihStudenata = new List<Student>();
            SpisakNepolozenihStudenata = new List<Student>();
        }

        private Semestar MakeSemestar(string sem)
        {
            if (sem.Equals("letnji"))
            {
                return Semestar.Letnji;
            }
            else
                return Semestar.Zimski;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdPredmet.ToString(),
                SifraPredmeta,
                NazivPredmeta,
                Semestar.ToString(),
                GodinaStudija.ToString(),
                IdProfesora.ToString(),
                BrojESPB.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdPredmet = int.Parse(values[0]);
            SifraPredmeta = values[1];
            NazivPredmeta = values[2];
            Semestar = (Semestar)Enum.Parse(typeof(Semestar), values[3]);
            GodinaStudija = int.Parse(values[4]);
            IdProfesora = int.Parse(values[5]);
            BrojESPB = int.Parse(values[6]);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID predmeta: " + IdPredmet);
            sb.AppendLine("Šifra predmeta: " + SifraPredmeta);
            sb.AppendLine("Naziv predmeta: " + NazivPredmeta);
            sb.AppendLine("Semestar: " + Semestar);
            sb.AppendLine("Godina studija: " + GodinaStudija);
            //sb.AppendLine("Predmetni profesor: " + profesor.ime + " " + profesor.prezime);
            sb.AppendLine("Broj ESPB bodova: " + BrojESPB);


            return sb.ToString();
        }
    }
}
