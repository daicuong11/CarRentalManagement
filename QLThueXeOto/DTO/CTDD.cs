using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class CTDD
    {
        int xeId;
        int donDatXeId;
        DateTime ngayThue;
        DateTime ngayTra;
        int soLuong;
        decimal donGia;
        decimal thanhTien;

        public CTDD()
        {
        }

        public CTDD(int xeId, int donDatXeId, DateTime ngayThue, DateTime ngayTra, int soLuong, decimal donGia, decimal thanhTien)
        {
            this.xeId = xeId;
            this.donDatXeId = donDatXeId;
            this.ngayThue = ngayThue;
            this.ngayTra = ngayTra;
            this.soLuong = soLuong;
            this.donGia = donGia;
            this.thanhTien = thanhTien;
        }

        public CTDD(DataRow row)
        {
            this.xeId = (int)row["xeId"];
            this.donDatXeId = (int)row["donDatXeId"];
            this.ngayThue = (DateTime)row["ngayThue"];
            this.ngayTra = (DateTime)row["ngayTra"];
            this.soLuong = (int)row["soLuong"];
            this.donGia = (decimal)row["donGia"];
            this.thanhTien = (decimal)row["thanhTien"];
        }

        public int XeId { get => xeId; set => xeId = value; }
        public int DonDatXeId { get => donDatXeId; set => donDatXeId = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public decimal DonGia { get => donGia; set => donGia = value; }
        public decimal ThanhTien { get => thanhTien; set => thanhTien = value; }
        public DateTime NgayThue { get => ngayThue; set => ngayThue = value; }
        public DateTime NgayTra { get => ngayTra; set => ngayTra = value; }
    }
}
