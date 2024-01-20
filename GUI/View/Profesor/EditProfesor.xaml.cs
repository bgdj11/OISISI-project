using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using GUI.View.DialogWindows;
using GUI.View.Student;

namespace GUI.View.Profesor
{
    /// <summary>
    /// Interaction logic for EditProfesor.xaml
    /// </summary>
    public partial class EditProfesor : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ProfesorDTO Profesor { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        //private ProfesorDAO profesorDAO;
        private ProfesorController profesorController;
        private PredmetController predmetController;
        private StudentController studentController;
        //private PredmetDAO predmetDAO;
        //private StudentDAO studentDAO;

        public ObservableCollection<PredmetDTO> Predmeti { get; set; }
        public ObservableCollection<StudentPredmetDTO> Studenti { get; set; }



        public EditProfesor(ProfesorController pc, PredmetController p, ProfesorDTO selectedProfesor)
        {
            InitializeComponent();
            DataContext = this;
            this.profesorController = pc;
            this.predmetController = p;
            Profesor = selectedProfesor;
            cmbZvanje.ItemsSource = new List<string>() { "redovni profesor", "vanredni profesor", "docent" };

            Predmeti = new ObservableCollection<PredmetDTO>();
            Studenti = new ObservableCollection<StudentPredmetDTO>();

            studentController = new StudentController();
            SelectedPredmet = new PredmetDTO();

            Update();
        }


        public void Update()
        {
            studentController.MakeStudent();
            profesorController.MakeProfesor();
            predmetController.MakePredmet();


            if (Profesor.PredmetiListaId != null)
            {
                Predmeti.Clear();
                foreach (int i in Profesor.PredmetiListaId)
                {
                    Predmeti.Add(new PredmetDTO(predmetController.GetPredmetById(i)));
                }
            }

            foreach (CLI.Model.Student student in studentController.GetAllStudents())
            {
/*                foreach (CLI.Model.Predmet predmet in predmetDAO.GetAllPredmeti())
                {
                    if (predmet.IdProfesora == Profesor.IdProfesor &&
                        student.NepolozeniIspiti.Contains(predmet))
                    {
                        Studenti.Add(new StudentPredmetDTO(student, predmet));
                    }
                }*/
                foreach(CLI.Model.Predmet predmet in student.NepolozeniIspiti)
                {
                    if (predmet.IdProfesora == Profesor.IdProfesor)
                    {
                        Studenti.Add(new StudentPredmetDTO(student, predmet));
                    }
                }
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Profesor pr = Profesor.toProfesor();
                pr.IdProfesor = Profesor.IdProfesor;
                pr.IdAdrese = Profesor.idAdrese;
                pr.AdresaStanovanja.IdAdrese = Profesor.idAdrese;

                profesorController.UpdateProfesor(pr);
                MessageBox.Show("Profesor je uspesno promenjen!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
               // MessageBox.Show("Popunite sva polja pre potvrde", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Update();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            var validations = new (TextBox textBox, string message, Func<string, bool> validator)[]
            {
                (txtBoxIme, "Unesite validno ime.", s => s.All(char.IsLetter)),
                (txtBoxPrezime, "Unesite validno prezime.", s => s.All(char.IsLetter)),
                (dpDatumRodjenja, "Unesite validan datum rodjenja u formatu d.M.yyyy.", s => DateTime.TryParseExact(s, "d.M.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)),
                (txtBoxUlica, "Unesite validnu adresu ulice.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxBroj, "Unesite validan broj.", s => s.All(char.IsDigit)),
                (txtBoxGrad, "Unesite validan grad.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxDrzava, "Unesite validnu drzavu (samo slova).", s => s.All(char.IsLetter)),
                (txtBoxKontakt, "Unesite validni kontakt telefon.", s => s.All(char.IsDigit)),
                (txtBoxEmail, "Unesite validnu email adresu.", s => s.Contains("@")),
                (txtBoxBrojLicneKarte, "Unesite validan broj licne karte.", s => s.All(char.IsDigit)),
                (txtBoxGodinaStaza, "Unesite validnu godinu staza.", s => s.All(char.IsDigit))
            };

            foreach (var validation in validations)
            {
                if (string.IsNullOrWhiteSpace(validation.textBox.Text) || !validation.validator(validation.textBox.Text))
                {
                    MessageBox.Show(validation.message);
                    return false;
                }
            }

            if (cmbZvanje.SelectedItem == null)
            {
                MessageBox.Show("Izaberite zvanje.");
                return false;
            }

            return true;
        }




        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DodajPredmet_Click(object sender, RoutedEventArgs e)
        {
            var selectSubjectProfesorWindow = new SelectSubjectProfesor(profesorController, predmetController, Profesor);
            selectSubjectProfesorWindow.Owner = this;
            selectSubjectProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectSubjectProfesorWindow.ShowDialog();
            Update();
        }

        private void UkloniPredmet_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet == null)
            {
                MessageBox.Show(this, "Izaberite predmet za brisanje!");
            }
            else
            {
                var confirmationDialog = new ConfirmationWindow("predmet");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    CLI.Model.Predmet pred = SelectedPredmet.toPredmet();
                    pred.IdPredmet = SelectedPredmet.predmetId;
                    pred.IdProfesora = -1;


                    Profesor.PredmetiListaId.Remove(SelectedPredmet.predmetId);
                    CLI.Model.Profesor prof = Profesor.toProfesor();

                    prof.IdProfesor = Profesor.IdProfesor;
                    prof.IdAdrese = Profesor.idAdrese;
                    prof.AdresaStanovanja.IdAdrese = Profesor.idAdrese;

                    predmetController.UpdatePredmet(pred);
                    profesorController.UpdateProfesor(prof);
                    Update();
                }
            }


        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;

            StudentiDataGrid.ItemsSource = FilterStudent(Studenti, searchTerm);
        }

        private ObservableCollection<StudentPredmetDTO> FilterStudent(ObservableCollection<StudentPredmetDTO> originalCollection, string searchTerm)
        {
            // Razdvajanje unetog upita na reči i konverzija u mala slova
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();

            // Filtriranje na osnovu broja unetih reči
            switch (terms.Count)
            {
                case 1: // Samo predmet
                    return new ObservableCollection<StudentPredmetDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.NazivPredmeta.ToLower().Contains(terms[0]))
                    );

                case 2: // Prezime i ime
                    return new ObservableCollection<StudentPredmetDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Prezime.ToLower().Contains(terms[0]) &&
                            studentDto.Ime.ToLower().Contains(terms[1]))
                    );

                case 3: // Indeks, ime i prezime
                    return new ObservableCollection<StudentPredmetDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Indeks.ToLower().Contains(terms[0]) &&
                            studentDto.Ime.ToLower().Contains(terms[1]) &&
                            studentDto.Prezime.ToLower().Contains(terms[2]))
                    );

                default:
                    return originalCollection;
            }
        }
    }
}
