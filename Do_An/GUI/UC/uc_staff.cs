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
    public partial class uc_staff : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_staff(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        bool validateDataRow(object obj)
        {
            return (obj == null || obj.ToString().Trim().Length == 0);
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

            if (xtraTabControl1.SelectedTabPageIndex==0)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        NhanVienBUS.Instances.delete(int.Parse(dr["MANV"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        NhanVienBUS.Instances.getDataGV(gcStaff);
                    }
                }
            }
            else
            {
                DataRow dr = gvRole.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá quyền " + dr["tenquyen"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
            
            DataRow dr = gvStaff.GetFocusedDataRow();
            if (dr != null)
            {
                if (XtraMessageBox.Show("Bạn có muốn reset mật khẩu nhân viên "+dr["TENNV"].ToString()+" ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    NhanVienBUS.Instances.resetPass(int.Parse(dr["MANV"].ToString()));
                    XtraMessageBox.Show("Reset mật khẩu thành công. Mật khẩu mới là 12345", "Thông báo");
                    NhanVienBUS.Instances.getDataGV(gcStaff);
                }
            }
        }

        private void gvStaff_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr;
            if (e.RowHandle >= 0)
            {
                dr = gvStaff.GetDataRow(e.RowHandle);
                if (validateDataRow(dr["TENNV"])
                    || validateDataRow(dr["DIACHI"])
                    || validateDataRow(dr["SDT"])
                    || validateDataRow(dr["NGAYVL"])
                    || validateDataRow(dr["LUONG"])
                    || validateDataRow(dr["TAIKHOAN"])
                    || validateDataRow(dr["maquyen"]))
                {
                    NhanVienBUS.Instances.getDataGV(gcStaff);
                    return;
                }
                else
                {
                    try
                    {
                        NhanVienBUS.Instances.update(dr["TENNV"].ToString().Trim(), dr["DIACHI"].ToString().Trim()
                         , dr["SDT"].ToString().Trim(), bool.Parse(dr["GIOITINH"].ToString().Trim()), DateTime.Parse(dr["NGAYVL"].ToString().Trim())
                         , double.Parse(dr["LUONG"].ToString().Trim()), "hinhanh", dr["taikhoan"].ToString().Trim()
                         , int.Parse(dr["maquyen"].ToString().Trim()), int.Parse(dr["MANV"].ToString().Trim()));
                    }
                    catch (Exception)
                    {

                    }
                }
                NhanVienBUS.Instances.getDataGV(gcStaff);
            }
            else
            {
                dr = gvStaff.GetDataRow(gvStaff.RowCount - 1);
                if (validateDataRow(dr["TENNV"])
                    || validateDataRow(dr["DIACHI"])
                    || validateDataRow(dr["SDT"])
                    || validateDataRow(dr["NGAYVL"])
                    || validateDataRow(dr["LUONG"])
                    || validateDataRow(dr["TAIKHOAN"])
                    || validateDataRow(dr["maquyen"]))
                {

                    NhanVienBUS.Instances.getDataGV(gcStaff);
                    return;
                }

                try
                {
                    NhanVienBUS.Instances.insert(dr["TENNV"].ToString().Trim(), dr["DIACHI"].ToString().Trim()
                         , dr["SDT"].ToString().Trim(), dr["GIOITINH"]==null||dr["GIOITINH"].ToString()==""?false: bool.Parse(dr["GIOITINH"].ToString().Trim()), DateTime.Parse(dr["NGAYVL"].ToString().Trim())
                         , double.Parse(dr["LUONG"].ToString().Trim()), "hinhanh", dr["taikhoan"].ToString().Trim()
                         , int.Parse(dr["maquyen"].ToString().Trim()));
                    XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                }
                catch (Exception ex)
                {

                }
                NhanVienBUS.Instances.getDataGV(gcStaff);
            }
        }

        private void gvRole_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr;
            if (e.RowHandle >= 0)
            {
                dr = gvRole.GetDataRow(e.RowHandle);
                if (validateDataRow(dr["tenquyen"]))
                {
                    QuyenBUS.Instances.getDataGV(gcRole);
                    return;
                }
                else
                {
                    try
                    {
                        QuyenBUS.Instances.update(dr["tenquyen"].ToString().Trim(), int.Parse(dr["maquyen"].ToString().Trim()));
                    }
                    catch (Exception)
                    {

                    }
                }
                QuyenBUS.Instances.getDataGV(gcRole);
            }
            else
            {
                dr = gvRole.GetDataRow(gvRole.RowCount - 1);
                if (validateDataRow(dr["tenquyen"]))
                {

                    QuyenBUS.Instances.getDataGV(gcRole);
                    return;
                }

                try
                {
                    QuyenBUS.Instances.insert(dr["tenquyen"].ToString().Trim());

                    XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                }
                catch (Exception ex)
                {

                }
                QuyenBUS.Instances.getDataGV(gcRole);
            }
        }
    }
}
