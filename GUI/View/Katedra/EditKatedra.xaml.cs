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
using CLI.Controller;
using GUI.View.Predmet;
using CLI.Observer;

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for EditKatedra.xaml
    /// </summary>
    public partial class EditKatedra : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProfesorDTO> Profesors { get; set; }

        public ObservableCollection<PredmetDTO> Predmeti { get; set; }

        public KatedraDTO Katedra { get; set; }

        public ProfesorDTO SelectedProfesor { get; set; }

        private KatedraController katedraController;

        private ProfesorController profesorController;

        private PredmetController predmetController;


        public EditKatedra(KatedraController katedraController, KatedraDTO katedraDTO, ProfesorController profesorDAO)
        {
            InitializeComponent();
            DataContext = this;

            this.profesorController = profesorDAO;

            this.katedraController = katedraController;
            this.Katedra = katedraDTO;

            Profesors = new ObservableCollection<ProfesorDTO>();
            Predmeti = new ObservableCollection<PredmetDTO>();

            predmetController = new PredmetController();

            Update();

        }

        public void Update()
        {
            katedraController.MakeKatedra();

            CLI.Model.Katedra k = katedraController.GetKatedraById(Katedra.katedraId);

            KatedraDTO kat = new KatedraDTO(k);

            // Mora da se prepise da se ne bi menjala referenca
            
            Katedra.Profesor = kat.Profesor;
            Katedra.IdSefa = kat.IdSefa;
            Katedra.profesorIds = kat.profesorIds;

            Profesors.Clear();
            Predmeti.Clear();

            if (Katedra.profesorIds.Count() != 0)
            {
                foreach (int i in Katedra.profesorIds)
                {

                    Profesors.Add(new ProfesorDTO(profesorController.GetProfesorById(i)));

                    foreach(CLI.Model.Predmet pr in predmetController.GetAllPredmet())
                    {
                        if(pr.IdProfesora == i)
                        {
                            Predmeti.Add(new PredmetDTO(pr));
                        }
                    }

                }
            }

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Katedra katedraM = Katedra.toKatedra();
                katedraM.idKatedre = Katedra.katedraId;
                katedraController.UpdateKatedra(katedraM);
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
                (txtBoxNaziv, "Unesite validan naziv katedre", s => s.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
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
            var selectSefWindow = new SelectSef(katedraController, Katedra);
            selectSefWindow.Owner = this;
            selectSefWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectSefWindow.ShowDialog();
            Update();

        }
        
        private void RemoveSef_Click(object sender, RoutedEventArgs e) 
        { 
            

        }

        private void DodajProfesora_Click(object sender, RoutedEventArgs e)
        {
            var chooseProfesorWindow = new ChooseProfesor(Katedra, profesorController);
            chooseProfesorWindow.Owner = this;
            chooseProfesorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            chooseProfesorWindow.ShowDialog();
            Update();

        }

        private void UkloniProfesora_Click(object sender, RoutedEventArgs e) 
        {
            if(SelectedProfesor == null)
            {
                MessageBox.Show(this, "Izaberi profesora.");
            } else
            {
                CLI.Model.Profesor p = profesorController.GetProfesorById(SelectedProfesor.IdProfesor);

                if (p == null)
                    MessageBox.Show("P je NULL");
                p.IdKatedre = -1;
                profesorController.UpdateProfesor(p);
                MessageBox.Show(this, "Profesor uspesno uklonjen.");
                Update();
            }

        }

        private void txtBoxProfesor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
