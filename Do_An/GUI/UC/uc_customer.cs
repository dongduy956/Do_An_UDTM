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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using GUI.Report;
using DevExpress.XtraReports.UI;
using GUI.FRM;

namespace GUI.UC
{
    public partial class uc_customer : XtraUserControl
    {

        frmMain frm;
        public uc_customer(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;


        }
        private void uc_customer_Load(object sender, EventArgs e)
        {
            KhachHangBUS.Instances.getDataGV(gcCustomer);
            LoaiKHBUS.Instances.getDataLkLoaiKH(lkLoaiKH);
            gvCustomer.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
            gvTypeCustomer.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvCustomer.IndicatorWidth = 50;
            gvTypeCustomer.IndicatorWidth = 50;
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }
        //xoá 1 dòng trong bảng khách hàng hoặc loại khách hàng
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá khách hàng " + dr["TENKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = KhachHangBUS.Instances.delete(int.Parse(dr["MAKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            KhachHangBUS.Instances.getDataGV(gcCustomer);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
            {
                DataRow dr = gvTypeCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại khách hàng " + dr["TENLOAIKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = LoaiKHBUS.Instances.delete(int.Parse(dr["MALOAIKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                            LoaiKHBUS.Instances.getDataLkLoaiKH(lkLoaiKH);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        //xoá bằng nút delete 1 dòng trong bảng khách hàng
        private void gcCustomer_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvCustomer.State != GridState.Editing)
            {
                DataRow dr = gvCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá khách hàng " + dr["TENKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = KhachHangBUS.Instances.delete(int.Parse(dr["MAKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            KhachHangBUS.Instances.getDataGV(gcCustomer);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        //ngăn chặn không cho chuyển dòng khi thêm sửa khách hàng dữ liệu không hợp lệ
        private void gvCustomer_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;

        }
        //thêm sửa bẳng khách hàng
        private void gvCustomer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvCustomer.GetRowCellValue(e.RowHandle, "TENKH").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên khách hàng.\n";
            }
            if (gvCustomer.GetRowCellValue(e.RowHandle, "DIACHI").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền địa chỉ.\n";
            }
            if (gvCustomer.GetRowCellValue(e.RowHandle, "SDT").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số điện thoại.\n";
            }
            if (gvCustomer.GetRowCellValue(e.RowHandle, "maloaikh").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng chọn loại khách hàng.\n";
            }
            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = KhachHangBUS.Instances.insert(gvCustomer.GetRowCellValue(e.RowHandle, "TENKH").ToString().Trim()
                             , gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH") == null || gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH").ToString() == "" ? false : bool.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                             , gvCustomer.GetRowCellValue(e.RowHandle, "DIACHI").ToString()
                             , gvCustomer.GetRowCellValue(e.RowHandle, "SDT").ToString()
                             , int.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "maloaikh").ToString().Trim()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công.", "Thông báo", DevExpress.Utils.DefaultBoolean.True);
                            KhachHangBUS.Instances.getDataGV(gcCustomer);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                    catch (Exception ex)
                    {

                    }
                }
                //sửa 
                else
                {
                    try
                    {
                        int i = KhachHangBUS.Instances.update(gvCustomer.GetRowCellValue(e.RowHandle, "TENKH").ToString().Trim()
                             , bool.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                             , gvCustomer.GetRowCellValue(e.RowHandle, "DIACHI").ToString()
                             , gvCustomer.GetRowCellValue(e.RowHandle, "SDT").ToString()
                             , int.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "maloaikh").ToString().Trim())
                             , int.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "Makh").ToString().Trim()));
                        if (i == 1)
                            KhachHangBUS.Instances.getDataGV(gcCustomer);
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                    catch (Exception)
                    {

                    }

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //xoá bằng nút delete 1 dòng trong bảng loại khách hàng
        private void gcTypeCustomer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvCustomer.State != GridState.Editing)
            {
                DataRow dr = gvTypeCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại khách hàng " + dr["TENLOAIKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = LoaiKHBUS.Instances.delete(int.Parse(dr["MALOAIKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                            LoaiKHBUS.Instances.getDataLkLoaiKH(lkLoaiKH);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        //ngăn chặn không cho chuyển dòng khi thêm sửa loại khách hàng dữ liệu không hợp lệ
        private void gvTypeCustomer_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;

        }
        //thêm sửa bẳng khách hàng
        private void gvTypeCustomer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvTypeCustomer.GetRowCellValue(e.RowHandle, "TENLOAIKH").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên loại.\n";
            }
            if (gvTypeCustomer.GetRowCellValue(e.RowHandle, "GIAMGIA").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền giảm giá.\n";
            }
            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                     int i=   LoaiKHBUS.Instances.insert(gvTypeCustomer.GetRowCellValue(e.RowHandle, "TENLOAIKH").ToString().Trim()
                           , double.Parse(gvTypeCustomer.GetRowCellValue(e.RowHandle, "GIAMGIA").ToString().Trim()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                            LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                            LoaiKHBUS.Instances.getDataLkLoaiKH(lkLoaiKH);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    catch (Exception ex)
                    {

                    }
                }
                //sửa 
                else
                {
                    try
                    {
                       int i= LoaiKHBUS.Instances.update(gvTypeCustomer.GetRowCellValue(e.RowHandle, "TENLOAIKH").ToString().Trim()
                            , double.Parse(gvTypeCustomer.GetRowCellValue(e.RowHandle, "GIAMGIA").ToString().Trim())
                            , int.Parse(gvTypeCustomer.GetRowCellValue(e.RowHandle, "MALOAIKH").ToString().Trim()));
                        if (i == 1)
                        {
                            LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                            LoaiKHBUS.Instances.getDataLkLoaiKH(lkLoaiKH);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    catch (Exception)
                    {

                    }

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //xuất ra file excel khách hàng hoặc loại khách hàng
        private void btnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel Files (*.xlsx)|*.xls";
            sf.Title = "Xuất ra file excel";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "khách hàng";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvCustomer.ExportToXls(sf.FileName);
                else
                {
                    gvTypeCustomer.ExportToXls(sf.FileName);
                    str = "loại khách hàng";
                }
                XtraMessageBox.Show("Xuất file excel " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file word khách hàng hoặc loại khách hàng
        private void btnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Word Files (*.docx)|*.docx";
            sf.Title = "Xuất ra file word";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "khách hàng";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvCustomer.ExportToDocx(sf.FileName);
                else
                {
                    gvTypeCustomer.ExportToDocx(sf.FileName);
                    str = "loại khách hàng";
                }
                XtraMessageBox.Show("Xuất file word " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Pdf khách hàng hoặc loại khách hàng
        private void btnPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Pdf Files (*.pdf)|*.pdf";
            sf.Title = "Xuất ra file pdf";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "khách hàng";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvCustomer.ExportToPdf(sf.FileName);
                else
                {
                    gvTypeCustomer.ExportToPdf(sf.FileName);
                    str = "loại khách hàng";
                }
                XtraMessageBox.Show("Xuất file pdf " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void gvCustomer_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }

        private void gvTypeCustomer_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
