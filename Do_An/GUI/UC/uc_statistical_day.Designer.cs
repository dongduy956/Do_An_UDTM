namespace GUI.UC
{
    partial class uc_statistical_day
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_statistical_day));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnStatistical = new DevExpress.XtraEditors.SimpleButton();
            this.txtTienThu = new DevExpress.XtraEditors.TextEdit();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtTienChi = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dtpStatisticaldate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTienThu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnDong = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienThu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStatisticaldate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTienThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnStatistical);
            this.layoutControl1.Controls.Add(this.txtTotal);
            this.layoutControl1.Controls.Add(this.txtTienThu);
            this.layoutControl1.Controls.Add(this.dateTimePicker1);
            this.layoutControl1.Controls.Add(this.txtTienChi);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(640, 208);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnStatistical
            // 
            this.btnStatistical.Location = new System.Drawing.Point(322, 54);
            this.btnStatistical.Name = "btnStatistical";
            this.btnStatistical.Size = new System.Drawing.Size(294, 32);
            this.btnStatistical.StyleController = this.layoutControl1;
            this.btnStatistical.TabIndex = 7;
            this.btnStatistical.Text = "Thống kê";
            this.btnStatistical.Click += new System.EventHandler(this.btnStatistical_Click);
            // 
            // txtTienThu
            // 
            this.txtTienThu.Location = new System.Drawing.Point(103, 90);
            this.txtTienThu.Name = "txtTienThu";
            this.txtTienThu.Properties.ReadOnly = true;
            this.txtTienThu.Size = new System.Drawing.Size(513, 28);
            this.txtTienThu.StyleController = this.layoutControl1;
            this.txtTienThu.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(103, 54);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(215, 27);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // txtTienChi
            // 
            this.txtTienChi.Location = new System.Drawing.Point(103, 122);
            this.txtTienChi.Name = "txtTienChi";
            this.txtTienChi.Properties.ReadOnly = true;
            this.txtTienChi.Size = new System.Drawing.Size(513, 28);
            this.txtTienChi.StyleController = this.layoutControl1;
            this.txtTienChi.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(640, 208);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.dtpStatisticaldate,
            this.lblTienThu,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(620, 188);
            this.layoutControlGroup1.Text = "Doanh thu theo ngày";
            // 
            // dtpStatisticaldate
            // 
            this.dtpStatisticaldate.Control = this.dateTimePicker1;
            this.dtpStatisticaldate.Location = new System.Drawing.Point(0, 0);
            this.dtpStatisticaldate.Name = "dtpStatisticaldate";
            this.dtpStatisticaldate.Size = new System.Drawing.Size(298, 36);
            this.dtpStatisticaldate.Text = "Chọn ngày";
            this.dtpStatisticaldate.TextSize = new System.Drawing.Size(76, 19);
            // 
            // lblTienThu
            // 
            this.lblTienThu.Control = this.txtTienThu;
            this.lblTienThu.Location = new System.Drawing.Point(0, 36);
            this.lblTienThu.Name = "lblTienThu";
            this.lblTienThu.Size = new System.Drawing.Size(596, 32);
            this.lblTienThu.Text = "Tiền thu";
            this.lblTienThu.TextSize = new System.Drawing.Size(76, 19);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtTienChi;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "Tiền thu";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(596, 32);
            this.layoutControlItem2.Text = "Tiền chi";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 19);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnStatistical;
            this.layoutControlItem1.Location = new System.Drawing.Point(298, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(298, 36);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnDong});
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "Main menu";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDong, true)});
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main menu";
            // 
            // btnDong
            // 
            this.btnDong.Caption = "Đóng";
            this.btnDong.Id = 3;
            this.btnDong.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.ImageOptions.Image")));
            this.btnDong.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDong.ImageOptions.LargeImage")));
            this.btnDong.Name = "btnDong";
            this.btnDong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnDong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDong_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(640, 40);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 248);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(640, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 40);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 208);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(640, 40);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 208);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtTotal;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(596, 34);
            this.layoutControlItem3.Text = "Tiền lời";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 19);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(103, 154);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(513, 28);
            this.txtTotal.StyleController = this.layoutControl1;
            this.txtTotal.TabIndex = 6;
            // 
            // uc_statistical_day
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "uc_statistical_day";
            this.Size = new System.Drawing.Size(640, 248);
            this.Tag = "Thống Kê Ngày";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTienThu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStatisticaldate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTienThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtTienThu;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem dtpStatisticaldate;
        private DevExpress.XtraLayout.LayoutControlItem lblTienThu;
        private DevExpress.XtraEditors.TextEdit txtTienChi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnStatistical;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnDong;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}
