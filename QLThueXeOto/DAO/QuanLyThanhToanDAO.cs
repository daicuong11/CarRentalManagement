using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class QuanLyThanhToanDAO
    {
        private static QuanLyThanhToanDAO instance;

        public static QuanLyThanhToanDAO Instance
        {
            get { if (instance == null) instance = new QuanLyThanhToanDAO(); return QuanLyThanhToanDAO.instance; }
            private set { QuanLyThanhToanDAO.instance = value; }
        }

        public bool Insert_ThanhToan(QuanLyThanhToan qltt)
        {
            return 0 < DataProvider.Instance.ExecuteNonQuery("insert into quanlythanhtoan values ( @lichTrinhId , @nguoiDungId , N'Đã trả xe', @tongTien )", new object[] {qltt.LichTrinhId, qltt.NguoiDungId, qltt.TongTien});
        }
    }
}
