using CLI.DAO;
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

namespace GUI.View.Predmet
{
    /// <summary>
    /// Interaction logic for SelectSubjectSubjcet.xaml
    /// </summary>
    public partial class SelectSubjectSubjcet : Window
    {
        public ObservableCollection<PredmetDTO> Predmeti { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }

        public PredmetDTO Predmet { get; set; }
        private PredmetDAO predmetDAO { get; set; }


        public SelectSubjectSubjcet(PredmetDAO predmetDAO, PredmetDTO predmetInitial)
        {
            InitializeComponent();
            DataContext = this;
            this.predmetDAO = predmetDAO;
            this.Predmet = predmetInitial;

            Predmeti = new ObservableCollection<PredmetDTO>();
            Update();


        }

        public void Update()
        {
            Predmeti.Clear();
            foreach(CLI.Model.Predmet predmet in predmetDAO.GetAllPredmeti())
            {
                if(Predmet.predmetId == predmet.IdPredmet)
                {
                    continue;
                }
                Predmeti.Add(new PredmetDTO(predmet));
            }
            
        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPredmet == null)
            {
                MessageBox.Show(this, "Izaberi predmet.");
            } else
            {
                Predmet = SelectedPredmet;
            }

            


            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
