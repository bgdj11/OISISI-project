using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for AddGrade.xaml
    /// </summary>
    public partial class AddGrade : Window, INotifyPropertyChanged
    {
        public OcenaDTO Ocena { get; set; }
        private OcenaController ocenaController;
        private StudentDTO Student;
        public PredmetDTO Predmet { get; set; }

        List<int> Ocene;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public AddGrade(PredmetDTO predmet, StudentDTO student, OcenaController oc)
        {
            InitializeComponent();

            DataContext = this;
            this.Student = student;
            this.Predmet = predmet;
            this.ocenaController = oc;

            Ocena = new OcenaDTO();

            Ocene = new List<int> { 6, 7, 8, 9, 10 };
            cmbOcena.ItemsSource = Ocene;

        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Ocena.IdPredmeta = Predmet.predmetId;
                Ocena.IdStudenta = Student.StudentId;

                OcenaNaUpisu o = Ocena.toOcena();

                ocenaController.AddOcena(o);

                Student.NotPassedIds.Remove(Ocena.IdPredmeta);
                Student.GradesIds.Add(o.IdOcene);
                Student.PassedIds.Add(Ocena.IdPredmeta);

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
                   cmbOcena.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtDatum.Text);
        }

    }
}
