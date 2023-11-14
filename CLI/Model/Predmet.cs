﻿using System;
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
            sb.AppendLine("Predmetni profesor: " + profesor.ime + " " + profesor.prezime);
            sb.AppendLine("Broj ESPB bodova: " + brojESPB);

            /*
            sb.AppendLine("Spisak položenih studenata:");
            foreach (Student student in spisakPolozenihStudenata)
            {
                sb.AppendLine(student.ToString());
            }

            sb.AppendLine("Spisak nepoloženih studenata:");
            foreach (Student student in spisakNepolozenihStudenata)
            {
                sb.AppendLine(student.ToString());
            
            */

            sb.AppendLine("Studenti koji su polozili: \n");

            foreach(Student s in spisakPolozenihStudenata)
            {
                sb.Append(s.indeks.ToString() + " " + s.ime + " " + s.prezime + "\n");
            }

            sb.AppendLine("Studenti koji nisu polozili: \n");
            foreach (Student s in spisakNepolozenihStudenata)
            {
                sb.Append(s.indeks.ToString() + " " + s.ime + " " + s.prezime + "\n");
            }


            return sb.ToString();
        }
    }
}
