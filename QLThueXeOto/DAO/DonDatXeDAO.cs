using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DAO
{
    public class DonDatXeDAO
    {
        private static DonDatXeDAO instance;
        private int donDatXeHienHanhId = -1;
        private int khachHangHienHanhId = -1;

        public static DonDatXeDAO Instance
        {
            get { if (instance == null) instance = new DonDatXeDAO(); return DonDatXeDAO.instance; }
            private set { DonDatXeDAO.instance = value; }
        }

        public int DonDatXeHienHanhId { get => donDatXeHienHanhId; set => donDatXeHienHanhId = value; }
        public int KhachHangHienHanhId { get => khachHangHienHanhId; set => khachHangHienHanhId = value; }

        public void setKH_ID_And_DDX_ID(int khachHangHienHanhId, int donDatXeHienHanhId)
        {
            this.khachHangHienHanhId = khachHangHienHanhId;
            this.donDatXeHienHanhId = donDatXeHienHanhId;
        }


        public int Insert_DonDatXe(DonDatXe donDatXe)
        {
            if(this.donDatXeHienHanhId < 0)
            {
                string query = "INSERT INTO dondatxe OUTPUT INSERTED.donDatXeId VALUES ( @ngaythue , @ngaytra , @tonggia , @khackHangId , @nguoidungid )";
                int result = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { donDatXe.NgayThue, donDatXe.NgayTra, donDatXe.TongGia, donDatXe.KhachHangId, donDatXe.NguoiDungId });
                if (result > 0)
                {
                    this.donDatXeHienHanhId = result;
                    return result;
                }
            }
            return this.donDatXeHienHanhId;

        }
        public bool Update_TongTien(decimal tongTien)
        {
            string query = "UPDATE dondatxe SET tongGia = @tongTien WHERE donDatXeId = @dondatxeId ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tongTien, this.donDatXeHienHanhId });
            return (result > 0);
        }
    }
}
