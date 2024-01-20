using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class ProfesorController
    {
        private readonly ProfesorDAO profesorDao;

        public ProfesorController()
        {
            profesorDao = new ProfesorDAO();
        }

        public List<Profesor> GetAllProfesor()
        {
            return profesorDao.GetAllProfesors();
        }

        public void DeleteProfesor(int id)
        {
            profesorDao.RemoveProfesor(id);
        }

        public void MakeProfesor()
        {
            profesorDao.MakeProfesor();
        }

        public void AddProfesor(Profesor p)
        {
            profesorDao.AddProfesor(p);
        }
        
        public void UpdateProfesor(Profesor p)
        {
            profesorDao.UpdateProfesor(p);
        }

        public Profesor? GetProfesorById(int id)
        {
            return profesorDao?.GetProfesorById(id);   
        }

    }

}
