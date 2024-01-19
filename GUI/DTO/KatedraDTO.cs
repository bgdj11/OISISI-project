using CLI.Model;
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

        private string sifra { get; set; }
        public string Sifra
        {
            get
            {
                return sifra;
            }
            set
            {
                if(sifra != value)
                {
                    sifra = value;
                    OnPropertyChanged("Sifra");
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

        private int idSefa { get; set; }

        public int katedraId { get; set; }
        
        public KatedraDTO() {
            idSefa = -1;
        }


        public KatedraDTO(CLI.Model.Katedra katedra, CLI.Model.Profesor prof)
        {
            naziv = katedra.nazivKatedre;
            if(katedra.idSefa == -1)
            {
                profesor = "sef nije dodat";
            }
            
            idSefa = katedra.idSefa;

            if (prof != null)
            {
                profesor = prof.Ime + " " + prof.Prezime;

            }
            sifra = katedra.sifra;

            katedraId = katedra.idKatedre;
        }

        public Katedra toKatedra()
        {
            return new Katedra(sifra, naziv, idSefa);
        }

    }
}
