using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QLThueXeOto
{
    public partial class QuanLyKhachHangfrm : Form
    {
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        public QuanLyKhachHangfrm()
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


        private void QuanLyKhachHangfrm_Load(object sender, EventArgs e)
        {
            //scLayer.SplitterDistance = maxWithSightBar;
            //this.SizeChanged += Homefrm_SizeChanged;
            //lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;
            LoadKhachHangData();
        }
        private void LoadKhachHangData()
        {
            lvKhachHang.Items.Clear();
            foreach (KhachHang row in KhachHangBLL.Instance.ListKhachHang())
            {
                ListViewItem lvi = new ListViewItem(row.KhachHangHangId.ToString());
                InsertListItem(row);
            }
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

        private void QuanLyKhachHangfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void lvKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvKhachHang.SelectedItems.Count == 0)
            {
                return;
            }
           ListViewItem lvi = lvKhachHang.SelectedItems[0];
            int ma =  Int32.Parse( lvi.Text);
            Hienthichitiet(ma);
        }
        private void Hienthichitiet(int ma)
        {
            KhachHang kh = KhachHangDAO.Instance.getKhachHangById(ma);
            textBox_ID.Text = kh.KhachHangHangId.ToString();
            textBox_hovaten.Text = kh.HoTen;
            textBox_sdt.Text = kh.SoDienThoai;
            textBox_diachi.Text = kh.DiaChi;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.HoTen = textBox_hovaten.Text;
            kh.SoDienThoai = textBox_sdt.Text;
            kh.DiaChi = textBox_diachi.Text;
            int check = KhachHangBLL.Instance.InsertKhachHang(kh);
            if (check > 0)
            {
                kh.KhachHangHangId = check;
                InsertListItem(kh);
                ResetText();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }
        public void InsertListItem(KhachHang kh)
        {
            ListViewItem lvi = new ListViewItem(kh.KhachHangHangId.ToString());
            lvi.SubItems.Add(kh.HoTen);
            lvi.SubItems.Add(kh.SoDienThoai);
            lvi.SubItems.Add(kh.DiaChi);
            lvKhachHang.Items.Add(lvi);
        }
        private void btn_luu_Click(object sender, EventArgs e)
        {
            String id = textBox_ID.Text;
            KhachHang kh = new KhachHang();
            kh.KhachHangHangId = Int32.Parse(id);
            kh.HoTen = textBox_hovaten.Text;
            kh.SoDienThoai = textBox_sdt.Text;
            kh.DiaChi = textBox_diachi.Text;
            if (KhachHangBLL.Instance.UpdateKhachHang(kh))
            {
                 LoadKhachHangData();

            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if(lvKhachHang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn khách hàng nào để xóa");
            }
            ListViewItem lvi = lvKhachHang.SelectedItems[0];
            int ma = Int32.Parse(lvi.Text);
            DialogResult ret = MessageBox.Show("Bạn có chắc xóa không", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                XoaKhachHang(ma);
            }
        }
        private void XoaKhachHang (int ma)
        {
            if (KhachHangBLL.Instance.DeleteKhachHang(ma))
            {
                LoadKhachHangData();
            } else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }
    }
}
