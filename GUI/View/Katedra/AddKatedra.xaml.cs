using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CLI.DAO;
using System.Runtime.CompilerServices;
using CLI.Controller;

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for AddKatedra.xaml
    /// </summary>
    public partial class AddKatedra : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public KatedraDTO Katedra { get; set; }
        private KatedraController katedraDAO;


        public AddKatedra(KatedraController katDAO)
        {
            InitializeComponent();
            DataContext = this;
            Katedra = new KatedraDTO();
            this.katedraDAO = katDAO;

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                katedraDAO.AddKatedra(Katedra.toKatedra());
                MessageBox.Show("katedra je uspesno dodata!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
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
                (txtBoxSifraKatedre, "Unesite validnu sifru katedre", s => s.All(c => char.IsLetterOrDigit(c))),
                (txtBoxNaziv, "Unesite validan naziv katedre", s => s.All(char.IsLetter))
            };

            foreach (var validation in validations)
            {
                if (string.IsNullOrWhiteSpace(validation.textBox.Text) || !validation.validator(validation.textBox.Text))
                {
                    MessageBox.Show(validation.message, "Validacija", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }

    }
}
