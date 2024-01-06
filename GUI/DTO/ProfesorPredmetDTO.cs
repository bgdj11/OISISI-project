using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfesorPredmetDTO : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string ime { get; set; }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        private string prezime { get; set; }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                if (prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        private string nazivPredmeta { get; set; }

        public string NazivPredmeta
        {
            get
            {
                return nazivPredmeta;
            }
            set
            {
                if (nazivPredmeta != value)
                {
                    nazivPredmeta = value;
                    OnPropertyChanged("NazivPredmeta");
                }
            }
        }

        public ProfesorPredmetDTO() { }

        public ProfesorPredmetDTO(CLI.Model.Profesor profesor, CLI.Model.Predmet predmet)
        {
            Ime = profesor.Ime;
            Prezime = profesor.Prezime;
            NazivPredmeta = predmet.NazivPredmeta;
        }


    }
}
