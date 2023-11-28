using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class Xe
    {
        int xeId;
        string tenXe;
        string hangXe;
        string mauXe;
        string bienSoXe;
        string trangThaiXe;
        string loaiNhienLieu;
        decimal giaChoThue;
        int loaiXeId;

        public Xe()
        {
        }

        public Xe(string tenXe, string hangXe, string mauXe, string bienSoXe, string trangThaiXe, string loaiNhienLieu, decimal giaChoThue, int loaiXeId)
        {
            this.tenXe = tenXe;
            this.hangXe = hangXe;
            this.mauXe = mauXe;
            this.bienSoXe = bienSoXe;
            this.trangThaiXe = trangThaiXe;
            this.loaiNhienLieu = loaiNhienLieu;
            this.giaChoThue = giaChoThue;
            this.loaiXeId = loaiXeId;
        }

        public Xe(int xeId, string tenXe, string hangXe, string mauXe, string bienSoXe, string trangThaiXe, string loaiNhienLieu, decimal giaChoThue, int loaiXeId)
        {
            this.xeId = xeId;
            this.tenXe = tenXe;
            this.hangXe = hangXe;
            this.mauXe = mauXe;
            this.bienSoXe = bienSoXe;
            this.trangThaiXe = trangThaiXe;
            this.loaiNhienLieu = loaiNhienLieu;
            this.giaChoThue = giaChoThue;
            this.loaiXeId = loaiXeId;
        }

        public Xe(DataRow row)
        {
            this.xeId = (int)row["xeId"];
            this.tenXe = row["tenXe"].ToString();
            this.hangXe = row["hangXe"].ToString();
            this.mauXe = row["mauXe"].ToString();
            this.bienSoXe = row["bienSoXe"].ToString();
            this.trangThaiXe = row["trangThaiXe"].ToString();
            this.loaiNhienLieu = row["loaiNhienLieu"].ToString();
            this.giaChoThue = (decimal)row["giaChoThue"];
            this.loaiXeId = (int)row["loaiXeId"];
        }

        public int XeId { get => xeId; set => xeId = value; }
        public string TenXe { get => tenXe; set => tenXe = value; }
        public string HangXe { get => hangXe; set => hangXe = value; }
        public string MauXe { get => mauXe; set => mauXe = value; }
        public string BienSoXe { get => bienSoXe; set => bienSoXe = value; }
        public string TrangThaiXe { get => trangThaiXe; set => trangThaiXe = value; }
        public decimal GiaChoThue { get => giaChoThue; set => giaChoThue = value; }
        public int LoaiXeId { get => loaiXeId; set => loaiXeId = value; }
        public string LoaiNhienLieu { get => loaiNhienLieu; set => loaiNhienLieu = value; }

        public override string ToString()
        {
            return "Tên:"  + tenXe + Environment.NewLine 
                + "Hãng: " + hangXe + Environment.NewLine 
                + "Mẫu: " + mauXe +Environment.NewLine 
                + "Biển số: " + bienSoXe + Environment.NewLine 
                + "Nhiên liệu: " + loaiNhienLieu + Environment.NewLine
                + "Giá: " + giaChoThue + "$/ngày" + Environment.NewLine  
                + "Trạng thái: " + trangThaiXe;
        }
    }
}
