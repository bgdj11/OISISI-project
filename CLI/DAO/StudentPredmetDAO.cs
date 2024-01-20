using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CLI.Model;
using CLI.Storage;

namespace CLI.DAO
{
public class StudentPredmetDAO
{
        private readonly List<StudentPredmet> _studentpredmet;
        private readonly Storage<StudentPredmet> _storage;


        public StudentPredmetDAO(){
            _storage = new Storage<StudentPredmet>("studentpredmet.csv");
            _studentpredmet = _storage.Load();
        }

        public void AddPredmetToStudent(int ids, int idp)
        {

            if (_studentpredmet.Any(sp => sp.IdStudent == ids && sp.IdPredmet == idp))
            {

                return;
            }

            StudentPredmet sp = new StudentPredmet(ids, idp);
            _studentpredmet.Add(sp);
            _storage.Save(_studentpredmet);
        }

        public StudentPredmet? GetByIds(int idStudenta, int idPredmeta)
        {
            foreach(StudentPredmet sp in _studentpredmet)
            {
                if (sp.IdStudent == idStudenta && idPredmeta == sp.IdPredmet)
                    return sp;
            }
            return null;
        }

        public void RemovePredmetFromStudent(StudentPredmet sp)
        {
            if(sp != null)
            {
                _studentpredmet.Remove(sp);
                _storage.Save(_studentpredmet);
            }

        }

        public void RemoveStudentPredmet(int idStudenta)
        {
            var zaUklanjanje = _studentpredmet.Where(sp => sp.IdStudent == idStudenta).ToList();

            foreach (var sp in zaUklanjanje)
            {
                _studentpredmet.Remove(sp);
            }

            _storage.Save(_studentpredmet);
        }
}
}
