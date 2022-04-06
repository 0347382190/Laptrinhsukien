using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BTLLTHSK_FINAL
{
    class GiangVienBLL
    {
        GiangVienDAL dalGV;
        public GiangVienBLL()
        {
            dalGV = new GiangVienDAL();
        }
        public DataTable getAllGiangVien()
        {
            return dalGV.getAllGiangVien();
        }
        public bool InsertGiangVien(tblGiangVien gv)
        {
            return dalGV.InsertGiangVien(gv);
        }
        public bool UpdateGiangVien(tblGiangVien gv)
        {
            return dalGV.UpdateGiangVien(gv);
        }
        public bool DeleteGiangVien(tblGiangVien gv)
        {
            return dalGV.DeleteGiangVien(gv);
        }
        public DataTable FindGiangVien(String gv)
        {
            return dalGV.FindGiangVien(gv);
        }
    }
}
