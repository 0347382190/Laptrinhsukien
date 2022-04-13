using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLLTHSK_FINAL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sinhViênToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmtblSinhvien frmtblSinhvien = new frmtblSinhvien();
            this.Hide();
            frmtblSinhvien.Show();
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmtblLop frmtblLop = new frmtblLop();
            this.Hide();
            frmtblLop.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ngườiLậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmtblNguoilapHD frmtblNguoilap = new frmtblNguoilapHD();
            this.Hide();
            frmtblNguoilap.Show();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmtblHoadon frmtblHoadon = new frmtblHoadon();
            this.Hide();
            frmtblHoadon.Show();
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmtblMonhoc frmtblMonhoc = new frmtblMonhoc();
            this.Hide();
            frmtblMonhoc.Show();
        }

        private void giảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fGiangVien fGiangVien = new fGiangVien();
            this.Hide();
            fGiangVien.Show();
        }
    }
}
