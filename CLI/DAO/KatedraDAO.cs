using CLI.Model;
using CLI.Storage;

using System.Collections.Generic;
using System.Linq;

namespace CLI.DAO
{
    internal class KatedraDAO
    {
        private readonly List<Katedra> _katedre;
        private readonly Storage<Katedra> _storage;

        public KatedraDAO()
        {
            _storage = new Storage<Katedra>("katedre.csv");
            _katedre = new List<Katedra>();
        }

        public int GenerateID()
        {
            if (_katedre.Count == 0) return 0;
            return _katedre[^1].idKatedre + 1;
        }

        private void MakeKatedra()
        {
            Storage<Profesor> _profesorStorage = new Storage<Profesor>("profesori.csv");
            List<Profesor> _profesori = _profesorStorage.Load();

            foreach (Katedra k in _katedre)
            {
                foreach(Profesor p in _profesori)
                {
                    if(p.IdKatedre == k.idKatedre)
                    {
                        k.profesoriNaKatedri.Add(p);
                    }
                }
            }

            _storage.Save(_katedre);
        }

        public Katedra AddKatedra(Katedra katedra)
        {
            katedra.idKatedre = GenerateID();
            _katedre.Add(katedra);
            MakeKatedra();
            _storage.Save(_katedre);
            return katedra;
        }

        public Katedra? UpdateKatedra(Katedra katedra)
        {
            Katedra? oldKatedra = GetKatedraById(katedra.idKatedre);
            if (oldKatedra == null) return null;

            oldKatedra.sifraKatedre = katedra.sifraKatedre;
            oldKatedra.nazivKatedre = katedra.nazivKatedre;
            oldKatedra.idSefa = katedra.idSefa;

            MakeKatedra();

            _storage.Save(_katedre);
            return oldKatedra;
        }

        public Katedra? RemoveKatedra(int id)
        {
            Katedra? katedra = GetKatedraById(id);
            if (katedra == null) return null;

            _katedre.Remove(katedra);
            MakeKatedra();
            _storage.Save(_katedre);
            return katedra;
        }

        private Katedra? GetKatedraById(int id)
        {
            return _katedre.Find(k => k.idKatedre == id);
        }

        public List<Katedra> GetAllKatedra() 
        {
            return _katedre;
        }


        public List<Katedra> GetAllKatedra(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Katedra> katedre = _katedre;

            switch (sortCriteria)
            {
                case "idKatedre":
                    katedre = _katedre.OrderBy(x => x.idKatedre);
                    break;
                case "sifraKatedre":
                    katedre = _katedre.OrderBy(x => x.sifraKatedre);
                    break;
                case "nazivKatedre":
                    katedre = _katedre.OrderBy(x => x.nazivKatedre);
                    break;
                case "idSefa":
                    katedre = _katedre.OrderBy(x => x.idSefa);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                katedre = katedre.Reverse();
            }

            katedre = katedre.Skip((page - 1) * pageSize).Take(pageSize);

            return katedre.ToList();
        }
    }
}
