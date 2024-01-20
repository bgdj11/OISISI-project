using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class PredmetController
    {
        private readonly PredmetDAO predmetDao;

        public PredmetController()
        {
            predmetDao = new PredmetDAO();
        }

        public void MakePredmet()
        {
            predmetDao.MakePredmet();
        }

        public void AddPredmet(Predmet p)
        {
            predmetDao.AddPredmet(p); 
        }

        public void RemovePredmet(int id)
        {
            predmetDao.RemovePredmet(id);
        }

        public List<Predmet> GetAllPredmet()
        {
            return predmetDao.GetAllPredmeti();
        }

        public void UpdatePredmet(Predmet p)
        {
            predmetDao.UpdatePredmet(p);
        }

        public Predmet? GetPredmetById(int id)
        {
            return predmetDao.GetPredmetById(id);
        }
    }
}
