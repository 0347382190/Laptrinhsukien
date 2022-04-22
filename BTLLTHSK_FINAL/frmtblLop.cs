using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
namespace BTLLTHSK_FINAL
{
    public partial class frmtblLop : Form
    {
        public frmtblLop()
        {
            InitializeComponent();
        }

        String connect= ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString;
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadMaGiangvien();
            LoadMamon();
            LoadDataGridviewtblLopvamon();
        }
        private void LoadMamon()
        {
            
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT* FROM tblMonHoc", cnn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboMon.DataSource = dt;
            comboMon.DisplayMember = "Ma Mon";
            comboMon.ValueMember = "sMamonhoc";
            cnn.Close();
        }
        private void LoadMaGiangvien()
        {
            
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT* FROM tblGiangvien", cnn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboMaGV.DataSource = dt;
            comboMaGV.DisplayMember = "Ma Giang Vien";
            comboMaGV.ValueMember = "sMaGiangVien";
            cnn.Close();
        }

        private void LoadDataGridviewtblLopvamon()
        {
            
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM dsLop", cnn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dgv_lopvamon.DataSource = dt;
            cnn.Close();
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_Malop.Text = dgv_lopvamon.CurrentRow.Cells["Mã lớp"].Value.ToString();
            tb_tenlop.Text = dgv_lopvamon.CurrentRow.Cells["Tên lớp"].Value.ToString();
            tb_soSV.Text = dgv_lopvamon.CurrentRow.Cells["Số sinh viên"].Value.ToString();
            comboMon.Text = dgv_lopvamon.CurrentRow.Cells["Mã môn học"].Value.ToString();
            comboMaGV.Text = dgv_lopvamon.CurrentRow.Cells["Mã giảng viên"].Value.ToString();
            t_Ngaybatdauhoc.Text = dgv_lopvamon.CurrentRow.Cells["Ngày bắt đầu học"].Value.ToString();
            btn_Xoa.Enabled = true;
            btn_Sua.Enabled = true;

        }
        private void btn_insert(object sender, EventArgs e)
        {
            if(checkvalue()==1)
            {
                string connect = @"Data Source=DESKTOP-7CRV2HV\SQLEXPRESS;Initial Catalog=QLHocPhiSV;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                SqlCommand cmd = new SqlCommand("proc_InserttblLop", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sMalop", tb_Malop.Text);
                cmd.Parameters.AddWithValue("sTenlophoc", tb_tenlop.Text);
                cmd.Parameters.AddWithValue("isoSV", Convert.ToInt32(tb_soSV.Text));
                cmd.Parameters.AddWithValue("sMamonhoc", comboMon.Text);
                cmd.Parameters.AddWithValue("dNgaybatdauhoc", t_Ngaybatdauhoc.Value);
                cmd.Parameters.AddWithValue("dNgaydangki", date_Ngaydki.Value);
                cmd.Parameters.AddWithValue("sMaGiangVien", comboMaGV.Text);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Thêm thành công !!!");
                    LoadDataGridviewtblLopvamon();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công !!!");
                }
                cnn.Close();
            }    
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
           
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("proc_updatetbllopbyPK", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("sMalop", tb_Malop.Text);
            cmd.Parameters.AddWithValue("sTenlophoc", tb_tenlop.Text);
            cmd.Parameters.AddWithValue("isoSV", Convert.ToInt32(tb_soSV.Text));
            cmd.Parameters.AddWithValue("sMamonhoc", comboMon.Text);
            cmd.Parameters.AddWithValue("dNgaybatdauhoc", t_Ngaybatdauhoc.Value);
            cmd.Parameters.AddWithValue("sMaGiangVien", comboMaGV.Text);
            cmd.Parameters.AddWithValue("dNgaydangki", date_Ngaydki.Value);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Sửa thành công !!!");
                LoadDataGridviewtblLopvamon();
            }
            else
            {
                MessageBox.Show("Sửa không thành công !!!");
            }
            cnn.Close();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            var confirmResult = MessageBox.Show("Bạn có muốn xóa lớp này không", "Thông báo", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("proc_xoa", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sMalop", tb_Malop.Text);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Xóa lớp " + tb_Malop.Text + " thành công !!!");
                    LoadDataGridviewtblLopvamon();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công !!!");
                }
            }
            cnn.Close();

        }
        private int checkvalue()
        {
            if(tb_Malop.Text=="")
            {
                errorProvider1.SetError(tb_Malop,"Vui lòng nhập dữ liệu!!!");
                return 0;
            } 
            else
            {
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                SqlCommand cmd = new SqlCommand("checkuniquetblLop", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sMalop", tb_Malop.Text);
                var check = cmd.ExecuteScalar();
                if (check!=null)
                {
                    MessageBox.Show("Bạn nhập mã bị trùng!!!");
                    return 0;

                }
                else
                {
                    if (tb_tenlop.Text == "")
                    {
                        errorProvider1.SetError(tb_tenlop, "Vui lòng nhập dữ liệu!!!");
                        return 0;
                    }
                    else
                    {
                            if (tb_soSV.Text == "")
                            {
                                errorProvider1.SetError(tb_soSV, "Vui lòng nhập dữ liệu!!!");
                                return 0;
                            }
                            else
                            {
                                return 1;
                            }
                    }    
                }    

            }
        }

        private void btn_tim_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            
            String sql = "Select * from tblLop where sMalop LIKE '%" + tb_Malop.Text + "%' or  sTenlophoc LIKE '%" + tb_tenlop.Text + "%' or  isoSV LIKE '%" + tb_soSV.Text + "%'";
           
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql,cnn);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dgv_lopvamon.DataSource = dt;
            cnn.Close();
        }


        private void tb_soSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar)==false)
            {
                errorProvider1.SetError(tb_soSV, "Bạn phải nhập số");
            }    
        }

        private void frmtblLop_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
        }
    }
}
