﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GUI.UC
{
    public partial class uc_order : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_order(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
    }
}
