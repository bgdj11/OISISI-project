using CLI.DAO;
using GUI.DTO;
using GUI.View.DialogWindows;
using GUI.View.Profesor;
using GUI.View.Student;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace GUI.View.Predmet
{
    /// <summary>
    /// Interaction logic for EditPredmet.xaml
    /// </summary>
    public partial class EditPredmet : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<StudentDTO> Studenti { get; set; }

        public PredmetDTO Predmet { get; set; }

        public PredmetDTO DrugiPredmet { get; set; }

        public PredmetDAO PredmetDAO { get; set; }
        public StudentDAO StudentDAO { get; set; }


        List<string> Semesters { get; set; }
        List<int> Godine { get; set; }

        public EditPredmet(PredmetDAO predmetDAO, PredmetDTO selectedPredmet)
        {
            InitializeComponent();
            DataContext = this;
            this.PredmetDAO = predmetDAO;
            this.Predmet = selectedPredmet;

            Semesters = new List<string> { "letnji", "zimski" };
            cmbSemestar.ItemsSource = Semesters;

            Godine = new List<int> { 1, 2, 3, 4 };
            cmbGodinaStudija.ItemsSource = Godine;

            this.StudentDAO = new StudentDAO();
            this.DrugiPredmet = null;
            Studenti = new ObservableCollection<StudentDTO>();

        }

        private void Update()
        {
            Predmet = new PredmetDTO(PredmetDAO.GetPredmetById(Predmet.predmetId));

            if (DrugiPredmet != null)
            {
                PredmetDAO.MakePredmet();
                StudentDAO.MakeStudent();

                foreach (CLI.Model.Student student in StudentDAO.GetAllStudents())
                {
/*                    if (student.NepolozeniIspiti.Count == 0)
                    {
                        MessageBox.Show(this, "PRAZNA.");
                    }*/
                    int counter = 0;
                    foreach(CLI.Model.Predmet predmet in student.NepolozeniIspiti)
                    {
                        if(predmet.IdPredmet == DrugiPredmet.predmetId ||
                            predmet.IdPredmet == Predmet.predmetId)
                        {
                            counter++;
                        }
                    }

                    if(counter == 2) {
                        Studenti.Add(new StudentDTO(student));
                    }
                }
            }
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Predmet pr = Predmet.toPredmet();
                pr.IdPredmet = Predmet.predmetId;

                PredmetDAO.UpdatePredmet(pr);
                MessageBox.Show("Predmet je uspesno promenjen!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Popunite sva polja pre potvrde", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtBoxSifraPredmeta.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxNaziv.Text) &&
                   !string.IsNullOrWhiteSpace(cmbSemestar.Text) &&
                   cmbGodinaStudija.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtESPB.Text);
        }

        private void AddProfessor_Click(object sender, RoutedEventArgs e)
        {
            if (Predmet.ProfesorID == -1)
            {
                var selectProfesorWindow = new SelectProfesor(PredmetDAO, Predmet);
                selectProfesorWindow.Owner = this;
                selectProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                selectProfesorWindow.ShowDialog();
                Update();
            }
        }

        private void RemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            if(Predmet.ProfesorID != -1)
            {

                var confirmationDialog = new ConfirmationWindow("Profesor");
                confirmationDialog.Owner = this;
                confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                confirmationDialog.ShowDialog();

                if (confirmationDialog.UserConfirmed)
                {
                    Predmet.Profesor = "nedostaje profesor";
                    CLI.Model.Predmet pr = Predmet.toPredmet();
                    pr.IdPredmet = Predmet.predmetId;
                    pr.IdProfesora = -1;

                    PredmetDAO.UpdatePredmet(pr);
                }
            }
            Update();
        }

        private void txtBoxProfesorID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IzaberiPredmet_Click(object sender, RoutedEventArgs e)
        {
            var selectSubjectSubjectWindow = new SelectSubjectSubjcet(PredmetDAO, Predmet);
            selectSubjectSubjectWindow.Owner = this;
            selectSubjectSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectSubjectSubjectWindow.ShowDialog();
            if(selectSubjectSubjectWindow.SelectedPredmet != null)
            {
                DrugiPredmet = selectSubjectSubjectWindow.SelectedPredmet;
            }
            Update();
        }

        private void PoloziliPrvi_Click(object sender, RoutedEventArgs e)
        {
            StudentiDataGrid.ItemsSource = FilterStudent();
        }

        public ObservableCollection<StudentDTO> FilterStudent()
        {

            Studenti.Clear();
            foreach (CLI.Model.Student student in StudentDAO.GetAllStudents())
            {
                int polozioPrviPredmet = 0;
                int polozioDrugiPredmet = 0;
                foreach(CLI.Model.OcenaNaUpisu ocena in student.PolozeniIspiti)
                {
                    if(ocena.IdPredmeta == Predmet.predmetId)
                    {
                        polozioPrviPredmet = 1;
                    }
                    if(ocena.IdPredmeta == DrugiPredmet.predmetId)
                    {
                        polozioDrugiPredmet = 1;
                    }
                }

                if(polozioPrviPredmet == 1 && polozioDrugiPredmet == 0)
                {
                    Studenti.Add(new StudentDTO(student));
                }
            }
                
                
            return Studenti;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
