using CLI.Model;
using CLI.Storage;
using System.Runtime.CompilerServices;

namespace CLI.DAO
{
    public class AdresaDAO
    {

        private readonly List<Adresa> _adrese;
        private readonly Storage<Adresa> _storage;

        public AdresaDAO()
        {
            _storage = new Storage<Adresa>("adrese.csv");
            _adrese = _storage.Load();

        }

        public int GenerateID()
        {
            if(_adrese.Count == 0) return 0;
            return _adrese[^1].idAdrese + 1;
        }

        public int GetLastID()
        {
            return _adrese[^1].idAdrese;
        }

        public Adresa AddAdresa(Adresa adresa)
        {
            adresa.idAdrese = GenerateID();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? UpdateAdresa(Adresa adresa)
        {
            Adresa? oldAdresa = GetAdresaById(adresa.idAdrese);
            if (oldAdresa == null) return null;

            oldAdresa.ulica = adresa.ulica;
            oldAdresa.grad = adresa.grad;
            oldAdresa.broj = adresa.broj;
            oldAdresa.drzava = adresa.drzava;

            _storage.Save(_adrese);
            return oldAdresa;
        }

        public Adresa? RemoveAdresa(int id)
        {
            Adresa? adresa = GetAdresaById(id);
            if (adresa == null) return null;

            _adrese.Remove(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        private Adresa? GetAdresaById(int id)
        {
            return _adrese.Find(a => a.idAdrese == id);
        }

        public List<Adresa> GetAllAdresa()
        {
            return _adrese;
        }

        public List<Adresa> GetAllAdresa(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Adresa> adrese = _adrese;

            switch (sortCriteria)
            {
                case "idAdrese":
                    adrese = _adrese.OrderBy(x => x.idAdrese);
                    break;
                case "grad":
                    adrese = _adrese.OrderBy(x => x.grad);
                    break;
                case "ulica":
                    adrese = _adrese.OrderBy(x => x.ulica);
                    break;
                case "broj":
                    adrese = _adrese.OrderBy(x => x.broj);
                    break;
                case "drzava":
                    adrese = _adrese.OrderBy(x => x.drzava);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
            {
                adrese = adrese.Reverse();
            }

            adrese = adrese.Skip((page - 1) * pageSize).Take(pageSize);

            return adrese.ToList();
        }

    }
}
