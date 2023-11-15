using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage;

namespace CLI.DAO
{
    class ProfesorPredmetDAO
    {

        private readonly Storage<ProfesorPredmet> _storage;
        private readonly List<ProfesorPredmet> _profPred;

        public ProfesorPredmetDAO()
        {
            _storage = new Storage<ProfesorPredmet>("ProfesorPredmet.csv");
            _profPred = _storage.Load();
        }

        public void AddPredmetToProfesor(int idp, int idpd)
        {
            ProfesorPredmet pp = new ProfesorPredmet(idp, idpd);

            _profPred.Add(pp);
            _storage.Save(_profPred);
        }
    }
}
