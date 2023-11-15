using System;
using System.Collections.Generic;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console
{
    class OcenaNaUpisuConsoleView
    {
        private readonly OcenaNaUpisuDAO _ocenaNaUpisuDao;

        public OcenaNaUpisuConsoleView(OcenaNaUpisuDAO ocenaNaUpisuDao)
        {
            _ocenaNaUpisuDao = ocenaNaUpisuDao;
        }

        private void PrintOceneNaUpisu(List<OcenaNaUpisu> oceneNaUpisu)
        {
            System.Console.WriteLine("Ocene na upisu: ");
            string header = $"ID {"",6} | Ocena {"",21} | Student {"",10} | Predmet {"",12} | Datum {"",12}";
            System.Console.WriteLine(header);
            foreach (OcenaNaUpisu ocenaNaUpisu in oceneNaUpisu)
            {
                System.Console.WriteLine(ocenaNaUpisu);
            }
        }

        private OcenaNaUpisu InputOcenaNaUpisu()
        {
            System.Console.WriteLine("Enter Student ID: ");
            int idStudenta = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter Predmet ID: ");
            int idPredmeta = ConsoleViewUtils.SafeInputInt();

            DateOnly datumpol;
            while (true)
            {
                System.Console.WriteLine("Unesite datum rođenja (yyyy-MM-dd): ");
                if (DateOnly.TryParse(System.Console.ReadLine(), out datumpol))
                {
                    break; // Break the loop if parsing is successful
                }
                else
                {
                    System.Console.WriteLine("Pogrešan format datuma. Ponovite unos.");
                }
            }

            System.Console.WriteLine("Enter Ocena (6-10): ");
            int ocena = ConsoleViewUtils.SafeInputInt();

            OcenaNaUpisu ocenaNaUpisu = new OcenaNaUpisu()
            {
                idStudenta = idStudenta,
                idPredmeta = idPredmeta,
                datum = datumpol,
                Ocena = ocena
            };

            return ocenaNaUpisu;
        }

        private int InputOcenaNaUpisuId()
        {
            System.Console.WriteLine("Enter OcenaNaUpisu ID: ");
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
            System.Console.WriteLine("1: Show All Ocene na upisu");
            System.Console.WriteLine("2: Add Ocena na upisu");
            System.Console.WriteLine("3: Update Ocena na upisu");
            System.Console.WriteLine("4: Remove Ocena na upisu");
            System.Console.WriteLine("0: Close");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllOceneNaUpisu();
                    break;
                case "2":
                    AddOcenaNaUpisu();
                    break;
                case "3":
                    UpdateOcenaNaUpisu();
                    break;
                case "4":
                    RemoveOcenaNaUpisu();
                    break;
            }
        }

        private void ShowAllOceneNaUpisu()
        {
            PrintOceneNaUpisu(_ocenaNaUpisuDao.GetAllOcena());
        }

        private void RemoveOcenaNaUpisu()
        {
            int id = InputOcenaNaUpisuId();
            OcenaNaUpisu removedOcenaNaUpisu = _ocenaNaUpisuDao.RemoveOcena(id);
            if (removedOcenaNaUpisu != null)
            {
                System.Console.WriteLine("Ocena na upisu removed");
            }
            else
            {
                System.Console.WriteLine("Ocena na upisu not found");
            }
        }

        private void UpdateOcenaNaUpisu()
        {
            int id = InputOcenaNaUpisuId();
            OcenaNaUpisu ocenaNaUpisu = InputOcenaNaUpisu();
            ocenaNaUpisu.idOcene = id;
            OcenaNaUpisu updatedOcenaNaUpisu = _ocenaNaUpisuDao.UpdateOcena(ocenaNaUpisu);
            if (updatedOcenaNaUpisu != null)
            {
                System.Console.WriteLine("Ocena na upisu updated");
            }
            else
            {
                System.Console.WriteLine("Ocena na upisu not found");
            }
        }

        private void AddOcenaNaUpisu()
        {
            OcenaNaUpisu ocenaNaUpisu = InputOcenaNaUpisu();
            _ocenaNaUpisuDao.AddOcena(ocenaNaUpisu);
            System.Console.WriteLine("Ocena na upisu added");
        }
    }
}
