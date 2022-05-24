using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;
using DAO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using GUI.FRM;

namespace GUI.UC
{
    public partial class uc_order_employee : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_order_employee(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        //load form khi chạy lần đầu
        private void uc_order_employee_Load(object sender, EventArgs e)
        {
            //lấy danh sách khách hàng
            KhachHangBUS.Instances.getDataLkKH(cbbKhachHang);
            //load danh sách hoá đơn chưa thanh toán
            HoaDonBUS.Instances.getDataGV(gcOrder, false);
            LinhKienBUS.Instances.getDataLkLK(lkLinhKien);
            gvOrderDetail.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvOrder.IndicatorWidth = 30;
            gvOrderDetail.IndicatorWidth = 30;
        }
        //xoá data gridview chi tiết hoá đơn
        void clearDataGVOrderDetail()
        {
            gcOrderDetail.DataSource = null;
            layoutGroupOrderDetail.Enabled = txtTienKhachDua.Enabled = false;
            txtTienKhachDua.Text = txtTienThua.Text = txtTienPhaiTra.Text = "";

        }
        //chọn khách hàng
        private void cbbKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            KHACHHANG kh = KhachHangBUS.Instances.layThongTinTheoMa(cbbKhachHang.GetColumnValue("Makh").ToString());
            txtGiamGia.Text = kh.LOAIKH.GIAMGIA.ToString().Trim();
            txtSDT.Text = kh.SDT.Trim();
            txtDiaChi.Text = kh.DIACHI.Trim();
        }
        //đóng user control bán hàng
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }
        //gọi các chi tiết của 1 hoá đơn có mã hoá đơn truyền vào
        private void callDataGVOrderDetail(int mahd)
        {
            ChiTietHDBUS.Instances.getDataGV(gcOrderDetail, mahd);
            LinhKienBUS.Instances.getDataLkLK(lkLinhKien);
            layoutGroupOrderDetail.Enabled = txtTienKhachDua.Enabled = true;
            layoutGroupOrderDetail.Text = "Chi tiết hoá đơn " + mahd;
            txtTienPhaiTra.Text = Support.convertVND(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "tongtien").ToString());
        }
        //click 1 dòng trong gridview hoá đơn
        private void gvOrder_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
                callDataGVOrderDetail(int.Parse(gvOrder.GetRowCellValue(e.RowHandle, "MAHD").ToString()));
        }
        //tạo 1 hoá đơn mới cho khách hàng       
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            var makh = cbbKhachHang.GetColumnValue("Makh");
            if (makh == null)
                XtraMessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                int i = HoaDonBUS.Instances.insert(1, int.Parse(makh.ToString()), double.Parse(txtGiamGia.Text));
                if (i != -1)
                {
                    XtraMessageBox.Show("Tạo hoá đơn thành công.", "Thông báo");
                    HoaDonBUS.Instances.getDataGV(gcOrder, false);
                    gvOrder.FocusedRowHandle = gvOrder.RowCount - 1;
                    callDataGVOrderDetail(HoaDonBUS.Instances.layHDVuaTao().MAHD);
                }
            }
        }
        //huỷ 1 hoá đơn trong gridview hoá đơn
        private void destroyOrder()
        {
            var mahd = gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD");
            if (mahd != null)
            {
                if (XtraMessageBox.Show("Bạn chắc chắn huỷ hoá đơn " + mahd.ToString() + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int i = HoaDonBUS.Instances.delete(int.Parse(mahd.ToString()));
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Huỷ hoá đơn thành công " + mahd + ".", "Thông báo");
                        HoaDonBUS.Instances.getDataGV(gcOrder, false);
                        clearDataGVOrderDetail();
                    }
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //sự kiện gọi hàm huỷ hoá đơn
        private void btnDestroy_Click(object sender, EventArgs e)
        {
            destroyOrder();
        }
        //click nút delete xoá 1 dòng trong chi tiết hoá đơn
        private void gcOrderDetail_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvOrderDetail.State != GridState.Editing)
            {
                var mahd = gvOrderDetail.GetRowCellValue(gvOrderDetail.FocusedRowHandle, "MAHD");
                var malinhkien = gvOrderDetail.GetRowCellValue(gvOrderDetail.FocusedRowHandle, "MALINHKIEN");
                if (mahd != null)
                {
                    if (XtraMessageBox.Show("Bạn chắc chắn xoá linh kiện " + LinhKienBUS.Instances.timTheoMa(int.Parse(malinhkien.ToString())).TENLINHKIEN + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = ChiTietHDBUS.Instances.delete(int.Parse(mahd.ToString()), int.Parse(malinhkien.ToString()));
                        if (i != -1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            ChiTietHDBUS.Instances.getDataGV(gcOrderDetail, int.Parse(mahd.ToString()));
                            HoaDonBUS.Instances.getDataGV(gcOrder, false);
                            LinhKienBUS.Instances.getDataLkLK(lkLinhKien);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //ngăn không cho thao tác khi thêm sửa 1 dòng trong bảng cthd khi dữ liệu sai
        private void gvOrderDetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //thêm sửa 1 dòng trong bảng chi tiết hoá đơn
        private void gvOrderDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng chọn linh kiện.\n";
            }
            if (gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số lượng.\n";
            }
            if (bVali)
            {
                int? soLuong = int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim());
                LINHKIEN lk = LinhKienBUS.Instances.timTheoMa(int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()));
                if (soLuong <= 0)
                {
                    bVali = false;
                    sErr += "Số lượng phải lớn hơn 0.\n";
                }
                //thêm mới
                if (e.RowHandle < 0)
                {                                
                    if (soLuong > lk.SOLUONGCON)
                    {
                        bVali = false;
                        sErr += "Hết hàng.\n";
                    }
                    if (!bVali)
                    {
                        e.Valid = false;
                        XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int i = ChiTietHDBUS.Instances.insert(
                     int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()),
                     int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()),
                     int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()));
                    if (i != -1)
                        XtraMessageBox.Show("Thêm thành công", "Thông báo", DevExpress.Utils.DefaultBoolean.True);
                    int row = gvOrder.FocusedRowHandle;
                    int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                    HoaDonBUS.Instances.getDataGV(gcOrder, false);
                    gvOrder.FocusedRowHandle = row;
                    callDataGVOrderDetail(mahd);
                }
                //sửa 
                else
                {
                    CHITIETHD cthd = ChiTietHDBUS.Instances.timCTHDTheoMaHDMaLK(int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()), lk.MALINHKIEN);
                    int? soLuongCon = cthd.SOLUONG + lk.SOLUONGCON;
                    if (soLuong > soLuongCon)
                    {
                        bVali = false;
                        sErr += "Hết hàng.\n";
                    }
                    if (!bVali)
                    {
                        e.Valid = false;
                        XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ChiTietHDBUS.Instances.update(
                                 int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()),
                                int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()),
                                int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()));
                    int row = gvOrder.FocusedRowHandle;
                    int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                    HoaDonBUS.Instances.getDataGV(gcOrder, false);
                    gvOrder.FocusedRowHandle = row;
                    callDataGVOrderDetail(mahd);

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //thanh toán 1 hoá đơn
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (txtTienPhaiTra.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Mời bạn chọn hoá đơn muốn thanh toán.", "Thông báo");
                return;
            }
            double tienPhaiTra = double.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "tongtien").ToString());

            if (tienPhaiTra == 0)
            {
                XtraMessageBox.Show("Hoá đơn chưa có sản phẩm không cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTienKhachDua.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Khách chưa đưa tiền.", "Thông báo");
                return;
            }
            double tienKhachDua = double.Parse(txtTienKhachDua.Text.Trim());

            if (tienPhaiTra > tienKhachDua)
            {
                XtraMessageBox.Show("Khách đưa không đủ tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int i = HoaDonBUS.Instances.update(int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()), true);
            if (i != -1)
            {
                XtraMessageBox.Show("Thanh toán thành công.", "Thông báo");
                txtTienThua.Text = Support.convertVND((tienKhachDua - tienPhaiTra).ToString());
                HoaDonBUS.Instances.getDataGV(gcOrder, false);
                clearDataGVOrderDetail();
            }

        }
        //chuyển về kiểu tiền tệ khi nhập tiền vào textbox
        private void txtTienKhachDua_KeyUp(object sender, KeyEventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");
            decimal value;
            try
            {
                value = decimal.Parse(txtTienKhachDua.Text, NumberStyles.AllowThousands);

            }
            catch (Exception ex)
            {
                value = 0;
            }
            txtTienKhachDua.Text = String.Format(culture, "{0:N0}", value);
            txtTienKhachDua.Select(txtTienKhachDua.Text.Length, 0);
            decimal tienPhaiTra = decimal.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "tongtien").ToString());
            txtTienThua.Text = Support.convertVND((value - tienPhaiTra).ToString());
        }
        //không cho nhập chữ vào ô textbox
        private void txtTienKhachDua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        //xoá 1 hoá đơn bằng nút delete
        private void gcOrder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            destroyOrder();
        }

        private void gvOrder_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }

        private void gvOrderDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
