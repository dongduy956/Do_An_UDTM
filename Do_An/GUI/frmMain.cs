using DevExpress.XtraBars;
using GUI.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public  void _close()
        {
            mainContainer.Controls.Remove(uc);
            mainContainer.BringToFront();
            lbTieuDe.Caption = "Trang chủ";

        }
        UserControl uc;
        public frmMain()
        {
            InitializeComponent();
            lbTieuDe.Caption = "Trang chủ";
        }
        void openUC(Type typeUC)
        {
            bool check = false;
            foreach (UserControl _uc in mainContainer.Controls)
            {

                if (_uc.GetType() == typeUC)
                {
                    _uc.BringToFront();
                    lbTieuDe.Caption = _uc.Tag.ToString();
                    check = true;
                    continue;
                }
                mainContainer.Controls.Remove(_uc);

            }
            if (check)
                return;
             uc = (UserControl)Activator.CreateInstance(typeUC,this);
            uc.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(uc);
            uc.BringToFront();
            lbTieuDe.Caption = uc.Tag.ToString();
        }
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_customer));
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_staff));

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_product));

        }
    }
}
