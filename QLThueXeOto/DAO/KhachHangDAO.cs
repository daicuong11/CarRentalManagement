using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;

        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return KhachHangDAO.instance; }
            private set { KhachHangDAO.instance = value; }
        }

        public int Insert_KhachHang(KhachHang kh)
        {
            string query = "INSERT INTO khachhang(hoTen, soDienThoai, diaChi) OUTPUT INSERTED.khachHangId VALUES( N'" +
                kh.HoTen +"', @sodienthoai , N'" +
                kh.DiaChi + "')";
            int id = (int) DataProvider.Instance.ExecuteScalar(query, new object[] { kh.SoDienThoai });
            return id;
        }
        public bool Update_KhachHang(KhachHang kh)
        {

            string query = "UPDATE khachhang SET hoTen = @hoTen , soDienThoai = @soDienThoai , diaChi = @diaChi WHERE khachHangId = @khachHangId ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { kh.HoTen, kh.SoDienThoai, kh.DiaChi, kh.KhachHangHangId });
            return (result > 0);
        }
        public bool Delete_KhachHang(int ma)
        {
            string query = "Update khachhang set daXoa = 1 where khachHangId = @ma ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {ma});
            return (result > 0);
        }


        public int getKhachHangBySoDienThoai(string soDienThoai)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("SELECT khachHangId FROM khachhang WHERE soDienThoai = @SoDienThoai  and daXoa = 0", new object[] { soDienThoai });
            if(tb.Rows.Count > 0)
            {
                return (int)tb.Rows[0]["khachHangId"];
            }
            return -1;
        }

        public KhachHang getKhachHangById(int id)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from khachhang where khachHangId = @khid and daXoa = 0", new object[] { id });
            if( tb.Rows.Count > 0)
            {
                return new KhachHang(tb.Rows[0]);
            }
            return null;
        }

        public KhachHang getKhachHangByIdAndDeleted(int id)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from khachhang where khachHangId = @khid", new object[] { id });
            if (tb.Rows.Count > 0)
            {
                return new KhachHang(tb.Rows[0]);
            }
            return null;
        }
        public DataTable getAllKhachHang()
        {
            
            return DataProvider.Instance.ExecuteQuery("select * from khachhang and daXoa = 0");
        }
        public DataTable getKhachHangByTuKhoaTimKiem(string tuKhoaTimKiem)
        {
            string query = "select * from khachhang where daXoa = 0 and (hoTen like N'%"
                + tuKhoaTimKiem + "%' or soDienThoai like '%"
                + tuKhoaTimKiem + "%' or diaChi like N'%"
                + tuKhoaTimKiem + "%')";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
