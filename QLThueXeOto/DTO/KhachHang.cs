using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class KhachHang
    {
        int khachHangHangId;
        string hoTen;
        string soDienThoai;
        string diaChi;

        public KhachHang()
        {
        }

        public KhachHang(string hoTen, string soDienThoai, string diaChi)
        {
            this.hoTen = hoTen;
            this.soDienThoai = soDienThoai;
            this.diaChi = diaChi;
        }

        public KhachHang(int khachHangHangId, string hoTen, string soDienThoai, string diaChi)
        {
            this.khachHangHangId = khachHangHangId;
            this.hoTen = hoTen;
            this.soDienThoai = soDienThoai;
            this.diaChi = diaChi;
        }

        public KhachHang(DataRow row)
        {
            this.khachHangHangId = (int)row["khachHangId"];
            this.hoTen = row["hoTen"].ToString();
            this.soDienThoai = row["soDienThoai"].ToString();
            this.diaChi = row["diaChi"].ToString();
        }

        public int KhachHangHangId { get => khachHangHangId; set => khachHangHangId = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}
