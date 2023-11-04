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
        public string sifraPredmeta { get; set; }
        public string nazivPredmeta { get; set; }
        public Semestar semestar { get; set; }
        public int godinaStudija { get; set; }
        public Profesor predmetniProfesor { get; set; }
        public int brojESPB { get; set; }
        public List<Student> spisakPolozenihStudenata { get; set; }
        public List<Student> spisakNepolozenihStudenata { get; set; }


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
