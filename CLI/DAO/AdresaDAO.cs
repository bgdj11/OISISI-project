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
            return _adrese[^1].IdAdrese + 1;
        }

        public int GetLastID()
        {
            return _adrese[^1].IdAdrese;
        }

        public Adresa AddAdresa(Adresa adresa)
        {
            adresa.IdAdrese = GenerateID();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? UpdateAdresa(Adresa adresa)
        {
            Adresa? oldAdresa = GetAdresaById(adresa.IdAdrese);
            if (oldAdresa == null) return null;

            oldAdresa.Ulica = adresa.Ulica;
            oldAdresa.Grad = adresa.Grad;
            oldAdresa.Broj = adresa.Broj;
            oldAdresa.Drzava = adresa.Drzava;

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
            return _adrese.Find(a => a.IdAdrese == id);
        }

        public List<Adresa> GetAllAdresa()
        {
            return _adrese;
        }

    }
}
