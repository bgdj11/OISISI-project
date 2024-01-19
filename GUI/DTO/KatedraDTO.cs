using CLI.DAO;
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
        
        public List<int> profesorIds { get; set; }

        public KatedraDTO() {
            idSefa = -1;
            profesorIds = new List<int>();
        }


        public KatedraDTO(CLI.Model.Katedra katedra)
        {
            naziv = katedra.nazivKatedre;
            if(katedra.idSefa == -1)
            {
                profesor = "sef nije dodat";
            }
            
            idSefa = katedra.idSefa;

            if (katedra.sefKatedre != null)
            {
                profesor = katedra.sefKatedre.Ime + " " + katedra.sefKatedre.Prezime;

            }
            sifra = katedra.sifra;

            katedraId = katedra.idKatedre;

            profesorIds = new List<int>();

            if(katedra.profesoriNaKatedri.Count() != 0)
            {
                foreach(Profesor p in katedra.profesoriNaKatedri)
                {
                    profesorIds.Add(p.IdProfesor);
                }
            }
        }

        public Katedra toKatedra()
        {
            return new Katedra(sifra, naziv, idSefa);
        }

        public KatedraDTO clone()
        {
            KatedraDTO ret = new KatedraDTO();
            ret.Sifra = this.Sifra;
            ret.profesorIds = this.profesorIds;
            ret.Profesor = this.Profesor;
            ret.idSefa = this.idSefa;
            ret.Naziv = this.Naziv;
            ret.katedraId = this.katedraId;

            return ret;
        }

    }
}
