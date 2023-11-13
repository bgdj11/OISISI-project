using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;

namespace CLI.DAO
{
    class PredmetDAO
    {
        private readonly List<Predmet> _predmeti;
        private readonly Storage<Predmet> _storage;

        public PredmetDAO()
        {
            _storage = new Storage<Predmet>("predmeti.txt");
            _predmeti = _storage.Load();
        }

        private int GenerateId()
        {
            if (_predmeti.Count == 0) return 0;
            return _predmeti[^1].idPredmet + 1;
        }

        public Predmet AddPredmet(Predmet predmet)
        {
            predmet.idPredmet = GenerateId();
            _predmeti.Add(predmet);
            _storage.Save(_predmeti);
            return predmet;
        }

        public Predmet? UpdatePredmet(Predmet predmet)
        {
            Predmet? oldPredmet = GetPredmetById(predmet.idPredmet);
            if (oldPredmet == null) return null;

            oldPredmet.sifraPredmeta = predmet.sifraPredmeta;
            oldPredmet.nazivPredmeta = predmet.nazivPredmeta;
            oldPredmet.semestar = predmet.semestar;
            oldPredmet.godinaStudija = predmet.godinaStudija;
            oldPredmet.idPredmetnogProfesora = predmet.idPredmetnogProfesora;
            oldPredmet.brojESPB = predmet.brojESPB;

            _storage.Save(_predmeti);
            return oldPredmet;
        }

        public Predmet? RemovePredmet(int id)
        {
            Predmet? predmet = GetPredmetById(id);
            if (predmet == null) return null;

            _predmeti.Remove(predmet);
            _storage.Save(_predmeti);
            return predmet;
        }

        public Predmet? GetPredmetById(int idPredmet)
        {
            return _predmeti.Find(p => p.idPredmet == idPredmet);
        }

        public List<Predmet> GetAllPredmeti()
        {
            return _predmeti;
        }

        public List<Predmet> GetAllPredmeti(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Predmet> predmeti = _predmeti;

            switch (sortCriteria)
            {
                case "Id":
                    predmeti = _predmeti.OrderBy(p => p.idPredmet);
                    break;
                case "SifraPredmeta":
                    predmeti = _predmeti.OrderBy(p => p.sifraPredmeta);
                    break;
                case "NazivPredmeta":
                    predmeti = _predmeti.OrderBy(p => p.nazivPredmeta);
                    break;
                case "Semestar":
                    predmeti = _predmeti.OrderBy(p => p.semestar);
                    break;
                case "GodinaStudija":
                    predmeti = _predmeti.OrderBy(p => p.godinaStudija);
                    break;
                case "IdPredmetnogProfesora":
                    predmeti = _predmeti.OrderBy(p => p.idPredmetnogProfesora);
                    break;
                case "BrojESPB":
                    predmeti = _predmeti.OrderBy(p => p.brojESPB);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                predmeti = predmeti.Reverse();

            predmeti = predmeti.Skip((page - 1) * pageSize).Take(pageSize);

            return predmeti.ToList();
        }
    }
}
