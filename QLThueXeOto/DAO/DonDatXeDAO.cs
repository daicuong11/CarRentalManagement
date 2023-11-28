using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DonDatXe getDonDatXeById(int id)
        {
            DataTable tb = DataProvider.Instance.ExecuteQuery("select * from dondatxe where donDatXeId = @donDatXeId ", new object[] { id });
            if(tb.Rows.Count > 0)
            {
                return new DonDatXe(tb.Rows[0]);
            }
            return null;
        }

        public int Insert_DonDatXe(DonDatXe donDatXe)
        {
            if(this.donDatXeHienHanhId < 0)
            {
                string query = "INSERT INTO dondatxe (tongGia, khachHangId, nguoiDungId) OUTPUT INSERTED.donDatXeId VALUES ( @tonggia , @khackHangId , @nguoidungid )";
                int result = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { donDatXe.TongGia, donDatXe.KhachHangId, donDatXe.NguoiDungId });
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

        public DataTable getAll()
        {
            return DataProvider.Instance.ExecuteQuery("select * from dondatxe");
        }


        public bool Update_TongTienByDonDatXeId(decimal tongTien, int donDatXeId)
        {
            string query = "UPDATE dondatxe SET tongGia = @tongTien WHERE donDatXeId = @dondatxeId ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tongTien, donDatXeId });
            return (result > 0);
        }
    }
}
