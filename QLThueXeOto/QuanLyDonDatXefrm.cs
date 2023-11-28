using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
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
    public partial class QuanLyDonDatXefrm : Form
    {
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        public QuanLyDonDatXefrm()
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
        private void LoadListDonDatXe(DataTable listDonDatXe)
        {
            this.dgvDonDatXe.DataSource = listDonDatXe;
            dgvDonDatXe.Refresh();
        }

        private void QuanLyDonDatXe_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;

            ///Load
            LoadListDonDatXe(QuanLyDonDatXeBLL.Instance.TableDonDatXe());
            ///
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

        private void QuanLyDonDatXe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void dgvDonDatXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ cột cụ thể trong dòng được click
                int donDatXeId = (int)dgvDonDatXe.Rows[e.RowIndex].Cells["ID"].Value;
                ChiTietDonDatfrm frm = new ChiTietDonDatfrm(donDatXeId);
                frm.ShowDialog();

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

        private void btnQLLichTrinh_Click(object sender, EventArgs e)
        {
            QuanLyLichTrinhfrm frm = new QuanLyLichTrinhfrm();
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
