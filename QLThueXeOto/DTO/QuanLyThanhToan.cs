using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class QuanLyThanhToan
    {
        int lichTrinhId;
        int nguoiDungId;
        string daTraXe;
        decimal tongTien;

        public QuanLyThanhToan()
        {
        }

        public QuanLyThanhToan(int lichTrinhId, int nguoiDungId, decimal tongTien)
        {
            this.lichTrinhId = lichTrinhId;
            this.nguoiDungId = nguoiDungId;
            this.tongTien = tongTien;
        }

        public int LichTrinhId { get => lichTrinhId; set => lichTrinhId = value; }
        public int NguoiDungId { get => nguoiDungId; set => nguoiDungId = value; }
        public string DaTraXe { get => daTraXe; set => daTraXe = value; }
        public decimal TongTien { get => tongTien; set => tongTien = value; }
    }
}
