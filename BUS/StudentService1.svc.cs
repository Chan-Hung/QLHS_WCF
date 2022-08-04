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
    public class StudentService1 : ISudentService1
    {
        StudentDAO studentDAO = new StudentDAO();
        public DataTable SelectStudent()
        {
            return studentDAO.SelectStudent();
        }
        public bool InsertStudent(StudentDTO student, string err)
        {
            return studentDAO.InsertStudent(student, err);
        }

        public bool UpdateStudent(StudentDTO student, string err)
        {
            return studentDAO.UpdateStudent(student, err);
        }
        public bool DeleteStudent(StudentDTO student, string err)
        {
            return studentDAO.DeleteStudent(student, err);
        }

       

       
    }
}
