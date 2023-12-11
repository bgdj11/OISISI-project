using System;
using System.Collections.Generic;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console
{
    class IndeksConsoleView
    {
        private readonly IndeksDAO _indeksDao;

        public IndeksConsoleView(IndeksDAO indeksDao)
        {
            _indeksDao = indeksDao;
        }

        private void PrintIndeksi(List<Indeks> indeksi)
        {
            System.Console.WriteLine("Indeksi: ");
            string header = $"ID {"",6} |  Oznaka Smera {"",21} | Broj Upisa {"",10} | Godina Upisa {"",12}";
            System.Console.WriteLine(header);
            foreach (Indeks indeks in indeksi)
            {
                System.Console.WriteLine(indeks);
            }
        }

        public Indeks InputIndeks()
        {
            System.Console.WriteLine("Unesite oznaku smera: ");
            string oznakaSmera = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite broj upisa: ");
            if (int.TryParse(System.Console.ReadLine(), out int brojUpisa))
            {
            }

            System.Console.WriteLine("Unesite godinu upisa: ");
            if (int.TryParse(System.Console.ReadLine(), out int godinaUpisa))
            {
            }

            return new Indeks(oznakaSmera, brojUpisa, godinaUpisa);
        }

        private int InputIndeksId()
        {
            System.Console.WriteLine("Unesi id indeksa: ");
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
            System.Console.WriteLine("\nIzaberite opciju: ");
            System.Console.WriteLine("1: Prikazi sve indekse");
            System.Console.WriteLine("2: Dodaj Indeks");
            System.Console.WriteLine("3: Azuriraj Indeks");
            System.Console.WriteLine("4: Izbaci Indeks");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllIndeksi();
                    break;
                case "2":
                    AddIndeks();
                    break;
                case "3":
                    UpdateIndeks();
                    break;
                case "4":
                    RemoveIndeks();
                    break;
            }
        }

        private void ShowAllIndeksi()
        {
            PrintIndeksi(_indeksDao.GetAllIndeks());
        }

        private void RemoveIndeks()
        {
            int id = InputIndeksId();
            Indeks removedIndeks = _indeksDao.RemoveIndeks(id);
            if (removedIndeks != null)
            {
                System.Console.WriteLine("Indeks izbrisan");
            }
            else
            {
                System.Console.WriteLine("Indeks nije pronadjen");
            }
        }

        private void UpdateIndeks()
        {
            int id = InputIndeksId();
            Indeks indeks = InputIndeks();
            indeks.idIndeksa = id;
            Indeks updatedIndeks = _indeksDao.UpdateIndeks(indeks);
            if (updatedIndeks != null)
            {
                System.Console.WriteLine("Indeks azuriran");
            }
            else
            {
                System.Console.WriteLine("Indeks nije pronadjen");
            }
        }

        private void AddIndeks()
        {
            Indeks indeks = InputIndeks();
            _indeksDao.AddIndeks(indeks);
            System.Console.WriteLine("Indeks dodat");
        }
    }
}
