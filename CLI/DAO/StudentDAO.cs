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
public class StudentDAO
{
        private readonly List<Student> _studenti;
        private readonly Storage<Student> _storage;

        private readonly AdresaDAO adresaDAO = new AdresaDAO();
        private readonly IndeksDAO indeksDAO = new IndeksDAO();
        public StudentDAO()
        {
            _storage = new Storage<Student>("studenti.csv");
            _studenti = _storage.Load();
            MakeStudent();
        }

        private int GenerateId()
        {
            if (_studenti.Count == 0) return 0;
            return _studenti[^1].IdStudent + 1;
        }

        public void MakeStudent()
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
                    if (s.IdAdrese == a.IdAdrese)
                    {
                        s.AdresaStanovanja = a;
                    }

                }
            }

            foreach (Student s in _studenti)
            {
                foreach (Indeks i in _indeksi)
                {
                    if (s.IdIndeksa == i.idIndeksa)
                    {
                        s.Indeks = i;
                    }

                }
            }

           
            // Izbegava dupliranje predmeta
            foreach (Student s in _studenti)
            {
                s.PolozeniIspiti.Clear(); 
                s.NepolozeniIspiti.Clear(); 

                foreach (StudentPredmet sp in _studPred)
                {
                    if (s.IdStudent == sp.IdStudent)
                    {
                        var ocena = _ocene.Find(n => n.IdPredmeta == sp.IdPredmet && n.IdStudenta == sp.IdStudent);
                        if (ocena != null)
                        {
                            s.PolozeniIspiti.Add(ocena);
                        }
                        else
                        {
                            var nepolozenPredmet = _predmeti.Find(n => n.idPredmet == sp.IdPredmet);
                            if (!s.NepolozeniIspiti.Contains(nepolozenPredmet))
                            {
                                s.NepolozeniIspiti.Add(nepolozenPredmet);
                            }
                        }
                    }
                }
            }

            /*
            foreach (Student s in _studenti)
            {
                foreach(StudentPredmet sp in _studPred)
                {
                    if(s.IdStudent == sp.IdStudent)
                    {
                        if(_ocene.Find(n => n.idPredmeta == sp.IdPredmet && n.idStudenta == sp.IdStudent) != null)
                        {
                            s.PolozeniIspiti.Add(_ocene.Find(n => n.idPredmeta == sp.IdPredmet && n.idStudenta == sp.IdStudent));
                        }
                        else
                        {
                            s.NepolozeniIspiti.Add(_predmeti.Find(n => n.idPredmet == sp.IdPredmet));
                        }
                    }
                }
            }
            */

            _storage.Save(_studenti);
            _adresaStorage.Save(_adrese);
            _indeksStorage.Save(_indeksi);

        }

        public Student AddStudent(Student student)
        {
            student.IdStudent = GenerateId();
            indeksDAO.AddIndeks(student.Indeks);
            adresaDAO.AddAdresa(student.AdresaStanovanja);
            student.IdIndeksa = indeksDAO.GetLastID();
            student.IdAdrese = adresaDAO.GetLastID();
            _studenti.Add(student);
            MakeStudent();
            _storage.Save(_studenti);
            return student;
        }

        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetStudentById(student.IdStudent);
            if (oldStudent == null) return null;

            oldStudent.Prezime = student.Prezime;
            oldStudent.Ime = student.Ime;
            oldStudent.DatumRodjenja = student.DatumRodjenja;
            oldStudent.IdAdrese = student.IdAdrese;
            adresaDAO.UpdateAdresa(student.AdresaStanovanja);
            oldStudent.KontaktTelefon = student.KontaktTelefon;
            oldStudent.EmailAdresa = student.EmailAdresa;
            oldStudent.IdIndeksa = student.IdIndeksa;
            indeksDAO.UpdateIndeks(student.Indeks);
            oldStudent.TrenutnaGodinaStudija = student.TrenutnaGodinaStudija;
            oldStudent.Status = student.Status;
            oldStudent.ProsecnaOcena = student.ProsecnaOcena;

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
            return _studenti.Find(s => s.IdStudent == idStudent);
        }

        public List<Student> GetAllStudents()
        {
            MakeStudent();
            return _studenti;
        }

        public List<Student> GetAllStudents(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Student> students = _studenti;

            switch (sortCriteria)
            {
                case "Id":
                    students = _studenti.OrderBy(x => x.IdStudent);
                    break;
                case "Name":
                    students = _studenti.OrderBy(x => x.Ime);
                    break;
                case "DatumRodjenja":
                    students = _studenti.OrderBy(x => x.DatumRodjenja);
                    break;
                case "AdresaStanovanja":
                    students = _studenti.OrderBy(x => x.AdresaStanovanja);
                    break;
                case "KontaktTelefon":
                    students = _studenti.OrderBy(x => x.KontaktTelefon);
                    break;
                case "EmailAdresa":
                    students = _studenti.OrderBy(x => x.EmailAdresa);
                    break;
                case "TrenutnaGodinaStudija":
                    students = _studenti.OrderBy(x => x.TrenutnaGodinaStudija);
                    break;
                case "Status":
                    students = _studenti.OrderBy(x => x.Status);
                    break;
                case "ProsecnaOcena":
                    students = _studenti.OrderBy(x => x.ProsecnaOcena);
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
