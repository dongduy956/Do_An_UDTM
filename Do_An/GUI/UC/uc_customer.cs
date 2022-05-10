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
                DataRow dr = gvCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá khách hàng " + dr["TENKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        KhachHangBUS.Instances.delete(int.Parse(dr["MAKH"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        KhachHangBUS.Instances.getDataGV(gcCustomer);
                    }
                }
            }
            else
            {
                DataRow dr = gvTypeCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loai khách hàng " + dr["TENLOAIKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        LoaiKHBUS.Instances.delete(int.Parse(dr["MALOAIKH"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                    }
                }
            }
        }

        private void gvCustomer_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr;
            if (e.RowHandle >= 0)
            {
                dr = gvCustomer.GetDataRow(e.RowHandle);
                if (validateDataRow(dr["TENKH"]) || validateDataRow(dr["DIACHI"]) || validateDataRow(dr["SDT"]) || validateDataRow(dr["maloaikh"]))
                {
                    KhachHangBUS.Instances.getDataGV(gcCustomer);
                    return;
                }
                else
                {
                    try
                    {
                        KhachHangBUS.Instances.update(dr["TENKH"].ToString(), bool.Parse(dr["GIOITINH"].ToString()), dr["DIACHI"].ToString(), dr["SDT"].ToString(), int.Parse(dr["maloaikh"].ToString().Trim()), int.Parse(dr["makh"].ToString().Trim()));
                    }
                    catch (Exception)
                    {

                    }
                }
                KhachHangBUS.Instances.getDataGV(gcCustomer);
            }
            else
            {
                dr = gvCustomer.GetDataRow(gvCustomer.RowCount - 1);
                if (validateDataRow(dr["TENKH"]) || validateDataRow(dr["DIACHI"]) || validateDataRow(dr["SDT"]) || validateDataRow(dr["maloaikh"]))
                {
                    KhachHangBUS.Instances.getDataGV(gcCustomer);

                    return;
                }
                try
                {
                    int row = KhachHangBUS.Instances.insert(dr["TENKH"].ToString(), dr["GIOITINH"] == null || dr["GIOITINH"].ToString() == "" ? false : bool.Parse(dr["GIOITINH"].ToString()), dr["DIACHI"].ToString(), dr["SDT"].ToString(), int.Parse(dr["maloaikh"].ToString()));
                    XtraMessageBox.Show("Thêm thành công", "Thông báo", DevExpress.Utils.DefaultBoolean.True);
                }
                catch (Exception ex)
                {

                }
                KhachHangBUS.Instances.getDataGV(gcCustomer);


            }
        }

        private void gvTypeCustomer_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr;
            if (e.RowHandle >= 0)
            {
                dr = gvTypeCustomer.GetDataRow(e.RowHandle);
                if (validateDataRow(dr["TENLOAIKH"]) || validateDataRow(dr["GIAMGIA"]))
                {
                    LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                    return;
                }
                else
                {
                    try
                    {
                        LoaiKHBUS.Instances.update(dr["TENLOAIKH"].ToString(),double.Parse(dr["GIAMGIA"].ToString().Trim()),int.Parse(dr["MALOAIKH"].ToString().Trim()));
                    }
                    catch (Exception)
                    {

                    }
                }
                LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
            }
            else
            {
                dr = gvTypeCustomer.GetDataRow(gvTypeCustomer.RowCount - 1);
                if (validateDataRow(dr["TENLOAIKH"]) || validateDataRow(dr["GIAMGIA"]))
                {
                    LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
                    return;
                }
                try
                {
                    LoaiKHBUS.Instances.insert(dr["TENLOAIKH"].ToString(), double.Parse(dr["GIAMGIA"].ToString().Trim()));

                    XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                }
                catch (Exception ex)
                {

                }
                LoaiKHBUS.Instances.getDataGV(gcTypeCustomer);
            }
        }
    }
}
