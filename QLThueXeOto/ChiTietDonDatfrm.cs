using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThueXeOto
{
    public partial class ChiTietDonDatfrm : Form
    {
        private int donDatXeId = -1;
        public ChiTietDonDatfrm()
        {
            InitializeComponent();
        }
        public ChiTietDonDatfrm(int donDatXeId)
        {
            InitializeComponent();
            this.donDatXeId = donDatXeId;
        }

        private void ChiTietDonDatfrm_Load(object sender, EventArgs e)
        {
            //Load
            LoadChiTietDonDat(ChiTietDonDatBLL.Instance.TableCTDD(this.donDatXeId));
            LoadKhachHang();
        }

        #region
        private void LoadKhachHang()
        {
            DonDatXe donDatXe = DonDatXeDAO.Instance.getDonDatXeById(donDatXeId);
            KhachHang kh = KhachHangDAO.Instance.getKhachHangByIdAndDeleted(donDatXe.KhachHangId);
            if (kh != null)
            {
                txtHoVaTen.Text = kh.HoTen;
                txtSoDienThoai.Text = kh.SoDienThoai;
                txtDiaChi.Text = kh.DiaChi;
            }
        }
        private void LoadChiTietDonDat(DataTable listCTDD)
        {
            DonDatXe ddx = DonDatXeDAO.Instance.getDonDatXeById((int)donDatXeId);
            txtTongTien.Text = ddx.TongGia.ToString();
            dgvChiTietDonDat.DataSource = listCTDD;
            dgvChiTietDonDat.Refresh();
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
