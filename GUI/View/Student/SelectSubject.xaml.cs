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
        private PredmetDAO predmetDAO { get; set; }

        public SelectSubject(PredmetDAO predmetDAO)
        {
            InitializeComponent();

            DataContext = this;

            Subjects = new ObservableCollection<PredmetDTO>();
            this.predmetDAO = predmetDAO;
            Update();
        }

        public void Update()
        {

            Subjects.Clear();
            foreach (CLI.Model.Predmet predmet in predmetDAO.GetAllPredmeti())
                Subjects.Add(new PredmetDTO(predmet));


                MessageBox.Show("test");
            

        }


        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
