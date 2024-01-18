using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.DAO;
namespace CLI.Controller
{
    public class StudentController
    {
        private readonly StudentDAO studentDAO;

        public StudentController()
        {
            studentDAO = new StudentDAO();
        }

        public List<Student> GetAllStudents() {
            return studentDAO.GetAllStudents();
        }

        public void AddStudent(Student student)
        {
            studentDAO.AddStudent(student);
        }

        public void DeleteStudent(int id)
        {
            studentDAO.RemoveStudent(id);
        }

        public void UpdateStudent(Student student)
        {
            studentDAO.UpdateStudent(student);
        }

        public void MakeStudent()
        {
            studentDAO.MakeStudent();
        }
    }
}
