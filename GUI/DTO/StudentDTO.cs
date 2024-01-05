using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CLI.Model;

namespace GUI.DTO
{
     public class StudentDTO : INotifyPropertyChanged
    {
        private int studentId { get; set; }
        public int StudentId
        {
            get
            {
                return studentId;
            }
            set
            {
                if (studentId != value)
                {
                    studentId = value;
                    OnPropertyChanged("StudentId");
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

        private string datumRodjenja { get; set; }
        public string DatumRodjenja
        {
            get
            {
                return datumRodjenja;
            }
            set
            {
                if (datumRodjenja != value)
                {
                    datumRodjenja = value;
                    OnPropertyChanged("DatumRodjenja");
                }
            }
        }

        private string adresaStanovanja { get; set; }
        public string AdresaStanovanja
        {
            get
            {
                return adresaStanovanja;
            }
            set
            {
                if (adresaStanovanja != value)
                {
                    adresaStanovanja = value;
                    OnPropertyChanged("AdresaStanovanja");
                }
            }
        }

        private string ulica { get; set; }
        public string Ulica
        {
            get
            {
                return ulica;
            }
            set
            {
                if (ulica != value)
                {
                    ulica = value;
                    OnPropertyChanged("Ulica");
                }
            }
        }

        private string grad { get; set; }
        public string Grad
        {
            get
            {
                return grad;
            }
            set
            {
                if (grad != value)
                {
                    grad = value;
                    OnPropertyChanged("Grad");
                }
            }
        }

        private int broj { get; set; }
        public int Broj
        {
            get
            {
                return broj;
            }
            set
            {
                if (broj != value)
                {
                    broj = value;
                    OnPropertyChanged("Broj");
                }
            }
        }

        private string drzava { get; set; }
        public string Drzava
        {
            get
            {
                return drzava;
            }
            set
            {
                if (drzava != value)
                {
                    drzava = value;
                    OnPropertyChanged("Drzava");
                }
            }
        }

        private string kontaktTelefon { get; set; }
        public string KontaktTelefon
        {
            get
            {
                return kontaktTelefon;
            }
            set
            {
                if (kontaktTelefon != value)
                {
                    kontaktTelefon = value;
                    OnPropertyChanged("KontaktTelefon");
                }
            }
        }

        private string emailAdresa { get; set; }
        public string EmailAdresa
        {
            get
            {
                return emailAdresa;
            }
            set
            {
                if (emailAdresa != value)
                {
                    emailAdresa = value;
                    OnPropertyChanged("EmailAdresa");
                }
            }
        }

        public int idAdrese { get; set; }

        public int idIndeksa { get; set; }

        private string indeks { get; set; }
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

        private string oznakaSmera { get; set; }
        public string OznakaSmera
        {
            get
            {
                return oznakaSmera;
            }
            set
            {
                if (oznakaSmera != value)
                {
                    oznakaSmera = value;
                    OnPropertyChanged("OznakaSmera");
                }
            }
        }

        private int brojIndeksa { get; set; }
        public int BrojIndeksa
        {
            get
            {
                return brojIndeksa;
            }
            set
            {
                if (brojIndeksa != value)
                {
                    brojIndeksa = value;
                    OnPropertyChanged("BrojIndeksa");
                }
            }
        }

        private int godinaUpisa { get; set; }
        public int GodinaUpisa
        {
            get
            {
                return godinaUpisa;
            }
            set
            {
                if (godinaUpisa != value)
                {
                    godinaUpisa = value;
                    OnPropertyChanged("GodinaUpisa");
                }
            }
        }


        private int trenutnaGodinaStudija { get; set; }
        public int TrenutnaGodinaStudija
        {
            get
            {
                return trenutnaGodinaStudija;
            }
            set
            {
                if (trenutnaGodinaStudija != value)
                {
                    trenutnaGodinaStudija = value;
                    OnPropertyChanged("TrenutnaGodinaStudija");
                }
            }
        }

        private string statusStudenta { get; set; }
        public string StatusStudenta
        {
            get
            {
                return statusStudenta;
            }
            set
            {
                if (statusStudenta != value)
                {
                    statusStudenta = value;

                    OnPropertyChanged("StatusStudenta");
                }
            }
        }

        private double prosecnaOcena { get; set; }

        public double ProsecnaOcena
        {
            get
            {
                return prosecnaOcena;
            }
            set
            {
                if (prosecnaOcena != value)
                {
                    prosecnaOcena = value;
                    OnPropertyChanged("ProsecnaOcena");
                }
            }
        }


        private int ukupnoEspb { get; set; }

        public int UkupnoEspb
        {
            get
            {
                return ukupnoEspb;
            }
            set
            {
                if (ukupnoEspb != value)
                {
                    ukupnoEspb = value;
                    OnPropertyChanged("UkupnoEspb");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public List<int> notPassedIds {  get; set; }
        public List<int> gradesIds { get; set; }


        public StudentDTO()
        {
            notPassedIds = new List<int>();
            gradesIds = new List<int>();
        }


        public Student toStudent()
        {
            
            return new Student(prezime, ime, datumRodjenja, ulica, broj, grad, drzava, kontaktTelefon, emailAdresa, oznakaSmera, brojIndeksa, godinaUpisa, trenutnaGodinaStudija, statusStudenta);
        }


        public StudentDTO(Student student)
        {
            StudentId = student.IdStudent;
            Ime = student.Ime;
            Prezime = student.Prezime;
            DatumRodjenja = student.DatumRodjenja.ToString();
            AdresaStanovanja = student.AdresaStanovanja.ToString();
            Ulica = student.AdresaStanovanja.Ulica;
            Grad = student.AdresaStanovanja.Grad;
            Broj = student.AdresaStanovanja.Broj;
            Drzava = student.AdresaStanovanja.Drzava;
            KontaktTelefon = student.KontaktTelefon;
            EmailAdresa = student.EmailAdresa;
            idAdrese = student.IdAdrese;
            idIndeksa = student.IdIndeksa;
            Indeks = student.Indeks.ToString();
            oznakaSmera = student.Indeks.oznakaSmera;
            godinaUpisa = student.Indeks.godinaUpisa;
            brojIndeksa = student.Indeks.brojUpisa;
            TrenutnaGodinaStudija = student.TrenutnaGodinaStudija;
            StatusStudenta = student.Status.ToString();
            prosecnaOcena = student.ProsecnaOcena;
            ukupnoEspb = -1;


            notPassedIds = new List<int>();
            gradesIds = new List<int>();
            
            if(student.NepolozeniIspiti.Any())
            {
                foreach (Predmet p in student.NepolozeniIspiti)
                {
                    notPassedIds.Add(p.IdPredmet);
                }
            }


            //ukupnoEspb = 0;

            if(student.PolozeniIspiti.Any())
            {
                foreach (OcenaNaUpisu o in student.PolozeniIspiti)
                {
                    gradesIds.Add(o.IdOcene);
                    //ukupnoEspb += o.Predmet.brojESPB;
                }
            }


        }


        public StudentDTO Clone()
        {
            return new StudentDTO
            {
                Ime = this.Ime,
                Prezime = this.Prezime,
                StudentId = this.StudentId,
                DatumRodjenja = this.DatumRodjenja,
                AdresaStanovanja = this.AdresaStanovanja,
                Grad = this.Grad,
                Ulica = this.Ulica,
                Drzava = this.Drzava,
                broj = this.Broj,
                KontaktTelefon = this.KontaktTelefon,
                EmailAdresa = this.EmailAdresa,
                Indeks = this.Indeks,
                OznakaSmera = this.OznakaSmera,
                BrojIndeksa = this.BrojIndeksa,
                GodinaUpisa = this.GodinaUpisa,
                StatusStudenta = this.StatusStudenta,
                TrenutnaGodinaStudija = this.TrenutnaGodinaStudija,
                prosecnaOcena = this.prosecnaOcena,
                ukupnoEspb = this.ukupnoEspb,
                idAdrese = this.idAdrese,
                idIndeksa = this.idIndeksa,
                notPassedIds = this.notPassedIds,
                gradesIds = this.gradesIds


            };
        }
    }
}
