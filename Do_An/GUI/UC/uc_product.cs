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

namespace GUI.UC
{
    public partial class uc_product : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
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
        bool validateDataRow(object obj)
        {
            return (obj == null || obj.ToString().Trim().Length == 0);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá linh kiện " + dr["TENLINHKIEN"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại linh kiện " + dr["TENLOAI"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        LoaiLKBUS.Instances.delete(int.Parse(dr["MALOAI"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
                    }
                }
            }
        }

        private void gvProduct_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr;
            if (e.RowHandle >= 0)
            {
                dr = gvProduct.GetDataRow(e.RowHandle);
                if (validateDataRow(dr["TENLINHKIEN"]) 
                    || validateDataRow(dr["MALOAI"]) 
                    || validateDataRow(dr["HANGSX"]) 
                    || validateDataRow(dr["DONGIA"])
                    || validateDataRow(dr["SOLUONGCON"])
                    )
                {
                    LinhKienBUS.Instances.getDataGV(gcProduct);
                    return;
                }
                else
                {
                    try
                    {
                        LinhKienBUS.Instances.update(
                            dr["TENLINHKIEN"].ToString()
                            ,int.Parse(dr["MALOAI"].ToString()), dr["HANGSX"].ToString()
                            ,decimal.Parse(dr["DONGIA"].ToString()),""
                            ,int.Parse(dr["SOLUONGCON"].ToString())
                            ,int.Parse(dr["MALINHKIEN"].ToString()));

                    }
                    catch (Exception)
                    {

                    }
                }
                LinhKienBUS.Instances.getDataGV(gcProduct);
            }
            else
            {
                dr = gvProduct.GetDataRow(gvProduct.RowCount - 1);
                if (validateDataRow(dr["TENLINHKIEN"])
                    || validateDataRow(dr["MALOAI"])
                    || validateDataRow(dr["HANGSX"])
                    || validateDataRow(dr["DONGIA"])
                    || validateDataRow(dr["SOLUONGCON"])
                    )
                {
                    LinhKienBUS.Instances.getDataGV(gcProduct);
                    return;
                }
                try
                {

                    LinhKienBUS.Instances.insert(
                        dr["TENLINHKIEN"].ToString()
                        , int.Parse(dr["MALOAI"].ToString()), dr["HANGSX"].ToString()
                        , decimal.Parse(dr["DONGIA"].ToString()), ""
                        , int.Parse(dr["SOLUONGCON"].ToString()));                        
                    XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                }
                catch (Exception ex)
                {

                }
                LinhKienBUS.Instances.getDataGV(gcProduct);
            }
        }

        private void gvTypeProduct_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr;
            if (e.RowHandle >= 0)
            {
                dr = gvTypeProduct.GetDataRow(e.RowHandle);
                if (validateDataRow(dr["TENLOAI"]))
                {
                    LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
                    return;
                }
                else
                {
                    try
                    {
                        LoaiLKBUS.Instances.update(dr["TENLOAI"].ToString(), int.Parse(dr["MALOAI"].ToString().Trim()));
                    }
                    catch (Exception)
                    {

                    }
                }
                LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
            }
            else
            {
                dr = gvTypeProduct.GetDataRow(gvTypeProduct.RowCount - 1);
                if (validateDataRow(dr["TENLOAI"]))
                {
                    LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
                    return;
                }
                try
                {
                    LoaiLKBUS.Instances.insert(dr["TENLOAI"].ToString());


                    XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                }
                catch (Exception ex)
                {

                }
                LoaiLKBUS.Instances.getDataGV(gcTypeProduct);
            }
        }
    }
}
