using DAO;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
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
        UserControl uc;
        NHANVIEN nv;
        frmSystem frm;
        public frmMain(frmSystem frm, NHANVIEN nv)
        {
            InitializeComponent();
            lbTieuDe.Caption = "Trang chủ";
            this.nv = nv;
            this.frm = frm;
            //btnAccount.Caption = nv.TENNV;
          //repositoryItemPictureEdit3.image = Image.FromFile("../../Images/" + nv.HINHANH);
        }
        public void _close()
        {
            mainContainer.Controls.Remove(uc);
            mainContainer.BringToFront();
            lbTieuDe.Caption = "Trang chủ";

        }

        private void openUC(Type typeUC)
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
            uc = (UserControl)Activator.CreateInstance(typeUC, this);
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
        private void btnBanHang_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_order_employee));

        }
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_import_employee));
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_order));

        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_import));

        }


        private void btnStatistical_Day_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_statistical));
        }

        private void btnStatistical_Year_Click(object sender, EventArgs e)
        {
            openUC(typeof(uc_statistical));
        }

        private void btnBackup_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "SQL Backup (*.bak)|*.bak";
            sf.Title = "Backup database";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                new frmBKRS(sf.FileName, 0).ShowDialog();
            }
        }

        private void btnRestore_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "SQL Backup (*.bak)|*.bak";
            op.Title = "Restore database";
            if (op.ShowDialog() == DialogResult.OK)
            {
                new frmBKRS(op.FileName, 1).ShowDialog();

            }
        }
        void logout()
        {
            if (XtraMessageBox.Show("Bạn muốn đăng xuất trái đất hả?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Hide();
                frm.Show();
            }
        }
        private void btnLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            logout();
        }
      
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            logout();
        }

        private void btnAccount_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
