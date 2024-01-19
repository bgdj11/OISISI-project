using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GUI.DTO;
using CLI.DAO;

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for EditKatedra.xaml
    /// </summary>
    public partial class EditKatedra : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProfesorDTO> Profesors { get; set; }

        public KatedraDTO Katedra { get; set; }

        private KatedraDAO katedraDAO;

        private ProfesorDAO profesorDAO;


        public EditKatedra(KatedraDAO katedraDAO, KatedraDTO katedraDTO)
        {
            InitializeComponent();
            DataContext = this;

            profesorDAO = new ProfesorDAO();

            this.katedraDAO = katedraDAO;
            this.Katedra = katedraDTO;

            Profesors = new ObservableCollection<ProfesorDTO>();

            Update();

        }

        public void Update()
        {
            katedraDAO.MakeKatedra();

            CLI.Model.Katedra k = katedraDAO.GetKatedraById(Katedra.katedraId);

            Katedra = new KatedraDTO(k);

            Profesors.Clear();

            if (Katedra.profesorIds.Count() != 0)
            {
                foreach (int i in Katedra.profesorIds)
                {

                    Profesors.Add(new ProfesorDTO(profesorDAO.GetProfesorById(i)));

                }
            }
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                katedraDAO.UpdateKatedra(Katedra.toKatedra());
                MessageBox.Show("Katedra je uspesno izmenjena!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
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
                (txtBoxSifra, "Unesite validnu sifru katedre", s => s.All(c => char.IsLetterOrDigit(c))),
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


        private void AddSef_Click(object sender, RoutedEventArgs e) 
        {
            
            
        }
        
        private void RemoveSef_Click(object sender, RoutedEventArgs e) 
        { 
            

        }

        private void DodajProfesora_Click(object sender, RoutedEventArgs e)
        {
            var chooseProfesorWindow = new ChooseProfesor(Katedra);
            chooseProfesorWindow.Owner = this;
            chooseProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            chooseProfesorWindow.ShowDialog();
            Update();

        }

        private void UkloniProfesora_Click(object sender, RoutedEventArgs e) 
        {
            

        }

    }
}
