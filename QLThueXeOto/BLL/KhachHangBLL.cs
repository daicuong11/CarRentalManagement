using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QLThueXeOto.BLL
{
    public class KhachHangBLL
    {
        private static KhachHangBLL instance;

        public static KhachHangBLL Instance
        {
            get { if (instance == null) instance = new KhachHangBLL(); return KhachHangBLL.instance; }
            private set => instance = value;
        }
        public List<KhachHang> ListKhachHang()
        {
            List<KhachHang> lst = new List<KhachHang>();
            DataTable table = KhachHangDAO.Instance.getAllKhachHang();

            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new KhachHang(dr));
            }
            return lst;
        }
        public int InsertKhachHang(KhachHang kh)
        {
            int check = KhachHangDAO.Instance.Insert_KhachHang(kh);
            return check;
        }
        public bool UpdateKhachHang(KhachHang kh)
        {
            bool check = KhachHangDAO.Instance.Update_KhachHang(kh);
            return check;
        }
        public bool DeleteKhachHang(int ma)
        {
            bool check = KhachHangDAO.Instance.Delete_KhachHang(ma);
            return check;
        }
        public int CheckDuplicateKhachHang(string sdt)
        {
            return KhachHangDAO.Instance.getKhachHangBySoDienThoai(sdt);
        }
        public List<KhachHang> ListKhachHangTuKhoaTimKiem(string tuKhoaSearch)
        {
            List<KhachHang> list = new List<KhachHang>();
            DataTable kh = KhachHangDAO.Instance.getKhachHangByTuKhoaTimKiem(tuKhoaSearch);
            foreach (DataRow dr in kh.Rows)
            {
                list.Add(new KhachHang(dr));
            }
            return list;
        }
    }
}
