using QLThueXeOto.DTO;
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
            return DataProvider.Instance.ExecuteQuery("select * from xe where daXoa = 0");
        }
        
        public DataTable getXeDetail()
        {
            return DataProvider.Instance.ExecuteQuery("select xe.xeId as N'ID', xe.tenXe as N'Tên xe', xe.hangXe as 'Hãng', xe.mauXe as N'Mẫu', xe.bienSoXe as N'Biển số', xe.trangThaiXe as N'Trạng thái' , xe.loaiNhienLieu as N'Nhiên liệu' ,xe.giaChoThue as N'Giá thuê' , loaixe.tenLoaiXe as N'Loại xe' from xe , loaixe where xe.loaiXeId = loaixe.loaiXeId and daXoa = 0");
        }

        public DataTable getXeDetailByTuKhoaTimKiem(string tuKhoaTimKiem)
        {
            string query = "select xe.xeId as N'ID', xe.tenXe as N'Tên xe', xe.hangXe as 'Hãng', xe.mauXe as N'Mẫu', xe.bienSoXe as N'Biển số', xe.trangThaiXe as N'Trạng thái' , xe.loaiNhienLieu as N'Nhiên liệu' ,xe.giaChoThue as N'Giá thuê' , loaixe.tenLoaiXe as N'Loại xe' from xe , loaixe where xe.loaiXeId = loaixe.loaiXeId and daXoa = 0 and (tenXe like N'%"
                        + tuKhoaTimKiem + "%' or hangXe like N'%"
                        + tuKhoaTimKiem + "%' or mauXe like N'%"
                        + tuKhoaTimKiem + "%' or loaixe.tenLoaiXe like N'%"
                        + tuKhoaTimKiem + "%')";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public Xe getXeByXeId(int xeId)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from xe where xeId = @loaiId and daXoa = 0", new object[] { xeId });
            if(tb.Rows.Count > 0)
            {
                return new Xe(tb.Rows[0]);
            }
            return null;
        }

        public Xe getXeByXeIdAndDeleted(int xeId)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from xe where xeId = @loaiId ", new object[] { xeId });
            if (tb.Rows.Count > 0)
            {
                return new Xe(tb.Rows[0]);
            }
            return null;
        }

        public DataTable getXeByLoaiXeId(int loaiId)
        {
            return DataProvider.Instance.ExecuteQuery("select * from xe where loaiXeId = @loaiId and daXoa = 0", new object[] { loaiId });
        }

        public DataTable getXeByTinhNangXeAndLoaiXeId (List<int> lstTinhNangId, int loaiXeId) 
        {
            string query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and daXoa = 0";
            if (lstTinhNangId.Count > 0)
            {
                query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and daXoa = 0 and xeId IN (SELECT xeId FROM tinhnangxe WHERE tinhNangId IN(" + string.Join(", ", lstTinhNangId) + ") GROUP BY xeId HAVING COUNT(DISTINCT tinhNangId) = " + lstTinhNangId.Count + ")";

            }
            return DataProvider.Instance.ExecuteQuery(query, new object[] { loaiXeId });
        }

        public DataTable getXeByTinhNangXeAndLoaiXeIdAndLoaiNhienLieu(List<int> lstTinhNangId, int loaiXeId, string loaiNhienLieu)
        {
            string query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and daXoa = 0 and loaiNhienLieu like N'" + loaiNhienLieu + "'";
            if (lstTinhNangId.Count > 0)
            {
                query = "SELECT * FROM xe WHERE loaiXeId = @loaiId and daXoa = 0 and loaiNhienLieu like N'" + loaiNhienLieu + "' and xeId IN (SELECT xeId FROM tinhnangxe WHERE tinhNangId IN(" + string.Join(", ", lstTinhNangId) + ") GROUP BY xeId HAVING COUNT(DISTINCT tinhNangId) = " + lstTinhNangId.Count + ")";

            }
            return DataProvider.Instance.ExecuteQuery(query, new object[] { loaiXeId });
        }

        public DataTable getXeByTuKhoaTimKiem(int loaiXeId, string tuKhoaTimKiem)
        {
            string query = "select * from xe where loaiXeId = @loaiXeId and daXoa = 0 and (tenXe like N'%"
                + tuKhoaTimKiem + "%' or hangXe like N'%"
                + tuKhoaTimKiem + "%' or mauXe like N'%"
                + tuKhoaTimKiem + "%')";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { loaiXeId});
        }

        public bool setTrangThaiXeChoThueByXeId(int xeId)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("UPDATE xe SET trangThaiXe = N'Đang cho thuê' WHERE xeId = @xeId and daXoa = 0", new object[] { xeId });
            return result > 0;
        }

        public bool Insert_Xe(Xe xe)
        {
            string query = "EXEC InsertXe N'" + xe.TenXe 
                + "', N'" + xe.HangXe 
                + "', N'" + xe.MauXe
                + "', N'" + xe.BienSoXe
                + "', N'" + xe.TrangThaiXe
                + "', N'" + xe.LoaiNhienLieu
                + "', N'" + xe.GiaChoThue
                + "', N'" + xe.LoaiXeId + "'";
            int result = (int)DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public Xe findXebyTenXe(string tenXe)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from xe where tenXe like @tenXe and daXoa = 0", new object[] { tenXe });
            if (tb.Rows.Count > 0)
            {
                return new Xe(tb.Rows[0]);
            }
            return null;
        }
        public Xe findXebyBienSoXe(string bienSoXe)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from xe where bienSoXe like @bienso and daXoa = 0", new object[] { bienSoXe });
            if (tb.Rows.Count > 0)
            {
                return new Xe(tb.Rows[0]);
            }
            return null;
        }

        public bool DelXeById(int id)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("UPDATE xe SET daXoa = 1 WHERE xeId = @id ", new object[] { id });
            return result > 0;
        }

        public bool Update_Xe(Xe xe)
        {
            string query = "UPDATE xe SET tenXe = N'"
                + xe.TenXe + "', hangXe = N'"
                + xe.HangXe + "', mauXe = N'"
                + xe.MauXe + "', bienSoXe = N'"
                + xe.BienSoXe + "', trangThaiXe = N'"
                + xe.TrangThaiXe + "', loaiNhienLieu = N'"
                + xe.LoaiNhienLieu + "', giaChoThue = "
                + xe.GiaChoThue + ", loaiXeId = "
                + xe.LoaiXeId +" WHERE xeId = "
                + xe.XeId + " AND daXoa = 0;";

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool Update_TrangThaiXe(string trangThaiXe, int xeId)
        {
            return 0 < ((int) DataProvider.Instance.ExecuteNonQuery("update xe set trangThaiXe = N'" + trangThaiXe + "' where xeId = @xeId ", new object[] { xeId }));
        }

        public bool Update_TrangThaiXe_ThanhToan(int donDatXeId)
        {
            return 0 < ((int) DataProvider.Instance.ExecuteNonQuery("update xe set trangThaiXe = N'Trống' where xeId in (select xeId from xe where trangThaiXe != N'Hỏng' and xeId in (select xeId from CTDD where donDatXeId = @donDatXeId ))", new object[] {donDatXeId}));
        }
    }
}
