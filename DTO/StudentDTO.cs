using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StudentDTO
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public StudentDTO() { }
        public StudentDTO(DataRow row)
        {
            StudentID = row["StudentID"].ToString();
            StudentName = row["StudentName"].ToString();
            Sex = row["Sex"].ToString();
            Email = row["Email"].ToString();
            DateOfBirth = (DateTime)row["DateOfBirth"];
            Address = row["Address"].ToString();
        }

    }
}
