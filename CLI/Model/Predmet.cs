using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Serialization;

enum Semestar
{
    L,
    Z
}

namespace CLI.Model
{
    class Predmet : ISerializable
    {
        public string SifraPredmeta { get; set; }
        public string NazivPredmeta { get; set; }
        public Semestar Semestar { get; set; }
        public int GodinaStudija { get; set; }
        public Profesor PredmetniProfesor { get; set; }
        public int BrojESPB { get; set; }
        public List<Student> SpisakPolozenihStudenata { get; set; }
        public List<Student> SpisakNepolozenihStudenata { get; set; }


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
