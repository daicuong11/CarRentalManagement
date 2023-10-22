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
        DateTime ngayThue;
        DateTime ngayTra;
        decimal tongGia;
        int khachHangId;
        int nguoiDungId;

        public DonDatXe()
        {
        }

        public DonDatXe(DateTime ngayThue, DateTime ngayTra, decimal tongGia, int khachHangId, int nguoiDungId)
        {
            this.ngayThue = ngayThue;
            this.ngayTra = ngayTra;
            this.tongGia = tongGia;
            this.khachHangId = khachHangId;
            this.nguoiDungId = nguoiDungId;
        }

        public DonDatXe(DataRow row)
        {
            this.donDatXeId = (int)row["donDatXeId"];
            this.ngayThue = (DateTime)row["ngayThue"];
            this.ngayTra = (DateTime)row["ngayTra"];
            this.tongGia = (Decimal)row["tongGia"];
            this.khachHangId = (int)row["khachHangId"];
            this.nguoiDungId = (int)row["nguoiDungId"];
        }

        public int DonDatXeId { get => donDatXeId; set => donDatXeId = value; }
        public DateTime NgayTra { get => ngayTra; set => ngayTra = value; }
        public DateTime NgayThue { get => ngayThue; set => ngayThue = value; }
        public decimal TongGia { get => tongGia; set => tongGia = value; }
        public int KhachHangId { get => khachHangId; set => khachHangId = value; }
        public int NguoiDungId { get => nguoiDungId; set => nguoiDungId = value; }

        public override string ToString()
        {
            return "Ngày thuê: " + ngayThue + Environment.NewLine
                + "Ngày trả: " + ngayTra + Environment.NewLine
                + "Tổng tiền: " + tongGia + Environment.NewLine
                + "Id Khách: " + khachHangId + Environment.NewLine
                + "Id nhân viên: " + nguoiDungId;
        }
    }
}
