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
using CLI.DAO;


namespace GUI.View.Predmet
{
    public partial class AddPredmet : Window, INotifyPropertyChanged
    {
        public PredmetDTO Predmet { get; set; }
        private PredmetDAO predmetDAO;

        public event PropertyChangedEventHandler? PropertyChanged;

        public AddPredmet(PredmetDAO predmetDAO)
        {
            InitializeComponent();
            DataContext = this;
            Predmet = new PredmetDTO();
            this.predmetDAO = predmetDAO;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                predmetDAO.AddPredmet(Predmet.toPredmet());
                MessageBox.Show("Predmet je uspesno dodat!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Popunite sva polja!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
