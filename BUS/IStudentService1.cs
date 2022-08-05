using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BUS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IStudentService1
    {        
        [OperationContract]
        [WebGet(UriTemplate = "/SelectAllStudents", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<StudentDTO> SelectAllStudents();

        [OperationContract]
        [WebInvoke(UriTemplate = "/InsertStudent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        bool InsertStudent(StudentDTO student);

        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateStudent",Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateStudent(StudentDTO student);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DeleteStudent", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DeleteStudent(string studentid);
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
