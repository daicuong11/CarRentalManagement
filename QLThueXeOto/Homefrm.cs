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
    public partial class Homefrm : Form
    {
        private NguoiDung user = AuthDAO.Instance.User;
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        public Homefrm()
        {
            InitializeComponent();
        }

        private void Homefrm_SizeChanged(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = minWithSightBar;
            btnChoThue.Text = "";
        }

        public void changeSightBar()
        {
            if (scLayer.SplitterDistance < maxWithSightBar)
            {
                scLayer.SplitterDistance = maxWithSightBar;
                btnHidenBar.Text = "<<";
                lbTitle.Text = "Công Ty";
                btnChoThue.Text = "HỢP ĐỒNG CHO THUÊ";
                btnQLOto.Text = "QUẢN LÝ OTO";
                btnQLLichTrinh.Text = "QUẢN LÝ LỊCH TRÌNH";
                btnQLKhachHang.Text = "QUẢN LÝ KHÁCH HÀNG";
                btnThongKe.Text = "THỐNG KÊ             >>";
                btnQLDonDatXe.Text = "QUẢN LÝ ĐƠN ĐẶT XE";
            }
            else
            {
                scLayer.SplitterDistance = minWithSightBar;
                btnHidenBar.Text = ">>";
                lbTitle.Text = "";
                btnChoThue.Text = "";
                btnQLOto.Text = "";
                btnQLLichTrinh.Text = "";
                btnQLKhachHang.Text = "";
                btnQLDonDatXe.Text = "";
                btnThongKe.Text = "";
                scItemThongKe.Height = heightItem;
                scItemThongKe.Panel2Collapsed = true;
            }
        }

        private void Homefrm_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            PhanQuyen();
        }

        public void PhanQuyen()
        {
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;

        }

        private void Homefrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kiểm tra nguyên nhân đóng form
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ngăn chặn đóng form
                e.Cancel = true;

                DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát", "Xác nhận", MessageBoxButtons.OKCancel);

                // Kiểm tra xem người dùng đã nhấn nút OK hay Cancel
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
                else if (result == DialogResult.Cancel)
                {

                }
            }
        }

        private void btnChoThue_Click_1(object sender, EventArgs e)
        {
            HopDongThueXefrm frm = new HopDongThueXefrm();
            //this.Hide();
            frm.Show();
        }

        private void btnQLOto_Click_1(object sender, EventArgs e)
        {
            QuanLyOtofrm frm = new QuanLyOtofrm();
            //this.Hide();
            frm.Show();
        }

        private void btnQLDonDatXe_Click_1(object sender, EventArgs e)
        {
            QuanLyDonDatXefrm frm = new QuanLyDonDatXefrm();
            //this.Hide();
            frm.Show();
        }

        private void btnQLLichTrinh_Click_1(object sender, EventArgs e)
        {
            QuanLyLichTrinhfrm frm = new QuanLyLichTrinhfrm();
            //this.Hide();
            frm.Show();
        }

        private void btnQLKhachHang_Click_1(object sender, EventArgs e)
        {
            QuanLyKhachHangfrm frm = new QuanLyKhachHangfrm();
            //this.Hide();
            frm.Show();
        }

        private void btnHidenBar_Click_1(object sender, EventArgs e)
        {
            changeSightBar();
        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(btnThongKe.Text))
            {
                changeSightBar();
                scItemThongKe.Height = (heightItem * 3);
                scItemThongKe.Panel2Collapsed = !scItemThongKe.Panel2Collapsed;
            }
            else
            {
                if (scItemThongKe.Height < 66)
                {
                    scItemThongKe.Height = (heightItem * 3);
                    btnThongKe.Text = "THỐNG KÊ             VV";
                }
                else
                {
                    scItemThongKe.Height = heightItem;
                    btnThongKe.Text = "THỐNG KÊ             >>";
                }
                scItemThongKe.Panel2Collapsed = !scItemThongKe.Panel2Collapsed;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (user.QuyenId != 1)
            {
                MessageBox.Show("Bạn không có quyền vào chức năng này", "Thông báo");
                return;
            }
            ThongKeDoanhThu frm = new ThongKeDoanhThu();
            //this.Hide();
            frm.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (user.QuyenId != 1)
            {
                MessageBox.Show("Bạn không có quyền vào chức năng này", "Thông báo");
                return;
            }
            ThongKeDoanhThu2 frm = new ThongKeDoanhThu2();
            //this.Hide();
            frm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DangNhapfrm frm = (DangNhapfrm)Application.OpenForms["dangnhapfrm"];
            if (frm != null)
            {
                frm.Show();
            }
            this.Hide();
        }
    }
}
