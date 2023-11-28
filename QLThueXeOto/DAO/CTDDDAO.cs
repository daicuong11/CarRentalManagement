using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace QLThueXeOto.DAO
{
    public class CTDDDAO
    {
        private static CTDDDAO instance;

        public static CTDDDAO Instance
        {
            get { if (instance == null) instance = new CTDDDAO(); return CTDDDAO.instance; }
            private set { CTDDDAO.instance = value; }
        }

        public DataTable getCTDDByDonDatXeId(int id)
        {
            return DataProvider.Instance.ExecuteQuery("select * from ctdd where donDatXeId = @donDatXeId ", new object[] { id });
        }

        public decimal getTongTienByDonDatXeId(int donDatXeId)
        {
            string query = "SELECT SUM(thanhTien) FROM CTDD where donDatXeId = @donDatXeId ";
            decimal result = (decimal) DataProvider.Instance.ExecuteScalar(query, new object[] {donDatXeId});
            if(result > 0) return result;
            return 0;
        }

        public bool Insert_CTDD(CTDD ctdd)
        {
            string query = "INSERT INTO CTDD VALUES ( @xeId , @dondatxeId , @ngayThue , @ngayTra , @soluong , @dongia , @thanhtien );";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ctdd.XeId, ctdd.DonDatXeId, ctdd.NgayThue, ctdd.NgayTra, ctdd.SoLuong, ctdd.DonGia, ctdd.ThanhTien });
            return result > 0;
        }

        public int getSoLuongXeOfDonDatXeId(int donDatXeId)
        {
            return (int)DataProvider.Instance.ExecuteScalar("select count(*) from CTDD where donDatXeId = @donDatXeId ", new object[] { donDatXeId });
        }

        public CTDD getCTDDbyXeIdAndDonDatXeId(int xeId, int donDatXeId)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from CTDD where xeId = @xeId and donDatXeId = @donDatXeId ", new object[] { xeId, donDatXeId });
            if(tb.Rows.Count > 0 )
            {
                return new CTDD(tb.Rows[0]);
            }
            return null;
        }

        public bool Update_NgayTra(DateTime ngayTra, int soLuong, decimal thanhTien, int xeId, int donDatXeId)
        {
            return 0 < ((int)DataProvider.Instance.ExecuteNonQuery("update CTDD set ngayTra = @ngayTra , soLuong = @soLuong , thanhTien = @thanhTien  where xeId = @xeId and donDatXeId = @donDatXeId ", new object[] { ngayTra, soLuong, thanhTien, xeId, donDatXeId }));
        }
    }
}
