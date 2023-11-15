using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;
using CLI.Serialization;
using CLI.DAO;

namespace CLI.DAO
{
    class ProfesorDAO
    {
        private readonly List<Profesor> _profesori;
        private readonly Storage<Profesor> _storage;

        public ProfesorDAO()
        {
            _storage = new Storage<Profesor>("profesori.csv");
            _profesori = _storage.Load();
            MakeProfesor();
        }

        private int GenerateId()
        {
            if (_profesori.Count == 0) return 0;
            return _profesori[^1].idProfesor + 1;
        }

        private void MakeProfesor()
        {
            Storage<Adresa> _adresaStorage = new Storage<Adresa>("adrese.csv");
            List<Adresa> _adrese = _adresaStorage.Load();

            Storage<Katedra> _katedreStorage = new Storage<Katedra>("katedre.csv");
            List<Katedra> _katedre = _katedreStorage.Load();

            Storage<Predmet> _predmetiStorage = new Storage<Predmet>("predmeti.csv");
            List<Predmet> _predmeti = _predmetiStorage.Load();

            Storage<ProfesorPredmet> _profPredStorage = new Storage<ProfesorPredmet>("ProfesorPredmet.csv");
            List<ProfesorPredmet> _profPred = _profPredStorage.Load();

            // dodajemo u katedru odgovarajuceg profesora

            foreach (Profesor p in _profesori)
            {
                if(_katedre.Find(n => n.idKatedre == p.IdKatedre) != null)
                {
                    _katedre.Find(n => n.idKatedre == p.IdKatedre).profesoriNaKatedri.Add(p);
                }
            }

            // dodajemo adresu stanovanja

            foreach(Profesor p in _profesori)
            {
                if(_adrese.Find(n => n.idAdrese == p.idAdrese) != null)
                {
                    p.adresaStanovanja = _adrese.Find(n => n.idAdrese == p.idAdrese);
                }
            }

            // dodajemo predmete u listu predmeta kod profesora

            foreach (Profesor p in _profesori)
            {
                if(_predmeti.Find(n => n.idProfesora == p.idProfesor) != null)
                {
                    p.spisakPredmeta.Add(_predmeti.Find(n => n.idPredmet == p.idProfesor));
                }
            }

            

            _katedreStorage.Save(_katedre);
            _storage.Save(_profesori);
        }

        public Profesor AddProfesor(Profesor profesor)
        {
            profesor.idProfesor = GenerateId();
            
            _profesori.Add(profesor);
            MakeProfesor();
            _storage.Save(_profesori);
            return profesor;
        }

        public Profesor? UpdateProfesor(Profesor profesor)
        {
            Profesor? oldProfesor = GetProfesorById(profesor.idProfesor);
            if (oldProfesor == null) return null;

            oldProfesor.prezime = profesor.prezime;
            oldProfesor.ime = profesor.ime;
            oldProfesor.datumRodjenja = profesor.datumRodjenja;
            oldProfesor.idAdrese = profesor.idAdrese;
            oldProfesor.kontaktTelefon = profesor.kontaktTelefon;
            oldProfesor.emailAdresa = profesor.emailAdresa;
            oldProfesor.brojLicneKarte = profesor.brojLicneKarte;
            oldProfesor.zvanje = profesor.zvanje;
            oldProfesor.godineStaza = profesor.godineStaza;

            MakeProfesor();

            _storage.Save(_profesori);
            return oldProfesor;
        }

        public Profesor? RemoveProfesor(int id)
        {
            Profesor? profesor = GetProfesorById(id);
            if (profesor == null) return null;

            _profesori.Remove(profesor);
            _storage.Save(_profesori);
            return profesor;
        }

        private Profesor? GetProfesorById(int idProfesor)
        {
            return _profesori.Find(p => p.idProfesor == idProfesor);
        }

        public List<Profesor> GetAllProfesors()
        {
            return _profesori;
        }

        public List<Profesor> GetAllProfesors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Profesor> profesori = _profesori;

            switch (sortCriteria)
            {
                case "Id":
                    profesori = _profesori.OrderBy(p => p.idProfesor);
                    break;
                case "Name":
                    profesori = _profesori.OrderBy(p => p.ime);
                    break;
                case "DatumRodjenja":
                    profesori = _profesori.OrderBy(p => p.datumRodjenja);
                    break;
                case "KontaktTelefon":
                    profesori = _profesori.OrderBy(p => p.kontaktTelefon);
                    break;
                case "EmailAdresa":
                    profesori = _profesori.OrderBy(p => p.emailAdresa);
                    break;
                case "BrojLicneKarte":
                    profesori = _profesori.OrderBy(p => p.brojLicneKarte);
                    break;
                case "Zvanje":
                    profesori = _profesori.OrderBy(p => p.zvanje);
                    break;
                case "GodineStaza":
                    profesori = _profesori.OrderBy(p => p.godineStaza);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                profesori = profesori.Reverse();

            profesori = profesori.Skip((page - 1) * pageSize).Take(pageSize);

            return profesori.ToList();
        }
    }
}
