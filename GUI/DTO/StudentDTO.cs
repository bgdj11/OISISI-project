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



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public StudentDTO()
        {
        }


        public Student toStudent()
        {

            return new Student(prezime, ime, datumRodjenja, adresaStanovanja, kontaktTelefon, emailAdresa, indeks, trenutnaGodinaStudija, statusStudenta, prosecnaOcena);
        }


        public StudentDTO(Student student)
        {
            StudentId = student.IdStudent;
            ime = student.Ime;
            prezime = student.Prezime;
            datumRodjenja = student.DatumRodjenja.ToString();
            adresaStanovanja = student.AdresaStanovanja.ToString();
            kontaktTelefon = student.KontaktTelefon;
            emailAdresa = student.EmailAdresa;

            indeks = student.Indeks.ToString();
            trenutnaGodinaStudija = student.TrenutnaGodinaStudija;
            statusStudenta = student.Status.ToString();
            prosecnaOcena = student.ProsecnaOcena;
        }
    }
}
