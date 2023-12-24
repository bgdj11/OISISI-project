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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<StudentDTO> Students { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentDAO studentDAO { get; set; }
        private AdresaDAO adresaDAO { get; set; }
        private IndeksDAO indeksDAO { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Students = new ObservableCollection<StudentDTO>();
            studentDAO = new StudentDAO();
            Update();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void MenuItem_Predmeti_Click()
        {
           
        }
        private void MenuItem_Predmeti_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void MenuItem_Profesori_Click(object sender, RoutedEventArgs e)
        {
          
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


        private void MenuItem_Studenti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Studenti"));
        }
        private void MenuItem_Studenti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Studenti_Click();
        }

        public void Update()
        {
            Students.Clear();
            foreach (Student student in studentDAO.GetAllStudents()) 
                Students.Add(new StudentDTO(student));
        }

        private void InitializeStatusBar()
        {
            // Update date and time every second
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
                var addStudentWindow = new AddStudent(studentDAO, indeksDAO, adresaDAO);
                addStudentWindow.Owner = this;
                addStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addStudentWindow.ShowDialog();
                Update();
            }
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
        }

        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityButton_Click();
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
