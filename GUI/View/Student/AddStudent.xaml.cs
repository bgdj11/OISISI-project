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

namespace GUI.View.Student
{

    public partial class AddStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO Student { get; set; }

        //private StudentDAO studentsDAO;
        private StudentController studentController;

        public event PropertyChangedEventHandler? PropertyChanged;

        List<int> Godine;
        List<string> Statusi;

        //public AddStudent(StudentDAO studentsDAO)
        public AddStudent(StudentController studentController)
        {
            InitializeComponent();

            DataContext = this;
            Student = new StudentDTO();
            this.studentController = studentController;

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
                studentController.AddStudent(Student.toStudent());
                studentController.MakeStudent();
                MessageBox.Show("Student je uspesno dodat!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
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
                (datpDatumRodjenja, "Unesite validan datum u formatu d.M.yyyy.", s => DateTime.TryParseExact(s, "d.M.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)),
                (txtBoxUlica, "Unesite validnu ulicu.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxBroj, "Unesite validan broj.", s => s.All(char.IsDigit)),
                (txtBoxGrad, "Unesite validan grad.", s => s.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))),
                (txtBoxDrzava, "Unesite validnu drzavu.", s => s.All(char.IsLetter)),
                (txtBoxKontakt, "Unesite kontakt informacije (samo brojevi).", s => s.All(char.IsDigit)),
                (txtBoxEmail, "Unesite validnu email adresu (sadrži '@').", s => s.Contains("@")),
                (txtBoxOznakaSmera, "Unesite oznaku smera (ne sme biti prazno).", s => !string.IsNullOrWhiteSpace(s)),
                (txtBoxBrojIndeksa, "Unesite broj indeksa (samo brojevi).", s => s.All(char.IsDigit)),
                (txtBoxGodinaUpisa, "Unesite validnu godinu upisa (samo brojevi).", s => s.All(char.IsDigit)),
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
                MessageBox.Show("Izaberite godinu studija.");
                return false;
            }


            if (cmbStatusStudenta.SelectedItem == null)
            {
                MessageBox.Show("Izaberite status studenta.");
                return false;
            }


            return true;
        }

    }
}

