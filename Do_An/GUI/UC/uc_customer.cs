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
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }
    }
}
