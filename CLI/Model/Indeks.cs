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
        public string OznakaSmera { get; set; }
        public int BrojUpisa { get; set; }  
        public int GodinaUpisa { get; set; }

        public Indeks(string oznakaSmera, int brojUpisa, int godinaUpisa)
        {
            OznakaSmera = oznakaSmera;
            BrojUpisa = brojUpisa;
            GodinaUpisa = godinaUpisa;
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
    }
}
