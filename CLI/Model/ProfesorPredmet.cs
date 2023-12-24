using CLI.Model;
using CLI.Serialization;
using System.Text;

namespace CLI.Model 
{
    public class ProfesorPredmet : ISerializable
    {
        public int IdProfesora {  get; set; }
        public int IdPredmeta {  get; set; }

        public ProfesorPredmet() { }

        public ProfesorPredmet(int IdProfesora, int IdPredmeta)
        {
            this.IdProfesora = IdProfesora;
            this.IdPredmeta = IdPredmeta;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IdProfesora: ").Append(IdProfesora).Append(",");
            sb.Append("IdPredmeta: ").Append(IdPredmeta).Append(",");

            return sb.ToString();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                    IdProfesora.ToString(),
                    IdPredmeta.ToString()
                };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdProfesora = int.Parse(values[0]);
            IdPredmeta = int.Parse(values[1]);
        }
}
}
