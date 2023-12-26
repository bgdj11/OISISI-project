using System;
using System.ComponentModel;
using System.Windows;
using CLI.Model;

namespace GUI.DTO
{
    public class ProfesorDTO : INotifyPropertyChanged
    {
        private int idProfesor { get; set; }

        public int IdProfesor
        {
            get
            {
                return idProfesor;
            }
            set
            {
                if (idProfesor != value)
                {
                    idProfesor = value;
                    OnPropertyChanged("IdProfesor");
                }
            }
        }

        private string prezime { get; set; }
        public string Prezime
        {
            get { return prezime; }
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
            get { return ime; }
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
            get { return datumRodjenja; }
            set
            {
                if (datumRodjenja != value)
                {
                    datumRodjenja = value;
                    OnPropertyChanged("DatumRodjenja");
                }
            }
        }

        /*
        private DateOnly datumRodjenja { get; set; }
        public DateOnly DatumRodjenja
        {
            get { return datumRodjenja; }
            set
            {
                if (datumRodjenja != value)
                {
                    datumRodjenja = value;
                    OnPropertyChanged("DatumRodjenja");
                }
            }
        } */

        private string adresa { get; set; }
        public string Adresa
        {
            get { return adresa; }
            set
            {
                if (adresa != value)
                {
                    adresa = value;
                    OnPropertyChanged("Adresa");
                }
            }
        }

        private string kontaktTelefon { get; set; }
        public string KontaktTelefon
        {
            get { return kontaktTelefon; }
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
            get { return emailAdresa; }
            set
            {
                if (emailAdresa != value)
                {
                    emailAdresa = value;
                    OnPropertyChanged("EmailAdresa");
                }
            }
        }

        private string brojLicneKarte { get; set; }
        public string BrojLicneKarte
        {
            get { return brojLicneKarte; }
            set
            {
                if (brojLicneKarte != value)
                {
                    brojLicneKarte = value;
                    OnPropertyChanged("BrojLicneKarte");
                }
            }
        }

        private string zvanje { get; set; }
        public string Zvanje
        {
            get { return zvanje; }
            set
            {
                if (zvanje != value)
                {
                    zvanje = value;
                    OnPropertyChanged("Zvanje");
                }
            }
        }

        private int godineStaza { get; set; }
        public int GodineStaza
        {
            get { return godineStaza; }
            set
            {
                if (godineStaza != value)
                {
                    godineStaza = value;
                    OnPropertyChanged("GodineStaza");
                }
            }
        }

        private int idKatedre { get; set; }
        public int IdKatedre
        {
            get { return idKatedre; }
            set
            {
                if (idKatedre != value)
                {
                    idKatedre = value;
                    OnPropertyChanged("IdKatedre");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProfesorDTO()
        {
        }

        public ProfesorDTO(Profesor profesor)
        {
            IdProfesor = profesor.IdProfesor;
            Prezime = profesor.Prezime;
            Ime = profesor.Ime;
            DatumRodjenja = profesor.DatumRodjenja.ToString();
            Adresa = profesor.AdresaStanovanja.ToString();
            KontaktTelefon = profesor.KontaktTelefon;
            EmailAdresa = profesor.EmailAdresa;
            BrojLicneKarte = profesor.BrojLicneKarte;
            Zvanje = profesor.Zvanje;
            GodineStaza = profesor.GodineStaza;
            IdKatedre = profesor.IdKatedre;
        }


        public ProfesorDTO Clone()
        {
            return new ProfesorDTO
            {
                Ime = this.Ime,
                Prezime = this.Prezime,
                IdProfesor = this.IdProfesor,
                DatumRodjenja = this.DatumRodjenja,
                Adresa = this.Adresa,
                KontaktTelefon = this.KontaktTelefon,
                EmailAdresa = this.EmailAdresa,
                BrojLicneKarte = this.BrojLicneKarte,
                Zvanje = this.Zvanje,
                GodineStaza = this.GodineStaza,
                IdKatedre = this.IdKatedre

        };
        }


        public Profesor toProfesor()
        {
            return new Profesor(Prezime, Ime, Adresa, DatumRodjenja, KontaktTelefon, EmailAdresa, BrojLicneKarte, Zvanje, GodineStaza, IdKatedre);
        }
    }
}
