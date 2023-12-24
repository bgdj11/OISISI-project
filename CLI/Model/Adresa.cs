using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    public class Adresa : ISerializable
    {
        public int idAdrese { get; set;}
        public string ulica {get;set;}
        public int broj {get;set;} 
        public string grad {get;set;}  
        public string drzava {get;set;}

        public Adresa() { }
        public Adresa(string ulica, int broj, string grad, string drzava)
        {
            this.ulica = ulica;
            this.broj = broj;
            this.grad = grad;
            this.drzava = drzava;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idAdrese.ToString(),
                ulica,
                broj.ToString(),
                grad,
                drzava
            
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idAdrese = int.Parse(values[0]);
            ulica = values[1];
            broj = int.Parse(values[2]);
            grad = values[3];
            drzava = values[4];
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Ulica: ").Append(ulica).Append(", ");
            sb.Append("Broj: ").Append(broj).Append(", ");
            sb.Append("Grad: ").Append(grad).Append(", ");
            sb.Append("Drzava: ").Append(drzava);
            return sb.ToString();
        }



    }
}
