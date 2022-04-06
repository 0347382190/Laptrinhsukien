using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BTLLTHSK_FINAL
{
    public partial class frmtblSinhvien : Form
    {
        public frmtblSinhvien()
        {
            InitializeComponent();
        }
        String chuoiketnoi = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString;

        private void txtTenSV_Validating(object sender, CancelEventArgs e)
        {
            if(txtTenSV.Text == "")
            {
                errorProvider1.SetError(txtTenSV, "Vui lòng nhập vào họ tên sinh viên");
            }
            else
            {
                errorProvider1.SetError(txtTenSV, "");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienthi();
            comboBox1.Items.Add("Nam");
            comboBox1.Items.Add("Nu");
            cbbMaSV.Focus();
        }
       

        private void txtSodienthoai_Validating(object sender, CancelEventArgs e)
        {
            if(txtSodienthoai.Mask == "")
            {
                errorProvider1.SetError(txtSodienthoai, "Vui lòng nhập vào số điện thoại");
            }
            else
            {
                errorProvider1.SetError(txtSodienthoai, "");
            }
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            if(txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Vui lòng nhập vào Email");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }    
        }

        public void hienthi()
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string sql = "select* from v_tblSinhvien";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dsSinhvien.DataSource = dt;
                dsSinhvien.Columns["MaSV"].Visible = false;
                dsSinhvien.Columns[0].HeaderText = "Mã sinh viên";
                dsSinhvien.Columns[1].HeaderText = "Tên sinh viên";
                dsSinhvien.Columns[2].HeaderText = "Ngày sinh";
                dsSinhvien.Columns[3].HeaderText = "Giới tính";
                dsSinhvien.Columns[4].HeaderText = "Số điện thoại";
                dsSinhvien.Columns[5].HeaderText = "Email";
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spSinhvien_Insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sMaSv", cbbMaSV.Text);
                cmd.Parameters.AddWithValue("sTenSV", txtTenSV.Text);
                cmd.Parameters.AddWithValue("dNgaySinh", txtNgaysinh.Value);
                cmd.Parameters.AddWithValue("sGioiTinh", rdbNam.Checked ? "nam" : "nu");
                cmd.Parameters.AddWithValue("sDienThoai", txtSodienthoai.Text);
                cmd.Parameters.AddWithValue("sEmail", txtEmail.Text);
                if (CheckmaSV() == 1)
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Thêm thành công !!!");
                        hienthi();
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công !!!");
                    }
                }
            }     

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spSinhvien_UpdateByPK", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sMaSv", cbbMaSV.Text);
                cmd.Parameters.AddWithValue("sTenSV", txtTenSV.Text);
                cmd.Parameters.AddWithValue("dNgaySinh", txtNgaysinh.Value);
                cmd.Parameters.AddWithValue("sGioiTinh", rdbNam.Checked ? "nam" : "nu");
                cmd.Parameters.AddWithValue("sDienThoai", txtSodienthoai.Text);
                cmd.Parameters.AddWithValue("sEmail", txtEmail.Text);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Sửa thành công !!!");
                    hienthi();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công !!!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(chuoiketnoi)) { 
                conn.Open();
            var confirmResult = MessageBox.Show("Bạn có muốn xóa lớp này không", "Thông báo", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("spSinhVien_DeleteByPK", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sMaSV", cbbMaSV.Text);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Xóa lớp " + cbbMaSV.Text + " thành công !!!");
                        hienthi();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công !!!");
                    }
                }
            }
        }

        private void dsSinhvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbbMaSV.Text = dsSinhvien.CurrentRow.Cells["MaSV"].Value.ToString();
            txtTenSV.Text = dsSinhvien.CurrentRow.Cells["name"].Value.ToString();
            txtNgaysinh.Text = dsSinhvien.CurrentRow.Cells["dateofbirth"].Value.ToString();
            if (dsSinhvien.CurrentRow.Cells["sex"].Value.ToString() == "nam")
                rdbNam.Checked = true;
            else
                rdbNu.Checked = true
                    ;
            txtSodienthoai.Text = dsSinhvien.CurrentRow.Cells["Phone"].Value.ToString();
            txtEmail.Text = dsSinhvien.CurrentRow.Cells["Email"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

        }
        private int CheckmaSV()
        {
            using (SqlConnection conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("checkMaSV", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sMaSv", cbbMaSV.Text);
                var i = cmd.ExecuteScalar();
                if (i != null)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại");
                    return 0;
                }
                else
                    return 1;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
            Application.Exit();
            }
        }

        private void txtNgaysinh_Validating(object sender, CancelEventArgs e)
        {
            int now = DateTime.Now.Year;
            int ns = txtNgaysinh.Value.Year;
            if (now - ns <= 17)
            {
                MessageBox.Show("Sinh vien nay chua du 18 tuoi");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Nam")
            {
                using(SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string sql = "select* from v_tblSinhvien where sex ='Nam'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dsSinhvien.DataSource = dt;
                    dsSinhvien.Columns[0].HeaderText = "Mã sinh viên";
                    dsSinhvien.Columns[1].HeaderText = "Tên sinh viên";
                    dsSinhvien.Columns[2].HeaderText = "Ngày sinh";
                    dsSinhvien.Columns[3].HeaderText = "Giới tính";
                    dsSinhvien.Columns[4].HeaderText = "Số điện thoại";
                    dsSinhvien.Columns[5].HeaderText = "Email";
                    dsSinhvien.Columns["MaSV"].Visible = false;
                }    
              

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {

                conn.Open();
                string sql = "select* from v_tblSinhvien where sex ='Nu'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dsSinhvien.DataSource = dt;
                dsSinhvien.Columns[0].HeaderText = "Mã sinh viên";
                dsSinhvien.Columns[1].HeaderText = "Tên sinh viên";
                dsSinhvien.Columns[2].HeaderText = "Ngày sinh";
                dsSinhvien.Columns[3].HeaderText = "Giới tính";
                dsSinhvien.Columns[4].HeaderText = "Số điện thoại";
                dsSinhvien.Columns[5].HeaderText = "Email";
                dsSinhvien.Columns["MaSV"].Visible = false;

                }
            }
        }

        private void cbbMaSV_Validating(object sender, CancelEventArgs e)
        {
            if(cbbMaSV.Text == "")
            {
                errorProvider1.SetError(cbbMaSV,"Mã sinh viên không được để trống");

            }else
            {
                errorProvider1.SetError(cbbMaSV, "");  
            }    
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    CrystalReport1 baocao = new CrystalReport1();
        //    Form2 f = new Form2();
        //    f.crystalReportViewer1.ReportSource = baocao;
        //    f.ShowDialog();
        //}

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Vui lòng nhập vào Email");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }
    }
}
