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
            System.Console.WriteLine("Unesite sifru predmeta: ");
            string sifraPredmeta = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite naziv predmeta: ");
            string nazivPredmeta = System.Console.ReadLine() ?? string.Empty;



            Semestar semestar;

            while (true)
            {
                System.Console.WriteLine("Unesite Semestar (L ili Z): ");
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



            System.Console.WriteLine("Unesite godinu studija: ");
            int godinaStudija = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesite id profesora: ");
            int idPredmetnogProfesora = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesite BrojESPB: ");
            int brojESPB = ConsoleViewUtils.SafeInputInt();

            Predmet predmet = new Predmet(sifraPredmeta, nazivPredmeta, semestar, godinaStudija, idPredmetnogProfesora, brojESPB);

            return predmet;
        }

        private int InputPredmetId()
        {
            System.Console.WriteLine("Unesite ID predmeta: ");
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
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikazi sve predmete");
            System.Console.WriteLine("2: Dodaj predmet");
            System.Console.WriteLine("3: Azuriraj predmet");
            System.Console.WriteLine("4: Izbaci predmet");
            System.Console.WriteLine("0: Zatvori");
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
                System.Console.WriteLine("Predmet izb risan");
            }
            else
            {
                System.Console.WriteLine("Predmet nije pronadjen");
            }
        }

        private void UpdatePredmet()
        {
            int id = InputPredmetId();
            Predmet predmet = InputPredmet();
            predmet.IdPredmet = id;
            Predmet updatedPredmet = _predmetDao.UpdatePredmet(predmet);
            if (updatedPredmet != null)
            {
                System.Console.WriteLine("Predmet azuriran");
            }
            else
            {
                System.Console.WriteLine("Predmet nije pronadjen");
            }
        }

        private void AddPredmet()
        {
            Predmet predmet = InputPredmet();
            _predmetDao.AddPredmet(predmet);
            System.Console.WriteLine("Predmet dodat");
        }
    }
}
