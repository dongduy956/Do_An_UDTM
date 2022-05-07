using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace GUI
{
    public partial class frmSystem : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmSystem()
        {
            InitializeComponent();
        }
        void openForm(Type typeForm)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm.GetType() == typeForm)
                {
                    frm.Activate();
                    return;
                }
            }
            Form f = (Form)Activator.CreateInstance(typeForm);
            f.MdiParent = this;
            f.Show();
        }
      

        private void btnConnect_ItemClick(object sender, ItemClickEventArgs e)
        {
            openForm(typeof(frmConnect));

        }

        private void btnLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            openForm(typeof(frmLogin));

        }

        private void frmSystem_Load(object sender, EventArgs e)
        {
            openForm(typeof(frmLogin));

        }
    }
}