using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{
    public class StudentPredmet : ISerializable
    {
        public int IdStudent { get; set; }
        public int IdPredmet { get; set; }

        public StudentPredmet() { }

        public StudentPredmet(int idStudent, int idPredmet)
        {
            IdStudent = idStudent;
            IdPredmet = idPredmet;
        }

        public void FromCSV(string[] values)
        {
            IdStudent = int.Parse(values[0]);
            IdPredmet = int.Parse(values[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdStudent.ToString(),
                IdPredmet.ToString()
            };

            return csvValues;
        }
    }
}
