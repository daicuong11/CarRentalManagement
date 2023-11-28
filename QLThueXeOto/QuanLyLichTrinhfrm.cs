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
    public partial class QuanLyLichTrinhfrm : Form
    {
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        public QuanLyLichTrinhfrm()
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


        private void QuanLyLichTrinhfrm_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;

            ///load
            LoadLichTrinh(LichTrinhDAO.Instance.getAll());
            ///
        }

        private void LoadLichTrinh(DataTable lishLichTrinh)
        {
            dgvLichTrinh.DataSource = lishLichTrinh;
            dgvLichTrinh.Refresh();
        }

        private void btnHidenBar_Click(object sender, EventArgs e)
        {
            changeSightBar();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
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

        private void QuanLyLichTrinhfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void dgvLichTrinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ cột cụ thể trong dòng được click
                int lichTrinhId = (int)dgvLichTrinh.Rows[e.RowIndex].Cells["ID lịch trình"].Value;
                LichTrinh lichTrinhSelected = LichTrinhDAO.Instance.getLichTrinhById(lichTrinhId);
                if(lichTrinhSelected.TrangThai == null || lichTrinhSelected.TrangThai == "Đã thanh toán")
                {
                    MessageBox.Show("Lịch trình này đã được thanh toán");
                    return;
                }
                if(lichTrinhSelected != null)
                {
                    ThanhToanfrm frm = new ThanhToanfrm(lichTrinhSelected);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn lịch trình nào");
                }
                LoadLichTrinh(LichTrinhDAO.Instance.getAll());
            }
        }

        private void btnChoThue_Click(object sender, EventArgs e)
        {
            HopDongThueXefrm frm = new HopDongThueXefrm();
            this.Close();
            frm.Show();
        }

        private void btnQLOto_Click(object sender, EventArgs e)
        {
            QuanLyOtofrm frm = new QuanLyOtofrm();
            this.Close();
            frm.Show();
        }

        private void btnQLDonDatXe_Click(object sender, EventArgs e)
        {
            QuanLyDonDatXefrm frm = new QuanLyDonDatXefrm();
            this.Close();
            frm.Show();
        }

        private void btnQLKhachHang_Click(object sender, EventArgs e)
        {
            QuanLyKhachHangfrm frm = new QuanLyKhachHangfrm();
            this.Close();
            frm.Show();
        }
    }
}
