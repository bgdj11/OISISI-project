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
using CLI.Controller;

namespace GUI.View.Predmet
{
    public partial class AddPredmet : Window, INotifyPropertyChanged
    {
        public PredmetDTO Predmet { get; set; }
        private PredmetController predmetController;

        public event PropertyChangedEventHandler? PropertyChanged;

        List<string> Semesters { get; set; }
        List<int> Godine { get; set; }

        public AddPredmet(PredmetController pc)
        {
            InitializeComponent();
            DataContext = this;
            Predmet = new PredmetDTO();
            this.predmetController = pc;

            Semesters = new List<string> { "letnji", "zimski" };
            cmbSemestar.ItemsSource = Semesters;

            Godine = new List<int> { 1, 2, 3, 4 };
            cmbGodinaStudija.ItemsSource = Godine;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                predmetController.AddPredmet(Predmet.toPredmet());
                MessageBox.Show("Predmet je uspesno dodat!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
               // MessageBox.Show("Popunite sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool ValidateFields()
        {
            var validations = new (TextBox textBox, string message, Func<string, bool> validator)[]
            {
                (txtBoxSifraPredmeta, "Unesite validnu sifru predmeta", s=>s.All(c => char.IsLetterOrDigit(c)  || char.IsWhiteSpace(c))),
                (txtBoxNaziv, "Unesite validan naziv predmeta", s => s.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))),
                (txtESPB, "Unesite validan broj ESPB bodova.", s => s.All(char.IsDigit))
            };


            foreach (var validation in validations)
            {
                if (string.IsNullOrWhiteSpace(validation.textBox.Text) || !validation.validator(validation.textBox.Text))
                {
                    MessageBox.Show(validation.message);
                    return false;
                }
            }

            if (cmbGodinaStudija.SelectedItem == null)
            {
                MessageBox.Show("Izaberite godinu.");
                return false;
            }


            return true;
        }
    }
}
