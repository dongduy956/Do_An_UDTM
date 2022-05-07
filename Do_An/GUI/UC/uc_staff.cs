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
    public partial class uc_staff : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_staff(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm._close();
        }

        private void uc_staff_Load(object sender, EventArgs e)
        {
            NhanVienBUS.Instances.getDataGV(gcStaff);
        }
    }
}
