using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class LoaiXe
    {
        int loaiXeId;
        string tenLoaiXe;
        int soChoNgoi;
        string loaiNhienLieu;

        public LoaiXe()
        {
        }

        public LoaiXe(DataRow row)
        {
            this.loaiXeId = (int)row["loaiXeId"];
            this.tenLoaiXe = row["tenLoaiXe"].ToString();
            this.soChoNgoi = (int)row["soChoNgoi"];
            this.loaiNhienLieu = row["loaiNhienLieu"].ToString();
        }

        public int LoaiXeId { get => loaiXeId; set => loaiXeId = value; }
        public string TenLoaiXe { get => tenLoaiXe; set => tenLoaiXe = value; }
        public int SoChoNgoi { get => soChoNgoi; set => soChoNgoi = value; }
        public string LoaiNhienLieu { get => loaiNhienLieu; set => loaiNhienLieu = value; }
    }
}
