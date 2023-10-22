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
    public partial class DonDatXefrm : Form
    {
        private Xe xeDuocChon = null;
        //att
        private int newDonDatXeId = -1;
        private int khachHangId = -1;
        public DonDatXefrm()
        {
            InitializeComponent();
        }

        public DonDatXefrm(Xe xe)
        {
            InitializeComponent();
            this.xeDuocChon = (Xe)xe;
        }

        private void DonDatXefrm_Load(object sender, EventArgs e)
        {
            LoadXeDuocChon();
            LoadDateTimePicker();
            LoadKhachHang();
        }

        #region
        private void LoadKhachHang()
        {
            int khId = DonDatXeDAO.Instance.KhachHangHienHanhId;
            KhachHang kh = KhachHangDAO.Instance.getKhachHangById(khId);
            if(kh != null)
            {
                txtHoVaTen.Text = kh.HoTen;
                txtSoDienThoai.Text = kh.SoDienThoai;
                txtDiaChi.Text = kh.DiaChi;
            }
        }
        private void LoadDateTimePicker()
        {
            DateTime ngayHienTai = DateTime.Now;

            //set ngày trả xe
            dtNgayTra.MinDate = ngayHienTai;
            dtNgayTra.Value = ngayHienTai.AddDays(1);

            //set ngày thuê
            dtNgayThue.MinDate = ngayHienTai;
            dtNgayThue.Value = ngayHienTai;
        }

        private void LoadXeDuocChon()
        {
            if(xeDuocChon != null)
            {
                txtTenXe.Text = xeDuocChon.TenXe;
                txtHangXe.Text = xeDuocChon.HangXe;
                txtMauXe.Text = xeDuocChon.MauXe;
                txtNhienLieu.Text = xeDuocChon.LoaiNhienLieu;
                txtBienSo.Text = xeDuocChon.BienSoXe;
                txtGia.Text = xeDuocChon.GiaChoThue.ToString() + "$/ngày.";
            }
        }

        private void Update_TongSoNgayThue()
        {
            DateTime ngayThue = dtNgayThue.Value;
            DateTime ngayTra = dtNgayTra.Value;

            // Tính số ngày đặt bằng cách trừ ngày trả xe cho ngày thuê
            TimeSpan soNgayDat = ngayTra - ngayThue;

            int soNgayDatInt = soNgayDat.Days;
            lblTongNgayDat.Text = "$/" + soNgayDatInt + " ngày.";

            txtTongTien.Text = (xeDuocChon.GiaChoThue * soNgayDatInt).ToString();
        }

        private KhachHang getKhachHangFormInput()
        {
            string hoTen = txtHoVaTen.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string message = DonDatXeBLL.Instance.Check_Input_KhachHang(hoTen, soDienThoai, diaChi);
            if (!String.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Thông báo");
                return null;
            }
            return new KhachHang(hoTen, soDienThoai, diaChi);
        }

        #endregion

        private void dtNgayThue_ValueChanged(object sender, EventArgs e)
        {
            Update_TongSoNgayThue();
        }

        private void dtNgayTra_ValueChanged(object sender, EventArgs e)
        {
            Update_TongSoNgayThue();
        }

        private void btnDatXe_Click(object sender, EventArgs e)
        {
            KhachHang kh = this.getKhachHangFormInput();
            if (kh == null) return;

            //save khachhang
            //check KhachHang đã tồn tại bằng số điện thoại
            khachHangId = KhachHangDAO.Instance.getKhachHangBySoDienThoai(kh.SoDienThoai);
            if(khachHangId < 0)
            {
                khachHangId = KhachHangDAO.Instance.Insert_KhachHang(kh);
            }

            //get nguoiDungId
            int nguoiDungId = AuthDAO.Instance.User.NguoiDungId;

            //get xeId
            int xeId = this.xeDuocChon.XeId;

            //get ngày thuê xe, ngày trả xe
            DateTime ngayThue = dtNgayThue.Value;
            DateTime ngayTra = dtNgayTra.Value;

            // get số ngày thuê
            TimeSpan soNgayDat = ngayTra - ngayThue;
            int soNgayThue = soNgayDat.Days;

            // get đơn giá thuê xe
            decimal donGia = xeDuocChon.GiaChoThue;

            //get tổng tiền
            decimal tongTien =(decimal)(donGia * soNgayThue);

            // Kiểm tra thêm mới hay thêm xe
            int donDatXeHienHanhId = DonDatXeDAO.Instance.DonDatXeHienHanhId;
            if(donDatXeHienHanhId < 0)
            {
                //new mới đơn đặt xe
                DonDatXe donDatXe = new DonDatXe(ngayThue, ngayTra, tongTien, khachHangId, nguoiDungId);
                // insert đơn đặt xe
                newDonDatXeId = DonDatXeDAO.Instance.Insert_DonDatXe(donDatXe);
                if (newDonDatXeId > 0)
                {
                    //insert chi tiết đơn đặt
                    CTDD newCTDD = new CTDD(xeId, newDonDatXeId, soNgayThue, donGia, tongTien);
                    bool kq = CTDDDAO.Instance.Insert_CTDD(newCTDD);
                    //update trạng thái của xe
                    if (kq)
                    {
                        XeDAO.Instance.setTrangThaiXeChoThueByXeId(xeId);
                    }
                    MessageBox.Show(donDatXe.ToString(), "Lập đơn thành công. Thông tin đơn:");
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Lập đơn thất bại!!!", "Thông báo");
                    this.Close();
                    return;
                }
            }

            // Nếu thêm xe thì chỉ cần thêm ctdd với xe mới và đơn đặt xe hiện hành
            CTDD ctdd = new CTDD(xeId, donDatXeHienHanhId, soNgayThue, donGia, tongTien);
            bool temp = CTDDDAO.Instance.Insert_CTDD(ctdd);
            //update trạng thái của xe
            if (temp)
            {
                XeDAO.Instance.setTrangThaiXeChoThueByXeId(xeId);
            }
            //get Tổng tiền
            decimal newTongTien = CTDDDAO.Instance.getTongTienByDonDatXeId(donDatXeHienHanhId);
            
            bool result = DonDatXeDAO.Instance.Update_TongTien(newTongTien);
            if (result)
            {
                MessageBox.Show("Đã cho thuê xe " + xeDuocChon.TenXe, "Thành công") ;
                DonDatXeDAO.Instance.setKH_ID_And_DDX_ID(-1, -1);
                this.Close() ;
                return;
            }
            else
            {
                MessageBox.Show("Thuê xe thất bại!!!", "Thông báo");
                this.Close();
                return;
            }

        }

        private void btnThemXe_Click(object sender, EventArgs e)
        {
            KhachHang kh = this.getKhachHangFormInput();
            if (kh == null) return;

            //save khachhang
            //check KhachHang đã tồn tại bằng số điện thoại
            khachHangId = KhachHangDAO.Instance.getKhachHangBySoDienThoai(kh.SoDienThoai);
            if (khachHangId < 0)
            {
                khachHangId = KhachHangDAO.Instance.Insert_KhachHang(kh);
            }

            //get nguoiDungId
            int nguoiDungId = AuthDAO.Instance.User.NguoiDungId;

            //get xeId
            int xeId = this.xeDuocChon.XeId;

            //get ngày thuê xe, ngày trả xe
            DateTime ngayThue = dtNgayThue.Value;
            DateTime ngayTra = dtNgayTra.Value;

            // get số ngày thuê
            TimeSpan soNgayDat = ngayTra - ngayThue;
            int soNgayThue = soNgayDat.Days;

            // get đơn giá thuê xe
            decimal donGia = xeDuocChon.GiaChoThue;

            //get tổng tiền
            decimal tongTien = (decimal)(donGia * soNgayThue);

            // Kiểm tra thêm mới hay thêm xe
            int donDatXeHienHanhId = DonDatXeDAO.Instance.DonDatXeHienHanhId;
            if (donDatXeHienHanhId < 0)
            {
                //new mới đơn đặt xe
                DonDatXe donDatXe = new DonDatXe(ngayThue, ngayTra, tongTien, khachHangId, nguoiDungId);
                // insert đơn đặt xe
                newDonDatXeId = DonDatXeDAO.Instance.Insert_DonDatXe(donDatXe);
                if (newDonDatXeId > 0)
                {
                    //insert chi tiết đơn đặt
                    CTDD newCTDD = new CTDD(xeId, newDonDatXeId, soNgayThue, donGia, tongTien);
                    bool kq = CTDDDAO.Instance.Insert_CTDD(newCTDD);
                    //update trạng thái của xe
                    if (kq)
                    {
                        XeDAO.Instance.setTrangThaiXeChoThueByXeId(xeId);
                    }

                    // set them xe moi
                    DonDatXeDAO.Instance.setKH_ID_And_DDX_ID(this.khachHangId, this.newDonDatXeId);
                    MessageBox.Show(donDatXe.ToString(), "Lập đơn thành công. Thông tin đơn:");
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Lập đơn thất bại!!!", "Thông báo");
                    this.Close();
                    return;
                }
            }

            // Nếu thêm xe thì chỉ cần thêm ctdd với xe mới và đơn đặt xe hiện hành
            CTDD ctdd = new CTDD(xeId, donDatXeHienHanhId, soNgayThue, donGia, tongTien);
            bool temp = CTDDDAO.Instance.Insert_CTDD(ctdd);
            //update trạng thái của xe
            if (temp)
            {
                XeDAO.Instance.setTrangThaiXeChoThueByXeId(xeId);
            }
            //get Tổng tiền
            decimal newTongTien = CTDDDAO.Instance.getTongTienByDonDatXeId(donDatXeHienHanhId);

            bool result = DonDatXeDAO.Instance.Update_TongTien(newTongTien);
            if (result)
            {
                MessageBox.Show("Đã cho thuê xe " + xeDuocChon.TenXe, "Thành công");
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Thuê xe thất bại!!!", "Thông báo");
                this.Close();
                return;
            }
        }
    }
}
