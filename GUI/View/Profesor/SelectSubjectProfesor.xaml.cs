using CLI.DAO;
using CLI.Observer;
using GUI.DTO;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace GUI.View.Profesor
{
    /// <summary>
    /// Interaction logic for SelectSubjectProfesor.xaml
    /// </summary>
    public partial class SelectSubjectProfesor : Window, IObserver
    {
        public ObservableCollection<PredmetDTO> Subjects { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        public ProfesorDTO Profesor { get; set; }
        private ProfesorDAO profesorDAO { get; set; }

        private PredmetDAO predmetDAO { get; set; }



        public SelectSubjectProfesor(ProfesorDAO profesorDAO, PredmetDAO predmetDao, ProfesorDTO profesor)
        {
            InitializeComponent();
            DataContext = this;

            this.Profesor = profesor;
            this.profesorDAO = profesorDAO;

            Subjects = new ObservableCollection<PredmetDTO>();
            predmetDAO = predmetDao;

            Update();

        }



        public void Update()
        {
            Subjects.Clear();

            foreach (CLI.Model.Predmet pr in predmetDAO.GetAllPredmeti())
            {
                if (!Profesor.PredmetiListaId.Contains(pr.IdPredmet))
                {
                    Subjects.Add(new PredmetDTO(pr));
                }
            }
        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet == null)
            {
                MessageBox.Show(this, "Izaberi predmet.");
            }

            CLI.Model.Profesor prof = Profesor.toProfesor();
            prof.IdProfesor = Profesor.IdProfesor;
            prof.IdAdrese = Profesor.idAdrese;
            prof.AdresaStanovanja.IdAdrese = Profesor.idAdrese;

            CLI.Model.Predmet pred = SelectedPredmet.toPredmet();
            pred.IdPredmet = SelectedPredmet.predmetId;
            pred.IdProfesora = Profesor.IdProfesor;

            Profesor.PredmetiListaId.Add(SelectedPredmet.predmetId);

            predmetDAO.UpdatePredmet(pred);
            profesorDAO.UpdateProfesor(prof);
            Update();

            MessageBox.Show("Predmet je uspesno dodat.", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
