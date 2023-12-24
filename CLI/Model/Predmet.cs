using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

public enum Semestar
{
    L,
    Z
}

namespace CLI.Model
{
    public class Predmet : ISerializable
    {
        public int idPredmet { get; set; }
        public string sifraPredmeta { get; set; }
        public string nazivPredmeta { get; set; }
        public Semestar semestar { get; set; }
        public int godinaStudija { get; set; }
        public Profesor profesor { get; set; }

        public int  idProfesora { get; set; }
        public int brojESPB { get; set; }
        public List<Student> spisakPolozenihStudenata { get; set; }
        public List<Student> spisakNepolozenihStudenata { get; set; }

        public Predmet()
        {
            spisakNepolozenihStudenata = new List<Student>();
            spisakPolozenihStudenata = new List<Student>();
        }

        public Predmet(string sifra, string naziv, Semestar sem, int godina, int idProfesora, int espb)
        {
            sifraPredmeta = sifra;
            nazivPredmeta = naziv;
            semestar = sem;
            godinaStudija = godina;
            this.idProfesora = idProfesora;
            brojESPB = espb;
            spisakPolozenihStudenata = new List<Student>();
            spisakNepolozenihStudenata = new List<Student>();
        }

        public Predmet(string sifra, string naziv, string sem, int godina, int idProfesora, int espb)
        {
            sifraPredmeta = sifra;
            nazivPredmeta = naziv;
            semestar = MakeSemestar(sem);
            godinaStudija = godina;
            this.idProfesora = idProfesora;
            brojESPB = espb;
            spisakPolozenihStudenata = new List<Student>();
            spisakNepolozenihStudenata = new List<Student>();
        }

        private Semestar MakeSemestar(string sem)
        {
            if (sem.Equals("letnji"))
            {
                return Semestar.L;
            }
            else
                return Semestar.Z;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idPredmet.ToString(),
                sifraPredmeta,
                nazivPredmeta,
                semestar.ToString(),
                godinaStudija.ToString(),
                idProfesora.ToString(),
                brojESPB.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idPredmet = int.Parse(values[0]);
            sifraPredmeta = values[1];
            nazivPredmeta = values[2];
            semestar = (Semestar)Enum.Parse(typeof(Semestar), values[3]);
            godinaStudija = int.Parse(values[4]);
            idProfesora = int.Parse(values[5]);
            brojESPB = int.Parse(values[6]);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID predmeta: " + idPredmet);
            sb.AppendLine("Šifra predmeta: " + sifraPredmeta);
            sb.AppendLine("Naziv predmeta: " + nazivPredmeta);
            sb.AppendLine("Semestar: " + semestar);
            sb.AppendLine("Godina studija: " + godinaStudija);
            //sb.AppendLine("Predmetni profesor: " + profesor.ime + " " + profesor.prezime);
            sb.AppendLine("Broj ESPB bodova: " + brojESPB);


            return sb.ToString();
        }
    }
}
