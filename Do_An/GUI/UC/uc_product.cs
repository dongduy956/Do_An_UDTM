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
using DevExpress.XtraPrinting.Native;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using GUI.FRM;

namespace GUI.UC
{
    public partial class uc_product : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        ImageCollection images = new ImageCollection();
        OpenFileDialog open;
        public uc_product(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        private void uc_product_Load(object sender, EventArgs e)
        {
            LinhKienBUS.Instances.getDataGV(gcProduct);
            LoaiLKBUS.Instances.getDataLkLoaiLK(lkLoaiLK);
            gvProduct.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
            gvTypeProduct.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }
        //hàm xoá 1 dòng bảng linh kiện hoặc loại linh kiện
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá linh kiện " + dr["TENLINHKIEN"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        LinhKienBUS.Instances.delete(int.Parse(dr["MALINHKIEN"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        LinhKienBUS.Instances.getDataGV(gcProduct);
                    }
                }
            }
            else
            {
                DataRow dr = gvTypeProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại linh kiện " + dr["TENLOAI"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        LoaiLKBUS.Instances.delete(int.Parse(dr["MALOAI"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
                    }
                }
            }
        }
        //hàm ngăn không cho chuyển dòng khác khi insert hoặc update sai dữ liệu bảng linh kiện
        private void gvProduct_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //chọn hình ảnh cho linh kiện
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
                    int i = gvProduct.GetSelectedRows()[0];
                    LinhKienBUS.Instances.update(
                          gvProduct.GetRowCellValue(i, "TENLINHKIEN").ToString().Trim()
                           , int.Parse(gvProduct.GetRowCellValue(i, "MALOAI").ToString().Trim())
                           , gvProduct.GetRowCellValue(i, "HANGSX").ToString().Trim()
                           , double.Parse(gvProduct.GetRowCellValue(i, "DONGIA").ToString().Trim())
                           , open.SafeFileName
                           , int.Parse(gvProduct.GetRowCellValue(i, "SOLUONGCON").ToString().Trim())
                       , int.Parse(gvProduct.GetRowCellValue(i, "MALINHKIEN").ToString().Trim()));

                    LinhKienBUS.Instances.getDataGV(gcProduct);
                    open = null;
                }
                catch (Exception)
                {

                }
            }
        }
        //load hình ảnh linh kiện
        private void gvProduct_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "HINHANH")
            {
                try
                {
                    Image img = Image.FromFile("../../Images/" + gvProduct.GetRowCellValue(e.RowHandle, "HINHANH").ToString());
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
        //nút delete cho bảng linh kiện
        private void gcProduct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvProduct.State != GridState.Editing)
            {
                DataRow dr = gvProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá linh kiện " + dr["TENLINHKIEN"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        LinhKienBUS.Instances.delete(int.Parse(dr["MALINHKIEN"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        LinhKienBUS.Instances.getDataGV(gcProduct);
                    }
                }
            }
        }
        //thêm sửa bảng linh kiện
        private void gvProduct_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvProduct.GetRowCellValue(e.RowHandle, "TENLINHKIEN").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên linh kiện.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng chọn loại linh kiện.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "HANGSX").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên hãng sản xuất.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền đơn giá.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "SOLUONGCON").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền số lượng còn.";
            }
            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {

                        LinhKienBUS.Instances.insert(
                           gvProduct.GetRowCellValue(e.RowHandle, "TENLINHKIEN").ToString().Trim()
                            , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim())
                            , gvProduct.GetRowCellValue(e.RowHandle, "HANGSX").ToString().Trim()
                            , double.Parse(gvProduct.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim())
                            , open == null || open.SafeFileName == null ? gvProduct.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                            , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "SOLUONGCON").ToString().Trim()));
                        XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                        LinhKienBUS.Instances.getDataGV(gcProduct);

                    }
                    catch (Exception ex)
                    {

                    }
                    open = null;
                }
                //sửa 
                else
                {

                    try
                    {
                        LinhKienBUS.Instances.update(
                            gvProduct.GetRowCellValue(e.RowHandle, "TENLINHKIEN").ToString().Trim()
                             , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim())
                             , gvProduct.GetRowCellValue(e.RowHandle, "HANGSX").ToString().Trim()
                             , double.Parse(gvProduct.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim())
                             , open == null || open.SafeFileName == null ? gvProduct.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                             , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "SOLUONGCON").ToString().Trim())
                         , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()));

                        LinhKienBUS.Instances.getDataGV(gcProduct);
                    }
                    catch (Exception)
                    {

                    }
                    open = null;
                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //nút delete cho bảng loại linh kiện
        private void gcTypeProduct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvProduct.State != GridState.Editing)
            {
                DataRow dr = gvTypeProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại linh kiện " + dr["TENLOAI"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        LoaiLKBUS.Instances.delete(int.Parse(dr["MALOAI"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
                    }
                }
            }
        }
        //hàm ngăn không cho chuyển dòng khác khi insert hoặc update sai dữ liệu bảng loại linh kiện
        private void gvTypeProduct_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;

        }
        //thêm sửa bảng loại linh kiện
        private void gvTypeProduct_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên loại linh kiện.\n";
            }

            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        LoaiLKBUS.Instances.insert(gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim());
                        LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
                        XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
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
                        LoaiLKBUS.Instances.update(gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim(), int.Parse(gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim()));
                        LoaiLKBUS.Instances.getDataGV(gcTypeProduct);

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
        //xuất ra file excel linh kiện hoặc loại linh kiện
        private void btnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel Files (*.xlsx)|*.xls";
            sf.Title = "Xuất ra file excel";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "linh kiện";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvProduct.ExportToXls(sf.FileName);
                else
                { 
                    gvTypeProduct.ExportToXls(sf.FileName);
                    str = "loại linh kiện";
                }
                XtraMessageBox.Show("Xuất file excel "+str+" thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Word linh kiện hoặc loại linh kiện
        private void btnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Word Files (*.docx)|*.docx";
            sf.Title = "Xuất ra file word";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "linh kiện";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvProduct.ExportToDocx(sf.FileName);
                else
                {
                    gvTypeProduct.ExportToDocx(sf.FileName);
                    str = "loại linh kiện";
                }
                XtraMessageBox.Show("Xuất file word " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Pdf linh kiện hoặc loại linh kiện
        private void btnPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Pdf Files (*.pdf)|*.pdf";
            sf.Title = "Xuất ra file pdf";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "linh kiện";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvProduct.ExportToPdf(sf.FileName);
                else
                {
                    gvTypeProduct.ExportToPdf(sf.FileName);
                    str = "loại linh kiện";
                }
                XtraMessageBox.Show("Xuất file pdf " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
