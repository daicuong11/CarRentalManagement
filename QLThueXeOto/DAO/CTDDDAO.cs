using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
