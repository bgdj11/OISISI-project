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
    public class ProfesorDAO
    {
        private  List<Profesor> _profesori;
        private readonly AdresaDAO adresaDAO = new AdresaDAO();
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
            return _profesori[^1].IdProfesor + 1;
        }

        public void MakeProfesor()
        {
            Storage<Adresa> _adresaStorage = new Storage<Adresa>("adrese.csv");
            List<Adresa> _adrese = _adresaStorage.Load();

            Storage<Katedra> _katedreStorage = new Storage<Katedra>("katedre.csv");
            List<Katedra> _katedre = _katedreStorage.Load();

            Storage<Predmet> _predmetiStorage = new Storage<Predmet>("predmeti.csv");
            List<Predmet> _predmeti = _predmetiStorage.Load();

            Storage<ProfesorPredmet> _profPredStorage = new Storage<ProfesorPredmet>("ProfesorPredmet.csv");
            List<ProfesorPredmet> _profPred = _profPredStorage.Load();
            

            // dodajemo adresu stanovanja

            foreach(Profesor p in _profesori)
            {
                if(_adrese.Find(n => n.IdAdrese == p.IdAdrese) != null)
                {
                    p.AdresaStanovanja = _adrese.Find(n => n.IdAdrese == p.IdAdrese);
                }
            }

            // dodajemo predmete u listu predmeta kod profesora

            foreach (Profesor p in _profesori)
            {
                p.SpisakPredmeta.Clear();
                foreach (Predmet predmet in _predmeti)
                {
                    if (predmet.IdProfesora == p.IdProfesor && !p.SpisakPredmeta.Contains(predmet))
                    {
                        p.SpisakPredmeta.Add(predmet);
                    }
                }
            }

            _storage.Save(_profesori);
        }

        public Profesor AddProfesor(Profesor profesor)
        {
            profesor.IdProfesor = GenerateId();
            adresaDAO.AddAdresa(profesor.AdresaStanovanja);
            profesor.IdAdrese = adresaDAO.GetLastID();
            _profesori.Add(profesor);
            MakeProfesor();
            _storage.Save(_profesori);
            return profesor;
        }

        public Profesor? UpdateProfesor(Profesor profesor)
        {
            MakeProfesor();

            Profesor? oldProfesor = GetProfesorById(profesor.IdProfesor);
            if (oldProfesor == null) return null;

            oldProfesor.Prezime = profesor.Prezime;
            oldProfesor.Ime = profesor.Ime;
            oldProfesor.DatumRodjenja = profesor.DatumRodjenja;
            oldProfesor.IdAdrese = profesor.IdAdrese;
            adresaDAO.UpdateAdresa(profesor.AdresaStanovanja);
            oldProfesor.KontaktTelefon = profesor.KontaktTelefon;
            oldProfesor.EmailAdresa = profesor.EmailAdresa;
            oldProfesor.BrojLicneKarte = profesor.BrojLicneKarte;
            oldProfesor.Zvanje = profesor.Zvanje;
            oldProfesor.GodineStaza = profesor.GodineStaza;
            oldProfesor.IdKatedre = profesor.IdKatedre; 

            _storage.Save(_profesori);
            return oldProfesor;
        }

        public Profesor? RemoveProfesor(int id)
        {
            Profesor? profesor = GetProfesorById(id);
            if (profesor == null) return null;

            adresaDAO.RemoveAdresa(profesor.IdProfesor);

            _profesori.Remove(profesor);
            _storage.Save(_profesori);
            return profesor;
        }

        public Profesor? GetProfesorById(int idProfesor)
        {
            MakeProfesor();
            return _profesori.Find(p => p.IdProfesor == idProfesor);
        }

        public List<Profesor> GetAllProfesors()
        {
            MakeProfesor();
            return _profesori;
        }

        public List<Profesor> GetAllProfesors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Profesor> profesori = _profesori;

            switch (sortCriteria)
            {
                case "Id":
                    profesori = _profesori.OrderBy(p => p.IdProfesor);
                    break;
                case "Name":
                    profesori = _profesori.OrderBy(p => p.Ime);
                    break;
                case "DatumRodjenja":
                    profesori = _profesori.OrderBy(p => p.DatumRodjenja);
                    break;
                case "KontaktTelefon":
                    profesori = _profesori.OrderBy(p => p.KontaktTelefon);
                    break;
                case "EmailAdresa":
                    profesori = _profesori.OrderBy(p => p.EmailAdresa);
                    break;
                case "BrojLicneKarte":
                    profesori = _profesori.OrderBy(p => p.BrojLicneKarte);
                    break;
                case "Zvanje":
                    profesori = _profesori.OrderBy(p => p.Zvanje);
                    break;
                case "GodineStaza":
                    profesori = _profesori.OrderBy(p => p.GodineStaza);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                profesori = profesori.Reverse();

            profesori = profesori.Skip((page - 1) * pageSize).Take(pageSize);

            return profesori.ToList();
        }
    }
}
