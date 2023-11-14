using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;

namespace CLI.DAO
{
class StudentPredmetDAO
{
        private readonly List<StudentPredmet> _studentpredmet;
        private readonly Storage<StudentPredmet> _storage;


        public StudentPredmetDAO(StudentDAO _student, PredmetDAO _predmet){
            _storage = new Storage<StudentPredmet>("studentpretmet.csv");
            _studentpredmet = _storage.Load();

            foreach(StudentPredmet studentpredmet in _studentpredmet ) {
                Predmet predmetTmp = new Predmet();
                predmetTmp = _predmet.GetPredmetById(studentpredmet.IdPredmet);
                Student studentTmp = new Student();
                studentTmp = _student.GetStudentById(studentpredmet.IdStudent);

                //metoda dodaj predmet studentu unutar modela student
                //_student.dodajPredmetStudentu(IdStudent, Predmet);
                //metoda dodaj studenta predmetu unutar modela predmet
                //_predmet.dodajStudentaPredmetu(IdPredmet, Student);
            }

        }
}
}
