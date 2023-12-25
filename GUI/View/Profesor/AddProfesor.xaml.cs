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
using GUI.DTO;

namespace GUI.View.Profesor
{
    public partial class AddProfesor : Window, INotifyPropertyChanged
    {
        public ProfesorDTO Profesor { get; set; }
        private ProfesorDAO profesorsDAO;
        private AdresaDAO adresaDAO;


        public event PropertyChangedEventHandler? PropertyChanged;

        public AddProfesor(ProfesorDAO profesorDAO, AdresaDAO adresaDAO) 
        {
            InitializeComponent();

            DataContext = this;
            Profesor = new ProfesorDTO();
            this.profesorsDAO = profesorDAO;
            this.adresaDAO = adresaDAO;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                //adresaDAO.AddAdresa(Student.toStudent().AdresaStanovanja);
                //indeksDAO.AddIndeks(Student.toStudent().Indeks);
                profesorsDAO.AddProfesor(Profesor.toProfesor());
                MessageBox.Show("Student je uspesno dodat!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
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
            return !string.IsNullOrWhiteSpace(txtBoxIme.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxPrezime.Text) &&
                   dpDatumRodjenja.SelectedDate.HasValue &&
                   !string.IsNullOrWhiteSpace(txtBoxAdresa.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxKontakt.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxBrojLicneKarte.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxZvanje.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxGodinaStaza.Text);
        }

    }
}
