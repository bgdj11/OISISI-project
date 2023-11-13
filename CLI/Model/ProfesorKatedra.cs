using CLI.Model;

namespace CLI.Model
{
    class ProfesorKatedra
    {
        public int IdProfesora {  get; set; }
        public int IdKatedre {  get; set; }

        ProfesorKatedra() { }

        ProfesorKatedra(int IdProfesora, int IdKatedre)
        {
            this.IdKatedre = IdKatedre;
            this.IdProfesora = IdProfesora;
        }
    }
}
