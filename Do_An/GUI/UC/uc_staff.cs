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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.IO;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.ViewInfo;
using System.Reflection;
using DevExpress.Utils.Menu;

namespace GUI.UC
{
    public partial class uc_staff : DevExpress.XtraEditors.XtraUserControl
    {
        private frmMain frm;
        private ImageCollection images = new ImageCollection();
        private OpenFileDialog open;
        public uc_staff(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }

        private void uc_staff_Load(object sender, EventArgs e)
        {
            NhanVienBUS.Instances.getDataGV(gcStaff);
            QuyenBUS.Instances.getDataLkQuyen(lkQuyen);
            QuyenBUS.Instances.getDataGV(gcRole);
            gvStaff.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvRole.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

        }
        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //nhân viên
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        NhanVienBUS.Instances.delete(int.Parse(dr["MANV"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        NhanVienBUS.Instances.getDataGV(gcStaff);
                    }
                }
            }
            //quyền
            else
            {
                DataRow dr = gvRole.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá quyền " + dr["tenquyen"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        QuyenBUS.Instances.delete(int.Parse(dr["maquyen"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        QuyenBUS.Instances.getDataGV(gcRole);
                    }
                }
            }

        }
        private void btnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn reset mật khẩu nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        NhanVienBUS.Instances.resetPass(int.Parse(dr["MANV"].ToString()));
                        XtraMessageBox.Show("Reset mật khẩu thành công. Mật khẩu mới là 12345", "Thông báo");
                        NhanVienBUS.Instances.getDataGV(gcStaff);
                    }
                }
            }
        }
        //load hình ảnh
        private void gvStaff_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "HINHANH")
            {

                try
                {
                    Image img = Image.FromFile("../../Images/" + gvStaff.GetDataRow(e.RowHandle)["HINHANH"].ToString());
                    images.Images.Clear();
                    images.Images.Add(img);
                }
                catch (Exception ex)
                {

                    Image img = Image.FromFile("../../Images/loadImg.png");
                    images.Images.Clear();
                    images.Images.Add(img);
                }

                imgHinhAnh.Images = images;
            }
        }
        //thay đổi hình ảnh nhân viên
        private void imgHinhAnh_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
                if (!File.Exists("../../Images/" + open.SafeFileName))
                {
                    pictureBox1.Image.Save("../../Images/" + open.SafeFileName);
                }
                try
                {
                    DataRow dr = gvStaff.GetFocusedDataRow();
                    NhanVienBUS.Instances.update(dr["TENNV"].ToString().Trim(), dr["DIACHI"].ToString().Trim()
                     , dr["SDT"].ToString().Trim(), bool.Parse(dr["GIOITINH"].ToString().Trim()), DateTime.Parse(dr["NGAYVL"].ToString().Trim())
                     , double.Parse(dr["LUONG"].ToString().Trim()), open.SafeFileName, dr["taikhoan"].ToString().Trim()
                     , int.Parse(dr["maquyen"].ToString().Trim()), int.Parse(dr["MANV"].ToString().Trim()));
                    NhanVienBUS.Instances.getDataGV(gcStaff);
                    open = null;


                }
                catch (Exception)
                {

                }
            }


        }
        //phím delete xoá nhân viên
        private void gcStaff_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvStaff.State != GridState.Editing)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        NhanVienBUS.Instances.delete(int.Parse(dr["MANV"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        NhanVienBUS.Instances.getDataGV(gcStaff);
                    }
                }
            }
        }
        //ngăn ko cho chuyển dòng khi sai dữ liệu nhân viên
        private void gvStaff_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //thêm sửa dữ liệu nhân viên
        private void gvStaff_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvStaff.GetRowCellValue(e.RowHandle, "TENNV").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên nhân viên.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "DIACHI").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền địa chỉ.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "SDT").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số điện thoại.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "NGAYVL").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền ngày vào làm.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "LUONG").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền lương.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "taikhoan").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền tài khoản.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "maquyen").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng chọn quyền.";
            }
            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        NhanVienBUS.Instances.insert(gvStaff.GetRowCellValue(e.RowHandle, "TENNV").ToString().Trim()
                            , gvStaff.GetRowCellValue(e.RowHandle, "DIACHI").ToString().Trim()
                         , gvStaff.GetRowCellValue(e.RowHandle, "SDT").ToString().Trim()
                         , gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH") == null || gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH").ToString() == "" ? false: bool.Parse(gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                         , DateTime.Parse(gvStaff.GetRowCellValue(e.RowHandle, "NGAYVL").ToString().Trim())
                         , double.Parse(gvStaff.GetRowCellValue(e.RowHandle, "LUONG").ToString().Trim())
                         , open == null || open.SafeFileName == null ? gvStaff.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                         , gvStaff.GetRowCellValue(e.RowHandle, "taikhoan").ToString().Trim()
                         , int.Parse(gvStaff.GetRowCellValue(e.RowHandle, "maquyen").ToString().Trim()));
                        open = null;
                        XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                    }
                    catch (Exception ex)
                    {

                    }
                    NhanVienBUS.Instances.getDataGV(gcStaff);
                }
                //sửa 
                else
                {

                    try
                    {
                        NhanVienBUS.Instances.update(gvStaff.GetRowCellValue(e.RowHandle, "TENNV").ToString().Trim()
                            , gvStaff.GetRowCellValue(e.RowHandle, "DIACHI").ToString().Trim()
                         , gvStaff.GetRowCellValue(e.RowHandle, "SDT").ToString().Trim()
                         , bool.Parse(gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                         , DateTime.Parse(gvStaff.GetRowCellValue(e.RowHandle, "NGAYVL").ToString().Trim())
                         , double.Parse(gvStaff.GetRowCellValue(e.RowHandle, "LUONG").ToString().Trim())
                         , open == null || open.SafeFileName == null ? gvStaff.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                         , gvStaff.GetRowCellValue(e.RowHandle, "taikhoan").ToString().Trim()
                         , int.Parse(gvStaff.GetRowCellValue(e.RowHandle, "maquyen").ToString().Trim())
                         , int.Parse(gvStaff.GetRowCellValue(e.RowHandle, "MANV").ToString().Trim()));
                        open = null;
                    }
                    catch (Exception)
                    {

                    }
                    NhanVienBUS.Instances.getDataGV(gcStaff);
                }
            }
            else
            {

                e.Valid = false;

                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //ngăn ko cho chuyển dòng khi sai dữ liệu trong bảng quyền
        private void gvRole_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //phím delete xoá quyền
        private void gcRole_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvStaff.State != GridState.Editing)
            {
                DataRow dr = gvRole.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá quyền " + dr["tenquyen"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        QuyenBUS.Instances.delete(int.Parse(dr["maquyen"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        QuyenBUS.Instances.getDataGV(gcRole);
                    }
                }
            }
        }
        //thêm sửa dữ liệu nhân viên
        private void gvRole_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvRole.GetRowCellValue(e.RowHandle, "tenquyen").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên quyền.\n";
            }

            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        QuyenBUS.Instances.insert(gvRole.GetRowCellValue(e.RowHandle, "tenquyen").ToString().Trim());

                        XtraMessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        QuyenBUS.Instances.getDataGV(gcRole);

                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Thêm thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                //sửa 
                else
                {
                    try
                    {
                        QuyenBUS.Instances.update(gvRole.GetRowCellValue(e.RowHandle, "tenquyen").ToString().Trim(), int.Parse(gvRole.GetRowCellValue(e.RowHandle, "maquyen").ToString().Trim()));
                        QuyenBUS.Instances.getDataGV(gcRole);

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
        //xuất ra file excel nhân viện hoặc quyền
        private void btnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel Files (*.xlsx)|*.xls";
            sf.Title = "Xuất ra file excel";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "nhân viên";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvStaff.ExportToXls(sf.FileName);
                else
                {
                    gvRole.ExportToXls(sf.FileName);
                    str = "quyền";
                }
                XtraMessageBox.Show("Xuất file excel " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file word nhân viện hoặc quyền
        private void btnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Word Files (*.docx)|*.docx";
            sf.Title = "Xuất ra file word";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "nhân viên";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvStaff.ExportToDocx(sf.FileName);
                else
                {
                    gvRole.ExportToDocx(sf.FileName);
                    str = "quyền";
                }
                XtraMessageBox.Show("Xuất file word " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Pdf nhân viện hoặc quyền
        private void btnPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Pdf Files (*.pdf)|*.pdf";
            sf.Title = "Xuất ra file pdf";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "nhân viên";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvStaff.ExportToPdf(sf.FileName);
                else
                {
                    gvRole.ExportToPdf(sf.FileName);
                    str = "quyền";
                }
                XtraMessageBox.Show("Xuất file pdf " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
