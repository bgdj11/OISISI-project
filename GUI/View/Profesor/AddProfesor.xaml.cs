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
using CLI.Controller;
using CLI.DAO;
using GUI.DTO;

namespace GUI.View.Profesor
{
    public partial class AddProfesor : Window, INotifyPropertyChanged
    {
        public ProfesorDTO Profesor { get; set; }
        //private ProfesorDAO profesorsDAO;
        private ProfesorController profesorController;


        public event PropertyChangedEventHandler? PropertyChanged;

        public AddProfesor(ProfesorController pc) 
        {
            InitializeComponent();

            cmbZvanje.ItemsSource = new List<string>() {"redovni profesor", "vanredni profesor", "docent" };

            DataContext = this;
            Profesor = new ProfesorDTO();
            this.profesorController = pc;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                profesorController.AddProfesor(Profesor.toProfesor());
                MessageBox.Show("Profesor je uspesno dodat!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                //MessageBox.Show("Popunite sva polja pre potvrde", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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
                (txtBoxIme, "Unesite validno ime.", s => s.All(char.IsLetter)),
                (txtBoxPrezime, "Unesite validno prezime.", s => s.All(char.IsLetter)),
                (dpDatumRodjenja, "Unesite validan datum rodjenja u formatu d.M.yyyy.", s => DateTime.TryParseExact(s, "d.M.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)),
                (txtBoxUlica, "Unesite validnu adresu ulice.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxBroj, "Unesite validan broj.", s => s.All(char.IsDigit)),
                (txtBoxGrad, "Unesite validan grad.", s => s.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))),
                (txtBoxDrzava, "Unesite validnu drzavu (samo slova).", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxKontakt, "Unesite validni kontakt telefon.", s => s.All(c => char.IsDigit(c) || c == '+')),
                (txtBoxEmail, "Unesite validnu email adresu.", s => s.Contains("@")),
                (txtBoxBrojLicneKarte, "Unesite validan broj licne karte.", s => s.All(char.IsDigit)),
                (txtBoxGodinaStaza, "Unesite validnu godinu staza.", s => s.All(char.IsDigit))
            };

            foreach (var validation in validations)
            {
                if (string.IsNullOrWhiteSpace(validation.textBox.Text) || !validation.validator(validation.textBox.Text))
                {
                    MessageBox.Show(validation.message);
                    return false;
                }
            }

            if (cmbZvanje.SelectedItem == null)
            {
                MessageBox.Show("Izaberite zvanje.");
                return false;
            }

            return true;
        }

    }
}
