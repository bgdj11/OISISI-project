using CLI.Model;
using CLI.Storage;

using System.Collections.Generic;
using System.Linq;

namespace CLI.DAO
{
    public class OcenaNaUpisuDAO
    {
        private readonly List<OcenaNaUpisu> _ocene;
        private readonly Storage<OcenaNaUpisu> _storage;

        public OcenaNaUpisuDAO()
        {
            _storage = new Storage<OcenaNaUpisu>("ocene.csv");
            _ocene = _storage.Load();
        }

        public int GenerateID()
        {
            if (_ocene.Count == 0) return 0;
            return _ocene[^1].IdOcene + 1;
        }

        public void MakeOcena()
        {
            Storage<Student> _studentStorage = new Storage<Student>("studenti.csv");
            List<Student> _studenti = _studentStorage.Load();

            Storage<Predmet> _predmetStorage = new Storage<Predmet>("predmeti.csv");
            List<Predmet> _predmeti = _predmetStorage.Load();

            foreach(OcenaNaUpisu o in _ocene)
            {
                Student s = _studenti.Find(n => n.IdStudent == o.IdStudenta);
                o.Student = s;

                if(o.IdStudenta == s.IdStudent)
                {
                    s.PolozeniIspiti.Add(o);
                }

                Predmet p = _predmeti.Find(n => n.IdPredmet == o.IdPredmeta);
                o.Predmet = p;
            }

            _storage.Save(_ocene);
            _studentStorage.Save(_studenti);

        }

        public OcenaNaUpisu AddOcena(OcenaNaUpisu ocena)
        {
            ocena.IdOcene = GenerateID();
            _ocene.Add(ocena);
            MakeOcena();
            _storage.Save(_ocene);
            return ocena;
        }

        public OcenaNaUpisu? UpdateOcena(OcenaNaUpisu ocena)
        {
            OcenaNaUpisu? oldOcena = GetOcenaById(ocena.IdOcene);
            if (oldOcena == null) return null;

            oldOcena.Ocena = ocena.Ocena;
            oldOcena.IdStudenta = ocena.IdStudenta;
            oldOcena.IdPredmeta = ocena.IdPredmeta;
            oldOcena.Datum = ocena.Datum;

            MakeOcena();

            _storage.Save(_ocene);
            return oldOcena;
        }

        public OcenaNaUpisu? RemoveOcena(int id)
        {
            OcenaNaUpisu? ocena = GetOcenaById(id);
            if (ocena == null) return null;

            _ocene.Remove(ocena);
            MakeOcena();
            _storage.Save(_ocene);
            return ocena;
        }

        public OcenaNaUpisu? GetOcenaById(int id)
        {
            MakeOcena();
            return _ocene.Find(o => o.IdOcene == id);
        }

        public List<OcenaNaUpisu> GetAllOcena()
        {
            return _ocene;
        }
    }
}
