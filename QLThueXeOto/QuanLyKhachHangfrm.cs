using OfficeOpenXml.Style;
using OfficeOpenXml;
using QLThueXeOto.BLL;
using QLThueXeOto.DAO;
using QLThueXeOto.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            scLayer.SplitterDistance = maxWithSightBar;
            this.SizeChanged += Homefrm_SizeChanged;
            lbUserName.Text = "Hi, " + AuthDAO.Instance.User.TenNguoiDung;
            LoadKhachHangData();
        }
        private void LoadKhachHangData(string searchkey = "")
        {
            lvKhachHang.Items.Clear();
            foreach (KhachHang row in KhachHangBLL.Instance.ListKhachHangTuKhoaTimKiem(searchkey))
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
            if(KhachHangBLL.Instance.CheckDuplicateKhachHang(kh.SoDienThoai) != -1)
            {
                MessageBox.Show("Số điện thoại đã tồn tại");
                return;
            } 
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
            // nếu trùng
            if(KhachHangBLL.Instance.CheckDuplicateKhachHang(kh.SoDienThoai) > 0)
            {
                // nếu ko phải update chính mình
                if (KhachHangBLL.Instance.CheckDuplicateKhachHang(kh.SoDienThoai) != kh.KhachHangHangId)
                {
                    MessageBox.Show("Số điện thoại đã tồn tại");
                    return;
                }
            }
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
                return;
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchkey = txt_timkiem.Text.Trim();
            LoadKhachHangData(searchkey);
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

        private void btnQLLichTrinh_Click(object sender, EventArgs e)
        {
            QuanLyLichTrinhfrm frm = new QuanLyLichTrinhfrm();
            this.Close();
            frm.Show();
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            ExportToExcel(lvKhachHang);
        }

        //handle export to file excel
        public void ExportToExcel(ListView listView)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // Đổ dữ liệu từ ListView vào Excel
                int rowIndex = 1;

                // Thêm header
                for (int i = 0; i < listView.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = listView.Columns[i].Text;
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                // Thêm dữ liệu
                foreach (ListViewItem item in listView.Items)
                {
                    rowIndex++;

                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        worksheet.Cells[rowIndex, i + 1].Value = item.SubItems[i].Text;
                    }
                }

                // Tự động căn chỉnh kích thước của cột dựa trên nội dung
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Mở cửa sổ SaveFileDialog để chọn nơi lưu trữ file Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";
                saveFileDialog.Title = "Save Excel File";

                // Đặt tên mặc định cho file là "DanhSachKhachHang"
                saveFileDialog.FileName = "DanhSachKhachHang";

                saveFileDialog.ShowDialog();

                // Lưu file Excel
                if (saveFileDialog.FileName != "")
                {
                    FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                    excelPackage.SaveAs(excelFile);
                    MessageBox.Show("File Excel đã được xuất thành công!");
                }
            }
        }
    }
}
