using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;

namespace CLI.DAO
{
    class ProfesorDAO
    {
        private readonly List<Profesor> _profesori;
        private readonly Storage<Profesor> _storage;

        public ProfesorDAO()
        {
            _storage = new Storage<Profesor>("profesori.txt");
            _profesori = _storage.Load();
        }

        private int GenerateId()
        {
            if (_profesori.Count == 0) return 0;
            return _profesori[^1].idProfesor + 1;
        }

        public Profesor AddProfesor(Profesor profesor)
        {
            profesor.idProfesor = GenerateId();
            _profesori.Add(profesor);
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
