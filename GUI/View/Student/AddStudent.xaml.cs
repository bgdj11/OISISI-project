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

namespace GUI.View.Student
{

    public partial class AddStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO Student { get; set; }
        private StudentDAO studentsDAO;

        public event PropertyChangedEventHandler? PropertyChanged;

        List<int> Godine;
        List<string> Statusi;

        public AddStudent(StudentDAO studentsDAO)
        {
            InitializeComponent();

            DataContext = this;
            Student = new StudentDTO();
            this.studentsDAO = studentsDAO;

            Godine = new List<int> { 1, 2, 3, 4 };
            cmbGodinaStudija.ItemsSource = Godine;

            Statusi = new List<string> { "samofinasiranje", "budzet" };
            cmbStatusStudenta.ItemsSource = Statusi;
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
                studentsDAO.AddStudent(Student.toStudent());
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
                   !string.IsNullOrWhiteSpace(datpDatumRodjenja.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxAdresa.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxKontakt.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxIndeks.Text) &&
                   cmbGodinaStudija.SelectedItem != null &&
                   cmbStatusStudenta.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtBoxProsecnaOcena.Text);
        }
    }
}

