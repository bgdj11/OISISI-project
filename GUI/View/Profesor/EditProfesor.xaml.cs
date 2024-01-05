﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CLI.Model;
using GUI.DTO;

namespace GUI.View.Profesor
{
    /// <summary>
    /// Interaction logic for EditProfesor.xaml
    /// </summary>
    public partial class EditProfesor : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ProfesorDTO Profesor { get; set; }
        private ProfesorDAO profesorDAO;


        public ObservableCollection<PredmetDTO> Predmeti { get; set; }

        public PredmetDTO SelectedPredmet { get; set; }
        private PredmetDAO predmetDAO;
        private ProfesorPredmetDAO profesorPredmetDAO;


        public EditProfesor(ProfesorDAO profesorDAO, ProfesorDTO selectedProfesor)
        {
            InitializeComponent();
            DataContext = this;
            this.profesorDAO = profesorDAO;
            Profesor = selectedProfesor;

            Predmeti = new ObservableCollection<PredmetDTO>();
            predmetDAO = new PredmetDAO();
            SelectedPredmet = new PredmetDTO();

            profesorPredmetDAO = new ProfesorPredmetDAO();

            Update();
        }


        public void Update()
        {
            profesorDAO.MakeProfesor();
            predmetDAO.MakePredmet();

            if (Profesor.PredmetiListaId != null)
            {
                Predmeti.Clear();
                foreach (int i in Profesor.PredmetiListaId)
                {
                    Predmeti.Add(new PredmetDTO(predmetDAO.GetPredmetById(i)));
                }
            }

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CLI.Model.Profesor pr = Profesor.toProfesor();
                pr.IdProfesor = Profesor.IdProfesor;
                pr.IdAdrese = Profesor.idAdrese;
                pr.AdresaStanovanja.IdAdrese = Profesor.idAdrese;

                profesorDAO.UpdateProfesor(pr);
                MessageBox.Show("Profesor je uspesno promenjen!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void btnDodajPredmet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUkloniPredmet_Click(Object sender, RoutedEventArgs e)
        {

        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtBoxIme.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxPrezime.Text) &&
                   !string.IsNullOrWhiteSpace(dpDatumRodjenja.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxAdresa.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxKontakt.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxBrojLicneKarte.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxZvanje.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxGodinaStaza.Text);
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DodajPredmet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UkloniPredmet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
