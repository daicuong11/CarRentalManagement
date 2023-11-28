using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class LichTrinhDAO
    {
        private static LichTrinhDAO instance;

        public static LichTrinhDAO Instance 
        {   get { if (instance == null) instance = new LichTrinhDAO(); return LichTrinhDAO.instance; }
            private set { LichTrinhDAO.instance = value; }
        }

        public DataTable getAll()
        {
            return DataProvider.Instance.ExecuteQuery("select lt.donDatXeId as 'ID lịch trình', kh.hoTen as N'Khách hàng', lt.diemDen as N'Điểm đến', dd.ngayLap as N'Ngày lập', dd.tongGia as N'Tổng tiền', lt.ngayThanhToan as N'Ngày thanh toán', lt.trangThai as N'Trạng thái' from lichtrinh lt, dondatxe dd, khachhang kh where lt.donDatXeId = dd.donDatXeId and dd.khachHangId = kh.khachHangId and kh.daXoa = 0");
        }
        public LichTrinh getLichTrinhById(int lichTrinhId)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from lichtrinh where lichTrinhId = @id ", new object[] { lichTrinhId });
            if(tb.Rows.Count > 0)
            {
                return new LichTrinh(tb.Rows[0]);
            }
            return null;
        }
        public bool Insert_LichTrinh(string diemDen,int donDatXeId)
        {
            string query = "insert into lichtrinh (diemDen, ngayThanhToan, donDatXeId) values ( @diemDen ,null , @donDatXeID )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { diemDen, donDatXeId });
            return result > 0;
        }

        public bool Update_ThanhToan(int lichTrinhId)
        {
            return 0 < ((int)DataProvider.Instance.ExecuteNonQuery("Update lichtrinh set ngayThanhToan = getdate(), trangThai = N'Đã thanh toán' where lichTrinhId = @lichTrinhId ", new object[] {lichTrinhId}));
        }
    }
}
