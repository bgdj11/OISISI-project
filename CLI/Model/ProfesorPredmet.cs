using CLI.Model;

namespace CLI.Model
{
    class ProfesorPredmet
    {
        public int IdProfesora {  get; set; }
        public int IdPredmeta {  get; set; }

        ProfesorPredmet() { }

        ProfesorPredmet(int IdProfesora, int IdPredmeta)
        {
            this.IdProfesora = IdProfesora;
            this.IdPredmeta = IdPredmeta;
        }   
    }
}
