using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public int idAdrese { get; set; }

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



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<int> PredmetiListaId { get; set; }

        public ProfesorDTO()
        {
            PredmetiListaId = new List<int>();
        }

        public ProfesorDTO(Profesor profesor)
        {
            IdProfesor = profesor.IdProfesor;
            Prezime = profesor.Prezime;
            Ime = profesor.Ime;
            DatumRodjenja = profesor.DatumRodjenja.ToString();
            Adresa = profesor.AdresaStanovanja.ToString();
            Ulica = profesor.AdresaStanovanja.Ulica;
            Grad = profesor.AdresaStanovanja.Grad;
            Broj = profesor.AdresaStanovanja.Broj;
            Drzava = profesor.AdresaStanovanja.Drzava;
            KontaktTelefon = profesor.KontaktTelefon;
            EmailAdresa = profesor.EmailAdresa;
            BrojLicneKarte = profesor.BrojLicneKarte;
            Zvanje = profesor.Zvanje;
            GodineStaza = profesor.GodineStaza;
            IdKatedre = profesor.IdKatedre;
            idAdrese = profesor.IdAdrese;

            PredmetiListaId = new List<int>();


            if (profesor.SpisakPredmeta.Any())
            {
                foreach (Predmet p in profesor.SpisakPredmeta)
                {
                    if (!PredmetiListaId.Contains(p.IdPredmet))
                    {
                        PredmetiListaId.Add(p.IdPredmet);
                    }
                }
            }

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
                Grad = this.Grad,
                Ulica = this.Ulica,
                Drzava = this.Drzava,
                broj = this.Broj,
                KontaktTelefon = this.KontaktTelefon,
                EmailAdresa = this.EmailAdresa,
                BrojLicneKarte = this.BrojLicneKarte,
                Zvanje = this.Zvanje,
                GodineStaza = this.GodineStaza,
                IdKatedre = this.IdKatedre,
                idAdrese = this.idAdrese,
                PredmetiListaId = this.PredmetiListaId

            };
        }


        public Profesor toProfesor()
        {
            return new Profesor(Prezime, Ime, Ulica, Broj, Grad,  Drzava, DatumRodjenja, KontaktTelefon, EmailAdresa, BrojLicneKarte, Zvanje, GodineStaza, IdKatedre);
        }
    }
}
