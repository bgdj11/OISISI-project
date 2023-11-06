using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    class OcenaNaUpisu : ISerializable
    {
        public int idOcene { get; set; }
        public Student student { get; set; }
        public Predmet predmet { get; set; }
        public DateTime datum { get; set; }

        private int ocena;

        public int Ocena
        {
            get { return ocena; }

            set
            {
                if (value >= 6 && value <= 10)
                {
                    ocena = value;
                }
                else
                {
                    throw new ArgumentException("Ocena mora biti u intervalu od 6 do 10");
                }
            }
        }
        public OcenaNaUpisu(Student student, Predmet predmet, DateTime datum, int ocena, int oc)
        {
            this.student = student;
            this.predmet = predmet;
            this.datum = datum;
            Ocena = oc;
            Ocena = oc;
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
            sb.Append("IdOcene: ").Append(idOcene).Append(", ");
            sb.Append("Student: ");
            if (student != null)
            {
                sb.Append(student.ToString()).Append(", ");
            }
            else
            {
                sb.Append("[null], ");
            }
            sb.Append("Predmet: ");
            if (predmet != null)
            {
                sb.Append(predmet.ToString()).Append(", ");
            }
            else
            {
                sb.Append("[null], ");
            }
            sb.Append("Datum: ").Append(datum);

            return sb.ToString();
        }


    }
}
