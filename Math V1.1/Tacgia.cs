using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
namespace Math_V1._1
{
    public partial class Tacgia : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Tacgia()
        {
            InitializeComponent();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/haicho10a1");
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            this.Close();
            Gmail fr = new Gmail();
            fr.ShowDialog();
        }
    }
}