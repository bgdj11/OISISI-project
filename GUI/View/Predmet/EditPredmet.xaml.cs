using CLI.DAO;
using GUI.DTO;
using System;
using System.Collections.Generic;
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

        public PredmetDAO predmetDAO { get; set; }
        public PredmetDTO Predmet { get; set; }

        public EditPredmet(PredmetDAO predmetDAO, PredmetDTO selectedPredmet)
        {
            InitializeComponent();
            DataContext = this;
            this.predmetDAO = predmetDAO;
            this.Predmet = selectedPredmet;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Predmet pr = Predmet.toPredmet();
                pr.idPredmet = Predmet.predmetId;

                predmetDAO.UpdatePredmet(pr);
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
                   !string.IsNullOrWhiteSpace(txtBoxProfesorID.Text) &&
                   !string.IsNullOrWhiteSpace(txtESPB.Text);
        }
    }
}
