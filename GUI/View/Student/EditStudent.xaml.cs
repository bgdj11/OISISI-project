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

namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        public ObservableCollection<PredmetDTO> NotPassedSubjects { get; set; }  // lista nepolozenih
        public ObservableCollection<OcenaDTO> Ocene { get; set; }
        public ObservableCollection<ProfesorPredmetDTO> Profesori { get; set; }


        public StudentDTO Student { get; set; }
        public ProfesorPredmetDTO SelectedProfesorPredmet { get; set; }
        public OcenaDTO SelectedOcena { get; set; }



        private StudentController studentController;
        private ProfesorController profesorController;
        private OcenaController ocenaController;

        //private ProfesorDAO profesorDAO;
        

        //private OcenaNaUpisuDAO ocenaDAO;

        public PredmetDTO SelectedSubject { get; set; } // selektovan nepolozen
        //private PredmetDAO predmetDAO;
        private PredmetController predmetController;
        private StudentPredmetController studentPredmetController;

        //private StudentPredmetDAO studentPredmetDAO;

        List<int> Godine;
        List<string> Statusi;

        public EditStudent(StudentController sc, StudentDTO selectedStudent)
        {
            InitializeComponent();
            DataContext = this;
            this.studentController = sc;
            Student = selectedStudent;

            NotPassedSubjects = new ObservableCollection<PredmetDTO>();
            //predmetDAO = new PredmetDAO();
            predmetController = new PredmetController();
            SelectedSubject = new PredmetDTO();

            Ocene = new ObservableCollection<OcenaDTO>();
            ocenaController = new OcenaController();
            SelectedOcena = new OcenaDTO();

            //studentPredmetDAO = new StudentPredmetDAO();
            studentPredmetController = new StudentPredmetController();

            Godine = new List<int> { 1, 2, 3, 4 };
            cmbGodinaStudija.ItemsSource = Godine;

            Statusi = new List<string> { "samofinasiranje", "budzet" };
            cmbStatusStudenta.ItemsSource = Statusi;

            profesorController = new ProfesorController();
            Profesori = new ObservableCollection<ProfesorPredmetDTO>();

            SelectedProfesorPredmet = new ProfesorPredmetDTO();

            Update();
        }

        public void Update()
        {
            studentController.MakeStudent();

            if (Student.NotPassedIds != null)
            {
                NotPassedSubjects.Clear();
                foreach (int i in Student.NotPassedIds)
                {
                    NotPassedSubjects.Add(new PredmetDTO(predmetController.GetPredmetById(i)));
                }
            }


            //if (Student.gradesIds == null || Student.gradesIds.Count == 0)
            //{
             //   MessageBox.Show("Nema ID-ova ocena za proveru.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
           // }

            if (Student.GradesIds != null)
            {
                Ocene.Clear();
                foreach (int i in Student.GradesIds)
                {

                    if (ocenaController.GetOcenaById(i) != null)
                    {
                       // MessageBox.Show("nasao je ocenuuu");
                        Ocene.Add(new OcenaDTO(ocenaController.GetOcenaById(i)));
                    }
                }

            }

            //ProveriOcene();

            Student.UkupnoEspb = izracunajEspb();
            Student.ProsecnaOcena = izracunajProsecnuOcenu();


            foreach(int ids in Student.NotPassedIds)
            {
                CLI.Model.Predmet pr = predmetController.GetPredmetById(ids);
                int idProf = pr.IdProfesora;

                CLI.Model.Profesor prof = profesorController.GetProfesorById(idProf);

                if(prof != null)
                    Profesori.Add(new ProfesorPredmetDTO(prof, pr));
            }

        }

        public int izracunajEspb()
        {
            int espb = 0;
            foreach (int i in Student.GradesIds)
            {
                espb += ocenaController.GetOcenaById(i).Predmet.BrojESPB;
            }


            return espb;
        }


        public double izracunajProsecnuOcenu()
        {
            double suma = 0;
            int count = 0;
            foreach (int i in Student.GradesIds)
            {
                suma += ocenaController.GetOcenaById(i).Ocena;
                ++count;
            }
            if (count == 0)
            {
                return 5;
            }
            else return Math.Round(suma / count, 2);
        }

        protected virtual void OnPrortyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Student sr = Student.toStudent();
                sr.IdStudent = Student.StudentId;

                sr.IdAdrese = Student.idAdrese;
                sr.AdresaStanovanja.IdAdrese = Student.idAdrese;
                
                sr.IdIndeksa = Student.idIndeksa;
                sr.Indeks.idIndeksa = Student.idIndeksa;

                

                studentController.UpdateStudent(sr);
                MessageBox.Show("Student je uspesno promenjen!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                //MessageBox.Show("Popunite sva polja pre potvrde", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                (datpDatumRodjenja, "Unesite validan datum u formatu d.M.yyyy.", s => DateTime.TryParseExact(s, "d.M.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)),
                (txtBoxUlica, "Unesite validnu ulicu.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxBroj, "Unesite validan broj.", s => s.All(char.IsDigit)),
                (txtBoxGrad, "Unesite validan grad.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxDrzava, "Unesite validnu drzavu.", s => s.All(char.IsLetter)),
                (txtBoxKontakt, "Unesite kontakt informacije (samo brojevi).", s => s.All(char.IsDigit)),
                (txtBoxEmail, "Unesite validnu email adresu (sadrži '@').", s => s.Contains("@")),
                (txtBoxOznakaSmera, "Unesite oznaku smera (ne sme biti prazno).", s => !string.IsNullOrWhiteSpace(s)),
                (txtBoxBrojIndeksa, "Unesite broj indeksa (samo brojevi).", s => s.All(char.IsDigit)),
                (txtBoxGodinaUpisa, "Unesite validnu godinu upisa (samo brojevi).", s => s.All(char.IsDigit)),
            };

            foreach (var validation in validations)
            {
                if (string.IsNullOrWhiteSpace(validation.textBox.Text) || !validation.validator(validation.textBox.Text))
                {
                    MessageBox.Show(validation.message);
                    return false;
                }
            }

            if (cmbGodinaStudija.SelectedItem == null)
            {
                MessageBox.Show("Izaberite godinu studija.");
                return false;
            }


            if (cmbStatusStudenta.SelectedItem == null)
            {
                MessageBox.Show("Izaberite status studenta.");
                return false;
            }


            return true;
        }




        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            var selectSubjectWindow  = new SelectSubject(predmetController, Student, studentPredmetController);
            selectSubjectWindow.Owner = this;
            selectSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectSubjectWindow.ShowDialog();
            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubject == null)
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
                    studentPredmetController.RemovePredmetFromStudent(studentPredmetController.GetByIds(Student.StudentId, SelectedSubject.predmetId));
                }

                // da bi se izmene odmah prikazale:
                Student.NotPassedIds.Remove(SelectedSubject.predmetId);
            }

            Update();
        }
    
        private void AddGrade_Click(object sender, RoutedEventArgs e) 
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show(this, "Izaberite predmet!");
            }
            else
            {
                var addGrade = new AddGrade(SelectedSubject, Student, ocenaController);
                addGrade.Owner = this;
                addGrade.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addGrade.ShowDialog();
            }

            Update();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PonistiOcenu_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show(this, "Izaberite predmet za brisanje!");
            }
            else
            {
                var confirmationDialog = new ConfirmationWindow("ocena");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    ocenaController.DeleteOcena(SelectedOcena.idOcene);
                }

                Student.NotPassedIds.Add(SelectedOcena.IdPredmeta);
                Student.GradesIds.Remove(SelectedOcena.idOcene);
                Student.PassedIds.Remove(SelectedOcena.IdPredmeta);

            }

            Update();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;
            ProfesorsDataGrid.ItemsSource = FilterProfesor(Profesori, searchTerm);
        }

        private ObservableCollection<ProfesorPredmetDTO> FilterProfesor(ObservableCollection<ProfesorPredmetDTO> originalCollection, string searchTerm)
        {
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();


            switch (terms.Count)
            {
                case 1: // Samo prezime
                    return new ObservableCollection<ProfesorPredmetDTO>(
                        originalCollection.Where(p =>
                            p.Prezime.ToLower().Contains(terms[0]))
                    );

                case 2: // Prezime i ime
                    return new ObservableCollection<ProfesorPredmetDTO>(
                        originalCollection.Where(p =>
                            p.Prezime.ToLower().Contains(terms[0]) &&
                            p.Ime.ToLower().Contains(terms[1]))
                    );

                case 3:
                    return new ObservableCollection<ProfesorPredmetDTO>(
                        originalCollection.Where(p =>
                            p.Prezime.ToLower().Contains(terms[0]) &&
                            p.Ime.ToLower().Contains(terms[1]) &&
                            p.NazivPredmeta.ToLower().Contains(terms[2]))
                    );

                default:
                    return originalCollection;
            }
        }

    }
}
