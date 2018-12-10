using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
using DevExpress.XtraBars.Navigation;
using DevExpress.Utils;
using DevExpress.XtraBars.Helpers;


namespace Math_V1._1
{
    public partial class Huongdansudung : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Huongdansudung()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Process.Start("https://drive.google.com/file/d/0BzYCsh6yeMDASFRjQzNsZFFXRTQ/view?usp=sharing");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Process.Start("https://drive.google.com/file/d/0BzYCsh6yeMDASFRjQzNsZFFXRTQ/view?usp=sharing");

        }
    }
}