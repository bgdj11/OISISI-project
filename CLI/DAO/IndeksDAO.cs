using CLI.Model;
using CLI.Storage;

using System.Collections.Generic;
using System.Linq;

namespace CLI.DAO
{
    internal class IndeksDAO
    {
        private readonly List<Indeks> _indeksi;
        private readonly Storage<Indeks> _storage;

        public IndeksDAO()
        {
            _storage = new Storage<Indeks>("indeksi.txt");
            _indeksi = new List<Indeks>();
        }

        public int GenerateID()
        {
            if (_indeksi.Count == 0) return 0;
            return _indeksi[^1].idIndeksa + 1;
        }

        public Indeks AddIndeks(Indeks indeks)
        {
            indeks.idIndeksa = GenerateID();
            _indeksi.Add(indeks);
            _storage.Save(_indeksi);
            return indeks;
        }

        public Indeks? UpdateIndeks(Indeks indeks)
        {
            Indeks? oldIndeks = GetIndeksById(indeks.idIndeksa);
            if (oldIndeks == null) return null;

            oldIndeks.oznakaSmera = indeks.oznakaSmera;
            oldIndeks.brojUpisa = indeks.brojUpisa;
            oldIndeks.godinaUpisa = indeks.godinaUpisa;

            _storage.Save(_indeksi);
            return oldIndeks;
        }

        public Indeks? RemoveIndeks(int id)
        {
            Indeks? indeks = GetIndeksById(id);
            if (indeks == null) return null;

            _indeksi.Remove(indeks);
            _storage.Save(_indeksi);
            return indeks;
        }

        private Indeks? GetIndeksById(int id)
        {
            return _indeksi.Find(i => i.idIndeksa == id);
        }

        public List<Indeks> GetAllIndeksi(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Indeks> indeksi = _indeksi;

            switch (sortCriteria)
            {
                case "idIndeksa":
                    indeksi = _indeksi.OrderBy(x => x.idIndeksa);
                    break;
                case "oznakaSmera":
                    indeksi = _indeksi.OrderBy(x => x.oznakaSmera);
                    break;
                case "brojUpisa":
                    indeksi = _indeksi.OrderBy(x => x.brojUpisa);
                    break;
                case "godinaUpisa":
                    indeksi = _indeksi.OrderBy(x => x.godinaUpisa);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                indeksi = indeksi.Reverse();
            }

            indeksi = indeksi.Skip((page - 1) * pageSize).Take(pageSize);

            return indeksi.ToList();
        }
    }
}
