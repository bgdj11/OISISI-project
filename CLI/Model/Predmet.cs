using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

enum Semestar
{
    L,
    Z
}

namespace CLI.Model
{
    class Predmet : ISerializable
    {
        public int idPredmet { get; set; }
        public string sifraPredmeta { get; set; }
        public string nazivPredmeta { get; set; }
        public Semestar semestar { get; set; }
        public int godinaStudija { get; set; }
        public Profesor predmetniProfesor { get; set; }
        public int brojESPB { get; set; }
        public List<Student> spisakPolozenihStudenata { get; set; }
        public List<Student> spisakNepolozenihStudenata { get; set; }

        public Predmet(string sifra, string naziv, Semestar sem, int godina, Profesor profesor, int espb)
        {
            sifraPredmeta = sifra;
            nazivPredmeta = naziv;
            semestar = sem;
            godinaStudija = godina;
            predmetniProfesor = profesor;
            brojESPB = espb;
            spisakPolozenihStudenata = new List<Student>();
            spisakNepolozenihStudenata = new List<Student>();
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
            sb.AppendLine("ID predmeta: " + idPredmet);
            sb.AppendLine("Šifra predmeta: " + sifraPredmeta);
            sb.AppendLine("Naziv predmeta: " + nazivPredmeta);
            sb.AppendLine("Semestar: " + semestar);
            sb.AppendLine("Godina studija: " + godinaStudija);
            sb.AppendLine("Predmetni profesor: " + predmetniProfesor.ime + " " + predmetniProfesor.prezime);
            sb.AppendLine("Broj ESPB bodova: " + brojESPB);

            sb.AppendLine("Spisak položenih studenata:");
            foreach (Student student in spisakPolozenihStudenata)
            {
                sb.AppendLine(student.ToString());
            }

            sb.AppendLine("Spisak nepoloženih studenata:");
            foreach (Student student in spisakNepolozenihStudenata)
            {
                sb.AppendLine(student.ToString());
            }

            return sb.ToString();
        }
    }
}
