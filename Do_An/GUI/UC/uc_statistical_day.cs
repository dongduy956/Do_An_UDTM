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
using DAO;
using BUS;

namespace GUI.UC
{
    public partial class uc_statistical_day : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_statistical_day(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        

        private void btnStatistical_Click(object sender, EventArgs e)
        {
            try
            {
                double? tienThu = StatisticalBus.Instance.tinhTienChi(DateTime.Parse(dtpStatisticaldate.Control.Text))??0;
                double? tienChi = StatisticalBus.Instance.tinhTienThu(DateTime.Parse(dtpStatisticaldate.Control.Text))??0;
                txtTienThu.Text = Support.convertVND(tienThu.ToString());
                txtTienChi.Text = Support.convertVND(tienChi.ToString());
                txtTotal.Text = Support.convertVND("" + (tienThu - tienChi));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            /*
             Ngày đó nhập bao nhiêu sản phẩm.
             Tổng tiền sản phẩm này
             Tiền sản phẩm bán ra
             */
        }
    }
}
