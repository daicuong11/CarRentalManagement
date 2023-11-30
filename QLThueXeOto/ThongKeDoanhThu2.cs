using Guna.UI2.WinForms;
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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLThueXeOto
{
    public partial class ThongKeDoanhThu2 : Form
    {
        private NguoiDung user = AuthDAO.Instance.User;
        public ThongKeDoanhThu2()
        {
            InitializeComponent();
            loadCbbLoaiXe();
            bieudotron.Titles.Add("Phân bố xe ô tô cho thuê theo loại");
            lbSoLuongXeHong.Text = ThongKeDAO.Instance.CountXeHong().ToString();

        }

        private void loadCbbLoaiXe()
        {
            cbbLoaiXe.DataSource = LoaiXeDAO.Instance.getAll();
            cbbLoaiXe.ValueMember = "loaiXeId";
            cbbLoaiXe.DisplayMember = "tenLoaiXe";
            cbbLoaiXe.SelectedIndex = 0;
        }

        private void ThongKeDoanhThu2_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho DateTimePicker2 là ngày hiện tại
            dtDenNgay.Value = DateTime.Today;

            // Thiết lập giá trị mặc định cho DateTimePicker1 là ngày hiện tại - 30
            dtTuNgay.Value = DateTime.Today.AddDays(-30);

            // Gắn sự kiện ValueChanged cho cả hai DateTimePicker
            dtTuNgay.ValueChanged += DateTimePicker_ValueChanged;
            dtDenNgay.ValueChanged += DateTimePicker_ValueChanged;

            // Gọi hàm LoadThongKe khi khởi tạo
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            DateTime ngayBatDau = dtTuNgay.Value;
            DateTime ngayKetThuc = dtDenNgay.Value;

            string ngayBatDauFomat = ngayBatDau.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string ngayKetThucFomat = ngayKetThuc.ToString("yyyy-MM-dd HH:mm:ss.fff");
            decimal getTotal = ThongKeDAO.Instance.GetTotalByTimes(ngayBatDauFomat, ngayKetThucFomat);
            int countDonDatXe = ThongKeDAO.Instance.CountDonDatXeByTime(ngayBatDauFomat, ngayKetThucFomat);
            lbDoanhThu.Text = getTotal.ToString();
            lbDonDatXe.Text = countDonDatXe.ToString();
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Guna2DateTimePicker dtp = (Guna2DateTimePicker)sender;

            // Kiểm tra nếu DateTimePicker1 lớn hơn DateTimePicker2 hoặc
            // DateTimePicker1 lớn hơn ngày hiện tại, thì đặt giá trị về ngày hiện tại
            if (dtp == dtTuNgay && dtTuNgay.Value >= dtDenNgay.Value)
            {
                dtTuNgay.Value = DateTime.Today.AddDays(-30);
                return;
            }
            else if (dtp == dtDenNgay && dtDenNgay.Value <= dtTuNgay.Value)
            {
                dtDenNgay.Value = DateTime.Today;
                return;
            }

            // Gọi hàm LoadThongKe khi có sự thay đổi
            LoadThongKe();
        }

        private void ThongKeDoanhThu2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Homefrm frm = (Homefrm)Application.OpenForms["Homefrm"];
            if (frm != null)
            {
                frm.Show();
            }
        }

        private void cbbLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLoaiXe.SelectedValue != null)
            {
                if (int.TryParse(cbbLoaiXe.SelectedValue.ToString(), out int loaiXeIdSelected))
                {
                    //MessageBox.Show("Loại Xe ID được chọn: " + loaiXeIdSelected.ToString());
                    loadBieuDoTron(loaiXeIdSelected);
                }
            }
            else
            {
                MessageBox.Show("Không có mục được chọn.");
            }
        }

        public void loadBieuDoTron(int loaiXeId)
        {
            // Tạo một DataTable giả định với thông tin về sự phân bố xe ô tô
            DataTable dataTable = ThongKeDAO.Instance.GetPhanBoXeTheoLoaiXe(loaiXeId);

            // Tạo biểu đồ tròn
            bieudotron.Series.Clear();

            // Tạo loạt dữ liệu từ DataTable
            Series series = bieudotron.Series.Add("XeOTo");
            series.ChartType = SeriesChartType.Pie;

            // Thêm dữ liệu vào loạt
            foreach (DataRow row in dataTable.Rows)
            {
                string tenXe = row["TenXe"].ToString();
                int soLuong = Convert.ToInt32(row["SoLuong"]);

                // Thêm điểm dữ liệu vào loạt
                DataPoint point = new DataPoint(); // Tạo một điểm dữ liệu mới
                point.SetValueY(soLuong);
                point.Label = $"{tenXe}  "; // Sử dụng Label để hiển thị tên và giá trị

                // Thêm điểm dữ liệu vào loạt
                series.Points.Add(point);

                // Tính toán và hiển thị phần trăm (nếu cần)
                double percentage = (soLuong / (double)dataTable.AsEnumerable().Sum(r => r.Field<int>("SoLuong"))) * 100;
                point.Label += $"{percentage:0.00}%";
            }


            // Tùy chỉnh các thuộc tính của biểu đồ tròn
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (user.QuyenId != 1)
            {
                MessageBox.Show("Bạn không có quyền vào chức năng này", "Thông báo");
                return;
            }
            ThongKeDoanhThu frm = new ThongKeDoanhThu();
            this.Hide();
            frm.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
