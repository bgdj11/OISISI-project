using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;


namespace CLI.DAO

{
class StudentDAO
{
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;


        public StudentDAO()
        {
            _storage = new Storage<Student>("students.csv");
            _students = _storage.Load();
        }

        private int GenerateId()
        {
            if(_students.Count == 0) return 0;
            return _students[^1].idStudent + 1;
        }

        public Student AddStudent(Student student)
        {
            student.idStudent = GenerateId();
            _students.Add(student);
            _storage.Save(_students);
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

            _storage.Save(_students);
            return oldStudent; 

        }

        public Student? RemoveStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return null;

            _students.Remove(student);
            _storage.Save(_students);
            return student;
        }

        public Student? GetStudentById(int idStudent)
        {
            return _students.Find(s => s.idStudent == idStudent);
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public List<Student> GetAllStudents(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Student> students = _students;

            switch (sortCriteria)
            {
                case "Id":
                    students = _students.OrderBy(x => x.idStudent);
                    break;
                case "Name":
                    students = _students.OrderBy(x => x.ime);
                    break;
                case "DatumRodjenja":
                    students = _students.OrderBy(x => x.datumRodjenja);
                    break;
                case "AdresaStanovanja":
                    students = _students.OrderBy(x => x.adresaStanovanja);
                    break;
                case "KontaktTelefon":
                    students = _students.OrderBy(x => x.kontaktTelefon);
                    break;
                case "EmailAdresa":
                    students = _students.OrderBy(x => x.emailAdresa);
                    break;
                case "BrojIndeksa":
                    students = _students.OrderBy(x => x.brojIndeksa);
                    break;
                case "TrenutnaGodinaStudija":
                    students = _students.OrderBy(x => x.trenutnaGodinaStudija);
                    break;
                case "Status":
                    students = _students.OrderBy(x => x.status);
                    break;
                case "ProsecnaOcena":
                    students = _students.OrderBy(x => x.prosecnaOcena);
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
