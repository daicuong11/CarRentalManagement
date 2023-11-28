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
    public partial class ThanhToanfrm : Form
    {
        private LichTrinh lichTrinhHienHanh = null;
        public ThanhToanfrm()
        {
            InitializeComponent();
        }

        public ThanhToanfrm(LichTrinh lt)
        {
            InitializeComponent();
            this.lichTrinhHienHanh = lt;
        }

        private void ThanhToanfrm_Load(object sender, EventArgs e)
        {
            LoadChiTietDonDat(ChiTietDonDatBLL.Instance.TableCTDD_ThanhToan(lichTrinhHienHanh.DonDatXeId));
            LoadKhachHang();
        }
        #region
        private void LoadKhachHang()
        {
            DonDatXe donDatXe = DonDatXeDAO.Instance.getDonDatXeById(lichTrinhHienHanh.DonDatXeId);
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
            DonDatXe ddx = DonDatXeDAO.Instance.getDonDatXeById((int)lichTrinhHienHanh.DonDatXeId);
            txtTongTien.Text = ddx.TongGia.ToString();
            lbSoLuongXe.Text = CTDDDAO.Instance.getSoLuongXeOfDonDatXeId(lichTrinhHienHanh.DonDatXeId).ToString();
            dgvXeKhachThue.DataSource = listCTDD;
            dgvXeKhachThue.Refresh();
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvXeKhachThue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ cột cụ thể trong dòng được click
                int xeId = (int)dgvXeKhachThue.Rows[e.RowIndex].Cells["ID"].Value;
                CTDD ctdd = CTDDDAO.Instance.getCTDDbyXeIdAndDonDatXeId(xeId, this.lichTrinhHienHanh.DonDatXeId);
                if (ctdd != null)
                {
                    ModelUpdateDonDatXefrm frm = new ModelUpdateDonDatXefrm(ctdd);
                    frm.ShowDialog();
                    decimal tongTien = CTDDDAO.Instance.getTongTienByDonDatXeId(lichTrinhHienHanh.DonDatXeId);
                    DonDatXeDAO.Instance.Update_TongTienByDonDatXeId(tongTien, lichTrinhHienHanh.DonDatXeId);
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn xe thuê nào");
                }
                
                LoadChiTietDonDat(ChiTietDonDatBLL.Instance.TableCTDD_ThanhToan(lichTrinhHienHanh.DonDatXeId));

            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            bool resultThanhToanLichTrinh = LichTrinhDAO.Instance.Update_ThanhToan(lichTrinhHienHanh.LichTrinhId);
            if(resultThanhToanLichTrinh)
            {
                //xữ lý xe trống
                XeDAO.Instance.Update_TrangThaiXe_ThanhToan(lichTrinhHienHanh.DonDatXeId);
                //xữ lý lưu thanh toán
                int nguoiDungId = AuthDAO.Instance.User.NguoiDungId;
                decimal tongTien = CTDDDAO.Instance.getTongTienByDonDatXeId(lichTrinhHienHanh.DonDatXeId);
                QuanLyThanhToan qltt = new QuanLyThanhToan(lichTrinhHienHanh.LichTrinhId, nguoiDungId, tongTien);

                QuanLyThanhToanDAO.Instance.Insert_ThanhToan(qltt);
                MessageBox.Show("Thanh toán thành công", "Thông báo");
                this.Close();
            }
        }
    }
}
