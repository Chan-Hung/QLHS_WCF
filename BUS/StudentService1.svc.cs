using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BUS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class StudentService1 : IStudentService1
    {
        StudentDAO studentDAO = new StudentDAO();
        public List<StudentDTO> SelectAllStudents()
        {
            return studentDAO.SelectAllStudents();
        }
        public bool InsertStudent(StudentDTO student)
        {
            return studentDAO.InsertStudent(student);
        }

        public bool UpdateStudent(StudentDTO student)
        {
            return studentDAO.UpdateStudent(student);
        }
        public bool DeleteStudent(StudentDTO student)
        {
            return studentDAO.DeleteStudent(student);
        }

        
    }
}
