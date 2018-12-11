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
    public partial class Thongbao : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Thongbao()
        {
            InitializeComponent();
        }
         public Thongbao(string strTextBox)
        {
            InitializeComponent();
            lblThongbao.Text = strTextBox;
            //Gán dữ liệu nhận được vào Label để thể hiện
        }
        private void Thongbao_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
        }
        int i = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            label1.Text = "Cửa sổ sẽ đóng sau " + i + " s";
            if (i==0)
            {
                this.Close();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}