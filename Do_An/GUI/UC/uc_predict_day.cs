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
using DevExpress.XtraCharts;

namespace GUI.UC
{
    public partial class uc_predict_day : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        DataTable tb;
        public uc_predict_day(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            tb = NoronNextDayBUS.Instances.loadDataGC(gcPredictNextDay);
            gvPredictNextDay.IndicatorWidth = 50;
            loadChartPredict();
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

        private void loadChartPredict()
        {
            Series _seri = new Series("", ViewType.SwiftPlot);
            ChartTitle title = new ChartTitle();
            title.Text = "Doanh thu 30 ngày gần nhất.";
            chartControl1.Titles.Add(title);
            chartControl1.Series.Add(_seri);
            //   _seri.AxisY.Label.TextPattern = "{V:n2}€";
       
            foreach (DataRow dr in tb.Rows)
                _seri.Points.Add(new SeriesPoint(dr[0].ToString(), dr[2].ToString().Equals("") ? "0" : dr[2].ToString()));
            //_seri.Label.TextPattern = "{S}: {V:n2}";
            foreach (Series series in chartControl1.Series)
            {
                series.CrosshairLabelPattern = "{A:d}: {V:N0}";
            }
           

        }

        private void chartControl1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {

            try
            {
                e.Item.Text = Support.convertVND(e.Item.Text);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
