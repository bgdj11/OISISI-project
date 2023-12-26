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

        public StudentDTO SelectedStudent { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        private ProfesorDAO profesorDAO { get; set; }
        private StudentDAO studentDAO { get; set; }
        private PredmetDAO predmetDAO { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            InitializeStatusBar();

            DataContext = this;

            Students = new ObservableCollection<StudentDTO>();
            studentDAO = new StudentDAO();

            Profesors = new ObservableCollection<ProfesorDTO>();
            profesorDAO = new ProfesorDAO();

            Subjects = new ObservableCollection<PredmetDTO>();
            predmetDAO = new PredmetDAO();

            Update();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void MenuItem_Predmeti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Predmeti"));
        }

        private void MenuItem_Predmeti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Predmeti_Click();
        }

        private void MenuItem_Profesori_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Profesori"));
        }

        private void MenuItem_Profesori_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Profesori_Click();
        }
        private void MenuItem_Studenti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Studenti"));
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
           
        }

        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            OpenAboutWindow();
        }



        public void Update()
        {
            Profesors.Clear();
            foreach (Profesor profesor in profesorDAO.GetAllProfesors())
                Profesors.Add(new ProfesorDTO(profesor));

            Students.Clear();
            foreach (Student student in studentDAO.GetAllStudents()) 
                Students.Add(new StudentDTO(student));

            Subjects.Clear();
            foreach (Predmet predmet in predmetDAO.GetAllPredmeti())
                Subjects.Add(new PredmetDTO(predmet));

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
            statusDate.Text = $"Date: {DateTime.Now.ToString("yyyy-MM-dd")}";
        }

        private void UpdateTime()
        {
            statusTime.Text = $"Time: {DateTime.Now.ToString("HH:mm:ss")}";
        }


        private void CreateEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                var addStudentWindow = new AddStudent(studentDAO);
                addStudentWindow.Owner = this;
                addStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addStudentWindow.ShowDialog();
                Update();
            }
            else if (MainTabControl.SelectedItem == ProfesorsTab)
            {
                var addProfesorWindow = new AddProfesor(profesorDAO);
                addProfesorWindow.Owner = this;
                addProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addProfesorWindow.ShowDialog();
                Update();
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                var addPredmetWindow = new AddPredmet(predmetDAO);
                addPredmetWindow.Owner = this;
                addPredmetWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addPredmetWindow.ShowDialog();
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
                    var editsProfesorWindow = new EditProfesor(profesorDAO, SelectedProfesor.Clone());
                    editsProfesorWindow.Owner = this;
                    editsProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editsProfesorWindow.ShowDialog();
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
                        studentDAO.RemoveStudent(SelectedStudent.StudentId);
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
                        profesorDAO.RemoveProfesor(SelectedProfesor.IdProfesor);
                    }
                }
            } else
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
                        predmetDAO.RemovePredmet(SelectedPredmet.predmetId);
                    }
                }
            }
            Update();
        }

        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityButton_Click();
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProfesorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
