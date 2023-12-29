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
        public int IdAdrese { get; set;}
        public string Ulica {get;set;}
        public int Broj {get;set;} 
        public string Grad {get;set;}  
        public string Drzava {get;set;}

        public Adresa() { }
        public Adresa(string ulica, int broj, string grad, string drzava)
        {
            this.Ulica = ulica;
            this.Broj = broj;
            this.Grad = grad;
            this.Drzava = drzava;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdAdrese.ToString(),
                Ulica,
                Broj.ToString(),
                Grad,
                Drzava
            
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdAdrese = int.Parse(values[0]);
            Ulica = values[1];
            Broj = int.Parse(values[2]);
            Grad = values[3];
            Drzava = values[4];
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Ulica).Append(", ");
            sb.Append(Broj).Append(", ");
            sb.Append(Grad).Append(", ");
            sb.Append(Drzava);
            return sb.ToString();
        }



    }
}
