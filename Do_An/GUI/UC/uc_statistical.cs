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
using DAO;

namespace GUI.UC
{
    public partial class uc_statistical : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_statistical(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if(DateTime.Parse(dateFrom.DateTime.ToShortDateString()).CompareTo(DateTime.Parse(dateTo.DateTime.ToShortDateString())) >0)
            {
                XtraMessageBox.Show("Ngày tìm không hợp lệ.", "Thông báo");
                return;
            }
            var sumStatistic = StatisticalBUS.Instance.tinhTienThu(dateFrom.DateTime, dateTo.DateTime);
            var sumSpend = StatisticalBUS.Instance.tinhTienChi(dateFrom.DateTime, dateTo.DateTime);
            txtSumStatistic.Text = Support.convertVND(sumStatistic.ToString());
            txtSumSpend.Text = Support.convertVND(sumSpend.ToString());
            txtProfit.Text = Support.convertVND((sumStatistic - sumSpend).ToString());
            StatisticalBUS.Instance.loadDetailStatistical(gcStatistical, dateFrom.DateTime, dateTo.DateTime);

        }
    }
}
