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
    public partial class HopDongThueXefrm : Form
    {
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;
        //
        private int soChoNgoiDuocChon = 0;
        private string kieuDangDuocChon = null;
        public HopDongThueXefrm()
        {
            InitializeComponent();
        }

        private void HopDongThueXefrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
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

        private void Refresh_Table_When_Choosed()
        {
            if(String.IsNullOrEmpty(this.kieuDangDuocChon) && this.soChoNgoiDuocChon == 0)
            {
                this.loadLoaiXe(HopDongThueXeBLL.Instance.loaiXes());
            }
            else if(String.IsNullOrEmpty(this.kieuDangDuocChon) && this.soChoNgoiDuocChon != 0)
            {
                this.loadLoaiXe(HopDongThueXeBLL.Instance.ListLoaiXeBySoChoNgoi(this.soChoNgoiDuocChon));
            }
            else if(!(String.IsNullOrEmpty(this.kieuDangDuocChon)) && this.soChoNgoiDuocChon == 0)
            {
                this.loadLoaiXe(HopDongThueXeBLL.Instance.ListLoaiXeByKieuDang(this.kieuDangDuocChon));
            }
            else
            {
                this.loadLoaiXe(HopDongThueXeBLL.Instance.ListLoaiXeByKieuDangAndSoChoNgoi(this.kieuDangDuocChon, this.soChoNgoiDuocChon));
            }
        }

        private void HopDongThueXefrm_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;
            loadLoaiXe(HopDongThueXeBLL.Instance.loaiXes());
        }

        public void loadLoaiXe(List<LoaiXe> loaiXes)
        {
            flTable.Controls.Clear();
            foreach(LoaiXe x in loaiXes)
            {
                Button btn = new Button();
                btn.Width = HopDongThueXeBLL.Instance.WidthBtn;
                btn.Height = HopDongThueXeBLL.Instance.HeightBtn;
                btn.Text = x.TenLoaiXe;
                btn.Image = Properties.Resources.Car2;
                btn.Padding = new Padding(5, 5, 5, 5);
                btn.Margin = new Padding(10, 10, 10, 10);
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.ForeColor = Color.DimGray; 
                btn.Font = new Font("Arial", 12, FontStyle.Bold); 
                btn.UseVisualStyleBackColor = false;
                btn.Cursor = Cursors.Hand;

                //event click of button
                btn.Click += Btn_Click;
                btn.Tag = x;
                flTable.Controls.Add(btn);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            int loaiXeId = ((sender as Button).Tag as LoaiXe).LoaiXeId;
            ThueXefrm frm = new ThueXefrm(loaiXeId);
            this.Hide();
            frm.Show();
        }

        private void btnHidenBar_Click(object sender, EventArgs e)
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

        private void rbTatCaChoNgoi_CheckedChanged(object sender, EventArgs e)
        {
            this.soChoNgoiDuocChon = 0;
            Refresh_Table_When_Choosed();
        }

        private void rbTatCaKieuDang_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = null;
            Refresh_Table_When_Choosed();

        }

        private void rb2Cho_CheckedChanged(object sender, EventArgs e)
        {
            this.soChoNgoiDuocChon = 2;
            Refresh_Table_When_Choosed();

        }

        private void rbMini_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = "Mini";
            Refresh_Table_When_Choosed();

        }

        private void rb4Cho_CheckedChanged(object sender, EventArgs e)
        {
            this.soChoNgoiDuocChon = 4;
            Refresh_Table_When_Choosed();

        }

        private void rb5Cho_CheckedChanged(object sender, EventArgs e)
        {
            this.soChoNgoiDuocChon = 5;
            Refresh_Table_When_Choosed();

        }

        private void rb7Cho_CheckedChanged(object sender, EventArgs e)
        {
            this.soChoNgoiDuocChon = 7;
            Refresh_Table_When_Choosed();

        }

        private void rbSedan_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = "Sedan";
            Refresh_Table_When_Choosed();

        }

        private void rbHB_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = "Hatchback";
            Refresh_Table_When_Choosed();

        }

        private void rbCUV_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = "CUV";
            Refresh_Table_When_Choosed();

        }

        private void rbMPV_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = "MPV";
            Refresh_Table_When_Choosed();

        }

        private void rbBanTai_CheckedChanged(object sender, EventArgs e)
        {
            this.kieuDangDuocChon = "Bán tải";
            Refresh_Table_When_Choosed();

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
