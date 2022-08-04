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
            string err = "";
            if (!obj.InsertStudent(studentDTO))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnRefresh_Click(sender, e);
                MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            dgvStudent.DataSource = obj.SelectStudent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StudentDTO studentDTO = new StudentDTO();
            TakeInput(studentDTO);
            string err = "";
            if (!obj.UpdateStudent(studentDTO))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnRefresh_Click(sender, e);
                MessageBox.Show("Đã sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                string err = "";
                StudentDTO studentDTO = new StudentDTO();
            TakeInput(studentDTO);
               
                if (!obj.DeleteStudent(studentDTO))
                {
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnRefresh_Click(sender, e);
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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