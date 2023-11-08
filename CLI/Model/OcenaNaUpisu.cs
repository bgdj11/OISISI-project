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
        public int idStudenta {  get; set; }
        public Student student { get; set; }
        public int idPredmeta { get; set; }
        public Predmet predmet { get; set; }
        public DateOnly datum { get; set; }

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
        public OcenaNaUpisu() { }
        public OcenaNaUpisu(Student student, Predmet predmet, DateOnly datum, int ocena, int oc)
        {
            this.student = student;
            this.predmet = predmet;
            this.datum = datum;
            Ocena = oc;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idOcene.ToString(),
                ocena.ToString(),
                idStudenta.ToString(),
                idPredmeta.ToString(),
                datum.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idOcene = int.Parse(values[0]);
            ocena = int.Parse(values[1]);
            idStudenta = int.Parse(values[2]);
            idPredmeta = int.Parse(values[3]);
            datum = DateOnly.Parse(values[4]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Ocena: ").Append(ocena.ToString()).Append(", ");
            sb.Append("IdStudenta: ").Append(idStudenta.ToString()).Append(", ");
            sb.Append("IdPredmeta: ").Append(idPredmeta.ToString()).Append(", ");  
            sb.Append("Datum: ").Append(datum);

            return sb.ToString();
        }


    }
}
