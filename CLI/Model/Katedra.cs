using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    class Katedra : ISerializable
    {

        public int sifraKatedre { get; set; }
        public string nazivKatedre { get; set; }
        public Profesor sefKatedre { get; set; }
        public List<Profesor> profesoriNaKatedri { get; set; }

        public Katedra(int sifra, string naziv, Profesor sef)
        {
            sifraKatedre = sifra;
            nazivKatedre = naziv;
            sefKatedre = sef;
            profesoriNaKatedri = new List<Profesor>();
        }       
]       public string[] ToCSV()
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
