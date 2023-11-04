using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Serialization;

namespace CLI.Model
{
    class Adresa : ISerializable
    {

        public string ulica {get;set;}
        public int broj {get;set;} 
        public string grad {get;set;}  
        public string drzava {get;set;}

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

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {

        }

    }
}
