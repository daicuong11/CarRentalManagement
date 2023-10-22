using QLThueXeOto.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThueXeOto.BLL
{
    public class DonDatXeBLL
    {
        private static DonDatXeBLL instance;

        public static DonDatXeBLL Instance
        {
            get { if (instance == null) instance = new DonDatXeBLL(); return DonDatXeBLL.instance; }
            private set { DonDatXeBLL.instance = value; }
        }

        public string Check_Input_KhachHang(string ten , string sdt, string diachi)
        {
            if (String.IsNullOrEmpty(ten))
            {
                return "Vui lòng nhập tên khách hàng";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                return "Vui lòng nhập số điện thoại khách hàng";
            }
            else if(String.IsNullOrEmpty(diachi))
            {
                return "Vui long nhập địa chỉ khách hàng";
            }
            return null;
        }
    }
}
