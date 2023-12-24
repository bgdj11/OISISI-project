using CLI.Model;
using System.Text;
using CLI.Serialization;

namespace CLI.Model
{
    public class ProfesorKatedra : ISerializable
    {
        public int IdProfesora {  get; set; }
        public int IdKatedre {  get; set; }

        ProfesorKatedra() { }

        ProfesorKatedra(int IdProfesora, int IdKatedre)
        {
            this.IdKatedre = IdKatedre;
            this.IdProfesora = IdProfesora;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IdProfesora: ").Append(IdProfesora).Append(",");
            sb.Append("IdKatedre: ").Append(IdKatedre).Append(",");

            return sb.ToString();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                IdProfesora.ToString(),
                IdKatedre.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdKatedre = int.Parse(values[0]);
            IdProfesora = int.Parse(values[1]);
        }

    }
}
