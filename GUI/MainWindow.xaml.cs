using CLI.DAO;
using GUI.DTO;
using CLI.Observer;
using CLI.Model;
using GUI.View.Student;
using GUI.View.DialogWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GUI.View.Profesor;
using GUI.View.Predmet;
using System.Xml.Serialization;
using GUI.View.Katedra;
using CLI.Controller;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ObservableCollection<ProfesorDTO> Profesors { get; set; }
        public ObservableCollection<PredmetDTO> Subjects { get; set; }

        public ObservableCollection<KatedraDTO> Katedre { get; set; }

        public StudentDTO SelectedStudent { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        public KatedraDTO SelectedKatedra { get; set; }

        //private ProfesorDAO profesorDAO { get; set; }
        //private StudentDAO studentDAO { get; set; }
        //private PredmetDAO predmetDAO { get; set; }
        private KatedraDAO katedraDAO { get; set; }

        private StudentController studentController;
        private ProfesorController profesorController;
        private PredmetController predmetController;
        private AdresaController adresaController;
        private KatedraController katedraController;
        private OcenaController ocenaController;
        private StudentPredmetController studentPredmetController;

        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CloseCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand HelpCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();


        public int TrenutnaStranica { get; set; } = 1;
        public int UkupnoStranica { get; set; }
        public int StavkiPoStranici { get; set; } = 16;

        private int SelectedPageIndex { get; set; } = 1;
private object SelectedEntity { get; set; }



        public MainWindow()
        {
            InitializeComponent();
            InitializeStatusBar();

            DataContext = this;

            Students = new ObservableCollection<StudentDTO>();
            studentController = new StudentController();
            studentController.Subscribe(this);

            Profesors = new ObservableCollection<ProfesorDTO>();
            //profesorDAO = new ProfesorDAO();
            profesorController = new ProfesorController();

            Subjects = new ObservableCollection<PredmetDTO>();
            predmetController = new PredmetController();
            Tab.Text = "Status: Studenti";

            Katedre = new ObservableCollection<KatedraDTO>();
            katedraController = new KatedraController();

            studentPredmetController = new StudentPredmetController();

            Update();

            CommandBindings.Add(new CommandBinding(NewCommand, CreateEntityButton_Click));
            CommandBindings.Add(new CommandBinding(SaveCommand, SaveApp));
            CommandBindings.Add(new CommandBinding(CloseCommand, CloseApp_Execution));
            CommandBindings.Add(new CommandBinding(EditCommand, EditEntityButton_Click));
            CommandBindings.Add(new CommandBinding(HelpCommand, OpenAboutWindow));
            CommandBindings.Add(new CommandBinding(DeleteCommand, DeleteEntityButton_Click));

            // Postavljanje Input Gestures
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CloseCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            HelpCommand.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MenuItem_Predmeti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Predmeti"));
            Tab.Text = "Status: Predmeti";
        }


        private void MenuItem_Predmeti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Predmeti_Click();
        }

        private void MenuItem_Katedre_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Katedre"));
            Tab.Text = "Status: Katedre";
        }

        private void MenuItem_Katedre_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Katedre_Click();
        }

        private void MenuItem_Profesori_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Profesori"));
            Tab.Text = "Status: Profesori";
        }

        private void MenuItem_Profesori_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Profesori_Click();
        }
        private void MenuItem_Studenti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Studenti"));
            Tab.Text = "Status: Studenti";
        }
        private void MenuItem_Studenti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Studenti_Click();
        }

        private void AddNewEntity(object sender, RoutedEventArgs e)
        {

        }

        private void SaveApp(object sender, RoutedEventArgs e)
        {

        }
        private void CloseApp_Execution(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenAboutWindow()
        {
            MessageBox.Show("Bogdan Djukic RA98/2021 i Mateja Jovanovic RA160/2021", "Informacije", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            OpenAboutWindow();
        }





        public void Update()
        {
            Profesors.Clear();
            foreach (Profesor profesor in profesorController.GetAllProfesor())
                Profesors.Add(new ProfesorDTO(profesor));

            Students.Clear();
            foreach (Student student in studentController.GetAllStudents()) 
                Students.Add(new StudentDTO(student));

            Subjects.Clear();
            foreach (Predmet predmet in predmetController.GetAllPredmet())
                Subjects.Add(new PredmetDTO(predmet));

            Katedre.Clear();
            foreach (Katedra k in katedraController.GetAllKatedra()) {
                Katedre.Add(new KatedraDTO(k));
            }
        }

        private void InitializeStatusBar()
        {
            var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, e) =>
            {
                UpdateDate();
                UpdateTime();
            };
            timer.Start();
        }

        private void UpdateDate()
        {
            statusDate.Text = $"Datum: {DateTime.Now.ToString("yyyy-MM-dd")}";
        }

        private void UpdateTime()
        {
            statusTime.Text = $"Vreme: {DateTime.Now.ToString("HH:mm:ss")}";
        }


        private void CreateEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                var addStudentWindow = new AddStudent(studentController);
                addStudentWindow.Owner = this;
                addStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addStudentWindow.ShowDialog();
                Update();
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                var addProfesorWindow = new AddProfesor(profesorController);
                addProfesorWindow.Owner = this;
                addProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addProfesorWindow.ShowDialog();
                Update();
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                var addPredmetWindow = new AddPredmet(predmetController);
                addPredmetWindow.Owner = this;
                addPredmetWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addPredmetWindow.ShowDialog();
                Update();
            } else if(MainTabControl.SelectedItem == KatedreTab)
            {
                var addKatedraWindow = new AddKatedra(katedraController);
                addKatedraWindow.Owner = this;
                addKatedraWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addKatedraWindow.ShowDialog();
                Update();
            }
        }

        private void EditEntityButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                if(SelectedProfesor == null)
                {
                    MessageBox.Show(this, "Izaberi profesora za izmenu.");
                }
                else
                {
                    var editsProfesorWindow = new EditProfesor(profesorController, predmetController, SelectedProfesor.Clone());
                    editsProfesorWindow.Owner = this;
                    editsProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editsProfesorWindow.ShowDialog();
                }
            }else if(MainTabControl.SelectedItem == SubjectsTab)
            {
                if(SelectedPredmet == null)
                {
                    MessageBox.Show(this, "Izaberi predmet za izmenu.");
                }
                else
                {
                    var editPredmetWindow = new EditPredmet(predmetController, SelectedPredmet.clone());
                    editPredmetWindow.Owner = this;
                    editPredmetWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editPredmetWindow.ShowDialog();
                }
            } else if(MainTabControl.SelectedItem == StudentsTab)
            {
                if(SelectedStudent == null)
                {
                    MessageBox.Show(this, "Izaberi studenta za izmenu.");
                }
                else
                {
                    var editStudentWIndow = new EditStudent(studentController, SelectedStudent.Clone());
                    editStudentWIndow.Owner = this;
                    editStudentWIndow.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                    editStudentWIndow.ShowDialog();
                }

            }
            else
            {
                if(SelectedKatedra == null)
                {
                    MessageBox.Show(this, "Izaberi katedru za izmenu.");
                }
                else
                {
                    var editKatedraWindow = new EditKatedra(katedraController, SelectedKatedra.clone(), profesorController);
                    editKatedraWindow.Owner = this;
                    editKatedraWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editKatedraWindow.ShowDialog();
                }
            }

            Update();
        }


        private void DeleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show(this, "Izaberite studenta za brisanje!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Student");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                  
                        studentController.DeleteStudent(SelectedStudent.StudentId);
                        Students.Remove(SelectedStudent);
                    }
                }
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab) {
                if (SelectedProfesor == null)
                {
                    MessageBox.Show(this, "Izaberite profesora za brisanje!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Profesor");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        profesorController.DeleteProfesor(SelectedProfesor.IdProfesor);
                        Profesors.Remove(SelectedProfesor);
                    }
                }
            } else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                if (SelectedPredmet == null)
                {
                    MessageBox.Show(this, "Izaberite predmet za brisanje!");
                } 
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Predmet");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        predmetController.RemovePredmet(SelectedPredmet.predmetId);
                        Subjects.Remove(SelectedPredmet);
                    }
                }
            }
            else if(MainTabControl.SelectedItem == KatedreTab)
            {
                if(SelectedKatedra == null)
                {
                    MessageBox.Show(this, "Izaberite katedru za brisanje!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Katedra");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        katedraController.RemoveKatedra(SelectedKatedra.katedraId);
                        Katedre.Remove(SelectedKatedra);
                    }
                }
            }

            AzurirajPaginacijuPosleBrisanja();
            Update();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchTextBox.Text;



            if (MainTabControl.SelectedItem == StudentsTab)
            {
                ObservableCollection<StudentDTO> filteredStudents = FilterStudent(Students, searchTerm);
                UkupnoStranica = (int)Math.Ceiling(filteredStudents.Count / (double)StavkiPoStranici);
                TrenutnaStranica = 1;
                PostaviEntiteteZaTrenutnuStranicu(filteredStudents);
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                ObservableCollection<ProfesorDTO> filteredProfesors = FilterProfesor(Profesors, searchTerm);
                UkupnoStranica = (int)Math.Ceiling(filteredProfesors.Count / (double)StavkiPoStranici);
                TrenutnaStranica = 1;
                PostaviEntiteteZaTrenutnuStranicu(filteredProfesors);
            }
            else if(MainTabControl.SelectedItem == SubjectsTab)
            {
                ObservableCollection<PredmetDTO> filteredSubjects = FilterSubject(Subjects, searchTerm);
                UkupnoStranica = (int)Math.Ceiling(filteredSubjects.Count / (double)StavkiPoStranici);
                TrenutnaStranica = 1;
                PostaviEntiteteZaTrenutnuStranicu(filteredSubjects);
            }




        }


        private ObservableCollection<StudentDTO> FilterStudent(ObservableCollection<StudentDTO> originalCollection, string searchTerm)
        {
            // Razdvajanje unetog upita na reči i konverzija u mala slova
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();

            // Filtriranje na osnovu broja unetih reči
            switch (terms.Count)
            {
                case 1: // Samo prezime
                    return new ObservableCollection<StudentDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Prezime.ToLower().Contains(terms[0]))
                    );

                case 2: // Prezime i ime
                    return new ObservableCollection<StudentDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Prezime.ToLower().Contains(terms[0]) &&
                            studentDto.Ime.ToLower().Contains(terms[1]))
                    );

                case 3: // Indeks, ime i prezime
                    return new ObservableCollection<StudentDTO>(
                        originalCollection.Where(studentDto =>
                            studentDto.Indeks.ToLower().Contains(terms[0]) &&
                            studentDto.Ime.ToLower().Contains(terms[1]) &&
                            studentDto.Prezime.ToLower().Contains(terms[2]))
                    );

                default:
                    return originalCollection;
            }
        }

        private ObservableCollection<ProfesorDTO> FilterProfesor(ObservableCollection<ProfesorDTO> originalCollection, string searchTerm)
        {
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();


            switch (terms.Count)
            {
                case 1: // Samo prezime
                    return new ObservableCollection<ProfesorDTO>(
                        originalCollection.Where(profesorDTO =>
                            profesorDTO.Prezime.ToLower().Contains(terms[0]))
                    );

                case 2: // Prezime i ime
                    return new ObservableCollection<ProfesorDTO>(
                        originalCollection.Where(profesorDTO =>
                            profesorDTO.Prezime.ToLower().Contains(terms[0]) &&
                            profesorDTO.Ime.ToLower().Contains(terms[1]))
                    );

                default:
                    return originalCollection;
            }
        }

        private ObservableCollection<PredmetDTO> FilterSubject(ObservableCollection<PredmetDTO> originalCollection, string searchTerm)
        {
            var terms = searchTerm.ToLower().Split(',').Select(s => s.Trim()).ToList();

            switch(terms.Count())
            {
                case 1: // Samo sifra predmeta
                    return new ObservableCollection<PredmetDTO>(
                        originalCollection.Where(s => s.SifraPredmeta.ToLower().Contains(terms[0]))
                        );

                case 2: // Sifra i naziv predmeta
                    return new ObservableCollection<PredmetDTO>(
                        originalCollection.Where(s => s.SifraPredmeta.ToLower().Contains(terms[0]) &&
                        s.NazivPredmeta.ToLower().Contains(terms[1]))
                        );

                default:
                    return originalCollection;
            }
        }

        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityButton_Click();
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tab.Text = "Status: Studenti";
        }
        private void ProfesorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tab.Text = "Status: Profesori";
        }
        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tab.Text = "Status: Predmeti";
        }

        private void KatedreDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tab.Text = "Status: Katedre";
        }

        private void TabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                Tab.Text = "Status: Studenti";
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                Tab.Text = "Status: Profesori";
            }
            else if(MainTabControl.SelectedItem == SubjectsTab)    
            {
                Tab.Text = "Status: Predmeti";
            }
            else if(MainTabControl.SelectedItem == KatedreTab)
                Tab.Text = "Status: Katedre";

            IzracunajPaginaciju();
        }

        private void IzracunajPaginaciju()
        {
            //TrenutnaStranica = 1;

            int ukupnoEntiteta = 0;

            if(MainTabControl.SelectedItem == StudentsTab)
            {
                ukupnoEntiteta = Students.Count;
                UkupnoStranica = (int)Math.Ceiling(ukupnoEntiteta / (double)StavkiPoStranici);
                PostaviEntiteteZaTrenutnuStranicu(Students);
            } else if(MainTabControl.SelectedItem == ProfesorsTab)
            {
                ukupnoEntiteta = Profesors.Count;
                UkupnoStranica = (int)Math.Ceiling(ukupnoEntiteta / (double)StavkiPoStranici);
                PostaviEntiteteZaTrenutnuStranicu(Profesors);
            } else
            {
                ukupnoEntiteta = Subjects.Count;
                UkupnoStranica = (int)Math.Ceiling(ukupnoEntiteta / (double)StavkiPoStranici);
                PostaviEntiteteZaTrenutnuStranicu(Subjects);
            }


        }

        private void PostaviEntiteteZaTrenutnuStranicu<T>(ObservableCollection<T> entiteti)
        {
            int startIndex = (TrenutnaStranica - 1) * StavkiPoStranici;
            var filtriraniEntiteti = entiteti.Skip(startIndex).Take(StavkiPoStranici);

            if (MainTabControl.SelectedItem == StudentsTab)
            {
                StudentsDataGrid.ItemsSource = new ObservableCollection<StudentDTO>(filtriraniEntiteti.Cast<StudentDTO>());
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                ProfesorsDataGrid.ItemsSource = new ObservableCollection<ProfesorDTO>(filtriraniEntiteti.Cast<ProfesorDTO>());
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                SubjectsDataGrid.ItemsSource = new ObservableCollection<PredmetDTO>(filtriraniEntiteti.Cast<PredmetDTO>());
            }
        }




        public void SledecaStranica()
        {
            if (TrenutnaStranica < UkupnoStranica)
            {
                TrenutnaStranica++;
                ResetSelectedEntities();
                OsveziTrenutnuStranicu();
            }
        }

        public void PrethodnaStranica()
        {
            if (TrenutnaStranica > 1)
            {
                TrenutnaStranica--;
                ResetSelectedEntities();
                OsveziTrenutnuStranicu();
            }
        }

        private void OsveziTrenutnuStranicu()

        {
            ResetSelectedEntities();

            if (MainTabControl.SelectedItem == StudentsTab)
            {
                PostaviEntiteteZaTrenutnuStranicu(Students);
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                PostaviEntiteteZaTrenutnuStranicu(Profesors);
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                PostaviEntiteteZaTrenutnuStranicu(Subjects);
            }
        }




        private void PrethodnaStranica_Click(object sender, RoutedEventArgs e)
        {
            PrethodnaStranica();
        }

        private void NarednaStranica_Click(object sender, RoutedEventArgs e)
        {
            SledecaStranica();
        }

        private void ResetSelectedEntities()
        {
            SelectedStudent = null;
            SelectedProfesor = null;
            SelectedPredmet = null;
            SelectedKatedra = null;

        }

        private void AzurirajPaginacijuPosleBrisanja()
        {
            UkupnoStranica = (int)Math.Ceiling(VratiTrenutniBrojElemenata() / (double)StavkiPoStranici);

            if (TrenutnaStranica > UkupnoStranica)
            {
                TrenutnaStranica = Math.Max(1, UkupnoStranica);
            }

            OsveziTrenutnuStranicu();
        }

        private int VratiTrenutniBrojElemenata()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                return Students.Count;
            }
            // Dodajte logiku za ostale tabove...

            return 0;
        }



    }
}

