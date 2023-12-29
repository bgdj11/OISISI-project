using CLI.DAO;
using CLI.Model;
using CLI.Console;

namespace CLI.Console
{
class ProfesorConsoleView
{
        private readonly ProfesorDAO _profesorsDao;
        private readonly AdresaDAO _adresa;
        public ProfesorConsoleView(ProfesorDAO profesorsDao, AdresaDAO adresa)
        {
            _profesorsDao = profesorsDao;
            _adresa = adresa;
        }

        private void PrintProfessors(List<Profesor> profesors)
        {
            System.Console.WriteLine("Profesori: ");


            foreach (Profesor profesor in profesors)
            {
                System.Console.WriteLine(profesor.ToString());
            }
        }

        private Profesor InputProfesor()
        {
            System.Console.WriteLine("Unesite prezime profesora: ");
            string prezime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite ime profesora: ");
            string ime = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite datum rođenja (yyyy-MM-dd): ");
            if (DateOnly.TryParse(System.Console.ReadLine(), out DateOnly datumRodjenja))
            {
                
            }


            System.Console.WriteLine("Unesite kontakt telefon: ");
            string kontaktTelefon = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite email adresu: ");
            string emailAdresa = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite broj lične karte: ");
            string brojLicneKarte = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite zvanje: ");
            string zvanje = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Unesite godine staza: ");
            int godineStaza = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Unesi id katedre: ");
            int idK = ConsoleViewUtils.SafeInputInt();

            
            AdresaConsoleView _adresaView = new AdresaConsoleView(_adresa);
            Adresa a = _adresaView.InputAdresa();
            _adresa.AddAdresa(a);

            int idAdrese = a.IdAdrese;


            return new Profesor(prezime, ime, datumRodjenja, idAdrese, kontaktTelefon, emailAdresa, brojLicneKarte, zvanje, godineStaza, idK, a);
        }



        private void ShowAllProfesors()
        {
            PrintProfessors(_profesorsDao.GetAllProfesors());
        }

        private int InputId()
        {
            System.Console.WriteLine("Unesite ID profesora: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }

        private void AddPredmetToProfesor()
        {
            int idProf = InputId();
            System.Console.WriteLine("Unesite ID predmeta: ");
            int idPredmeta = ConsoleViewUtils.SafeInputInt();

            ProfesorPredmetDAO pp = new ProfesorPredmetDAO();
            pp.AddPredmetToProfesor(idProf, idPredmeta);

        }

        private void RemoveProfesor()
        {
            int id = InputId();
            Profesor? removedProfesor = _profesorsDao.RemoveProfesor(id);
            if (removedProfesor is null)
            {
                System.Console.WriteLine("Profesor nije pronadjen");
                return;
            }

            System.Console.WriteLine("Profesor izbrisan");
        }

        private void UpdateProfesor()
        {
            int id = InputId();
            Profesor profesor = InputProfesor();
            profesor.IdProfesor = id;
            Profesor? updatedProfesor = _profesorsDao.UpdateProfesor(profesor);
            if (updatedProfesor is null)
            {
                System.Console.WriteLine("Profesor nije pronadjen");
                return;
            }

            System.Console.WriteLine("Profesor azuriran");
        }

        private void AddProfesor()
        {
            Profesor profesor = InputProfesor();
            _profesorsDao.AddProfesor(profesor);
            System.Console.WriteLine("Profesor dodat");
        }


        private void ShowAndSortProfesors()
        {
            System.Console.WriteLine("\nEnter page: ");
            int page = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("\nEnter page size: ");
            int pageSize = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("\nEnter sort criteria: ");
            string sortCriteria = System.Console.ReadLine() ?? string.Empty;
            System.Console.WriteLine("\nEnter 0 for ascending, any key for descending: ");
            int sortDirectionInput = ConsoleViewUtils.SafeInputInt();
            SortDirection sortDirection = sortDirectionInput == 0 ? SortDirection.Ascending : SortDirection.Descending;

            PrintProfessors(_profesorsDao.GetAllProfesors(page, pageSize, sortCriteria, sortDirection));
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
            System.Console.WriteLine("2: Dodaj profesora");
            System.Console.WriteLine("3: Azuriraj profesora");
            System.Console.WriteLine("4: Izbaci profesora");
            System.Console.WriteLine("5: Prikazi i sortiraj profesore");
            System.Console.WriteLine("6: Dodaj predmet profesoru");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllProfesors();
                    break;
                case "2":
                    AddProfesor();
                    break;
                case "3":
                    UpdateProfesor();
                    break;
                case "4":
                    RemoveProfesor();
                    break;
                case "5":
                    ShowAndSortProfesors();
                    break;
                case "6":
                    AddPredmetToProfesor();
                    break;
            }
        }


    }
}
