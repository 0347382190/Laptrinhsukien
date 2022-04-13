using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
namespace BTLLTHSK_FINAL
{
    public partial class frmtblMonhoc : Form
    {
        public frmtblMonhoc()
        {
            InitializeComponent();
        }
        String chuoiketnoi = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString;

        private void loaddtg()
        {
            using (SqlConnection sotinchi = new SqlConnection(chuoiketnoi)) 
            sotinchi.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from dsMonhoc",chuoiketnoi);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void loaddtg2()
        {
            string sqlSelect = "Select * from dsMonhoc";
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            SqlCommand cmd = new SqlCommand(sqlSelect,conn);
           
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
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"themMh";
            cmd.Parameters.AddWithValue("@maMh", textBox1.Text);
            cmd.Parameters.AddWithValue("@tenmh", textBox2.Text);
            cmd.Parameters.AddWithValue("@sotinchi", listBox1.Text);


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

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["Mã môn học"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Tên môn học"].Value.ToString();
            listBox1.Text = dataGridView1.CurrentRow.Cells["Số tín chỉ"].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiketnoi);
            string ma = textBox1.Text = dataGridView1.CurrentRow.Cells["Mã môn học"].Value.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"suaMh";
            cmd.Parameters.AddWithValue("@maMh", ma);
            cmd.Parameters.AddWithValue("@sotinchi",listBox1.Text);
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
            cmd.CommandText = @"xoa_Mh";
            cmd.Parameters.AddWithValue("@maMh", ma);
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
            string sotinchi = listBox1.Text;
            string select = "select * from tblMonHoc where iSotinchi ='" + sotinchi + "'";
            SqlCommand cmd = new SqlCommand(select,conn);
            cmd.Parameters.AddWithValue("@maMh", textBox1.Text);
            cmd.Parameters.AddWithValue("@tenMh", textBox2.Text);
            cmd.Parameters.AddWithValue("@sotinchi",listBox1.Text );
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;

            if (dataGridView1.Rows.Count > 0) { 
                MessageBox.Show("tìm thấy số tín chỉ "+sotinchi+" là "+dataGridView1.Rows.Count);
                
            }
            else
            {
                MessageBox.Show("Số tín chỉ không hợp lệ");
            }
        }

        private void frmtblMonhoc_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
        }
    }
    }
