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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISudentService1
    {

        [OperationContract]
        DataTable SelectStudent();

        [OperationContract]
        bool InsertStudent(StudentDTO student, string err);

        [OperationContract]
        bool UpdateStudent(StudentDTO student, string err);

        [OperationContract]
        bool DeleteStudent(StudentDTO student, string err);
    }

    [DataContract]
    public class Student
    {
        int studentid;
        string studentname;
        string sex;
        string email;
        DateTime dateofbirth;
        string address;
        [DataMember]
        public int StudentID
        {
            get { return studentid; }
            set { studentid = value; }
        }
        [DataMember]
        public string StudentName
        {
            get { return studentname; }
            set { studentname = value; }
        }
        [DataMember]
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        [DataMember]
        public DateTime DateOfBirth
        {
            get { return dateofbirth; }
            set { dateofbirth = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
