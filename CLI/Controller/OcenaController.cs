using CLI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;

namespace CLI.Controller
{
    public class OcenaController
    {
        private readonly OcenaNaUpisuDAO ocenaDao;
        
        public OcenaController()
        {
            ocenaDao = new OcenaNaUpisuDAO();
        }

        public List<CLI.Model.OcenaNaUpisu> getAllOcena()
        {
            return ocenaDao.GetAllOcena();
        }

        public void MakeOcena()
        {
            ocenaDao.MakeOcena();
        }

        public void DeleteOcena(int id)
        {
            ocenaDao.RemoveOcena(id);
        }

        public void AddOcena(CLI.Model.OcenaNaUpisu ocena)
        {
            ocenaDao.AddOcena(ocena);
        }

        public OcenaNaUpisu? GetOcenaById(int id)
        {
            return ocenaDao.GetOcenaById(id);
        }

    }
}
