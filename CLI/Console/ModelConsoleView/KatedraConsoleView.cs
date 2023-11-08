using System;
using System.Collections.Generic;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console
{
    class KatedraConsoleView
    {
        private readonly KatedraDAO _katedraDao;

        public KatedraConsoleView(KatedraDAO katedraDao)
        {
            _katedraDao = katedraDao;
        }

        private void PrintKatedre(List<Katedra> katedre)
        {
            System.Console.WriteLine("Katedre: ");
            string header = $"ID {"",6} | SifraKatedre {"",12} | NazivKatedre {"",18} | SefKatedre {"",14} | ProfesoriNaKatedri";
            System.Console.WriteLine(header);
            foreach (Katedra katedra in katedre)
            {
                System.Console.WriteLine(katedra);
            }
        }

        private Katedra InputKatedra()
        {
            System.Console.WriteLine("Enter SifraKatedre: ");
            int sifraKatedre = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter NazivKatedre: ");
            string nazivKatedre = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Enter IdSefa: ");
            int idSefa = ConsoleViewUtils.SafeInputInt();

            Katedra katedra = new Katedra(sifraKatedre, nazivKatedre, idSefa);

            return katedra;
        }

        private int InputKatedraId()
        {
            System.Console.WriteLine("Enter Katedra ID: ");
            return ConsoleViewUtils.SafeInputInt();
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
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All Katedre");
            System.Console.WriteLine("2: Add Katedra");
            System.Console.WriteLine("3: Update Katedra");
            System.Console.WriteLine("4: Remove Katedra");
            System.Console.WriteLine("0: Close");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllKatedre();
                    break;
                case "2":
                    AddKatedra();
                    break;
                case "3":
                    UpdateKatedra();
                    break;
                case "4":
                    RemoveKatedra();
                    break;
            }
        }

        private void ShowAllKatedre()
        {
            PrintKatedre(_katedraDao.GetAllKatedra());
        }

        private void RemoveKatedra()
        {
            int id = InputKatedraId();
            Katedra removedKatedra = _katedraDao.RemoveKatedra(id);
            if (removedKatedra != null)
            {
                System.Console.WriteLine("Katedra removed");
            }
            else
            {
                System.Console.WriteLine("Katedra not found");
            }
        }

        private void UpdateKatedra()
        {
            int id = InputKatedraId();
            Katedra katedra = InputKatedra();
            katedra.idKatedre = id;
            Katedra updatedKatedra = _katedraDao.UpdateKatedra(katedra);
            if (updatedKatedra != null)
            {
                System.Console.WriteLine("Katedra updated");
            }
            else
            {
                System.Console.WriteLine("Katedra not found");
            }
        }

        private void AddKatedra()
        {
            Katedra katedra = InputKatedra();
            _katedraDao.AddKatedra(katedra);
            System.Console.WriteLine("Katedra added");
        }
    }
}
