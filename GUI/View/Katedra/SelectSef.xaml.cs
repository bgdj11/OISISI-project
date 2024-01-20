using GUI.DTO;
using System;
using System.Collections.Generic;
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
using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using System.Collections.ObjectModel;

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for SelectSef.xaml
    /// </summary>
    public partial class SelectSef : Window, IObserver
    {

        public ObservableCollection<ProfesorDTO> Profesori { get; set; }
        public KatedraDTO selectedKatedra { get; set; }

        public ProfesorDTO SelectedProfesor { get; set; }
        private ProfesorController profesorController;
        private KatedraController katedraController;


        public SelectSef(KatedraController kc, KatedraDTO kt)
        {
            InitializeComponent();
            DataContext = this;
            Profesori = new ObservableCollection<ProfesorDTO>();
            profesorController = new ProfesorController();
            katedraController = kc;
            selectedKatedra = kt;

            Update();

        }

        public void Update()
        {
            Profesori.Clear();
            foreach(CLI.Model.Profesor profesor in profesorController.GetAllProfesor())
            {
                if(profesor.IdKatedre == selectedKatedra.katedraId)
                {
                    Profesori.Add(new ProfesorDTO(profesor));
                }
            }
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedProfesor == null)
            {
                MessageBox.Show(this, "Izaberi profesora.");
            } else
            {
                if(!((SelectedProfesor.Zvanje == "redovni profesor" || SelectedProfesor.Zvanje == "vanredni profesor") && (SelectedProfesor.GodineStaza > 5)))
                {
                    MessageBox.Show(this, "Izaberi profesora koji zadovoljava uslove.");
                } else
                {
                    CLI.Model.Katedra k = selectedKatedra.toKatedra();
                    k.idKatedre = selectedKatedra.katedraId;
                    k.idSefa = SelectedProfesor.IdProfesor;
                    katedraController.UpdateKatedra(k);
                    MessageBox.Show(this, "Sef uspesno postavljen.");
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
