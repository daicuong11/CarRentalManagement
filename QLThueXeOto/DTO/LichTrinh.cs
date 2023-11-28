using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class LichTrinh
    {
        int lichTrinhId;
        string diemDen;
        DateTime? ngayThanhToan;
        string trangThai;
        int donDatXeId;

        public LichTrinh(DateTime? ngayThanhToan, string trangThai, int donDatXeId, string diemDen = null)
        {
            this.ngayThanhToan = ngayThanhToan;
            this.trangThai = trangThai;
            this.donDatXeId = donDatXeId;
            this.diemDen = diemDen;
        }

        public LichTrinh(DataRow row) 
        {
            this.lichTrinhId = (int)row["lichTrinhId"];
            this.diemDen = row["diemDen"].ToString();
            if(!string.IsNullOrEmpty(row["ngayThanhToan"].ToString()))
            {
                this.ngayThanhToan = (DateTime)row["ngayThanhToan"];
            }
            else
            {
                this.ngayThanhToan = null;
            }
            this.trangThai = row["trangThai"].ToString();
            this.donDatXeId = (int)row["donDatXeId"];
        }


        public int LichTrinhId { get => lichTrinhId; set => lichTrinhId = value; }
        public DateTime? NgayThanhToan { get => ngayThanhToan; set => ngayThanhToan = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public int DonDatXeId { get => donDatXeId; set => donDatXeId = value; }
    }
}
