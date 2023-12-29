using System;
using System.Collections.Generic;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console
{
    class AdresaConsoleView
    {
        private readonly AdresaDAO _adresaDao;

        public AdresaConsoleView(AdresaDAO adresaDao)
        {
            _adresaDao = adresaDao;
        }

        private void PrintAdrese(List<Adresa> adrese)
        {
            System.Console.WriteLine("Adrese: ");
            string header = $"ID {"",6} |  Ulica {"",21} | Broj {"",10} | Grad {"",12} | Drzava {"",12}";
            System.Console.WriteLine(header);
            foreach (Adresa adresa in adrese)
            {
                System.Console.WriteLine(adresa);
            }
        }

        public Adresa InputAdresa()
        {
            System.Console.WriteLine("Unesite ulicu: ");
            string ulica = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite broj: ");
            if (int.TryParse(System.Console.ReadLine(), out int broj))
            {
            }

            System.Console.WriteLine("Unesite grad: ");
            string grad = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite drzavu: ");
            string drzava = System.Console.ReadLine() ?? string.Empty;

            return new Adresa(ulica, broj, grad, drzava);
        }

        private int InputAdresaId()
        {
            System.Console.WriteLine("Unesite id adrese: ");
            if (int.TryParse(System.Console.ReadLine(), out int id))
            {
                return id;
            }
            return 0;
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string userInput = System.Console.ReadLine() ?? "0";
                if (userInput == "0") break;
                HandleMenuInput(userInput);
            }
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikazi sve adrese");
            System.Console.WriteLine("2: Dodaj adresu");
            System.Console.WriteLine("3: Azuriraj adresu");
            System.Console.WriteLine("4: Izbaci Adresu");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllAdrese();
                    break;
                case "2":
                    AddAdresa();
                    break;
                case "3":
                    UpdateAdresa();
                    break;
                case "4":
                    RemoveAdresa();
                    break;
            }
        }

        private void ShowAllAdrese()
        {
            PrintAdrese(_adresaDao.GetAllAdresa());
        }

        private void RemoveAdresa()
        {
            int id = InputAdresaId();
            Adresa removedAdresa = _adresaDao.RemoveAdresa(id);
            if (removedAdresa != null)
            {
                System.Console.WriteLine("Adresa ");
            }
            else
            {
                System.Console.WriteLine("Adresa nije pronadjena");
            }
        }

        private void UpdateAdresa()
        {
            int id = InputAdresaId();
            Adresa adresa = InputAdresa();
            adresa.IdAdrese = id;
            Adresa updatedAdresa = _adresaDao.UpdateAdresa(adresa);
            if (updatedAdresa != null)
            {
                System.Console.WriteLine("Adresa azurirana");
            }
            else
            {
                System.Console.WriteLine("Adresa nije pronadjena");
            }
        }

        private void AddAdresa()
        {
            Adresa adresa = InputAdresa();
            _adresaDao.AddAdresa(adresa);
            System.Console.WriteLine("Adresa dodata");
        }
    }
}
