using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.BLL
{
    public class HopDongThueXeBLL
    {
        private static HopDongThueXeBLL instance;
        private int widthBtn = 250;
        private int heightBtn = 190;

        public static HopDongThueXeBLL Instance
        {
            get { if (instance == null) instance = new HopDongThueXeBLL(); return HopDongThueXeBLL.instance; }
            private set { HopDongThueXeBLL.instance = value; }
        }

        public int WidthBtn { get => widthBtn; set => widthBtn = value; }
        public int HeightBtn { get => heightBtn; set => heightBtn = value; }

        public List<LoaiXe> loaiXes()
        {
            List<LoaiXe> lst = new List<LoaiXe>();
            DataTable table = LoaiXeDAO.Instance.getAll();

            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new LoaiXe(dr));
            }
            return lst;
        }

        public List<LoaiXe> ListLoaiXeBySoChoNgoi( int soChoNgoi)
        {
            List<LoaiXe> lst = new List<LoaiXe>();
            DataTable table = LoaiXeDAO.Instance.getLoaiXeBySoChoNgoi(soChoNgoi);

            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new LoaiXe(dr));
            }
            return lst;
        }

        public List<LoaiXe> ListLoaiXeByKieuDang(string kieuDang)
        {
            List<LoaiXe> lst = new List<LoaiXe>();
            DataTable table = LoaiXeDAO.Instance.getLoaiXeByKieuDang(kieuDang);

            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new LoaiXe(dr));
            }
            return lst;
        }

        public List<LoaiXe> ListLoaiXeByKieuDangAndSoChoNgoi(string kieuDang, int soChoNgoi)
        {
            List<LoaiXe> lst = new List<LoaiXe>();
            DataTable table = LoaiXeDAO.Instance.getLoaiXeByKieuDangAndSoChoNgoi(kieuDang, soChoNgoi);

            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new LoaiXe(dr));
            }
            return lst;
        }
    }
}
