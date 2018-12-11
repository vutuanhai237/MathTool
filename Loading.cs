using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Math_V1._1
{
    public partial class Loading : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Loading()
        {
            InitializeComponent();
            
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
            
        }
        int i = 2;
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            progressPanel1.Caption = "Thiết lập xong sau " + i + "s";
            i = i - 1;
            progressPanel1.Caption = "Thiết lập xong sau " + i + "s";

            if (i== 0)
            {
                this.Close();
               
            }
        }
    }
}