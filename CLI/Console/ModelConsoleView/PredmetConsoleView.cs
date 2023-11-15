using System;
using System.Collections.Generic;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console
{
    class PredmetConsoleView
    {
        private readonly PredmetDAO _predmetDao;

        public PredmetConsoleView(PredmetDAO predmetDao)
        {
            _predmetDao = predmetDao;
        }

        private void PrintPredmeti(List<Predmet> predmeti)
        {
            System.Console.WriteLine("Predmeti: ");

            foreach (Predmet predmet in predmeti)
            {
                System.Console.WriteLine(predmet.ToString());
            }
        }

        private Predmet InputPredmet()
        {
            System.Console.WriteLine("Unesi SifraPredmeta: ");
            string sifraPredmeta = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesi NazivPredmeta: ");
            string nazivPredmeta = System.Console.ReadLine() ?? string.Empty;



            Semestar semestar;

            while (true)
            {
                System.Console.WriteLine("Unesi Semestar (L ili Z): ");
                string input = System.Console.ReadLine();

                if (Enum.TryParse(input, out semestar))
                {
                    break; 
                }
                else
                {
                    System.Console.WriteLine("Pogrešan unos. Ponovite unos.");
                }
            }



            System.Console.WriteLine("Unesi godinu studija: ");
            int godinaStudija = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesi id profesora: ");
            int idPredmetnogProfesora = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesi BrojESPB: ");
            int brojESPB = ConsoleViewUtils.SafeInputInt();

            Predmet predmet = new Predmet(sifraPredmeta, nazivPredmeta, semestar, godinaStudija, idPredmetnogProfesora, brojESPB);

            return predmet;
        }

        private int InputPredmetId()
        {
            System.Console.WriteLine("Enter Predmet ID: ");
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
            System.Console.WriteLine("1: Show All Predmeti");
            System.Console.WriteLine("2: Add Predmet");
            System.Console.WriteLine("3: Update Predmet");
            System.Console.WriteLine("4: Remove Predmet");
            System.Console.WriteLine("0: Close");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllPredmeti();
                    break;
                case "2":
                    AddPredmet();
                    break;
                case "3":
                    UpdatePredmet();
                    break;
                case "4":
                    RemovePredmet();
                    break;
            }
        }

        private void ShowAllPredmeti()
        {
            PrintPredmeti(_predmetDao.GetAllPredmeti());
        }

        private void RemovePredmet()
        {
            int id = InputPredmetId();
            Predmet removedPredmet = _predmetDao.RemovePredmet(id);
            if (removedPredmet != null)
            {
                System.Console.WriteLine("Predmet removed");
            }
            else
            {
                System.Console.WriteLine("Predmet not found");
            }
        }

        private void UpdatePredmet()
        {
            int id = InputPredmetId();
            Predmet predmet = InputPredmet();
            predmet.idPredmet = id;
            Predmet updatedPredmet = _predmetDao.UpdatePredmet(predmet);
            if (updatedPredmet != null)
            {
                System.Console.WriteLine("Predmet updated");
            }
            else
            {
                System.Console.WriteLine("Predmet not found");
            }
        }

        private void AddPredmet()
        {
            Predmet predmet = InputPredmet();
            _predmetDao.AddPredmet(predmet);
            System.Console.WriteLine("Predmet added");
        }
    }
}
