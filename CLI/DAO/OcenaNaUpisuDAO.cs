using CLI.Model;
using CLI.Storage;

using System.Collections.Generic;
using System.Linq;

namespace CLI.DAO
{
    internal class OcenaNaUpisuDAO
    {
        private readonly List<OcenaNaUpisu> _ocene;
        private readonly Storage<OcenaNaUpisu> _storage;

        public OcenaNaUpisuDAO()
        {
            _storage = new Storage<OcenaNaUpisu>("ocene.txt");
            _ocene = new List<OcenaNaUpisu>();
        }

        public int GenerateID()
        {
            if (_ocene.Count == 0) return 0;
            return _ocene[^1].idOcene + 1;
        }

        public OcenaNaUpisu AddOcena(OcenaNaUpisu ocena)
        {
            ocena.idOcene = GenerateID();
            _ocene.Add(ocena);
            _storage.Save(_ocene);
            return ocena;
        }

        public OcenaNaUpisu? UpdateOcena(OcenaNaUpisu ocena)
        {
            OcenaNaUpisu? oldOcena = GetOcenaById(ocena.idOcene);
            if (oldOcena == null) return null;

            oldOcena.Ocena = ocena.Ocena;
            oldOcena.idStudenta = ocena.idStudenta;
            oldOcena.idPredmeta = ocena.idPredmeta;
            oldOcena.datum = ocena.datum;

            _storage.Save(_ocene);
            return oldOcena;
        }

        public OcenaNaUpisu? RemoveOcena(int id)
        {
            OcenaNaUpisu? ocena = GetOcenaById(id);
            if (ocena == null) return null;

            _ocene.Remove(ocena);
            _storage.Save(_ocene);
            return ocena;
        }

        private OcenaNaUpisu? GetOcenaById(int id)
        {
            return _ocene.Find(o => o.idOcene == id);
        }

        public List<OcenaNaUpisu> GetAllOcena()
        {
            return _ocene;
        }

        public List<OcenaNaUpisu> GetAllOcena(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<OcenaNaUpisu> ocene = _ocene;

            switch (sortCriteria)
            {
                case "idOcene":
                    ocene = _ocene.OrderBy(x => x.idOcene);
                    break;
                case "ocena":
                    ocene = _ocene.OrderBy(x => x.Ocena);
                    break;
                case "idStudenta":
                    ocene = _ocene.OrderBy(x => x.idStudenta);
                    break;
                case "idPredmeta":
                    ocene = _ocene.OrderBy(x => x.idPredmeta);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                ocene = ocene.Reverse();
            }

            ocene = ocene.Skip((page - 1) * pageSize).Take(pageSize);

            return ocene.ToList();
        }
    }
}
