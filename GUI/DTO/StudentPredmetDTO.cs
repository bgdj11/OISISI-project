using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class StudentPredmetDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private string indeks;
        public string Indeks
        {
            get
            {
                return indeks;
            }
            set
            {
                if (indeks != value)
                {
                    indeks = value;
                    OnPropertyChanged("Indeks");
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

        public StudentPredmetDTO()
        {

        }

        //public StudentPredmetDTO(Student student, Predmet predmet)
        public StudentPredmetDTO(Student student, Predmet predmet)
        {
            Ime = student.Ime;
            Prezime = student.Prezime;
            Indeks = student.Indeks.ToString();
            NazivPredmeta = predmet.NazivPredmeta;

        }
    }
}
