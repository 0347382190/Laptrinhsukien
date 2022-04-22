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

        private void loadlop()
        {
            SqlConnection nl = new SqlConnection(chuoiketnoi);
            nl.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from tblLop", nl);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox4.DataSource = dt;
            comboBox4.DisplayMember = "sMalop";
            comboBox4.ValueMember = "sTenlophoc";
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

            dataGridView1.Columns["Mã người lập"].Visible = false;
            dataGridView1.Columns["Mã sinh viên"].Visible = false;
            dataGridView1.Columns["Mã hóa đơn"].Visible = false;
        }

        private void Hoadon_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString.ToString();
            conn = new SqlConnection(conString);
            conn.Open();
            loadnguoilap();
            loaddtghd();
           loadsv();
            loadlop();
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
            cmd.Parameters.AddWithValue("@malop", comboBox4.Text);
            cmd.Parameters.AddWithValue("@Ngaylap", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@Ngaydong", dateTimePicker2.Text);
          



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
            cmd.Parameters.AddWithValue("@dngaylap", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@malop", comboBox4.Text);
            cmd.Parameters.AddWithValue("@masv", comboBox3.Text);
            cmd.Parameters.AddWithValue("@manl", comboBox1.Text);
            

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
            comboBox4.Text = dataGridView1.CurrentRow.Cells["Mã lớp"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["Tên sinh viên"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["Ngày lập"].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["Ngày đóng"].Value.ToString();
               


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
            SqlConnection hd = new SqlConnection(chuoiketnoi);
            hd.Open();

            String sql = "Select * from dsHoadon where [Tên sinh viên] LIKE N'%" + textBox2.Text + "%' ";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, hd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            hd.Close();
            

            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("tìm thấy hóa đơn của sinh viên " + textBox2.Text); 

            }
            else
            {
                MessageBox.Show("không tìm thấy");
            }
        }
    

        private void frmtblHoadon_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"chitiethoadonsv";
            cmd.Parameters.AddWithValue("@masv", comboBox3.Text);
            DataTable dt = new DataTable("BanginHoaDon");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dt);
            maxchitiet baocao = new maxchitiet();
            baocao.SetDataSource(dt);
            Forminvao forminvao = new Forminvao();
            forminvao.crystalReportViewer1.ReportSource = baocao;
            this.Hide();
            forminvao.Show();
            //

        }
    }
}
