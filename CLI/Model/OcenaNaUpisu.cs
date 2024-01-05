using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    public class OcenaNaUpisu : ISerializable
    {
        public int IdOcene { get; set; }
        public int IdStudenta {  get; set; }
        public Student Student { get; set; }
        public int IdPredmeta { get; set; }
        public Predmet Predmet { get; set; }
        public DateOnly Datum { get; set; }

        public int Espb { get; set; }

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
        public OcenaNaUpisu(Student student, Predmet predmet, string datum, int ocena)
        {
            this.Student = student;
            this.Predmet = predmet;
            //this.Espb = espb;
            this.Datum = DateOnly.Parse(datum);
            this.ocena = ocena;

        }

        public OcenaNaUpisu(int StudentId, int PredmetId, string datum, int ocena)
        {
            this.IdStudenta = StudentId;
            this.IdPredmeta = PredmetId;
            this.Datum = DateOnly.Parse(datum);
            this.ocena = ocena;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdOcene.ToString(),
                ocena.ToString(),
                IdStudenta.ToString(),
                IdPredmeta.ToString(),
                Datum.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdOcene = int.Parse(values[0]);
            ocena = int.Parse(values[1]);
            IdStudenta = int.Parse(values[2]);
            IdPredmeta = int.Parse(values[3]);
            Datum = DateOnly.Parse(values[4]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Ocena: ").Append(ocena.ToString()).Append(", ");
            sb.Append("IdStudenta: ").Append(IdStudenta.ToString()).Append(", ");
            sb.Append("IdPredmeta: ").Append(IdPredmeta.ToString()).Append(", ");  
            sb.Append("Datum: ").Append(Datum);

            return sb.ToString();
        }


    }
}
