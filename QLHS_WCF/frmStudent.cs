using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Reflection;

namespace QLHS_WCF
{
    public partial class frmStudent : Form
    {
        StudentService1 obj = new StudentService1();
        public frmStudent()
        {
            InitializeComponent();
        }

        //Ẩn URL trong file Config.xml
        //Địa chỉ của file xml: D:\NH_Solutions\QLHS_WCF\QLHS_WCF\bin\Debug
        //Việc ẩn các URL giúp việc fix bug và bảo mật hơn
        public static string GetURIFromXMLFile(string sql)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(Application.StartupPath + "\\Config.xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodBase.GetCurrentMethod().Name + ": " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            XmlNodeList nodeLst = doc.GetElementsByTagName(sql);
            return nodeLst.Item(0).InnerText;

        }

        //Hàm lấy data từ trường textbox và fill vào object
        private void TakeInput(StudentDTO studentDTO)
        {
            studentDTO.StudentID = txtStudentID.Text;
            studentDTO.StudentName = txtStudentName.Text;
            studentDTO.Sex = cbSex.Text;
            studentDTO.Email = txtEmail.Text;
            studentDTO.DateOfBirth = dtpDateOfBirth.Value;
            studentDTO.Address = txtAddress.Text;

        }

        private void ClearBox()
        {
            txtStudentID.Text = "";
            txtStudentName.Text = "";
            cbSex.Text = "";
            txtEmail.Text = "";
            dtpDateOfBirth.Value = DateTime.Now;
            txtAddress.Text = "";

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //Khởi tạo đối tượng studentDTO từ class cùng tên
            StudentDTO studentDTO = new StudentDTO();
            TakeInput(studentDTO);

            //Web client cho phép send/receive data từ URI
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(StudentDTO));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, studentDTO);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            
            //Chuỗi URI thực sự gọi từ hàm GetURIFromXMLFile
            string uriInsert = GetURIFromXMLFile("Insert");
            webClient.UploadString(uriInsert, "POST", data);
            btnRefresh_Click(sender, e);
        }


        //Hàm này lấy dữ liệu và fill vào list result (là 1 list các đối tượng studentDTO
        //Định nghĩa hàm: WCF RESTful Service https://www.c-sharpcorner.com/article/wcf-restful-service/#:~:text=RESTful%20service%20follows%20the%20REST,mechanism%20like%20SOAP%20for%20communication
        void proxy_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Stream stream = new MemoryStream(Encoding.Unicode.GetBytes(e.Result));
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(List<StudentDTO>));
            List<StudentDTO> result = obj.ReadObject(stream) as List<StudentDTO>;

            dgvStudent.DataSource = result;
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {        
            WebClient proxy = new WebClient();
            string UriSelect = GetURIFromXMLFile("Select");
            proxy.DownloadStringAsync(new Uri(UriSelect));
            proxy.DownloadStringCompleted += proxy_DownloadStringCompleted;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StudentDTO studentDTO = new StudentDTO();
            TakeInput(studentDTO);

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(StudentDTO));
            ser.WriteObject(ms, studentDTO);
            string uriUpdate = GetURIFromXMLFile("Update");
            client.UploadData(uriUpdate, "PUT", ms.ToArray());
            btnRefresh_Click(sender, e);
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vitri = e.RowIndex;
            if (vitri >= 0)
            {
                txtStudentID.Text = dgvStudent.Rows[vitri].Cells[0].Value.ToString();
                txtStudentName.Text = dgvStudent.Rows[vitri].Cells[1].Value.ToString();
                cbSex.Text = dgvStudent.Rows[vitri].Cells[2].Value.ToString();
                txtEmail.Text = dgvStudent.Rows[vitri].Cells[3].Value.ToString();
                txtAddress.Text = dgvStudent.Rows[vitri].Cells[5].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                StudentDTO studentDTO = new StudentDTO();
            TakeInput(studentDTO);

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";

                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string));
                ser.WriteObject(ms, txtStudentID.Text);
                string uriDelete = GetURIFromXMLFile("Delete");
                byte[] data = client.UploadData(uriDelete, "DELETE", ms.ToArray());
                btnRefresh_Click(sender, e);
            }
            
            else if (dlr == DialogResult.No)
                return;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmStudent_Load(sender, e);
            ClearBox();
        }
    }
}