using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
namespace BTLLTHSK_FINAL
{
    public partial class frmtblNguoilapHD : Form
    {
        public frmtblNguoilapHD()
        {
            InitializeComponent();
        }
        String chuoiketnoi = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString;

        private void loaddtg()
        {
            SqlConnection chucvu = new SqlConnection(chuoiketnoi);
            chucvu.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from dsNguoiLap", chucvu);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void loaddtg2()
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string sqlSelect = "Select * from dsNguoiLap";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);          
            SqlDataReader dr =  cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource= dt;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
           
            loaddtg();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"themNL";
            cmd.Parameters.AddWithValue("@maNl", textBox1.Text);
            cmd.Parameters.AddWithValue("@ten", textBox2.Text);
            cmd.Parameters.AddWithValue("@NgaySinh", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@CMND", textBox4.Text);
            cmd.Parameters.AddWithValue("@chucvu", listBox1.Text);
            int i= cmd.ExecuteNonQuery();
            if(i > 0)
            {
                MessageBox.Show("Thêm thành công");
                loaddtg();
            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
            conn.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["Mã người lập"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Tên người lập"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["Căn cước"].Value.ToString();
           dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["Ngày sinh"].Value.ToString();
            listBox1.Text = dataGridView1.CurrentRow.Cells["Chức vụ"].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string ma = textBox1.Text = dataGridView1.CurrentRow.Cells["Mã người lập"].Value.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"suaNl";
            cmd.Parameters.AddWithValue("@maNl", ma);
            cmd.Parameters.AddWithValue("@chucvu",listBox1.Text);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Sửa thành công");
                loaddtg();
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string ma = textBox1.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"xoa_Nl";
            cmd.Parameters.AddWithValue("@maNl", ma);
            int i = cmd.ExecuteNonQuery();
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
             MessageBoxIcon.Question) == DialogResult.Yes)
            if (i > 0)
            {
                MessageBox.Show("Xóa thành công");
                loaddtg();
                
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string chucvu = listBox1.Text;
            string select = "select * from tblNguoiLap where sChucVu ='" + chucvu + "'";
            SqlCommand cmd = new SqlCommand(select,conn);
            cmd.Parameters.AddWithValue("@maNl", textBox1.Text);
            cmd.Parameters.AddWithValue("@ten", textBox2.Text);
            cmd.Parameters.AddWithValue("@NgaySinh", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@CMND", textBox4.Text);
            cmd.Parameters.AddWithValue("@chucvu",listBox1.Text );
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;

            if (dataGridView1.Rows.Count > 0) { 
                MessageBox.Show("tìm thấy chức vụ "+chucvu+" có số lượng là "+dataGridView1.Rows.Count);
                
            }
            else
            {
                MessageBox.Show("Chức vụ không hợp lệ");
            }
        }

        private void frmtblNguoilapHD_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
        }
    }
    }
