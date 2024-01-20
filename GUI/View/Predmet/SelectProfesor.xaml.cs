using CLI.Controller;
using CLI.DAO;
using CLI.Observer;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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

namespace GUI.View.Predmet
{
    /// <summary>
    /// Interaction logic for SelectProfesor.xaml
    /// </summary>
    public partial class SelectProfesor : Window, IObserver
    {
        public List<ProfesorDTO> Profesors { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }

        //private ProfesorDAO profesorDAO;
        private ProfesorController profesorController;
        private PredmetDTO subject;
        //private PredmetDAO predmetDAO;
        private PredmetController predmetController;

        public void Update()
        {
            Profesors.Clear();
            foreach (CLI.Model.Profesor profesor in profesorController.GetAllProfesor())
                Profesors.Add(new ProfesorDTO(profesor));
        }
        public SelectProfesor(PredmetController pr, PredmetDTO subjectDTO)
        {
            InitializeComponent();
            DataContext = this;
            Profesors = new List<ProfesorDTO>();
            profesorController = new ProfesorController();
            this.predmetController = pr;
            subject = subjectDTO;

            Update();
        }


        public void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedProfesor == null)
            {
                MessageBox.Show(this, "Izaberi profesora.");
            }
            else
            {   
                CLI.Model.Predmet pr = subject.toPredmet();
                pr.IdPredmet = subject.predmetId;
                pr.IdProfesora = SelectedProfesor.IdProfesor;

                //SelectedProfesor.PredmetiListaId.Add(pr.IdPredmet);

                predmetController.UpdatePredmet(pr);
                this.Close();
            }
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e) 
        { 
        
        }
    }
}
