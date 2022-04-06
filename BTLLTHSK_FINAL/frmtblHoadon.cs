using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BTLLTHSK_FINAL
{
    public partial class frmtblHoadon : Form
    {
        public frmtblHoadon()
        {
            InitializeComponent();
        }
        SqlConnection conn ;
        String chuoiketnoi = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString;
        private void loadnguoilap()
        {
            SqlConnection nl = new SqlConnection(chuoiketnoi);
            nl.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from tblNguoiLap", nl);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "sMaNguoiLap";
            comboBox1.ValueMember = "sTenNguoiLap";
        }
        private void loadsv()
        {
            SqlConnection sv = new SqlConnection(chuoiketnoi);
            sv.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from tblSinhVien", sv);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "sMaSv";
            comboBox3.ValueMember = "sTenSV";
        }
        private void loaddtghd()
        {
            SqlConnection hd = new SqlConnection(chuoiketnoi);
            hd.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from dsHoaDon", hd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Hoadon_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString.ToString();
            conn = new SqlConnection(conString);
            conn.Open();
            loadnguoilap();
            loaddtghd();
            loadsv();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"themHD";
            cmd.Parameters.AddWithValue("@maHD", textBox1.Text);
            cmd.Parameters.AddWithValue("@maNl", comboBox1.Text);
            cmd.Parameters.AddWithValue("@maSV", comboBox3.Text);
            cmd.Parameters.AddWithValue("@Ngaylap", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@Ngaydong", dateTimePicker2.Text);
            cmd.Parameters.AddWithValue("@tongtien", textBox2.Text);



            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Thêm thành công");
                loaddtghd();
            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text = dataGridView1.CurrentRow.Cells["Mã hóa đơn"].Value.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"suahd";
            cmd.Parameters.AddWithValue("@mahd", ma);
            cmd.Parameters.AddWithValue("@dngaydong", dateTimePicker2.Text);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Sửa thành công");
                loaddtghd();
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
                textBox1.Text = dataGridView1.CurrentRow.Cells["Mã hóa đơn"].Value.ToString();
                comboBox1.Text =dataGridView1.CurrentRow.Cells["Mã người lập"].Value.ToString();
                comboBox3.Text = dataGridView1.CurrentRow.Cells["Mã sinh viên"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["Ngày lập"].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["Ngày đóng"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["Tổng tiền"].Value.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"xoa_hd";
            cmd.Parameters.AddWithValue("@mahd", ma);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Xóa thành công");
                loaddtghd();
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string chucvu = textBox2.Text;
            string select = "select * from tblHoaDon where fTongtien >='" + chucvu + "'";
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.AddWithValue("@maHD", textBox1.Text);
            cmd.Parameters.AddWithValue("@maNl", comboBox1.Text);
            cmd.Parameters.AddWithValue("@maSV", comboBox3.Text);
            cmd.Parameters.AddWithValue("@Ngaylap", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@Ngaydong", dateTimePicker2.Text);
            cmd.Parameters.AddWithValue("@tongien", textBox2.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;

            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("tìm thấy hóa đơn lớn hơn " + chucvu + " có số lượng là " + dataGridView1.Rows.Count);

            }
            else
            {
                MessageBox.Show("không tìm thấy");
            }
        }
    }
}
