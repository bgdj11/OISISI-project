using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CLI.Model;


namespace GUI.DTO
{
    public class PredmetDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int predmetId { get; set; }
        private string sifraPredmeta { get; set; }

        public string SifraPredmeta
        {
            get
            {
                return sifraPredmeta;
            }
            set
            {
                if (sifraPredmeta != value)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged("SifraPredmeta");
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

        private string semestar { get; set;}

        public string Semestar
        {
            get
            {
                return semestar;
            }
            set
            {
                if(semestar != value)
                {
                    semestar = value;
                    OnPropertyChanged("Semestar");
                }
            }
        }

        private int godinaStudija { get; set;}

        public int GodinaStudija
        {
            get
            {
                return godinaStudija;
            }
            set
            {
                if(godinaStudija != value)
                {
                    godinaStudija = value;
                    OnPropertyChanged("GodinuStudija");
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
                if(profesor != value)
                {
                    profesor = value;
                    OnPropertyChanged("Profesor");
                }
            }
        }

        private int profesorID { get; set;}
        public int ProfesorID
        {
            get
            {
                return profesorID;
            }
            set
            {
                if(profesorID != value)
                {
                    profesorID = value;
                    OnPropertyChanged("ProfesorID");
                }
            }
        }

        private int espb { get; set; }
        public int ESPB
        {
            get
            {
                return espb;
            }
            set
            {
                if (espb != value)
                {
                    espb = value;
                    OnPropertyChanged("ESPB");
                }
            }
        }

        public PredmetDTO() { }

        public Predmet toPredmet()
        {
            return new Predmet(sifraPredmeta, nazivPredmeta, semestar, godinaStudija, profesorID, espb);
        }

        public PredmetDTO(Predmet predmet)
        {
            predmetId = predmet.idPredmet;
            sifraPredmeta = predmet.sifraPredmeta;
            nazivPredmeta = predmet.nazivPredmeta;
            semestar = predmet.semestar.ToString();
            godinaStudija = predmet.godinaStudija;
            if(predmet.profesor != null)
            {
                profesor = predmet.profesor.Ime + " " + predmet.profesor.Prezime;
                profesorID = predmet.idProfesora;
            } else
            {
                profesor = "nedostaje profesor";
                profesorID = -1;
            }


            espb = predmet.brojESPB;
        }

        public PredmetDTO clone()
        {
            PredmetDTO pr = new PredmetDTO();

            pr.predmetId = this.predmetId;
            pr.sifraPredmeta = this.sifraPredmeta;
            pr.nazivPredmeta = this.nazivPredmeta;
            pr.semestar = this.semestar;
            pr.godinaStudija = this.godinaStudija;
            pr.profesor = this.profesor;
            pr.profesorID = this.profesorID;
            pr.espb = this.ESPB;

            return pr;
        }

    }
}
