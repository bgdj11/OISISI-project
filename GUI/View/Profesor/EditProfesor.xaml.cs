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

        private ProfesorDAO profesorDAO;
        private PredmetDAO predmetDAO;
        private StudentDAO studentDAO;

        public ObservableCollection<PredmetDTO> Predmeti { get; set; }
        public ObservableCollection<StudentPredmetDTO> Studenti { get; set; }



        public EditProfesor(ProfesorDAO profesorDAO, PredmetDAO predmetDao, ProfesorDTO selectedProfesor)
        {
            InitializeComponent();
            DataContext = this;
            this.profesorDAO = profesorDAO;
            this.predmetDAO = predmetDao;
            Profesor = selectedProfesor;

            Predmeti = new ObservableCollection<PredmetDTO>();
            Studenti = new ObservableCollection<StudentPredmetDTO>();

            studentDAO = new StudentDAO();
            SelectedPredmet = new PredmetDTO();

            Update();
        }


        public void Update()
        {
            studentDAO.MakeStudent();
            profesorDAO.MakeProfesor();
            predmetDAO.MakePredmet();


            if (Profesor.PredmetiListaId != null)
            {
                Predmeti.Clear();
                foreach (int i in Profesor.PredmetiListaId)
                {
                    Predmeti.Add(new PredmetDTO(predmetDAO.GetPredmetById(i)));
                }
            }

            foreach (CLI.Model.Student student in studentDAO.GetAllStudents())
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

                profesorDAO.UpdateProfesor(pr);
                MessageBox.Show("Profesor je uspesno promenjen!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Popunite sva polja pre potvrde", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Update();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtBoxIme.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxPrezime.Text) &&
                   !string.IsNullOrWhiteSpace(dpDatumRodjenja.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxUlica.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxBroj.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxGrad.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxDrzava.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxKontakt.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxBrojLicneKarte.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxZvanje.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxGodinaStaza.Text);
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DodajPredmet_Click(object sender, RoutedEventArgs e)
        {
            var selectSubjectProfesorWindow = new SelectSubjectProfesor(profesorDAO, predmetDAO, Profesor);
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

                    predmetDAO.UpdatePredmet(pred);
                    profesorDAO.UpdateProfesor(prof);
                    Update();
                }
            }


        }
    }
}
