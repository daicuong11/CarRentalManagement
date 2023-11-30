using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.DTO
{
    public class NguoiDung
    {
        int nguoiDungId;
        string tenNguoiDung;
        string username;
        string password;
        string diaChi;
        int quyenId;

        public NguoiDung()
        {
        }

        public NguoiDung(DataRow row)
        {
            this.nguoiDungId = (int)row["nguoiDungId"];
            this.tenNguoiDung = row["tenNguoiDung"].ToString();
            this.username = row["username"].ToString();
            this.password = row["password"].ToString();
            this.diaChi = row["diaChi"].ToString();
            this.quyenId = (int)row["quyenId"];
        }

        public int NguoiDungId { get => nguoiDungId; set => nguoiDungId = value; }
        public string TenNguoiDung { get => tenNguoiDung; set => tenNguoiDung = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public int QuyenId { get => quyenId; set => quyenId = value; }

        public override string ToString()
        {
            return this.nguoiDungId + ", " + this.tenNguoiDung + ", " + quyenId;
        }
    }
}
