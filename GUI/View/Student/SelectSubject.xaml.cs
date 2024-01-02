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


namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for SelectSubject.xaml
    /// </summary>
    public partial class SelectSubject : Window, IObserver
    {
        public ObservableCollection<PredmetDTO> Subjects { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        public StudentDTO SelectedStudent { get; set; }
        private PredmetDAO predmetDAO { get; set; }

        private StudentPredmetDAO studentPredmetDAO { get; set; }

        public SelectSubject(PredmetDAO predmetDAO, StudentDTO student, StudentPredmetDAO spDAO)
        {
            InitializeComponent();

            DataContext = this;

            this.SelectedStudent = student;
            this.predmetDAO = predmetDAO;

            Subjects = new ObservableCollection<PredmetDTO>();
            studentPredmetDAO = spDAO;


            Update();
        }

        public void Update()
        {

            Subjects.Clear();
            foreach (CLI.Model.Predmet predmet in predmetDAO.GetAllPredmeti())
                Subjects.Add(new PredmetDTO(predmet));
        }


        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            studentPredmetDAO.AddPredmetToStudent(SelectedStudent.StudentId, SelectedPredmet.predmetId);
            MessageBox.Show("Predmet je uspesno dodat!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedStudent.notPassedIds.Add(SelectedPredmet.predmetId);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
