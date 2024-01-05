using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;

namespace CLI.DAO
{
    public class PredmetDAO
    {
        private readonly List<Predmet> _predmeti;
        private readonly Storage<Predmet> _storage;

        public PredmetDAO()
        {
            _storage = new Storage<Predmet>("predmeti.csv");
            _predmeti = _storage.Load();
            MakePredmet();
        }

        private int GenerateId()
        {
            if (_predmeti.Count == 0) return 0;
            return _predmeti[^1].IdPredmet + 1;
        }

        public void MakePredmet()
        {
            Storage<Student> _studentStorage = new Storage<Student>("studenti.csv");
            List<Student> _studenti = _studentStorage.Load();

            Storage<Profesor> _profesorStorage = new Storage<Profesor>("profesori.csv");
            List<Profesor> _profesori = _profesorStorage.Load();


            foreach (Predmet p in _predmeti)
            {
                
                foreach (Student s in _studenti)
                {
                    if (s.NepolozeniIspiti.Find(n => n.IdPredmet == p.IdPredmet) != null)
                    {
                        p.SpisakNepolozenihStudenata.Add(s);
                    }

                    if (s.PolozeniIspiti.Find(n => n.IdPredmeta == p.IdPredmet) != null)
                    {
                        p.SpisakPolozenihStudenata.Add(s);
                    }
                }
               

                foreach (Profesor pr in _profesori)
                {
                    if (pr.IdProfesor == p.IdProfesora)
                    {
                        p.Profesor = pr;
                    }
                }
            }

            _storage.Save(_predmeti);
            _profesorStorage.Save(_profesori);
        }

        public Predmet AddPredmet(Predmet predmet)
        {
            predmet.IdPredmet = GenerateId();
            MakePredmet();
            _predmeti.Add(predmet);
            
            _storage.Save(_predmeti);
            return predmet;
        }

        public Predmet? UpdatePredmet(Predmet predmet)
        {
            Predmet? oldPredmet = GetPredmetById(predmet.IdPredmet);
            if (oldPredmet == null) return null;

            oldPredmet.SifraPredmeta = predmet.SifraPredmeta;
            oldPredmet.NazivPredmeta = predmet.NazivPredmeta;
            oldPredmet.Semestar = predmet.Semestar;
            oldPredmet.GodinaStudija = predmet.GodinaStudija;
            oldPredmet.IdProfesora = predmet.IdProfesora;
            oldPredmet.BrojESPB = predmet.BrojESPB;

            MakePredmet();

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
            MakePredmet();
            return _predmeti.Find(p => p.IdPredmet == idPredmet);
        }

        public List<Predmet> GetAllPredmeti()
        {
            MakePredmet();
            return _predmeti;
        }

        public List<Predmet> GetAllPredmeti(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Predmet> predmeti = _predmeti;

            switch (sortCriteria)
            {
                case "Id":
                    predmeti = _predmeti.OrderBy(p => p.IdPredmet);
                    break;
                case "SifraPredmeta":
                    predmeti = _predmeti.OrderBy(p => p.SifraPredmeta);
                    break;
                case "NazivPredmeta":
                    predmeti = _predmeti.OrderBy(p => p.NazivPredmeta);
                    break;
                case "Semestar":
                    predmeti = _predmeti.OrderBy(p => p.Semestar);
                    break;
                case "GodinaStudija":
                    predmeti = _predmeti.OrderBy(p => p.GodinaStudija);
                    break;
                case "IdPredmetnogProfesora":
                    predmeti = _predmeti.OrderBy(p => p.IdProfesora);
                    break;
                case "BrojESPB":
                    predmeti = _predmeti.OrderBy(p => p.BrojESPB);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                predmeti = predmeti.Reverse();

            predmeti = predmeti.Skip((page - 1) * pageSize).Take(pageSize);

            return predmeti.ToList();
        }
    }
}
