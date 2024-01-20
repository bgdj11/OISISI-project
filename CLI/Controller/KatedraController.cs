using CLI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;

namespace CLI.Controller
{


    public class KatedraController
    {
        private readonly KatedraDAO katedraDao;

        public KatedraController()
        {
            katedraDao = new KatedraDAO();
        }

        public List<Katedra> GetAllKatedra()
        {
            return katedraDao.GetAllKatedra();
        }

        public void MakeKatedra()
        {
            katedraDao.MakeKatedra();
        }

        public void DeleteKatedra(int id)
        {
            katedraDao.RemoveKatedra(id);
        }

        public void AddKatedra(Katedra k)
        {
            katedraDao.AddKatedra(k);
        }

        public void UpdateKatedra(Katedra k)
        {
            katedraDao.UpdateKatedra(k);
        }

        public Katedra? GetKatedraById(int id)
        {
            return katedraDao.GetKatedraById(id);
        }

        public Katedra? RemoveKatedra(int id)
        {
            return katedraDao.RemoveKatedra(id);
        }

    }


}
