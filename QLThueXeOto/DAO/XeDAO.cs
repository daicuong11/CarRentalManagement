using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThueXeOto.DAO
{
    public class XeDAO
    {
        private static XeDAO instance;
        public static XeDAO Instance
        {
            get { if (instance == null) { instance = new XeDAO(); } return XeDAO.instance;  }
            private set { instance = value; }
        }

        public DataTable getAll()
        {
            return DataProvider.Instance.ExecuteQuery("select * from xe");
        }

        public DataTable getXeByLoaiXeId(int loaiId)
        {
            return DataProvider.Instance.ExecuteQuery("select * from xe where loaiXeId = @loaiId ", new object[] { loaiId });
        }

        public DataTable getXeByTinhNangXeAndLoaiXeId (List<int> lstTinhNangId, int loaiXeId) 
        {
            string query = "SELECT * FROM xe WHERE loaiXeId = @loaiId";
            if (lstTinhNangId.Count > 0)
            {
                query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and xeId IN (SELECT xeId FROM tinhnangxe WHERE tinhNangId IN(" + string.Join(", ", lstTinhNangId) + ") GROUP BY xeId HAVING COUNT(DISTINCT tinhNangId) = " + lstTinhNangId.Count + ")";

            }
            return DataProvider.Instance.ExecuteQuery(query, new object[] { loaiXeId });
        }

        public DataTable getXeByTinhNangXeAndLoaiXeIdAndLoaiNhienLieu(List<int> lstTinhNangId, int loaiXeId, string loaiNhienLieu)
        {
            string query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and loaiNhienLieu like N'" + loaiNhienLieu + "'";
            if (lstTinhNangId.Count > 0)
            {
                query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and loaiNhienLieu like N'" + loaiNhienLieu + "' and xeId IN (SELECT xeId FROM tinhnangxe WHERE tinhNangId IN(" + string.Join(", ", lstTinhNangId) + ") GROUP BY xeId HAVING COUNT(DISTINCT tinhNangId) = " + lstTinhNangId.Count + ")";

            }
            return DataProvider.Instance.ExecuteQuery(query, new object[] { loaiXeId });
        }

        public DataTable getXeByTuKhoaTimKiem(int loaiXeId, string tuKhoaTimKiem)
        {
            string query = "select * from xe where loaiXeId = @loaiXeId and (tenXe like N'%"
                + tuKhoaTimKiem + "%' or hangXe like N'%"
                + tuKhoaTimKiem + "%' or mauXe like N'%"
                + tuKhoaTimKiem + "%')";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { loaiXeId});
        }

        public bool setTrangThaiXeChoThueByXeId(int xeId)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("UPDATE xe SET trangThaiXe = N'Đang cho thuê' WHERE xeId = @xeId ", new object[] { xeId });
            return result > 0;
        }
    }
}
