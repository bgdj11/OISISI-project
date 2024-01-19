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
using System.Xml.Linq;
using CLI.DAO;
using CLI.Observer;
using GUI.DTO;

namespace GUI.View.Katedra
{
    /// <summary>
    /// Interaction logic for ChooseProfesor.xaml
    /// </summary>
    public partial class ChooseProfesor : Window, IObserver
    {

        public List<ProfesorDTO> Profesors { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }

        private ProfesorDAO profesorDAO;
        private KatedraDAO katedraDAO;

        public KatedraDTO Katedra;

        public ChooseProfesor(KatedraDTO katedraDTO)
        {
            InitializeComponent();
            DataContext = this;
            Profesors = new List<ProfesorDTO>();
            profesorDAO = new ProfesorDAO();
            katedraDAO = new KatedraDAO();
            Katedra = katedraDTO;

            Update();
        }

        public void Update()
        {
            Profesors.Clear();
            foreach(CLI.Model.Profesor p in profesorDAO.GetAllProfesors())
            {
                if(p.IdKatedre != Katedra.katedraId)
                {
                    Profesors.Add(new ProfesorDTO(p));
                }
            }
        }

        public void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfesor == null)
            {
                MessageBox.Show(this, "Izaberi profesora.");
            }
            else
            {
                CLI.Model.Profesor p = profesorDAO.GetProfesorById(SelectedProfesor.IdProfesor);

                if (p == null)
                    MessageBox.Show("P je NULL");

                p.IdKatedre = Katedra.katedraId;
                
                profesorDAO.UpdateProfesor(p);
                
                this.Close();
            }
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
