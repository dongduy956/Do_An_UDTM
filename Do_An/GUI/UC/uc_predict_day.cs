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
using GUI.FRM;
using BUS;
using DAO;

namespace GUI.UC
{
    public partial class uc_predict_day : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_predict_day(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            NoronNextDayBUS.Instances.loadDataGC(gcPredictNextDay);
            gvPredictNextDay.IndicatorWidth = 50;
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }

        private void btnPredict_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMessageBox.Show("Doanh thu ngày " + DateTime.Now.AddDays(1).ToShortDateString() + " là : " + Support.convertVND(NoronNextDayBUS.Instances.ReturnResult().ToString()), "Thông báo");
        }

        private void gvPredictNextDay_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }        
    }
}
