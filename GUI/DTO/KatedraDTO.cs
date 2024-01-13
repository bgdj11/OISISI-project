using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.DTO
{
    public class KatedraDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private string naziv { get; set; }
        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        private string profesor { get; set; }
        public string Profesor
        {
            get
            {
                return profesor;
            }
            set
            {
                if (profesor != value)
                {
                    profesor = value;
                    OnPropertyChanged("Profesor");
                }
            }
        }

        public KatedraDTO() { }


        public KatedraDTO(CLI.Model.Katedra katedra, CLI.Model.Profesor prof)
        {
            naziv = katedra.nazivKatedre;
            if (prof != null)
            {
                profesor = prof.Ime + " " + prof.Prezime;

            }
        }

    }
}
