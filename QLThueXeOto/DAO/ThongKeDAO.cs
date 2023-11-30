using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace QLThueXeOto.DAO
{
    public class ThongKeDAO
    {
        private static ThongKeDAO instance;

        public static ThongKeDAO Instance
        {
            get { if (instance == null) instance = new ThongKeDAO(); return ThongKeDAO.instance; }
            private set { ThongKeDAO.instance = value; }
        }

        public DataTable GetDoanhThuTheoHangXe()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT xe.hangXe AS HangXe, SUM(thanhTien) AS DoanhThu FROM CTDD JOIN xe ON CTDD.xeId = xe.xeId WHERE xe.hangXe in (select hangXe from xe) GROUP BY xe.hangXe;");
        }

        public DataTable GetDoanhThuTheoMauXe()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT xe.mauXe AS MauXe, SUM(thanhTien) AS DoanhThu FROM CTDD JOIN xe ON CTDD.xeId = xe.xeId WHERE xe.mauXe in (select mauXe from xe) GROUP BY xe.mauXe;");
        }

        public DataTable GetPhanBoXeTheoLoaiXe(int loaiXeId)
        {
            return DataProvider.Instance.ExecuteQuery("SELECT xe.tenXe AS TenXe , count(*) AS SoLuong FROM CTDD JOIN xe ON CTDD.xeId = xe.xeId where xe.loaiXeId = @loaixeId group by xe.tenXe", new object[] { loaiXeId});
        }

        public decimal GetTotalByTimes(String tuNgay, String denNgay)
        {
            var result = DataProvider.Instance.ExecuteScalar("select SUM(tongTien) from lichtrinh, quanlythanhtoan where lichtrinh.lichTrinhId = quanlythanhtoan.lichTrinhId and ngayThanhToan between '" + tuNgay + "' and '" + denNgay + "'");
            //MessageBox.Show("result= " + result.ToString());
            if (String.IsNullOrEmpty(result.ToString()))
            {
                return 0;
            }
            return (decimal)result;
        }

        public int CountDonDatXeByTime(String tuNgay, String denNgay)
        {
            return (int) DataProvider.Instance.ExecuteScalar("select count(*) from dondatxe  where ngayLap between '" + tuNgay + "' and '" + denNgay + "'");
        }

        public int CountXeHong()
        {
            return (int)DataProvider.Instance.ExecuteScalar("select count(*) from xe where trangThaiXe = N'Hỏng'");
        }
    }
}
