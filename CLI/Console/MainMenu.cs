using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.DAO;
using CLI.Model;

namespace CLI.Console
{
    class MainMenu
    {
        public MainMenu() 
        {
            
        }

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string userInput = System.Console.ReadLine() ?? "0";
                if (userInput == "0") break;
                HandleMenu(userInput);
            }
        }
        private void ShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Studenti");
            System.Console.WriteLine("2: Predmeti");
            System.Console.WriteLine("3: Profesori");
            System.Console.WriteLine("4: Katedre");
            System.Console.WriteLine("5: Adrese");
            System.Console.WriteLine("6: Inedeksi");
            System.Console.WriteLine("7: Ocene");
            
            System.Console.WriteLine("0: Close");
        }

        private void HandleMenu(string input)
        {
            switch (input)
            {
                case "1":
                    StudentDAO student = new StudentDAO();
                    StudentConsoleView studentView = new StudentConsoleView(student);
                    studentView.RunMenu();
                    break;
                case "2":
                    PredmetDAO predmet = new PredmetDAO();
                    PredmetConsoleView predmetView = new PredmetConsoleView(predmet);
                    predmetView.RunMenu();
                    break;
                case "3":
                    ProfesorDAO profesor = new ProfesorDAO();
                    ProfesorConsoleView profesorView = new ProfesorConsoleView(profesor);
                    profesorView.RunMenu();
                    break;
                case "4":
                    KatedraDAO katedra = new KatedraDAO();
                    KatedraConsoleView katedraView = new KatedraConsoleView(katedra);
                    katedraView.RunMenu();
                    break;
                case "5":
                    AdresaDAO adresa = new AdresaDAO();
                    AdresaConsoleView adresaView = new AdresaConsoleView(adresa);
                    adresaView.RunMenu();
                    break;
                case "6":
                    IndeksDAO indeks = new IndeksDAO();
                    IndeksConsoleView indeksView = new IndeksConsoleView(indeks);
                    indeksView.RunMenu();
                    break;
                case "7":
                    OcenaNaUpisuDAO ocena = new OcenaNaUpisuDAO();
                    OcenaNaUpisuConsoleView ocenaView = new OcenaNaUpisuConsoleView(ocena);
                    ocenaView.RunMenu();
                    break;
                case "0":
                    break;
            }

        }
           

    }
}
