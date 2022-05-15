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

namespace GUI.UC
{
    public partial class uc_order : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_order(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        //load data khi form khởi chạy
        private void uc_order_Load(object sender, EventArgs e)
        {
            HoaDonBUS.Instances.getDataGV(gcOrder, true);
        }
        //đóng form hoá đơn
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }

        private void gvOrder_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            var mahd = gvOrder.GetRowCellValue(e.RowHandle, "MAHD");
            if (mahd != null)
                e.IsEmpty = ChiTietHDBUS.Instances.getDataGV(int.Parse(mahd.ToString())).Count == 0;
        }

        private void gvOrder_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var mahd = gvOrder.GetRowCellValue(e.RowHandle, "MAHD");
            if (mahd != null)
            {
                e.ChildList = ChiTietHDBUS.Instances.getDataGV(int.Parse(mahd.ToString()));
                gvOrderDetail.ViewCaption = "Chi tiết hoá đơn " + mahd;

            }
        }

        private void gvOrder_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void gvOrder_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Chi tiết hoá đơn";
        }
        private void deleteOrder()
        {
            int row = gvOrder.FocusedRowHandle;
            if (row >= 0)
            {
                int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                if (XtraMessageBox.Show("Bạn chắc chắn xoá hoá đơn " + mahd + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    int i = HoaDonBUS.Instances.delete(mahd);
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Xoá hoá đơn thành công.", "Thông báo");
                        HoaDonBUS.Instances.getDataGV(gcOrder, true);
                    }
                }
            }
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            deleteOrder();
        }



        private void gcOrder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvOrder.State != GridState.Editing)
                deleteOrder();
        }
    }
}
