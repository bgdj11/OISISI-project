using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    class Indeks : ISerializable
    {   
        public int idIndeksa { get; set; }
        public string oznakaSmera { get; set; }
        public int brojUpisa { get; set; }  
        public int godinaUpisa { get; set; }

        public Indeks() { }

        public Indeks(string oznakaSmera, int brojUpisa, int godinaUpisa)
        {
            this.oznakaSmera = oznakaSmera;
            this.brojUpisa = brojUpisa;
            this.godinaUpisa = godinaUpisa;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idIndeksa.ToString(),
                oznakaSmera, 
                brojUpisa.ToString(),
                godinaUpisa.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idIndeksa = int.Parse(values[0]);
            oznakaSmera = values[1];
            brojUpisa = int.Parse(values[2]);
            godinaUpisa = int.Parse(values[3]);

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(oznakaSmera).Append("/");
            sb.Append(brojUpisa).Append("/");
            sb.Append(godinaUpisa);
            return sb.ToString();
        }

    }
}
