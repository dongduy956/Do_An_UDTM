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
    public partial class uc_import_employee : DevExpress.XtraEditors.XtraUserControl
    {
        frmMain frm;
        public uc_import_employee(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }


    }
}
