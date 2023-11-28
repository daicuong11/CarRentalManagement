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
    public class QuanLyDonDatXeBLL
    {
        private static QuanLyDonDatXeBLL instance;

        public static QuanLyDonDatXeBLL Instance
        {
            get { if (instance == null) instance = new QuanLyDonDatXeBLL(); return QuanLyDonDatXeBLL.instance; }
            private set { QuanLyDonDatXeBLL.instance = value; }
        }

        public DataTable TableDonDatXe()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ID", typeof(int));
            tb.Columns.Add("Khách hàng", typeof(string));
            tb.Columns.Add("Người lập", typeof(string));
            tb.Columns.Add("Ngày lập", typeof(DateTime));
            tb.Columns.Add("Tổng tiền", typeof(decimal));

            DataTable dsDonDatXe = DonDatXeDAO.Instance.getAll();
            foreach (DataRow dr in dsDonDatXe.Rows)
            {
                DonDatXe donDatXe = new DonDatXe(dr);
                NguoiDung nguoiDung = NguoiDungDAO.Instance.getNguoiDungById(donDatXe.NguoiDungId);
                KhachHang khachHang = KhachHangDAO.Instance.getKhachHangByIdAndDeleted(donDatXe.KhachHangId);
                DataRow newRow = tb.NewRow();
                newRow["ID"] = donDatXe.DonDatXeId;
                newRow["Khách hàng"] = khachHang.HoTen;
                newRow["Người lập"] = nguoiDung.TenNguoiDung;
                newRow["Ngày lập"] = donDatXe.NgayLap;
                newRow["Tổng tiền"] = donDatXe.TongGia;
                tb.Rows.Add(newRow);
            }
            return tb;
        }
    }
}
