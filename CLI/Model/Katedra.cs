using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    class Katedra : ISerializable
    {
        public int idKatedre { get; set; }
        public int sifraKatedre { get; set; }
        public string nazivKatedre { get; set; }

        public int idSefa {  get; set; }
        public Profesor sefKatedre { get; set; }
        public List<Profesor> profesoriNaKatedri { get; set; }

        public Katedra() { }

        public Katedra(int sifra, string naziv, int id)
        {
            sifraKatedre = sifra;
            nazivKatedre = naziv;
            idSefa = id;
            profesoriNaKatedri = new List<Profesor>();
        }       
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                   idKatedre.ToString(),
                   sifraKatedre.ToString(),
                   nazivKatedre,
                   idSefa.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idKatedre = int.Parse(values[0]);
            sifraKatedre = int.Parse(values[1]);
            nazivKatedre = values[2];
            idSefa = int.Parse(values[3]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IdKatedre: ").Append(idKatedre).Append(", ");
            sb.Append("SifraKatedre: ").Append(sifraKatedre).Append(", ");
            sb.Append("NazivKatedre: ").Append(nazivKatedre).Append(", ");

            if (sefKatedre != null)
            {
                sb.Append("SefKatedre: ").Append(sefKatedre.ToString()).Append(", ");
            }
            else
            {
                sb.Append("SefKatedre: [null], ");
            }

            sb.Append("ProfesoriNaKatedri: [");
            if (profesoriNaKatedri != null && profesoriNaKatedri.Count > 0)
            {
                foreach (var profesor in profesoriNaKatedri)
                {
                    sb.Append(profesor.ToString()).Append(", ");
                }
                
                sb.Length -= 2;
            }
            sb.Append("]");

            return sb.ToString();
        }

    }
}
