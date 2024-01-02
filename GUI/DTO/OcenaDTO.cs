using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class OcenaDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int idOcene { get; set; }

        private int idStudenta;
        public int IdStudenta
        {
            get { return idStudenta; }
            set
            {
                if (idStudenta != value)
                {
                    idStudenta = value;
                    OnPropertyChanged(nameof(IdStudenta));
                }
            }
        }

        private Student student;
        public Student Student
        {
            get { return student; }
            set
            {
                if (student != value)
                {
                    student = value;
                    OnPropertyChanged(nameof(Student));
                }
            }
        }

        private int idPredmeta;
        public int IdPredmeta
        {
            get { return idPredmeta; }
            set
            {
                if (idPredmeta != value)
                {
                    idPredmeta = value;
                    OnPropertyChanged(nameof(IdPredmeta));
                }
            }
        }

        private Predmet predmet;
        public Predmet Predmet
        {
            get { return predmet; }
            set
            {
                if (predmet != value)
                {
                    predmet = value;
                    OnPropertyChanged(nameof(Predmet));
                }
            }
        }

        private string sifraPredmeta;
        public string SifraPredmeta
        {
            get { return sifraPredmeta; }
            set
            {
                if (sifraPredmeta != value)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged(nameof(SifraPredmeta));
                }
            }
        }

        private string nazivPredmeta;
        public string NazivPredmeta
        {
            get { return nazivPredmeta; }
            set
            {
                if (nazivPredmeta != value)
                {
                    nazivPredmeta = value;
                    OnPropertyChanged(nameof(NazivPredmeta));
                }
            }
        }

        private int brojESPB;
        public int BrojESPB
        {
            get { return brojESPB; }
            set
            {
                if (brojESPB != value)
                {
                    brojESPB = value;
                    OnPropertyChanged(nameof(BrojESPB));
                }
            }
        }

        private string datum;
        public string Datum
        {
            get { return datum; }
            set
            {
                if (datum != value)
                {
                    datum = value;
                    OnPropertyChanged(nameof(Datum));
                }
            }
        }

        private int ocena;
        public int Ocena
        {
            get { return ocena; }
            set
            {
                if (ocena != value)
                {
                    ocena = value;
                    OnPropertyChanged(nameof(Ocena));
                }
            }
        }

        public OcenaDTO(OcenaNaUpisu o)
        {
            idOcene = o.IdOcene;
            idStudenta = o.IdStudenta;
            student = o.Student;
            idPredmeta = o.IdPredmeta;
            predmet = o.Predmet;
            datum = o.Datum.ToString();
            ocena = o.Ocena;
            sifraPredmeta = o.Predmet.sifraPredmeta;
            nazivPredmeta = o.Predmet.nazivPredmeta;
            brojESPB = o.Predmet.brojESPB;
        }

        public OcenaDTO()
        {

        }

        public OcenaNaUpisu toOcena()
        {
            return new OcenaNaUpisu(student, predmet, datum, ocena);
        }


    }
}
