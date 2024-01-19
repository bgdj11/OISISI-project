using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    public class Katedra : ISerializable
    {
        public int idKatedre { get; set; }
        public int sifraKatedre { get; set; }

        public string sifra { get; set;}
        public string nazivKatedre { get; set; }

        public int idSefa {  get; set; }
        public Profesor sefKatedre { get; set; }
        public List<Profesor> profesoriNaKatedri { get; set; }

        public Katedra() {
            profesoriNaKatedri = new List<Profesor>();
        }

        public Katedra(int sifra, string naziv, int id)
        {
            sifraKatedre = sifra;
            nazivKatedre = naziv;
            idSefa = id;
            profesoriNaKatedri = new List<Profesor>();
        }  
        
        public Katedra(string sifra, string naziv, int id)
        {
            this.sifra = sifra;
            nazivKatedre = naziv;
            idSefa = id;
            profesoriNaKatedri = new List<Profesor>();
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                   idKatedre.ToString(),
                   sifra,
                   nazivKatedre,
                   idSefa.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idKatedre = int.Parse(values[0]);
            sifra = values[1];
            nazivKatedre = values[2];
            idSefa = int.Parse(values[3]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IdKatedre: ").Append(idKatedre).Append(", ");
            sb.Append("SifraKatedre: ").Append(sifra).Append(", ");
            sb.Append("NazivKatedre: ").Append(nazivKatedre).Append(", ");
            sb.Append("IdSefa: ").Append(idSefa).Append(", ");

            return sb.ToString();
        }

    }
}
