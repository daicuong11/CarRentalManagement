using QLThueXeOto.BLL;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class LoaiXeDAO
    {
        private static LoaiXeDAO instance;

        public static LoaiXeDAO Instance
        {
            get { if (instance == null) instance = new LoaiXeDAO(); return LoaiXeDAO.instance; }
            private set { LoaiXeDAO.instance = value; }
        }

        public DataTable getAll()
        {
            return DataProvider.Instance.ExecuteQuery("select * from loaixe");
        }

        public DataTable getLoaiXeBySoChoNgoi(int soChoNgoi)
        {
            return DataProvider.Instance.ExecuteQuery("select * from loaixe where soChoNgoi = @soChoNgoi ", new object[] { soChoNgoi });
        }

        public DataTable getLoaiXeByKieuDang(string kieuDang)
        {
            string query = "select * from loaixe where tenLoaiXe like N'%" + kieuDang + "%'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable getLoaiXeByKieuDangAndSoChoNgoi(string kieuDang, int soChoNgoi)
        {
            string query = "select * from loaixe where tenLoaiXe like N'%" + kieuDang + "%' and soChoNgoi = @soChoNgoi";
            return DataProvider.Instance.ExecuteQuery(query, new object[] {soChoNgoi});
        }

        public LoaiXe getLoaiXeById(int id)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from loaixe where loaiXeId = @id ", new object[] { id });
            if(tb.Rows.Count > 0)
            {
                return new LoaiXe(tb.Rows[0]);
            }
            return null;
        }
    }
}
