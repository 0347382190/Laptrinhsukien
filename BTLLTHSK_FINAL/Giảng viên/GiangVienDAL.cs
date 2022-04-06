using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BTLLTHSK_FINAL
{
    class GiangVienDAL
    {
        String chuoiketnoi = ConfigurationManager.ConnectionStrings["QLHPSV"].ConnectionString;
        SqlDataAdapter da;
        SqlCommand cmd;
        public DataTable getAllGiangVien()
        {
            SqlConnection con = new SqlConnection(chuoiketnoi);
            //B1: Tạo câu lệnh Sql để lấy toàn bộ giảng viên
            string sql = "SELECT *FROM tblGiangVien";
            //B3: Khởi tạo đối tượng của lớp SqlDataAdapter
            da = new SqlDataAdapter(sql, con);
            //B4: Mở chuỗi kết nối
            con.Open();
            //B5: Đổ dữ liệu SqlDataAlapter vào DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            //B6: Đóng chuỗi kết nối
            con.Close();
            return dt;
        }
        //câu lệnh bool nếu thêm sửa xóa thành công thì trả về true nếu k thành công trả về Fale
        public bool InsertGiangVien(tblGiangVien gv)
        {
            string sql = "INSERT INTO tblGiangVien(sMaGiangVien, sTenGiangVien, sGioiTinh, sSoDT, sDiaChi, makhoa) VALUES (@sMaGiangVien, @sTenGiangVien, @sGioiTinh, @sSoDT, @sDiaChi, @makhoa)";
            SqlConnection con = new SqlConnection(chuoiketnoi);
            try
            {
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@sMaGiangVien", SqlDbType.NVarChar).Value = gv.sMaGiangVien;
                cmd.Parameters.Add("@sTenGiangVien", SqlDbType.NVarChar).Value = gv.sTenGiangVien;
                cmd.Parameters.Add("@sGioiTinh", SqlDbType.NVarChar).Value = gv.sGioiTinh;
                cmd.Parameters.Add("@sSoDT", SqlDbType.NVarChar).Value = gv.sSoDT;
                cmd.Parameters.Add("@sDiaChi", SqlDbType.NVarChar).Value = gv.sDiaChi;
                cmd.Parameters.Add("@makhoa", SqlDbType.NVarChar).Value = gv.makhoa;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public bool UpdateGiangVien(tblGiangVien gv)
        {
            string sql = "UPDATE tblGiangVien SET sTenGiangVien = @sTenGiangVien, sGioiTinh = @sGioiTinh, sSoDT=@sSoDT, sDiaChi=@sDiaChi, makhoa=@makhoa  WHERE sMaGiangVien = @sMaGiangVien";
            SqlConnection con = new SqlConnection(chuoiketnoi);
            try
            {
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@sMaGiangVien", SqlDbType.NVarChar).Value = gv.sMaGiangVien;
                cmd.Parameters.Add("@sTenGiangVien", SqlDbType.NVarChar).Value = gv.sTenGiangVien;
                cmd.Parameters.Add("@sGioiTinh", SqlDbType.NVarChar).Value = gv.sGioiTinh;
                cmd.Parameters.Add("@sSoDT", SqlDbType.NVarChar).Value = gv.sSoDT;
                cmd.Parameters.Add("@sDiaChi", SqlDbType.NVarChar).Value = gv.sDiaChi;
                cmd.Parameters.Add("@makhoa", SqlDbType.NVarChar).Value = gv.makhoa;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool DeleteGiangVien(tblGiangVien gv)
        {
            string sql = "DELETE tblGiangVien WHERE sMaGiangVien=@sMagiangVien";
            SqlConnection con = new SqlConnection(chuoiketnoi);
            try
            {
                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add("@sMaGiangVien", SqlDbType.NVarChar).Value = gv.sMaGiangVien;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
        public DataTable FindGiangVien(String gv)
        {
            string sql = "SELECT *FROM tblGiangVien WHERE sTenGiangVien like N'%" + gv +"%' OR sDiachi like N'%" + gv +"%'";
            //B2: Tạo 1 chuỗi kết nối đến SQL
            SqlConnection con = new SqlConnection(chuoiketnoi);
            //B3: Khởi tạo đối tượng của lớp SqlDataAdapter
            da = new SqlDataAdapter(sql, con);
            //B4: Mở chuỗi kết nối
            con.Open();
            //B5: Đổ dữ liệu SqlDataAlapter vào DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            //B6: Đóng chuỗi kết nối
            con.Close();
            return dt;
        }
    }
}
