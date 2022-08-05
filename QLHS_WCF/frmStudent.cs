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

namespace QLHS_WCF
{
    public partial class frmStudent : Form
    {
        StudentService1 obj = new StudentService1();
        public frmStudent()
        {
            InitializeComponent();
        }

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
            StudentDTO studentDTO = new StudentDTO();
            TakeInput(studentDTO);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(StudentDTO));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, studentDTO);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString("http://localhost:53431/StudentService1.svc/InsertStudent", "POST", data);
            btnRefresh_Click(sender, e);
        }

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
            proxy.DownloadStringAsync(new Uri("http://localhost:53431/StudentService1.svc/SelectAllStudents"));
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

            client.UploadData("http://localhost:53431/StudentService1.svc/UpdateStudent", "PUT", ms.ToArray());
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
                byte[] data = client.UploadData("http://localhost:53431/StudentService1.svc/DeleteStudent", "DELETE", ms.ToArray());
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