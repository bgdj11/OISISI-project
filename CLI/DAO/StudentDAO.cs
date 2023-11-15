using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Serialization;
using CLI.Storage;


namespace CLI.DAO

{
class StudentDAO
{
        private readonly List<Student> _studenti;
        private readonly Storage<Student> _storage;


        public StudentDAO()
        {
            _storage = new Storage<Student>("studenti.csv");
            _studenti = _storage.Load();
        }

        private int GenerateId()
        {
            if (_studenti.Count == 0) return 0;
            return _studenti[^1].idStudent + 1;
        }

        void MakeStudent()
        {
            Storage<Predmet> _predmetiStorage = new Storage<Predmet>("predmeti.csv");
            List<Predmet> _predmeti = _predmetiStorage.Load();

            Storage<OcenaNaUpisu> _oceneStorage = new Storage<OcenaNaUpisu>("ocene.csv");
            List<OcenaNaUpisu> _ocene = _oceneStorage.Load();

            Storage<StudentPredmet>  _spStorage = new Storage<StudentPredmet>("studentpredmet.csv");
            List<StudentPredmet> _studPred = _spStorage.Load();

            Storage<Adresa> _adresaStorage = new Storage<Adresa>("adrese.csv");
            List<Adresa> _adrese = _adresaStorage.Load();

            Storage<Indeks> _indeksStorage = new Storage<Indeks>("indeksi.csv");
            List<Indeks> _indeksi = _indeksStorage.Load();

            foreach(Student s in _studenti)
            {
                foreach (Adresa a in _adrese)
                {
                    if (s.idAdrese == a.idAdrese)
                    {
                        s.adresaStanovanja = a;
                    }

                }
            }

            foreach (Student s in _studenti)
            {
                foreach (Indeks i in _indeksi)
                {
                    if (s.idIndeksa == i.idIndeksa)
                    {
                        s.indeks = i;
                    }

                }
            }

            foreach (Student s in _studenti)
            {
                foreach(StudentPredmet sp in _studPred)
                {
                    if(s.idStudent == sp.IdStudent)
                    {
                        if(_ocene.Find(n => n.idPredmeta == sp.IdPredmet) != null)
                        {
                            s.PolozeniIspiti.Add(_ocene.Find(n => n.idPredmeta == sp.IdPredmet));
                        }
                        else
                        {
                            s.NepolozeniIspiti.Add(_predmeti.Find(n => n.idPredmet == sp.IdPredmet));
                        }
                    }
                }
            }

            _storage.Save(_studenti);

        }

        public Student AddStudent(Student student)
        {
            student.idStudent = GenerateId();
            _studenti.Add(student);
            MakeStudent();
            _storage.Save(_studenti);
            return student;
        }

        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetStudentById(student.idStudent);
            if (oldStudent == null) return null;

            oldStudent.prezime = student.prezime;
            oldStudent.ime = student.ime;
            oldStudent.datumRodjenja = student.datumRodjenja;
            oldStudent.idAdrese = student.idAdrese;
            oldStudent.kontaktTelefon = student.kontaktTelefon;
            oldStudent.emailAdresa = student.emailAdresa;
            oldStudent.brojIndeksa = student.brojIndeksa;
            oldStudent.trenutnaGodinaStudija = student.trenutnaGodinaStudija;
            oldStudent.status = student.status;
            oldStudent.prosecnaOcena = student.prosecnaOcena;

            MakeStudent();

            _storage.Save(_studenti);
            return oldStudent; 

        }

        public Student? RemoveStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return null;

            _studenti.Remove(student);
            _storage.Save(_studenti);
            return student;
        }

        public Student? GetStudentById(int idStudent)
        {
            return _studenti.Find(s => s.idStudent == idStudent);
        }

        public List<Student> GetAllStudents()
        {
            return _studenti;
        }

        public List<Student> GetAllStudents(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Student> students = _studenti;

            switch (sortCriteria)
            {
                case "Id":
                    students = _studenti.OrderBy(x => x.idStudent);
                    break;
                case "Name":
                    students = _studenti.OrderBy(x => x.ime);
                    break;
                case "DatumRodjenja":
                    students = _studenti.OrderBy(x => x.datumRodjenja);
                    break;
                case "AdresaStanovanja":
                    students = _studenti.OrderBy(x => x.adresaStanovanja);
                    break;
                case "KontaktTelefon":
                    students = _studenti.OrderBy(x => x.kontaktTelefon);
                    break;
                case "EmailAdresa":
                    students = _studenti.OrderBy(x => x.emailAdresa);
                    break;
                case "BrojIndeksa":
                    students = _studenti.OrderBy(x => x.brojIndeksa);
                    break;
                case "TrenutnaGodinaStudija":
                    students = _studenti.OrderBy(x => x.trenutnaGodinaStudija);
                    break;
                case "Status":
                    students = _studenti.OrderBy(x => x.status);
                    break;
                case "ProsecnaOcena":
                    students = _studenti.OrderBy(x => x.prosecnaOcena);
                    break;
            }

            // promeni redosled ukoliko ima potrebe za tim
            if (sortDirection == SortDirection.Descending)
                students = students.Reverse();

            // paginacija
            students = students.Skip((page - 1) * pageSize).Take(pageSize);

            return students.ToList();
        }

    }
}
