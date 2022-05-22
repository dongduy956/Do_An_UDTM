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
using GUI.FRM;
using DAO;
using GUI.Report;
using DevExpress.XtraReports.UI;

namespace GUI.UC
{
    public partial class uc_import : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        List<CHITIETNK> lstDetailImport;

        public uc_import(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        //load data khi form khởi chạy
        private void uc_import_Load(object sender, EventArgs e)
        {
            NhapKhoBUS.Instances.getDataGV(gcImport, true);

        }
        //đóng form
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }

        private void gvImport_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            var mapn = gvImport.GetRowCellValue(e.RowHandle, "MAPN");
            if (mapn != null)
                e.IsEmpty = ChiTietNKBUS.Instances.getDataGV(int.Parse(mapn.ToString())).Count == 0;
        }

        private void gvImport_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var mapn = gvImport.GetRowCellValue(e.RowHandle, "MAPN");
            if (mapn != null)
            {
                e.ChildList = ChiTietNKBUS.Instances.getDataGV(int.Parse(mapn.ToString()));
                gvImportDetail.ViewCaption = "Chi tiết phiếu nhập " + mapn;

            }
        }

        private void gvImport_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void gvImport_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Chi tiết phiếu nhập";
        }
        private void deleteImport()
        {
            int row = gvImport.FocusedRowHandle;
            if (row >= 0)
            {
                int mapn= int.Parse(gvImport.GetRowCellValue(row, "MAPN").ToString());
                if (XtraMessageBox.Show("Bạn chắc chắn xoá phiếu nhập " + mapn + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    int i = NhapKhoBUS.Instances.delete(mapn);
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Xoá phiếu nhập thành công.", "Thông báo");
                        NhapKhoBUS.Instances.getDataGV(gcImport, true);
                    }
                }
            }
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            deleteImport();
        }

        private void gcImport_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvImport.State != GridState.Editing)
                deleteImport();
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gvImport.FocusedRowHandle;
            if (row >= 0)
            {
                int mapn = int.Parse(gvImport.GetRowCellValue(row, "MAPN").ToString());
                lstDetailImport = ChiTietNKBUS.Instances.getDataGV(mapn);
                NHAPKHO nk = NhapKhoBUS.Instances.findOrderCode(mapn);
                var rp = new rpImport();
                rp.DataSource = lstDetailImport;
                rp.lbNguoiLap.Value = frm.nv.TENNV;
                rp.lbCodeImport.Value = "BÁO CÁO PHIẾU NHẬP " + mapn;
                rp.lbDate.Value = nk.NGAYNHAP;
                rp.lbStaff.Value = nk.NHANVIEN.TENNV;
                rp.ShowPreviewDialog();
            }
        }
    }
}
