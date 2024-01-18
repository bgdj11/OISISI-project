using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Controller
{
    public class StudentPredmetController
    {
        private readonly StudentPredmetDAO studentPredmetDAO;
        public StudentPredmetController() { 
            studentPredmetDAO = new StudentPredmetDAO();
        }

        public void AddPredmetToStudent(int ids, int idp)
        {
            studentPredmetDAO.AddPredmetToStudent(ids, idp);
        }

        public StudentPredmet? GetByIds(int ids, int idp)
        {
            return studentPredmetDAO.GetByIds(ids, idp);
        }

        public void RemovePredmetFromStudent(StudentPredmet sp)
        {
            studentPredmetDAO.RemovePredmetFromStudent(sp);
        }


    }
}
