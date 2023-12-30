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

namespace GUI.View.Student
{
    /// <summary>
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public StudentDTO Student { get; set; }
        private StudentDAO studentDAO;

        public ObservableCollection<PredmetDTO> NotPassedSubjects { get; set; }  // lista nepolozenih
        public PredmetDTO SelectedSubject { get; set; } // selektovan nepolozen
        private PredmetDAO predmetDAO;

        List<int> Godine;
        List<string> Statusi;

        public EditStudent(StudentDAO studentDAO, StudentDTO selectedStudent)
        {
            InitializeComponent();
            DataContext = this;
            this.studentDAO = studentDAO;
            Student = selectedStudent;

            NotPassedSubjects = new ObservableCollection<PredmetDTO>();
            predmetDAO = new PredmetDAO();
            SelectedSubject = new PredmetDTO();

            Godine = new List<int> { 1, 2, 3, 4 };
            cmbGodinaStudija.ItemsSource = Godine;

            Statusi = new List<string> { "samofinasiranje", "budzet" };
            cmbStatusStudenta.ItemsSource = Statusi;

            Update();
        }

        private void Update()
        {
            if (Student.notPassedIds != null && Student.notPassedIds.Any())
            {
                NotPassedSubjects.Clear();
                foreach (int i in Student.notPassedIds)
                {
                    NotPassedSubjects.Add(new PredmetDTO(predmetDAO.GetPredmetById(i)));
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
                CLI.Model.Student sr = Student.toStudent();
                sr.IdStudent = Student.StudentId;

                sr.IdAdrese = Student.idAdrese;
                sr.AdresaStanovanja.IdAdrese = Student.idAdrese;
                
                sr.IdIndeksa = Student.idIndeksa;
                sr.Indeks.idIndeksa = Student.idIndeksa;

                

                studentDAO.UpdateStudent(sr);
                MessageBox.Show("Student je uspesno promenjen!", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
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
                   !string.IsNullOrWhiteSpace(txtBoxUlica.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxBroj.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxGrad.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxDrzava.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxKontakt.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxOznakaSmera.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxBrojIndeksa.Text) &&
                   !string.IsNullOrWhiteSpace(txtBoxGodinaUpisa.Text) &&
                   cmbGodinaStudija.SelectedItem != null &&
                   cmbStatusStudenta.SelectedItem != null;
                   //!string.IsNullOrWhiteSpace(txtBoxProsecnaOcena.Text);
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            var selectSubjectWindow  = new SelectSubject(predmetDAO);
            selectSubjectWindow.Owner = this;
            selectSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectSubjectWindow.ShowDialog();
            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddGrade_Click(object sender, RoutedEventArgs e) 
        {
        
        }

    }
}
