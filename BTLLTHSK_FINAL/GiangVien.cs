using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BTLLTHSK_FINAL
{
    public partial class fGiangVien : Form
    {
        GiangVienBLL bllGV;
        public fGiangVien()
        {
            InitializeComponent();
            bllGV = new GiangVienBLL();
        }
        public void ShowAllGiangVien()
        {
            DataTable dt = bllGV.getAllGiangVien();
            dataGridGiangVien.DataSource = dt;
        }
        public bool CheckData()
        {
            if(string.IsNullOrEmpty(txtMaGV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã giảng viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaGV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Bạn chưa nhập họ tên ","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtGioiTinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập giới tính ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGioiTinh.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Bạn chưa nhập số ĐT ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaKhoa.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã khoa ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhoa.Focus();
                return false;
            }

            return true;
        }
        private void fGiangVien_Load(object sender, EventArgs e)
        {
            ShowAllGiangVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                tblGiangVien gv = new tblGiangVien();
                gv.sMaGiangVien = txtMaGV.Text;
                gv.sTenGiangVien = txtHoTen.Text;
                gv.sGioiTinh = txtGioiTinh.Text;
                gv.sSoDT = txtSDT.Text;
                gv.sDiaChi = txtDiaChi.Text;
                gv.makhoa = txtMaKhoa.Text;
                if (bllGV.InsertGiangVien(gv))
                {
                    ShowAllGiangVien();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra xin thử lại sau", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        
        private void dataGridGiangVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >=0)
            {
                txtMaGV.Text = dataGridGiangVien.Rows[index].Cells["sMaGiangVien"].Value.ToString();
                txtHoTen.Text = dataGridGiangVien.Rows[index].Cells["sTenGiangVien"].Value.ToString();
                txtGioiTinh.Text = dataGridGiangVien.Rows[index].Cells["sDiaChi"].Value.ToString();
                txtSDT.Text = dataGridGiangVien.Rows[index].Cells["sSoDT"].Value.ToString();
                txtDiaChi.Text = dataGridGiangVien.Rows[index].Cells["sDiaChi"].Value.ToString();
                txtMaKhoa.Text = dataGridGiangVien.Rows[index].Cells["makhoa"].Value.ToString();

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                tblGiangVien gv = new tblGiangVien();
                gv.sMaGiangVien = txtMaGV.Text;
                gv.sTenGiangVien = txtHoTen.Text;
                gv.sGioiTinh = txtGioiTinh.Text;
                gv.sSoDT = txtSDT.Text;
                gv.sDiaChi = txtDiaChi.Text;
                gv.makhoa = txtMaKhoa.Text;
                if(bllGV.UpdateGiangVien(gv))
                {
                    ShowAllGiangVien();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra xin thử lại sau", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn xóa không","Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
            {
                tblGiangVien gv = new tblGiangVien();
                if (bllGV.DeleteGiangVien(gv))
                {
                    ShowAllGiangVien();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra xin thử lại sau", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            string value = txtFind.Text;
            if(!string.IsNullOrEmpty(value))
            {
                DataTable dt = bllGV.FindGiangVien(value);
                dataGridGiangVien.DataSource = dt;
            }
            else
            {
                ShowAllGiangVien();
            }
        }

        private void fGiangVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
        }
    }
}
