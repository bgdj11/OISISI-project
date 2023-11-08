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
            string header = $"ID {"",6} | SifraPredmeta {"",12} | NazivPredmeta {"",18} | Semestar {"",12} | GodinaStudija {"",15} | PredmetniProfesor {"",20} | BrojESPB {"",8}";
            System.Console.WriteLine(header);
            foreach (Predmet predmet in predmeti)
            {
                System.Console.WriteLine(predmet);
            }
        }

        private Predmet InputPredmet()
        {
            System.Console.WriteLine("Enter SifraPredmeta: ");
            string sifraPredmeta = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Enter NazivPredmeta: ");
            string nazivPredmeta = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Enter Semestar (L or Z): ");
            if (Enum.TryParse(System.Console.ReadLine(), out Semestar semestar))
            {
                throw new Exception("Invalid input");
            }

            System.Console.WriteLine("Enter GodinaStudija: ");
            int godinaStudija = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter PredmetniProfesor ID: ");
            int idPredmetnogProfesora = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter BrojESPB: ");
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
