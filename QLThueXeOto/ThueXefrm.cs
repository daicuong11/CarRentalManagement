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
    public partial class ThueXefrm : Form
    {
        private int minWithSightBar = 54;
        private int maxWithSightBar = 300;
        private int heightItem = 64;

        // maping checkbox and radio button

        private List<int> listTinhNang = new List<int>();

        private string loaiNhienLieu = null;

        //attibute
        private int loaiXeId = 0;
        public ThueXefrm()
        {
            InitializeComponent();
        }
        
        public ThueXefrm(int loaiXeId)
        {
            InitializeComponent();
            this.loaiXeId = loaiXeId;
        }


        private void btnHidenBar_Click(object sender, EventArgs e)
        {
            changeSightBar();
        }

        private void ThueXefrm_Load(object sender, EventArgs e)
        {
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;
            LoadXeInTable(ThueXeBLL.Instance.ListXeByLoaiXeId(this.loaiXeId));
            LoadTitle();
        }

        private void LoadTitle()
        {
            LoaiXe lx = LoaiXeDAO.Instance.getLoaiXeById(loaiXeId);
            lbXe.Text = "XE(" + lx.TenLoaiXe + ")";
        }

        #region
        public void LoadXeInTable(List<Xe> lstXe)
        {
            flTable.Controls.Clear();
            if(lstXe != null )
            {
                foreach(Xe x in lstXe)
                {
                    Button btn = new Button();
                    btn.Size = new Size(160, 160);
                    btn.Text = x.ToString();
                    btn.Font = new Font("Arial", 11);
                    btn.Margin = new Padding(10);
                    btn.Tag = x;
                    switch (x.TrangThaiXe)
                    {
                        case "Trống":
                            btn.BackColor = Color.ForestGreen;
                            btn.Click += Btn_Click;
                            btn.Cursor = Cursors.Hand;
                            break;
                        case "Hỏng":
                            btn.Enabled = false;
                            btn.BackColor = Color.Red;
                            break;
                        case "Đang cho thuê":
                            btn.Enabled = false;
                            btn.BackColor = Color.Fuchsia;
                            break;
                        default: 
                            btn.Enabled = false;
                            btn.BackColor = Color.Yellow;
                            break;
                    }
                    this.flTable.Controls.Add(btn);
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Xe xe = ((sender as Button).Tag as Xe);
            DonDatXefrm frm = new DonDatXefrm(xe);
            frm.ShowDialog();
            LoadXeInTable(ThueXeBLL.Instance.ListXeByLoaiXeId(this.loaiXeId));
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
        #endregion

        private void ThueXefrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DonDatXeDAO.Instance.setKH_ID_And_DDX_ID(-1, -1);
            HopDongThueXefrm frm = (HopDongThueXefrm)Application.OpenForms["HopDongThueXefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void cbNapThung_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNapThung.Checked)
            {
                listTinhNang.Add(6);
            }
            else
            {
                listTinhNang.Remove(6);
            }
        }

        private void cbCamBienVaCham_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCamBienVaCham.Checked)
            {
                listTinhNang.Add(9);
            }
            else
            {
                listTinhNang.Remove(9);
            }
        }

        private void cbCamBienLop_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCamBienLop.Checked)
            {
                listTinhNang.Add(3);
            }
            else
            {
                listTinhNang.Remove(3);
            }
        }

        private void cbCMRHanhTrinh_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCMRHanhTrinh.Checked)
            {
                listTinhNang.Add(8);
            }
            else
            {
                listTinhNang.Remove(8);
            }
        }

        private void cbBluetooth_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBluetooth.Checked)
            {
                listTinhNang.Add(7);
            }
            else
            {
                listTinhNang.Remove(7);
            }
        }

        private void cbKheCamUSB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbKheCamUSB.Checked)
            {
                listTinhNang.Add(5);
            }
            else
            {
                listTinhNang.Remove(5);
            }
        }

        private void cbCuaSoTroi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCuaSoTroi.Checked)
            {
                listTinhNang.Add(4);
            }
            else
            {
                listTinhNang.Remove(4);
            }
        }

        private void cbCMRCapLe_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCMRCapLe.Checked)
            {
                listTinhNang.Add(2);
            }
            else
            {
                listTinhNang.Remove(2);
            }
        }

        private void cbBanDo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBanDo.Checked)
            {
                listTinhNang.Add(1);
            }
            else
            {
                listTinhNang.Remove(1);
            }
        }

        private void cbCanhBaoTocDo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCanhBaoTocDo.Checked)
            {
                listTinhNang.Add(14);
            }
            else
            {
                listTinhNang.Remove(14);
            }
        }

        private void cbCMR360_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCMR360.Checked)
            {
                listTinhNang.Add(12);
            }
            else
            {
                listTinhNang.Remove(12);
            }
        }

        private void cbCMRLui_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCMRLui.Checked)
            {
                listTinhNang.Add(13);
            }
            else
            {
                listTinhNang.Remove(13);
            }
        }

        private void cbLopDuPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLopDuPhong.Checked)
            {
                listTinhNang.Add(11);
            }
            else
            {
                listTinhNang.Remove(11);
            }
        }

        private void cbDinhViGPS_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDinhViGPS.Checked)
            {
                listTinhNang.Add(10);
            }
            else
            {
                listTinhNang.Remove(10);
            }
        }

        private void rdDien_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDien.Checked)
            {
                loaiNhienLieu = "Điện";
            }
        }

        private void rdXang_CheckedChanged(object sender, EventArgs e)
        {
            if (rdXang.Checked)
            {
                loaiNhienLieu = "Xăng";
            }
        }

        private void rdDauDiesel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDauDiesel.Checked)
            {
                loaiNhienLieu = "Dầu Diesel";
            }
        }

        private void rbTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTatCa.Checked)
            {
                loaiNhienLieu = null;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (rbTatCa.Checked)
            {
                LoadXeInTable(ThueXeBLL.Instance.ListXeByTinhNangXeAndLoaiXeId(this.listTinhNang, this.loaiXeId));
            }
            else
            {
                LoadXeInTable(ThueXeBLL.Instance.ListXeByTinhNangXeAndLoaiXeIdAndLoaiNhienLieu(this.listTinhNang, this.loaiXeId, this.loaiNhienLieu));
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoaTimKiem = txtTimKiem.Text.Trim();
            if (String.IsNullOrEmpty(tuKhoaTimKiem))
            {
                LoadXeInTable(ThueXeBLL.Instance.ListXeByLoaiXeId(this.loaiXeId));
            }
            else
            {
                LoadXeInTable(ThueXeBLL.Instance.ListXeByTuKhoaTimKiem(this.loaiXeId, tuKhoaTimKiem));
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoaTimKiem = txtTimKiem.Text.Trim();
            if (String.IsNullOrEmpty(tuKhoaTimKiem))
            {
                LoadXeInTable(ThueXeBLL.Instance.ListXeByLoaiXeId(this.loaiXeId));
            }
            else
            {
                LoadXeInTable(ThueXeBLL.Instance.ListXeByTuKhoaTimKiem(this.loaiXeId, tuKhoaTimKiem));
            }
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

