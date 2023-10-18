using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QLThueXeOto.BLL
{
    public class DangNhapBLL
    {
        private static DangNhapBLL instance;
        public static DangNhapBLL Instance
        {
            get { if (instance == null) instance = new DangNhapBLL(); return DangNhapBLL.instance; }
            private set { DangNhapBLL.instance = value; }
        }

        public string checkFormLogin(string username, string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                return "Tên tài khoản không được để trống!";
            }
            else if(String.IsNullOrEmpty(password)) {
                return "Mật khẩu không được để trống!";
            }
            return null;
        }

        public bool checkLogin(string username, string password)
        {
            NguoiDung user = AuthDAO.Instance.login(username, password);
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
