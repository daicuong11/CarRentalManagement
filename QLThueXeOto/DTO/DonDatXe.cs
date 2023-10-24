using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QLThueXeOto.DTO
{
    public class DonDatXe
    {
        int donDatXeId;
        DateTime? ngayLap;
        decimal tongGia;
        int khachHangId;
        int nguoiDungId;

        public DonDatXe()
        {
        }

        public DonDatXe(decimal tongGia, int khachHangId, int nguoiDungId)
        {
            this.tongGia = tongGia;
            this.khachHangId = khachHangId;
            this.nguoiDungId = nguoiDungId;
        }

        public DonDatXe(DateTime ngayLap, decimal tongGia, int khachHangId, int nguoiDungId)
        {
            this.ngayLap = ngayLap;
            this.tongGia = tongGia;
            this.khachHangId = khachHangId;
            this.nguoiDungId = nguoiDungId;
        }

        public DonDatXe(DataRow row)
        {
            this.donDatXeId = (int)row["donDatXeId"];
            this.ngayLap = (DateTime)row["ngayLap"];
            this.tongGia = (Decimal)row["tongGia"];
            this.khachHangId = (int)row["khachHangId"];
            this.nguoiDungId = (int)row["nguoiDungId"];
        }

        public int DonDatXeId { get => donDatXeId; set => donDatXeId = value; }
        public decimal TongGia { get => tongGia; set => tongGia = value; }
        public int KhachHangId { get => khachHangId; set => khachHangId = value; }
        public int NguoiDungId { get => nguoiDungId; set => nguoiDungId = value; }
        public DateTime? NgayLap { get => ngayLap; set => ngayLap = value; }

        public override string ToString()
        {
            return "Ngày lập đơn: " + ngayLap + Environment.NewLine
                + "Tổng tiền: " + tongGia + Environment.NewLine
                + "Id Khách: " + khachHangId + Environment.NewLine
                + "Id nhân viên: " + nguoiDungId;
        }
    }
}
