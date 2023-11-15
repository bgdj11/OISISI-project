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
class StudentPredmetDAO
{
        private readonly List<StudentPredmet> _studentpredmet;
        private readonly Storage<StudentPredmet> _storage;


        public StudentPredmetDAO(){
            _storage = new Storage<StudentPredmet>("studentpredmet.csv");
            _studentpredmet = _storage.Load();
        }

        public void AddPredmetToStudent(int ids, int idp)
        {
            StudentPredmet sp = new StudentPredmet(ids, idp);
            _studentpredmet.Add(sp);
            _storage.Save(_studentpredmet);
        }
}
}
