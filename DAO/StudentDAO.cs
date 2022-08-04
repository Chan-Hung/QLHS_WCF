using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DTO;

namespace DAO
{

    public class StudentDAO
    {
        DataAccessHelper dah = new DataAccessHelper();
        #region: Retrieving

        public List<StudentDTO> SelectAllStudents()
        {
            List<StudentDTO> listStudent = new List<StudentDTO>();
            DataTable dtStudent = dah.GetTable("SELECT * FROM Student");
            foreach(DataRow item in dtStudent.Rows)
            {
                StudentDTO stu = new StudentDTO(item);
                listStudent.Add(stu);
            }

            return listStudent;
        }

        #endregion

        #region: Inserting
        public bool InsertStudent(StudentDTO student)
        {
            bool result = dah.ExecuteNonQuery("spInsertStudent",
                CommandType.StoredProcedure,
                new SqlParameter("@StudentID", student.StudentID),
                new SqlParameter("@StudentName", student.StudentName),
                new SqlParameter("@Sex", student.Sex),
                new SqlParameter("@email", student.Email),
                new SqlParameter("@DateOfBirth", student.DateOfBirth),
                new SqlParameter("@Address", student.Address));
            return result;
        }
        #endregion


        #region: Updating
        public bool UpdateStudent(StudentDTO student)
        {
            bool result = dah.ExecuteNonQuery("spUpdateStudent",
                CommandType.StoredProcedure,
                 new SqlParameter("@StudentID", student.StudentID),
                new SqlParameter("@StudentName", student.StudentName),
                new SqlParameter("@Sex", student.Sex),
                new SqlParameter("@Email", student.Email),
                new SqlParameter("@DateOfBirth", student.DateOfBirth),
                new SqlParameter("@Address", student.Address)); 
            return result;

        }
        #endregion

        #region: Deleting
        public bool DeleteStudent(StudentDTO student)
        {
            bool result = dah.ExecuteNonQuery(
            "spDeleteStudent",
            CommandType.StoredProcedure,
            new SqlParameter("@StudentID", student.StudentID)
            );
            return result;
        }

        #endregion
    }
}
