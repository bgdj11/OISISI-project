using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class AdresaController
    {
        private readonly AdresaDAO _adresaDao;

        public AdresaController()
        {
            _adresaDao = new AdresaDAO();
        }

        public List<Adresa> GetAllAdresa()
        {
            return _adresaDao.GetAllAdresa();
        }

        public void Add(Adresa adresa)
        {
            _adresaDao.AddAdresa(adresa);
        }

        public void RemoveAdresa(int id)
        {
            _adresaDao.RemoveAdresa(id);
        }

    }
}
