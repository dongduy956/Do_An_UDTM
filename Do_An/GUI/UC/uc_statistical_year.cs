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
    public partial class uc_statistical_year : DevExpress.XtraEditors.XtraUserControl
    {
        private frmMain frmMain;
        public uc_statistical_year(frmMain frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                double? tienThu = StatisticalBus.Instance.tinhTienChi(DateTime.Parse(dtpStatisticaldate.Text), 2) ?? 0;
                double? tienChi = StatisticalBus.Instance.tinhTienThu(DateTime.Parse(dtpStatisticaldate.Text), 2) ?? 0;
                txtTienThu.Text =Support.convertVND(tienThu.ToString());
                txtTienChi.Text = Support.convertVND(tienChi.ToString());
                txtTongLoi.Text = Support.convertVND("" + (tienThu - tienChi));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmMain._close();
        }
    }
}
