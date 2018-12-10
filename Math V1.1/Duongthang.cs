using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils.Animation;
namespace Math_V1._1
{
    public partial class Duongthang : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region"Maytinh
        public bool CoKetqua = false;//Lưu trạng thái phím dấu =
        public bool CoTrangthainhap = false;//Lưu trạng thái nhap so
        public bool CoPheptinh = false;//Lưu trạng thái phím Phép tính +,-,*,/
        public bool CoTeliet = false;//Lưu trạng thái Tê liệt bàn phím 
        public string Pheptinh = "";//Lưu phép tính +,-,*,/
        public double Sohang1 = 0;//Lưu số hạng 1
        public double Sohang2 = 0;//Lưu số hạng 2 
        public double Nho = 0;//Lưu biến nhớ của MS và M+
        public string Copy = "";
        public bool CoPhantram = false;
        public double BienPhantram = 0;
        public bool CoTinhdon = false;
        public void Nhapso(string so)
        {
            if (!CoTeliet)
            {
                if (CoKetqua) Sohang1 = double.Parse(txtOutput.Text);
                if ((!CoKetqua) && (!CoTrangthainhap) && (!CoPheptinh))
                {//chưa bấm một trong các phím trên thì thực hiện viêc nối vào sau
                    if (txtOutput.Text == "0") //đấu tiên là số 0
                        txtOutput.Text = "";//thì xóa hắn đi
                    txtOutput.Text = txtOutput.Text + so;//sau đó nối vào bên phải
                }
                else//còn bấm rồi thì thay thế
                {
                    txtOutput.Text = so;//thay thế
                    CoTrangthainhap = false;
                    CoKetqua = false;
                    CoPheptinh = false;
                }
            }
        }
        public void Nhappheptinh(string pt)
        {
            txtOutput.Text = double.Parse(txtOutput.Text).ToString();
            //đưa về giá trị của màn hình vd nhập 0.100000 = 0.1
            if (CoTinhdon) Pheptinh = "";
            if ((Pheptinh != "") && (!CoPheptinh))
                Tinhtoan();
            Sohang1 = double.Parse(txtOutput.Text);//Lưu vào số hạng 1
            Pheptinh = pt;//gán vào biến pt(nhận các giá trị +,-,*,/)
            CoKetqua = false;
            CoTrangthainhap = false;
            CoTinhdon = false;
            CoPheptinh = true;
            CoPhantram = true;
            BienPhantram = Sohang1 / 100;
        }
        public void Tinhtoan()
        {
            txtOutput.Text = double.Parse(txtOutput.Text).ToString();
            double Ketqua;
            if (!CoKetqua)
                Sohang2 = double.Parse(txtOutput.Text);
            if (Pheptinh == "+")
            {
                if (!CoKetqua)
                    Ketqua = Sohang1 + Sohang2;
                else
                    Ketqua = double.Parse(txtOutput.Text) + Sohang2;
                txtOutput.Text = Ketqua.ToString();
            }
            else
                if (Pheptinh == "-")
                {
                    if (!CoKetqua)
                        Ketqua = Sohang1 - Sohang2;
                    else
                        Ketqua = double.Parse(txtOutput.Text) - Sohang2;
                    txtOutput.Text = Ketqua.ToString();
                }
                else
                    if (Pheptinh == "*")
                    {
                        if (!CoKetqua)
                            Ketqua = Sohang1 * Sohang2;
                        else
                            Ketqua = double.Parse(txtOutput.Text) * Sohang2;
                        txtOutput.Text = Ketqua.ToString();
                    }
                    else
                        if (Pheptinh == "/")
                        {
                            if (Sohang2 != 0)//khác 0 mới chia
                            {
                                if (!CoKetqua)
                                    Ketqua = Sohang1 / Sohang2;
                                else
                                    Ketqua = double.Parse(txtOutput.Text) / Sohang2;
                                txtOutput.Text = Ketqua.ToString();
                            }
                            else
                            {
                                txtOutput.Text = "Không thể chia cho 0!";
                                CoTeliet = true;//bật thằng này lên để khỏi bấm phím ngoài CE và C
                            }
                        }
            CoKetqua = true;
            CoPheptinh = false;
        }
        public Duongthang()
        {
            InitializeComponent();
        }
        private void cmdBackSpace_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                if ((!CoKetqua) && (!CoTrangthainhap) && (!CoPheptinh))
                    if ((txtOutput.Text.Length == 1) || ((txtOutput.Text.Length == 2) && (txtOutput.Text.Contains("-"))))
                        //Nếu số từ -9 đến 9 mà không phải số thập phân thì cho về 0
                        txtOutput.Text = "0";
                    else
                    {   //Xóa ký tự ngoài cùng bên phải
                        txtOutput.Text = txtOutput.Text.Substring(0, txtOutput.Text.Length - 1);
                    }
            }
        }
        private void cmd0_Click(object sender, EventArgs e)
        {
            Nhapso("0");
        }
        private void cmd1_Click(object sender, EventArgs e)
        {
            Nhapso("1");
        }
        private void cmd2_Click(object sender, EventArgs e)
        {
            Nhapso("2");
        }
        private void cmd3_Click(object sender, EventArgs e)
        {
            Nhapso("3");
        }
        private void cmd4_Click(object sender, EventArgs e)
        {
            Nhapso("4");
        }
        private void cmd5_Click(object sender, EventArgs e)
        {
            Nhapso("5");
        }
        private void cmd6_Click(object sender, EventArgs e)
        {
            Nhapso("6");
        }
        private void cmd7_Click(object sender, EventArgs e)
        {
            Nhapso("7");
        }
        private void cmd8_Click(object sender, EventArgs e)
        {
            Nhapso("8");
        }
        private void cmd9_Click(object sender, EventArgs e)
        {
            Nhapso("9");
        }
        private void cmdCE_Click(object sender, EventArgs e)
        {
            CoTeliet = false;
            txtOutput.Text = "0";
        }
        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "0";
            CoKetqua = false;
            CoTrangthainhap = false;
            CoPheptinh = false;
            CoTeliet = false;
            Pheptinh = "";
            Sohang1 = 0;
            Sohang2 = 0;
            CoPhantram = false;
        }
        private void cmdDoidau_Click(object sender, EventArgs e)
        {
            if (!CoTeliet) txtOutput.Text = (-double.Parse(txtOutput.Text)).ToString();

        }
        private void cmdThapphan_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
                if ((!txtOutput.Text.Contains(".") && (!CoKetqua) && (!CoTrangthainhap) && (!CoPheptinh)))
                    txtOutput.Text = txtOutput.Text + ".";
        }
        private void cmdCong_Click(object sender, EventArgs e)
        {
            if (!CoTeliet) Nhappheptinh("+");
        }
        private void cmdTru_Click(object sender, EventArgs e)
        {
            if (!CoTeliet) Nhappheptinh("-");
        }
        private void cmdNhan_Click(object sender, EventArgs e)
        {
            if (!CoTeliet) Nhappheptinh("*");
        }
        private void cmdChia_Click(object sender, EventArgs e)
        {
            if (!CoTeliet) Nhappheptinh("/");
        }
        private void cmdBang_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                Tinhtoan();
                CoTinhdon = true;
            }
        }
        private void cmdMCong_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                if (txtOutput.Text != "0")// Khác 0 mới lưu
                {
                    double nho1 = double.Parse(txtOutput.Text);//Lưu vào biến nho1
                    Nho = Nho + nho1;
                }
                //tắt các cờ liên quan đến công việc nhập số
                if (Nho == 0)
                    lblNho.Text = "";
                else lblNho.Text = "M";//Hiển thị chữ M (đã có nhớ)
                CoKetqua = false;
                CoTrangthainhap = true;
            }
        }
        private void cmdMS_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {

                if (txtOutput.Text != "0")
                {
                    Nho = double.Parse(txtOutput.Text);
                    CoTrangthainhap = true;
                }
                if (Nho == 0)
                    lblNho.Text = "";
                else lblNho.Text = "M";//Hiển thị chữ M (đã có nhớ)
                CoKetqua = false;
            }
        }
        private void cmdMC_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                CoTrangthainhap = false;
                Nho = 0;//Lúc này bấm phím gọi nhớ MR sẽ cho kq là 0
                lblNho.Text = "";
            }
        }
        private void cmdMR_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Nho.ToString();
                CoKetqua = false;
                CoTrangthainhap = true;
            }
        }
        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmdsin_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Sin((double.Parse(txtOutput.Text) * Math.PI) / 180).ToString();
                CoTrangthainhap = true;
            }
        }
        private void cmdcos_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Cos((double.Parse(txtOutput.Text) * Math.PI) / 180).ToString();
                CoTrangthainhap = true;
            }
        }
        private void cmdtan_Click(object sender, EventArgs e)
        {
            txtOutput.Text = Math.Tan((double.Parse(txtOutput.Text) * Math.PI) / 180).ToString();
            CoTrangthainhap = true;
        }
        private void cmdLuythua_Click(object sender, EventArgs e)
        {

        }
        private void cmdSqrt_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Sqrt((double.Parse(txtOutput.Text))).ToString();
                CoTrangthainhap = true;
            }
        }
        private void cmdLapphuong_Click(object sender, EventArgs e)
        {
            double tam = double.Parse(txtOutput.Text);
            txtOutput.Text = (tam * tam * tam).ToString();
            CoTrangthainhap = true;
        }
        private void cmdBinhphuong_Click(object sender, EventArgs e)
        {
            double tam = double.Parse(txtOutput.Text);
            txtOutput.Text = (tam * tam).ToString();
            CoTrangthainhap = true;
        }
        private void cmdNgichdao_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                if (double.Parse(txtOutput.Text) != 0)
                {//Nếu màn hình khác 0 mới nghịch đảo
                    double tam = 1 / double.Parse(txtOutput.Text);//Lấy 1 chia cho hắn là xong
                    txtOutput.Text = tam.ToString();//Xuất ra màn hình thằng vừa chia được
                    CoTrangthainhap = true;//bật cờ lên để nhập số lại đó mà
                }
                else
                {
                    txtOutput.Text = "Bạn không thể chia cho 0!";
                    CoTeliet = true;//Bật cờ Tê liệt lên để khỏi bấm phím gì hết ngoài CE và C
                }
            }
        }
        private void cmdGiaithua_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                int i;
                double giaithua = 1;
                for (i = 1; i <= double.Parse(txtOutput.Text); i++)
                    giaithua = giaithua * i;
                txtOutput.Text = giaithua.ToString();
                CoTrangthainhap = true;
            }
        }
        private void cmdPi_Click(object sender, EventArgs e)
        {
            txtOutput.Text = Math.PI.ToString();
            CoTrangthainhap = true;
        }
        private void cmdSta_Click(object sender, EventArgs e)
        {

        }
        private void cmdLn_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                if (double.Parse(txtOutput.Text) > 0)
                {
                    txtOutput.Text = Math.Log(double.Parse(txtOutput.Text)).ToString();
                    CoTrangthainhap = true;
                }
                else
                {
                    txtOutput.Text = "Invalid Input for function";
                    CoTeliet = true;
                }
            }
        }
        private void cmdlog_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                if (double.Parse(txtOutput.Text) > 0)
                {
                    txtOutput.Text = Math.Log10(double.Parse(txtOutput.Text)).ToString();
                    CoTrangthainhap = true;
                }
                else
                {
                    txtOutput.Text = "Invalid Input for function";
                    CoTeliet = true;
                }
            }
        }
        private void txtOutput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.')
                e.Handled = true;
            if (txtOutput.Text.Contains(".") && (e.KeyChar == '.') && (txtOutput.Text != ""))
                e.Handled = true;
            if (txtOutput.Text == "" && e.KeyChar == '.') e.Handled = true;
            if (e.KeyChar == '-' && !txtOutput.Text.Contains("-"))
                txtOutput.Text = "-" + txtOutput.Text;
            if (e.KeyChar == '-' && txtOutput.Text.Contains("-"))
                e.Handled = true;
        }
        #endregion// may tinh
        private BaseTransition CreateTransitionInstance(Transitions transitionType)
        {
            switch (transitionType)
            {
                case Transitions.Clock: return new ClockTransition();
                case Transitions.Dissolve: return new DissolveTransition();
                case Transitions.Fade: return new FadeTransition();
                case Transitions.Shape: return new ShapeTransition();
                case Transitions.SlideFade: return new SlideFadeTransition();
                case Transitions.Cover: return new CoverTransition();
                case Transitions.Comb: return new CombTransition();
                default: return new PushTransition();
            }
        }
        private void Duongthang_Load(object sender, EventArgs e)
        {
            Loading fr1 = new Loading();
            fr1.ShowDialog();
            txtOutput.Text = "0";
            lblTime.Caption = "";
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
            lcAnimatedControl = xtraTabControl1;
            transitionManager1.CustomTransition += transitionManager1_CustomTransition;
            mySetTransitionType();
            this.mode.SelectedIndex = 0;
            this.sensitivity.Enabled = false;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((txt1.Text == "") || (txt2.Text == "") || (txt3.Text == "") || (txt4.Text == ""))
            {
                ve1.Enabled = false;

            }
            else
            {
                ve1.Enabled = true;
            }
            if ((txt5.Text == "") || (txt6.Text == "") || (txt7.Text == "") || (txt8.Text == ""))
            {
                ve2.Enabled = false;

            }
            else
            {
                ve2.Enabled = true;
            }
            if ((txt9.Text == "") || (txt10.Text == "") || (txt11.Text == "") || (txt12.Text == ""))
            {
                ve3.Enabled = false;

            }
            else
            {
                ve3.Enabled = true;
            }
            if ((txt13.Text == "") || (txt14.Text == "") || (txt15.Text == ""))
            {
                ve4.Enabled = false;

            }
            else
            {
                ve4.Enabled = true;
            }
            if ((txt16.Text == "") || (txt17.Text == "") || (txt18.Text == "") || (txt19.Text == "") || (txt20.Text == ""))
            {
                ve5.Enabled = false;

            }
            else
            {
                ve5.Enabled = true;
            }
            if ((txt21.Text == "") || (txt22.Text == "") || (txt23.Text == "") || (txt24.Text == "") || (txt25.Text == ""))
            {
                ve6.Enabled = false;

            }
            else
            {
                ve6.Enabled = true;
            }
            if ((txt26.Text == "") || (txt27.Text == "") || (txt28.Text == "") || (txt30.Text == "") || (txt31.Text == "") || (txt32.Text == ""))
            {
                ve7.Enabled = false;

            }
            else
            {
                ve7.Enabled = true;
            }
            lblTime.Caption = (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;
         
        }
        #region "Var
        private DevExpress.Data.Utils.IEasingFunction _EasingFunc = new DevExpress.Data.Utils.BackEase();
        private System.Random rand = new System.Random();
        private Control lcAnimatedControl;
        #endregion
        #region"tapcontrol
        void transitionManager1_CustomTransition(ITransition transition, CustomTransitionEventArgs e)
        {
            e.Regions = new Rectangle[] { xtraTabControl1.SelectedTabPage.Bounds };
            e.EasingFunction = _EasingFunc;
        }
        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            transitionManager1.EndTransition();
            mySetTransitionType();
        }
        void xtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (lcAnimatedControl == null) return;
            transitionManager1.StartTransition(lcAnimatedControl);
        }
        private void mySetTransitionType()
        {
            if (transitionManager1.Transitions[lcAnimatedControl] == null)
            {
                Transition transition1 = new Transition();
                transition1.Control = lcAnimatedControl;
                transitionManager1.Transitions.Add(transition1);
            }
            // Specify the transition type.
            Transitions trType = (Transitions)(rand.Next(0, 7));
            transitionManager1.Transitions[lcAnimatedControl].TransitionType = CreateTransitionInstance(trType);
        }
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage1.Show();
        }
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage2.Show();
        }
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage3.Show();
        }
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage4.Show();
        }
        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage5.Show();
        }
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage6.Show();
        }
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage7.Show();
        }
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage8.Show();
        }
        #endregion
       
        #region"ptdtqua2diem
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (txt1.Text == "")
            {
               Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xA");
                fr.ShowDialog();
            }
            else
            {
                if (txt2.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yA");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt3.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xB");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt4.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yB");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double xa = Convert.ToDouble(txt1.Text);
                            double ya = Convert.ToDouble(txt2.Text);
                            double xb = Convert.ToDouble(txt3.Text);
                            double yb = Convert.ToDouble(txt4.Text);
                            if (xa == xb)
                            {
                                if (ya == yb)
                                {
                                    textBox1.Text = "Có vô số đường thẳng đi qua 2 điểm vừa nhập";
                                }
                                else
                                {
                                    textBox1.Text = "Phương trình ĐT Δ có dạng : " + Environment.NewLine + "x = " + Math.Round(xa, 5);
                                }
                            }
                            else
                            {
                                if (ya == yb)
                                {
                                    textBox1.Text = "Phương trình ĐT Δ có dạng : " + Environment.NewLine + "y = " + Math.Round(ya, 5);
                                }
                                else
                                {
                                    textBox1.Text = "Phương trình ĐT Δ có dạng : " + Environment.NewLine + Math.Round(yb - ya, 5) + "x";
                                    if  (xa - xb > 0)
                                    {
                                        textBox1.Text += " + " + Math.Round(xa - xb, 5) + "y";
                                    }
                                    if (xa - xb < 0)
                                    {
                                        textBox1.Text += " - " + -Math.Round(xa - xb, 5) + "y";
                                    }
                                    if ((ya * (xa - xb) + xa * (yb - ya)) > 0)
                                    {
                                        textBox1.Text += " - " + Math.Round((ya * (xa - xb) + xa * (yb - ya)), 5);
                                    }
                                    if ((ya * (xa - xb) + xa * (yb - ya)) < 0)
                                    {
                                        textBox1.Text += " + " + -Math.Round((ya * (xa - xb) + xa * (yb - ya)), 5);

                                    }
                                    textBox1.Text += " = 0";
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            textBox1.Clear();
        }
        private void textEdit1_DoubleClick(object sender, EventArgs e)
        {
            txt1.Text = txtOutput.Text.ToString();
        }
        private void txt3_DoubleClick(object sender, EventArgs e)
        {
            txt3.Text = txtOutput.Text.ToString();
        }
        private void txt2_DoubleClick(object sender, EventArgs e)
        {
            txt2.Text = txtOutput.Text.ToString();
        }
        private void txt4_DoubleClick(object sender, EventArgs e)
        {
            txt4.Text = txtOutput.Text.ToString();
        }
        #endregion    

        #region"vectophaptuyen
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (txt5.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt6.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt7.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập n₁");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt8.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập n₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double xa = Convert.ToDouble(txt5.Text);
                            double ya = Convert.ToDouble(txt6.Text);
                            double xn = Convert.ToDouble(txt7.Text);
                            double yn = Convert.ToDouble(txt8.Text);
                            if (xn == yn && xn == 0)
                            {
                                Thongbao fr = new Thongbao("Lưu ý, Véctơ pháp tuyến phải khác véctơ không");
                                fr.ShowDialog();
                            }
                            else
                            {
                                textBox2.Text = "PT đường thẳng Δ có dạng : " + Environment.NewLine + Math.Round(xn, 5) + "x" ;
                                if (yn > 0)
                                {
                                    textBox2.Text += " + " + Math.Round(yn, 5) + "y";
                                }
                                if (yn < 0)
                                {
                                    textBox2.Text += " - " + -Math.Round(yn, 5) + "y";
                                }
                                if (-xn * xa - yn * ya > 0)
                                {
                                    textBox2.Text += " + " + Math.Round(-xn * xa - yn * ya, 5);
                                }
                                else
                                {
                                    textBox2.Text += " - " + -Math.Round(-xn * xa - yn * ya, 5);

                                }
                                textBox2.Text += " = 0";
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
            textBox2.Clear();
        }
        private void txt5_DoubleClick(object sender, EventArgs e)
        {
            txt5.Text = txtOutput.Text.ToString();

        }
        private void txt6_DoubleClick(object sender, EventArgs e)
        {
            txt6.Text = txtOutput.Text.ToString();

        }
        private void txt7_DoubleClick(object sender, EventArgs e)
        {
            txt7.Text = txtOutput.Text.ToString();

        }
        private void txt8_DoubleClick(object sender, EventArgs e)
        {
            txt8.Text = txtOutput.Text.ToString();

        }
        #endregion

        #region"vectochiphuong
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            if (txt9.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt10.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt11.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập u₁");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt12.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập u₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double xa = Convert.ToDouble(txt9.Text);
                            double ya = Convert.ToDouble(txt10.Text);
                            double xu = Convert.ToDouble(txt11.Text);
                            double yu = Convert.ToDouble(txt12.Text);
                            if (xu == yu || xu == 0)
                            {
                                Thongbao fr = new Thongbao("Lưu ý, Véctơ chỉ phương phải khác véctơ không");
                                fr.ShowDialog();
                            }
                            else
                            {
                                textBox3.Text = "PT đường thẳng Δ có dạng : ";
                                if (xa != 0 && ya != 0)
                                {
                                    textBox3.Text += Environment.NewLine + " Cách 1 : PT chính tắc : (x";
                                    if (xa > 0)
                                    {
                                        textBox3.Text += " - " + Math.Round(xa, 5) + ") / " + Math.Round(xu,5);
                                    }
                                    if (xa < 0)
                                    {
                                        textBox3.Text += " + " + -Math.Round(xa, 5) + ") / " + Math.Round(xu,5);
                                    }
                                    textBox3.Text += " = (y";
                                    if (ya > 0)
                                    {
                                        textBox3.Text += " - " + Math.Round(ya, 5) + ") / " + Math.Round(yu,5);
                                    }
                                    if (ya < 0)
                                    {
                                        textBox3.Text += " + " + -Math.Round(ya, 5) + ") / " + Math.Round(yu,5);

                                    }
                                    
                                    textBox3.Text += Environment.NewLine + " Cách 2 : PT tham số : x = ";
                                    if (xa != 0)
                                    {
                                        textBox3.Text += Math.Round(xa, 5);
                                    }
                                    if (xu > 0)
                                    {
                                        textBox3.Text += " + " + Math.Round(xu, 5) + "t";
                                    }
                                    if (xu < 0)
                                    {
                                        textBox3.Text += " - " + -Math.Round(xu, 5) + "t";
                                    }
                                    textBox3.Text += " và y = ";
                                    if (ya != 0)
                                    {
                                        textBox3.Text += Math.Round(ya, 5);
                                    }
                                    if (yu > 0)
                                    {
                                        textBox3.Text += " + " + Math.Round(yu, 5) + "t";
                                    }
                                    if (yu < 0)
                                    {
                                        textBox3.Text += " - " + -Math.Round(yu, 5) + "t";
                                    }
                                    textBox3.Text += " (t là tham số)";
                                }
                                else
                                {
                                    textBox3.Text += Environment.NewLine + " Cách 1: PT tổng quát : ";
                                    if (-yu != 0)
                                    {
                                        textBox3.Text += Math.Round(-yu, 5) + "x";
                                    }
                                    if (xu > 0)
                                    {
                                        textBox3.Text += " + " + Math.Round(xu,5) + "y";
                                    }
                                    if (xu < 0)
                                    {
                                        textBox3.Text += " - " + -Math.Round(xu, 5) + "y";

                                    }
                                    if (-yu * xa + xu * ya > 0)
                                    {
                                        textBox3.Text += " - " + Math.Round(-yu * xa + xu * ya, 5);
                                    }
                                    if (-yu * xa + xu * ya < 0)
                                    {
                                        textBox3.Text += " + " + -Math.Round(-yu * xa + xu * ya, 5) ;

                                    }
                                    textBox3.Text += " = 0";
                                    textBox3.Text += Environment.NewLine + " Cách 2 : PT tham số : x = ";
                                    if (xa != 0)
                                    {
                                        textBox3.Text += Math.Round(xa, 5);
                                    }
                                    if (xu > 0)
                                    {
                                        textBox3.Text += " + " + Math.Round(xu, 5) + "t";
                                    }
                                    if (xu < 0)
                                    {
                                        textBox3.Text += " - " + -Math.Round(xu, 5) + "t";
                                    }
                                    textBox3.Text += " và y = ";
                                    if (ya != 0)
                                    {
                                        textBox3.Text += Math.Round(ya, 5);
                                    }
                                    if (yu > 0)
                                    {
                                        textBox3.Text += " + " + Math.Round(yu, 5) + "t";
                                    }
                                    if (yu < 0)
                                    {
                                        textBox3.Text += " - " + -Math.Round(yu, 5) + "t";
                                    }
                                    textBox3.Text += " (t là tham số)";
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            txt9.Text = "";
            txt10.Text = "";
            txt11.Text = "";
            txt12.Text = "";
            textBox3.Clear();
        }
        private void txt9_DoubleClick(object sender, EventArgs e)
        {
            txt9.Text = txtOutput.Text.ToString();
        }
        private void txt10_DoubleClick(object sender, EventArgs e)
        {
            txt10.Text = txtOutput.Text.ToString();
        }
        private void txt11_DoubleClick(object sender, EventArgs e)
        {
            txt11.Text = txtOutput.Text.ToString();
        }
        private void txt12_DoubleClick(object sender, EventArgs e)
        {
            txt12.Text = txtOutput.Text.ToString();
        }
        #endregion

        #region"hesok
        private void simpleButton20_Click(object sender, EventArgs e)
        {
            if (txt13.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt14.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt15.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số k");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double xa = Convert.ToDouble(txt13.Text);
                        double ya = Convert.ToDouble(txt14.Text);
                        double k = Convert.ToDouble(txt15.Text);
                        if (k == 0)
                        {
                            textBox4.Text = "PT đường thẳng Δ có dạng : " + Environment.NewLine + "y = " + Math.Round(ya, 5);
                            textBox4.Text += Environment.NewLine + "Góc tạo với chiều (+) trục hoành α1 = 0⁰";
                            textBox4.Text += Environment.NewLine + "Góc tạo với chiều (-) trục hoành α2 = 180⁰ ";
                   
                        }
                        else
                        {
                            textBox4.Text = "PT đường thẳng Δ có dạng : " + Environment.NewLine + "y = " + Math.Round(k, 5) + "x" ;
                            if (-xa == 0 || k == 0)
                            {

                            }
                            else
                            {
                                if (-xa > 0)
                                {
                                    textBox4.Text += " + " + Math.Round(-xa, 5);
                                    if (k > 0)
                                    {
                                        textBox4.Text += "." + Math.Round(k, 5);
                                    }
                                    else
                                    {
                                        textBox4.Text += ". (" + -Math.Round(k, 5) + ")";
                                    }
                                }
                                else
                                {
                                    textBox4.Text += " - (" + -Math.Round(-xa, 5) + ")";
                                    if (k > 0)
                                    {
                                        textBox4.Text += "." + Math.Round(k, 5);
                                    }
                                    else
                                    {
                                        textBox4.Text += ". (" + -Math.Round(k, 5) + ")";
                                    }
                                }
                            }
                            if (ya > 0)
                            {
                                textBox4.Text += " + " + Math.Round(ya,5);
                            }
                            if (ya < 0)
                            {
                                textBox4.Text += " - " + -Math.Round(ya, 5);
                            }
                            textBox4.Text += Environment.NewLine + " <=> y = " + Math.Round(k, 5) + "x";
                            if (-xa*k+ya > 0)
                            {
                                textBox4.Text += " + " + Math.Round(-xa * k + ya, 5);
                            }
                            if (-xa*k+ya < 0)
                            {
                                textBox4.Text += " - " + -Math.Round(-xa * k + ya, 5);
                            }
                            textBox4.Text += Environment.NewLine + "Góc tạo với chiều (+) trục hoành α1 = " + Math.Round(Math.Atan(k) * 180 / Math.PI, 5) + "⁰ ";
                            textBox4.Text += Environment.NewLine + "Góc tạo với chiều (-) trục hoành α2 = " + Math.Round(180 - Math.Atan(k) * 180 / Math.PI, 5) + "⁰ ";
                   
                        }
                      
                    }
                }
            }
        }
        private void simpleButton19_Click(object sender, EventArgs e)
        {
            txt13.Text = "";
            txt14.Text = "";
            txt15.Text = "";
            textBox4.Text = "";
        }
        private void txt13_DoubleClick(object sender, EventArgs e)
        {
            txt13.Text = txtOutput.Text.ToString();
        }
        private void txt14_DoubleClick(object sender, EventArgs e)
        {
            txt14.Text = txtOutput.Text.ToString();
        }
        private void txt15_DoubleClick(object sender, EventArgs e)
        {
            txt15.Text = txtOutput.Text.ToString();
        }
        #endregion

        #region"khoangcach
        private void simpleButton25_Click(object sender, EventArgs e)
        {
            if (txt16.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt17.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt18.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt19.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt20.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xa = Convert.ToDouble(txt16.Text);
                                double ya = Convert.ToDouble(txt17.Text);
                                double a = Convert.ToDouble(txt18.Text);
                                double b = Convert.ToDouble(txt19.Text);
                                double c = Convert.ToDouble(txt20.Text);
                                if (a == 0 && b == 0 )
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    textBox5.Text = "Khoảng cách từ A đến đường thẳng Δ là :" + Environment.NewLine + " d(A, Δ) = " + Math.Round((Math.Abs(a * xa + b * ya + c)) / (Math.Sqrt(a * a + b * b)), 5);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton21_Click(object sender, EventArgs e)
        {
            txt16.Text = "";
            txt17.Text = "";
            txt18.Text = "";
            txt19.Text = "";
            txt20.Text = "";
            textBox5.Clear();
        }
        private void txt16_DoubleClick(object sender, EventArgs e)
        {
            txt16.Text = txtOutput.Text.ToString();

        }
        private void txt17_DoubleClick(object sender, EventArgs e)
        {
            txt17.Text = txtOutput.Text.ToString();

        }
        private void txt18_DoubleClick(object sender, EventArgs e)
        {
            txt18.Text = txtOutput.Text.ToString();

        }
        private void txt19_DoubleClick(object sender, EventArgs e)
        {
            txt19.Text = txtOutput.Text.ToString();

        }
        private void txt20_DoubleClick(object sender, EventArgs e)
        {
            txt20.Text = txtOutput.Text.ToString();

        }
        #endregion

        #region"hinhchieuvadoixung
        private void simpleButton33_Click(object sender, EventArgs e)
        {
            if (txt21.Text == "")
            {

                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                
                fr.ShowDialog();
            }
            else
            {
                if (txt22.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt23.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt24.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt25.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xa = Convert.ToDouble(txt21.Text);
                                double ya = Convert.ToDouble(txt22.Text);
                                double a = Convert.ToDouble(txt23.Text);
                                double b = Convert.ToDouble(txt24.Text);
                                double c = Convert.ToDouble(txt25.Text);
                                double d = a * a + b * b;
                                double dx = -c * a + (b * xa - a * ya) * b;
                                double dy = a * (a * ya - b * xa) - c * b;
                                if (a == 0 && b == 0 )
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (d != 0)
                                    {
                                        textBox6.Text = "Tọa độ hình chiếu vuông góc B của điểm A trên đường thẳng Δ : (" + Math.Round(dx / d, 5) + "; " + Math.Round(dy / d, 5) + ")";
                                        textBox6.Text += Environment.NewLine + "Tọa độ điểm đối xứng với A qua đường thằng Δ là : (" + Math.Round(2 * (dx / d) - xa, 5) + "; " + Math.Round(2 * dy / d - ya, 5) + ")";

                                    }
                                    else
                                    {
                                        if (dx == dy && dy == 0)
                                        {
                                            textBox6.Text = "Tọa độ hình chiếu vuông góc B của điểm A trên đường thẳng Δ : (x; y), (x; y thuộc R)";
                                            textBox6.Text += Environment.NewLine + "Tọa độ hình chiếu vuông góc B của điểm A trên đường thẳng Δ : (x; y), (x; y thuộc R)";

                                        }
                                        else
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, Không tồn tại tọa độ hình chiếu và điểm đối xứng");
                                            fr.ShowDialog();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton32_Click(object sender, EventArgs e)
        {
            txt21.Text = "";
            txt22.Text = "";
            txt23.Text = "";
            txt24.Text = "";
            txt25.Text = "";
            textBox6.Clear();
        }
        private void txt21_DoubleClick(object sender, EventArgs e)
        {
            txt21.Text = txtOutput.Text.ToString();

        }
        private void txt22_DoubleClick(object sender, EventArgs e)
        {
            txt22.Text = txtOutput.Text.ToString();

        }
        private void txt23_DoubleClick(object sender, EventArgs e)
        {
            txt23.Text = txtOutput.Text.ToString();

        }
        private void txt24_DoubleClick(object sender, EventArgs e)
        {
            txt24.Text = txtOutput.Text.ToString();

        }
        private void txt25_DoubleClick(object sender, EventArgs e)
        {
            txt25.Text = txtOutput.Text.ToString();

        }
        #endregion

        #region"goc2dt
        private void simpleButton40_Click(object sender, EventArgs e)
        {
            if (txt26.Text == "")
            {

                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₁");

                fr.ShowDialog();
            }
            else
            {
                if (txt27.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt28.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c₁");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt30.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt31.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₂");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt32.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c₂");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a1 = Convert.ToDouble(txt26.Text);
                                    double b1 = Convert.ToDouble(txt27.Text);
                                    double c1 = Convert.ToDouble(txt28.Text);
                                    double a2 = Convert.ToDouble(txt30.Text);
                                    double b2 = Convert.ToDouble(txt31.Text);
                                    double c2 = Convert.ToDouble(txt32.Text);
                                    double d = a1 * b2 - a2 * b1;
                                    double dx = c1 * b2 - c2 * b1;
                                    double dy = a1 * c2 - a2 * c1;
                                    if (a1 == 0 && b1 == 0 )
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, PT Δ₁ phải tồn tại");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (a2 == 0 && b2 == 0)
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, PT Δ₂ phải tồn tại");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            textBox7.Text = "Góc giữa ĐT Δ₁ và Δ₂ có số đo là : " + Math.Round(Math.Acos(Math.Abs(a1 * a2 + b1 * b2) / Math.Sqrt((a1 * a1 + b1 * b1) * (a2 * a2 + b2 * b2))) * 180 / Math.PI, 5) + "⁰";
                                            if (d!= 0 )
                                            {
                                                txt7.Text += Environment.NewLine + "Giao điểm giữa Δ₁ và Δ2 có tọa độ là : (" + Math.Round(dx / d, 5) + "; " + Math.Round(dy / d, 5) + ")";
                                            }
                                            else
                                            {
                                                if (dx ==0 && dy ==0)
                                                {
                                                    textBox7.Text += Environment.NewLine + "Giao điểm giữa Δ₁ và Δ2 có tọa độ là : (x; y) (x; y thuộc R)";
                                                }
                                                else
                                                {
                                                    Thongbao fr = new Thongbao("Lưu ý, Δ₁ và Δ₂ phải cắt nhau hoặc trùng nhau");
                                                    fr.ShowDialog();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton39_Click(object sender, EventArgs e)
        {
            txt26.Text = "";
            txt27.Text = "";
            txt28.Text = "";
            txt30.Text = "";
            txt31.Text = "";
            txt32.Text = "";
            textBox7.Text = "";
        }
        private void txt26_DoubleClick(object sender, EventArgs e)
        {
            txt26.Text = txtOutput.Text.ToString();

        }
        private void txt27_DoubleClick(object sender, EventArgs e)
        {
            txt27.Text = txtOutput.Text.ToString();

        }
        private void txt28_DoubleClick(object sender, EventArgs e)
        {
            txt28.Text = txtOutput.Text.ToString();

        }
        private void txt30_DoubleClick(object sender, EventArgs e)
        {
            txt30.Text = txtOutput.Text.ToString();

        }
        private void txt31_DoubleClick(object sender, EventArgs e)
        {
            txt31.Text = txtOutput.Text.ToString();

        }
        private void txt32_DoubleClick(object sender, EventArgs e)
        {
            txt32.Text = txtOutput.Text.ToString();

        }
        #endregion

        #region"duongthangtaogoc
        private void simpleButton55_Click(object sender, EventArgs e)
        {
            if (txt33.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                fr.ShowDialog();
            }
            else
            {
                if (txt34.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt35.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt36.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt37.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt38.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập α");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a = Convert.ToDouble(txt33.Text);
                                    double b = Convert.ToDouble(txt34.Text);
                                    double c = Convert.ToDouble(txt35.Text);
                                    double xa = Convert.ToDouble(txt36.Text);
                                    double ya = Convert.ToDouble(txt37.Text);
                                    double alpha = Convert.ToDouble(txt38.Text);
                                    double r = Math.Cos(alpha * Math.PI / 180);
                                    double k = r * r * (a * a + b * b);
                                    double aa = a * a - k;
                                    double bb = 2*a *b;
                                    double cc = b*b-k;
                                    double delta = bb * bb - 4 * aa * cc;
                                    if (a == 0 && b == 0 )
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                      if( a == 0 )
                                      {
                                        if (Math.Sqrt((1-r*r)/r) <= 0)
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            textBox8.Text = "Phương trình ĐT Δ có dạng : " + Environment.NewLine + "A(x";
                                            if (xa > 0)
                                            {
                                                textBox8.Text += " - " + Math.Round(xa, 5);
                                            }
                                            if (xa < 0)
                                            {
                                                textBox8.Text += " + " + -Math.Round(xa, 5);
                                            }
                                            textBox8.Text += ") + B(y";
                                             if (ya > 0)
                                            {
                                                textBox8.Text += " - " + Math.Round(ya, 5);
                                            }
                                            if (ya < 0)
                                            {
                                                textBox8.Text += " + " + -Math.Round(ya, 5);
                                            }
                                            textBox8.Text += ") = 0, trong đó A = " + Math.Round(Math.Sqrt((1 - r * r) / r), 5);
                                            double trunggian = Math.Round(Math.Sqrt((1 - r * r) / r), 5);
                                            textBox8.Text += Environment.NewLine + "Hoặc : x";
                                            if (trunggian > 0)
                                            {
                                                textBox8.Text += " + " + Math.Round(trunggian,5) + "y";
                                            }
                                            if (trunggian < 0)
                                            {
                                                textBox8.Text += " - " + -Math.Round(trunggian,5) + "y";
                                            }
                                             if ((-xa -ya*trunggian) > 0)
                                            {
                                                textBox8.Text += " + " + Math.Round((-xa -ya*trunggian),5) ;
                                            }
                                            if ((-xa -ya*trunggian) < 0)
                                            {
                                                textBox8.Text += " - " + -Math.Round((-xa -ya*trunggian),5);
                                            }
                                            textBox8.Text += " = 0";
                                        }
                                      }
                                      else
                                      {
                                        if (delta <= 0 )
                                        {
                                            textBox8.Text = "Phương trình ĐT Δ không tồn tại";
                                        }
                                        if (delta == 0 )
                                        {
                                            double aa1 = cc;
                                            double bb1 = bb * (-a*b/(a*a-k));
                                            double cc1 = aa * (-a*b/(a*a-k)) * (-a*b/(a*a-k));
                                            double delta1 = bb1 * bb1 - 4 * aa1 * cc1;
                                            if (delta1 < 0 )
                                            {
                                                textBox8.Text = "Phương trình ĐT Δ không tồn tại";

                                            }
                                            if (delta1 == 0 )
                                            {
                                                textBox8.Text = "Phương trình ĐT Δ có dạng : " + Math.Round((-a * b / (a * a - k)), 5) + "x"  ;
                                                if (-bb1/(2*aa1) > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round(-bb1 / (2 * aa1), 5) + "y";
                                                }
                                                if (-bb1 / (2 * aa1) < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round(-bb1 / (2 * aa1), 5) + "y";
                                                }
                                                if ((-a * b / (a * a - k)) * xa + -bb1 / (2 * aa1) * ya > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round((-a * b / (a * a - k)) * xa + -bb1 / (2 * aa1) * ya, 5);
                                                }
                                                if ((-a * b / (a * a - k)) * xa + -bb1 / (2 * aa1) * ya < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round((-a * b / (a * a - k)) * xa + -bb1 / (2 * aa1) * ya, 5);

                                                }
                                                textBox8.Text += " = 0";
                                            }
                                            if (delta1 > 0 )
                                            {
                                                textBox8.Text = "Phương trình ĐT Δ có 2 dạng :";
                                                textBox8.Text += Environment.NewLine + " Δ₁ : " + Math.Round((-a * b / (a * a - k)), 5) + "x";
                                                if ((-bb1 + Math.Sqrt(delta1)) / (2 * aa1) > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round((-bb1 + Math.Sqrt(delta1)) / (2 * aa1), 5) + "y";
                                                }
                                                if (-bb1 / (2 * aa1) < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round((-bb1 + Math.Sqrt(delta1)) / (2 * aa1), 5) + "y";
                                                }
                                                if ((-a * b / (a * a - k)) * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round((-a * b / (a * a - k)) * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya, 5);
                                                }
                                                if ((-a * b / (a * a - k)) * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round((-a * b / (a * a - k)) * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya, 5);

                                                }
                                                textBox8.Text += " = 0";
                                                textBox8.Text += Environment.NewLine + " Δ₂ : " + Math.Round((-a * b / (a * a - k)), 5) + "x"; if ((-bb1 - Math.Sqrt(delta1)) / (2 * aa1) > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round((-bb1 - Math.Sqrt(delta1)) / (2 * aa1), 5) + "y";
                                                }
                                                if (-bb1 / (2 * aa1) < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round((-bb1 - Math.Sqrt(delta1)) / (2 * aa1), 5) + "y";
                                                }
                                                if ((-a * b / (a * a - k)) * xa + (-bb1 - Math.Sqrt(delta1)) / (2 * aa1) * ya > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round((-a * b / (a * a - k)) * xa + (-bb1 - Math.Sqrt(delta1)) / (2 * aa1) * ya, 5);
                                                }
                                                if ((-a * b / (a * a - k)) * xa + (-bb1 - Math.Sqrt(delta1)) / (2 * aa1) * ya < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round((-a * b / (a * a - k)) * xa + (-bb1 - Math.Sqrt(delta1)) / (2 * aa1) * ya, 5);

                                                }
                                                textBox8.Text += " = 0";
                                            }
                                        }
                                        if (delta > 0 )
                                        {
                                            double x1 = (-2 * a * b + Math.Sqrt(delta)) / (2 * (a * a - k));
                                            double x2 = (-2 * a * b - Math.Sqrt(delta)) / (2 * (a * a - k));
                                            textBox8.Text = "Phương trình ĐT Δ có 2 dạng :";
                                            double aa1 = cc;
                                            double bb1 = bb * x1;
                                            double cc1 = aa * x1 * x1;
                                            double delta1 = bb1 * bb1 - 4 * aa1 * cc1;
                                            textBox8.Text += Environment.NewLine + " Δ₁ : ";
                                            if (x1 != 0)
                                            {
                                                textBox8.Text += Math.Round(x1,5) + "x";
                                            }
                                            if ((-bb1 + Math.Sqrt(delta1)) / (2 * aa1) > 0)
                                            {
                                                textBox8.Text += " + " + Math.Round((-bb1 + Math.Sqrt(delta1)) / (2 * aa1), 5) + "y";
                                            }
                                            if ((-bb1 + Math.Sqrt(delta1)) / (2 * aa1) < 0)
                                            {
                                                textBox8.Text += " - " + -Math.Round((-bb1 + Math.Sqrt(delta1)) / (2 * aa1), 5) + "y";
                                            }
                                            if (x1 * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya > 0)
                                            {
                                                textBox8.Text += " + " + Math.Round(x1 * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya,5);

                                            }
                                            else
                                            {
                                                textBox8.Text += " - " + -Math.Round(x1 * xa + (-bb1 + Math.Sqrt(delta1)) / (2 * aa1) * ya,5);

                                            }
                                            textBox8.Text += " = 0";
                                            double aa2 = cc;
                                            double bb2 = bb * x2;
                                            double cc2 = aa * x2 * x2;
                                            double delta2 = bb2 * bb2 - 4 * aa2* cc2;
                                            textBox8.Text += Environment.NewLine + " Δ₂ : ";
                                            if (x2 != 0)
                                            {
                                                textBox8.Text += Math.Round(x2, 5) + "x";
                                            }
                                            if ((-bb2 + Math.Sqrt(delta2)) / (2 * aa2) > 0)
                                            {
                                                textBox8.Text += " + " + Math.Round((-bb2 + Math.Sqrt(delta2)) / (2 * aa2), 5) + "y";
                                            }
                                            if ((-bb2 + Math.Sqrt(delta2)) / (2 * aa2) < 0)
                                            {
                                                textBox8.Text += " - " + -Math.Round((-bb2 + Math.Sqrt(delta2)) / (2 * aa2), 5) + "y";
                                            }
                                            if (x2 * xa + (-bb2 + Math.Sqrt(delta2)) / (2 * aa2) * ya > 0)
                                            {
                                                textBox8.Text += " + " + Math.Round(x2 * xa + (-bb2 + Math.Sqrt(delta2)) / (2 * aa2) * ya, 5);

                                            }
                                            else
                                            {
                                                textBox8.Text += " - " + -Math.Round(x2 * xa + (-bb2 + Math.Sqrt(delta2)) / (2 * aa2) * ya, 5);

                                            }
                                            textBox8.Text += " = 0";
                                        }
                                      }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton54_Click(object sender, EventArgs e)
        {
            txt33.Text = "";
            txt34.Text = "";
            txt35.Text = "";
            txt36.Text = "";
            txt37.Text = "";
            txt38.Text = "";
            textBox8.Text = "";
        }
        private void txt33_DoubleClick(object sender, EventArgs e)
        {
            txt33.Text = txtOutput.Text.ToString();

        }
        private void txt34_DoubleClick(object sender, EventArgs e)
        {
            txt34.Text = txtOutput.Text.ToString();

        }
        private void txt35_DoubleClick(object sender, EventArgs e)
        {
            txt35.Text = txtOutput.Text.ToString();

        }
        private void txt36_DoubleClick(object sender, EventArgs e)
        {
            txt36.Text = txtOutput.Text.ToString();

        }
        private void txt37_DoubleClick(object sender, EventArgs e)
        {
            txt37.Text = txtOutput.Text.ToString();

        }
        private void txt38_DoubleClick(object sender, EventArgs e)
        {
            txt38.Text = txtOutput.Text.ToString();

        }
        #endregion

        private void simpleButton45_Click(object sender, EventArgs e)
        {
            
        }
        //////////////////////////////////////////////////////////////////
        Graph form;
        Color[] colorLevels = { Color.Red,  Color.Green, Color.Blue,  
            Color.Purple, Color.Brown, Color.Orange, Color.Chocolate, 
            Color.Maroon, Color.Navy, Color.YellowGreen };
        string[] strFunctions ={ "abs", "sin", "cos", "tan", "sec", "cosec", "cot", "arcsin", 
            "arccos", "arctan", "exp", "ln", "log", "antilog", "sqrt", "sinh", "cosh", "tanh", 
            "arcsinh", "arccosh", "arctanh" };    
        #region Menu Area
        
        #endregion
        #region Helper Functions
        //this functions handles coloring of expressions
        private void WriteText(string text)
        {
            int colorIndex = 0;
            this.txtExpression.Text = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    colorIndex++;
                    if (colorIndex == colorLevels.Length)
                        colorIndex = 0;
                    txtExpression.SelectionColor = colorLevels[colorIndex];
                }

                this.txtExpression.AppendText(text[i].ToString());

                if (text[i] == ')')
                {
                    colorIndex--;
                    if (colorIndex < 0)
                        colorIndex = colorLevels.Length - 1;
                    txtExpression.SelectionColor = colorLevels[colorIndex];
                }
            }
        }

        private bool IsFunction(string text)
        {
            for (int i = 0; i < strFunctions.Length; i++)
                if (string.Compare(text, strFunctions[i], true) == 0)
                    return true;
            return false;
        }

        private void AddExpression()
        {
            if (this.txtExpression.Text.Length == 0)
                return;
            this.txtExpression.Text = CompleteParenthesis(this.txtExpression.Text);
            string expText = this.txtExpression.Text;
            IEvaluatable exp = new Expression(expText);
            if (!exp.IsValid)
            {
                if (MessageBox.Show("Biểu thức bạn định thêm vào danh sách không hợp lệ! Bạn vẫn muốn thêm nó vào danh sách?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            this.lstExpressions.Items.Add(expText);
            this.txtExpression.Text = string.Empty;
        }
        private string CompleteParenthesis(string exp)
        {
            int leftBracket = 0;
            int rightBracket = 0;
            for (int i = 0; i < exp.Length; i++)
            {
                if (exp[i] == '(')
                    leftBracket++;
                else if (exp[i] == ')')
                    rightBracket++;
            }
            exp = exp.PadRight(exp.Length + leftBracket - rightBracket, ')');
            return exp;
        }

        #endregion
        #region"control
        private bool CheckDuplication()
        {
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                if (txtExpression.Text == lstExpressions.Items[i].ToString())
                {
                    MessageBox.Show("Biểu thức đã cho trùng với biểu thức có trong danh sách");
                    return false;
                }
            }

            AddExpression();
            this.lstExpressions.SelectedIndex = -1;
            this.lstExpressions.Refresh();
            return true;
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = this.txtExpression.SelectionStart;
            WriteText(this.txtExpression.Text);
            this.txtExpression.SelectionStart = cursorPosition;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = this.lstExpressions.SelectedIndex;
            this.lstExpressions.Items.Remove(this.lstExpressions.SelectedItem);
            if (index == this.lstExpressions.Items.Count)
                index--;
            if (index != -1)
                this.lstExpressions.SelectedIndex = index;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lstExpressions.Items.Clear();

        }

        private void mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mode.SelectedIndex == 1)
                this.sensitivity.Enabled = true;
            else
                this.sensitivity.Enabled = false;
        }

        private void txtExpression_KeyPress(object sender, KeyPressEventArgs e)
        {
               int cursorPos = this.txtExpression.SelectionStart;
                if (cursorPos > 0)
                {
                    //if the previous char is a digit, add a *
                    if (char.IsDigit(this.txtExpression.Text[cursorPos - 1]))
                    {
                        this.txtExpression.Text = this.txtExpression.Text.Insert(cursorPos, "*" + e.KeyChar);
                        cursorPos += 2;
                        this.txtExpression.SelectionStart = cursorPos;
                        e.Handled = true;
                    }
                    //if a function is formed, add a "("
                    else
                    {
                        string text = string.Empty;
                        int i = cursorPos - 1;
                        while (i >= 0)
                        {
                            if (!char.IsLetter(this.txtExpression.Text[i]))
                                break;
                            i--;
                        }
                        i++;
                        //now i is the index where last text is started
                        if (i < cursorPos)
                            text = this.txtExpression.Text.Substring(i, cursorPos - i) + e.KeyChar;
                        if (IsFunction(text))
                        {
                            this.txtExpression.Text = this.txtExpression.Text.Insert(cursorPos, e.KeyChar.ToString() + "(");
                            cursorPos += 2;
                            this.txtExpression.SelectionStart = cursorPos;
                            e.Handled = true;
                        }
                    }
                }
            }

        private void lstExpressions_SelectedIndexChanged(object sender, EventArgs e)
        {
         if (this.lstExpressions.SelectedIndex != -1)
                this.txtExpression.Text = this.lstExpressions.Items[this.lstExpressions.SelectedIndex].ToString();
        }
        #endregion

        private void ve1_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt1.Text);
            double ya = Convert.ToDouble(txt2.Text);
            double xb = Convert.ToDouble(txt3.Text);
            double yb = Convert.ToDouble(txt4.Text);
            if (xa == xb)
            {
                if (ya == yb)
                {
                    Thongbao fr = new Thongbao("Lưu ý, 2 điểm phải không trùng nhau");
                    fr.ShowDialog();
                }
                else
                {
                    if (xa > 0)
                    {
                        txtExpression.Text = "x-" + Math.Round(xa, 5);
                    }
                    else
                    {
                        txtExpression.Text = "x+" + -Math.Round(xa, 5);
                    }
                   
                }
            }
            else
            {
                if (ya == yb)
                {
                    txtExpression.Text =  Math.Round(ya, 5).ToString();
                }
                else
                {
                    if (Math.Round((ya * (xa - xb) + xa * (yb - ya)) / (xa - xb), 5) > 0)
                    {
                        txtExpression.Text = -Math.Round((yb - ya) / (xa - xb), 5) + "*x+" + Math.Round((ya * (xa - xb) + xa * (yb - ya)) / (xa - xb), 5);

                    }
                    else
                    {
                        txtExpression.Text = -Math.Round((yb - ya) / (xa - xb), 5) + "*x-" + - Math.Round((ya * (xa - xb) + xa * (yb - ya)) / (xa - xb), 5);

                    }
                }
            }


            this.CheckDuplication();    
            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }

        private void ve2_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt5.Text);
            double ya = Convert.ToDouble(txt6.Text);
            double xn = Convert.ToDouble(txt7.Text);
            double yn = Convert.ToDouble(txt8.Text);
            double trunggian = Math.Round(-xn * xa - yn * ya, 5);
            if (xn ==0)
            {
                if (yn == 0)
                {
                    Thongbao fr = new Thongbao("Lưu ý, Véctơ pháp tuyến phải khác véctơ không");
                    fr.ShowDialog();
                }
                else
                {
                    txtExpression.Text = Math.Round(trunggian / yn, 5).ToString();
                }
            }
            else
            {
                if (yn ==0)
                {
                    // ?
                }
                else
                {
                    if ((xn * xa + yn * ya) / yn>0)
                    {
                        txtExpression.Text = Math.Round(-xn / yn, 5) + "*x+" + Math.Round((xn * xa + yn * ya) / yn, 5);

                    }
                    else
                    {
                        txtExpression.Text = Math.Round(-xn / yn, 5) + "*x-" + -Math.Round((xn * xa + yn * ya) / yn, 5);

                    }

                }
            }
            this.CheckDuplication();
     
            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }

        private void ve3_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt9.Text);
            double ya = Convert.ToDouble(txt10.Text);
            double xu = Convert.ToDouble(txt11.Text);
            double yu = Convert.ToDouble(txt12.Text);
            double xn = yu;
            double yn = -xu;
            double trunggian = Math.Round(-xn * xa - yn * ya, 5);
            if (xu == 0)
            {
                if (yu == 0)
                {
                    Thongbao fr = new Thongbao("Lưu ý, Véctơ chỉ phương phải khác véctơ không");
                    fr.ShowDialog();
                }
                else
                {
                    txtExpression.Text = Math.Round(trunggian / yn, 5).ToString();
                }
            }
            else
            {
                if (yn == 0)
                {
                    // ?
                }
                else
                {
                    if ((xn * xa + yn * ya) / yn > 0)
                    {
                    txtExpression.Text = Math.Round(-xn / yn, 5) + "*x+" + Math.Round((xn * xa + yn * ya) / yn, 5);

                    }
                    else
                    {
                        txtExpression.Text = Math.Round(-xn / yn, 5) + "*x-" + -Math.Round((xn * xa + yn * ya) / yn, 5);

                    }
                }
            }
            this.CheckDuplication();

            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }

        private void ve4_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt13.Text);
            double ya = Convert.ToDouble(txt14.Text);
            double k = Convert.ToDouble(txt15.Text);
            if (-xa * k + ya > 0)
            {
                txtExpression.Text = Math.Round(k, 5) + "*x+" + Math.Round(-xa * k + ya, 5);

            }
            else
            {
                txtExpression.Text = Math.Round(k, 5) + "*x-" + -Math.Round(-xa * k + ya, 5);

            }
            this.CheckDuplication();

            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }

        private void ve5_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt16.Text);
            double ya = Convert.ToDouble(txt17.Text);
            double a = Convert.ToDouble(txt18.Text);
            double b = Convert.ToDouble(txt19.Text);
            double c = Convert.ToDouble(txt20.Text);
            if (a == 0 && b == 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                fr.ShowDialog();
            }
            else
            {
                if (b == 0)
                {

                }
                else
                {
                    if (-c/a > 0)
                    {
                        txtExpression.Text = Math.Round(-b / a, 5) + "*x+" + Math.Round(-c / a, 5);

                    }
                    else
                    {
                        txtExpression.Text = Math.Round(-b / a, 5) + "*x-" + -Math.Round(-c / a, 5);

                    }
                }
            }
            this.CheckDuplication();

            if (a == 0 && b == 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                fr.ShowDialog();
            }
            else
            {
                double A = Convert.ToDouble(txt19.Text);
                double B = -Convert.ToDouble(txt18.Text);
                double C = a*ya-b*xa;
                if (B == 0)
                {

                }
                else
                {
                    if (-C / A > 0)
                    {
                        txtExpression.Text = Math.Round(-B / A, 5) + "*x-" + Math.Round(-C / A, 5);

                    }
                    else
                    {
                        txtExpression.Text = Math.Round(-B / A, 5) + "*x+" + -Math.Round(-C / A, 5);

                    }
                  

                }
            }
            this.CheckDuplication();
            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }

        private void ve6_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt21.Text);
            double ya = Convert.ToDouble(txt22.Text);
            double a = Convert.ToDouble(txt23.Text);
            double b = Convert.ToDouble(txt24.Text);
            double c = Convert.ToDouble(txt25.Text);
            if (a == 0 && b == 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                fr.ShowDialog();
            }
            else
            {
                if (b == 0)
                {

                }
                else
                {
                    if (-c / a > 0)
                    {
                        txtExpression.Text = Math.Round(-b / a, 5) + "*x+" + Math.Round(-c / a, 5);

                    }
                    else
                    {
                        txtExpression.Text = Math.Round(-b / a, 5) + "*x-" + -Math.Round(-c / a, 5);

                    }
                }
            }
            this.CheckDuplication();

            if (a == 0 && b == 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                fr.ShowDialog();
            }
            else
            {
                double A = Convert.ToDouble(txt24.Text);
                double B = -Convert.ToDouble(txt23.Text);
                double C = a * ya - b * xa;
                if (B == 0)
                {

                }
                else
                {
                    if (-C / A > 0)
                    {
                        txtExpression.Text = Math.Round(-B / A, 5) + "*x-" + Math.Round(-C / A, 5);

                    }
                    else
                    {
                        txtExpression.Text = Math.Round(-B / A, 5) + "*x+" + -Math.Round(-C / A, 5);

                    }


                }
            }
            this.CheckDuplication();
            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }

        private void ve7_Click(object sender, EventArgs e)
        {
            double a1 = Convert.ToDouble(txt26.Text);
            double b1 = Convert.ToDouble(txt27.Text);
            double c1 = Convert.ToDouble(txt28.Text);
            double a2 = Convert.ToDouble(txt30.Text);
            double b2 = Convert.ToDouble(txt31.Text);
            double c2 = Convert.ToDouble(txt32.Text);
            if (b1 != 0)
            {
                if (-c1/b1 > 0)
                {
                    txtExpression.Text = Math.Round(-a1 / b1, 5) + "*x+" + Math.Round(-c1 / b1, 5);

                }
                else
                {
                    txtExpression.Text = Math.Round(-a1 / b1, 5) + "*x-" + -Math.Round(-c1 / b1, 5);

                }
            }
            this.CheckDuplication();
            if (b2 != 0)
            {
                if (-c2/b2 > 0)
                {
                    txtExpression.Text = Math.Round(-a2 / b2, 5) + "*x+" + Math.Round(-c2 / b2, 5);

                }
                else
                {
                    txtExpression.Text = Math.Round(-a2 / b2, 5) + "*x-" + -Math.Round(-c2 / b2, 5);

                }
            }
            
            this.CheckDuplication();
            if (mode.Text == "Cartesian")
            {
                ExpressionHelper.Cartesian = true;
                ExpressionHelper.Polar = false;
            }
            else
            {
                ExpressionHelper.Polar = true;
                ExpressionHelper.Cartesian = false;
            }

            ExpressionHelper.XStartValue = Convert.ToDouble(startX.Value);
            ExpressionHelper.XEndValue = Convert.ToDouble(endX.Value);

            if (form == null || form.IsDisposed)
            {
                form = new Graph();
                form.Show();
            }

            form.SetRange((Double)startX.Value, (Double)endX.Value, (Double)startY.Value, (Double)endY.Value);
            form.SetDivisions((int)this.divX.Value, (int)this.divY.Value);
            form.SetPenWidth((int)this.penWidth.Value);

            if (this.mode.SelectedItem.ToString() == "Polar")
                form.SetMode(GraphMode.Polar, (int)this.sensitivity.Value);
            else
                form.SetMode(GraphMode.Rectangular, 50);

            //form.RemoveAllExpressions();
            ExpressionHelper.ArrExpression.Clear();
            for (int i = 0; i < lstExpressions.Items.Count; i++)
            {
                form.AddExpression((string)lstExpressions.Items[i], colorLevels[i % colorLevels.Length]);
                //Add expression to Array Expression (ThemBotBieuThuc form)
                ExpressionHelper.ArrExpression.Add(lstExpressions.Items[i]);
            }

            form.Refresh();
            form.Activate();
            this.lstExpressions.Items.Clear();
        }
        private void simpleButton48_Click(object sender, EventArgs e)
        {
            txt39.Text = "";
            txt40.Text = ""; 
            txt41.Text = "";
            txt42.Text = "";
            txt43.Text = "";
            txt44.Text = "";
            txt45.Text = "";

            txt46.Text = "";
            txt47.Text = "";
            txt48.Text = "";

            txt49.Text = "";
            txt50.Text = "";
            textBox9.Text = "";
        }
        private void txt39_DoubleClick(object sender, EventArgs e)
        {
            txt39.Text = txtOutput.Text.ToString();
        }
        private void txt41_DoubleClick(object sender, EventArgs e)
        {
            txt41.Text = txtOutput.Text.ToString();

        }
        private void txt43_DoubleClick(object sender, EventArgs e)
        {
            txt43.Text = txtOutput.Text.ToString();

        }
        private void txt40_DoubleClick(object sender, EventArgs e)
        {
            txt40.Text = txtOutput.Text.ToString();

        }
        private void txt42_DoubleClick(object sender, EventArgs e)
        {
            txt42.Text = txtOutput.Text.ToString();

        }
        private void txt44_DoubleClick(object sender, EventArgs e)
        {
            txt44.Text = txtOutput.Text.ToString();

        }
        private void txt45_DoubleClick(object sender, EventArgs e)
        {
            txt45.Text = txtOutput.Text.ToString();

        }
        private void txt47_DoubleClick(object sender, EventArgs e)
        {
            txt47.Text = txtOutput.Text.ToString();

        }
        private void txt49_DoubleClick(object sender, EventArgs e)
        {
            txt49.Text = txtOutput.Text.ToString();

        }
        private void txt46_DoubleClick(object sender, EventArgs e)
        {
            txt46.Text = txtOutput.Text.ToString();

        }
        private void txt48_DoubleClick(object sender, EventArgs e)
        {
            txt48.Text = txtOutput.Text.ToString();

        }
        private void txt50_DoubleClick(object sender, EventArgs e)
        {
            txt50.Text = txtOutput.Text.ToString();

        }
        private void simpleButton49_Click(object sender, EventArgs e)
        {
            if (txt39.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₁");
                fr.ShowDialog();
            }
            else
            {
                if (txt40.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt41.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₂");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt42.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt43.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₃");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt44.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₃");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (txt45.Text == "")
                                    {
                                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₄");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (txt46.Text == "")
                                        {
                                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₄");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if (txt47.Text == "")
                                            {
                                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₅");
                                                fr.ShowDialog();
                                            }
                                            else
                                            {
                                                if (txt48.Text == "")
                                                {
                                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₅");
                                                    fr.ShowDialog();
                                                }
                                                else
                                                {
                                                    if (txt39.Text == "")
                                                    {
                                                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₆");
                                                        fr.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        if (txt39.Text == "")
                                                        {
                                                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₆");
                                                            fr.ShowDialog();
                                                        }
                                                        else
                                                        {
                                                            double a1 = Convert.ToDouble(txt39.Text);
                                                            double b1 = Convert.ToDouble(txt40.Text);
                                                            double a2 = Convert.ToDouble(txt41.Text);
                                                            double b2 = Convert.ToDouble(txt42.Text);
                                                            double a3 = Convert.ToDouble(txt43.Text);
                                                            double b3 = Convert.ToDouble(txt44.Text);
                                                            double a4 = Convert.ToDouble(txt45.Text);
                                                            double b4 = Convert.ToDouble(txt46.Text);
                                                            double a5 = Convert.ToDouble(txt47.Text);
                                                            double b5 = Convert.ToDouble(txt48.Text);
                                                            double a6 = Convert.ToDouble(txt49.Text);
                                                            double b6 = Convert.ToDouble(txt50.Text);
                                                            //d
                                                            double aa1 = a1 * a5 - a2 * a4;
                                                            double bb1 = a1 * b5 + a5 * b1 - a2 * b4 - a4 * b2;
                                                            double cc1 = b1 * b1 - b2 * b4;
                                                            //dx
                                                            double aa2 = a3 * a5 - a2 * a6;
                                                            double bb2 = a5 * b3 + a3 * b5 - a2 * b6 - a6 * b2;
                                                            double cc2 = b3 * b5 - b2 * b6;
                                                            // dy
                                                            double aa3 = a1 * a6 - a2 * a6;
                                                            double bb3 = a5 * b3 + a3 * b5 - a2 * b6 - a6 * b2;
                                                            double cc3 = b3 * b5 - b2 * b6;
                                                            // delta từng phân thức
                                                            double delta1 = bb1 * bb1 - 4 * aa1 * cc1;
                                                            double delta2 = bb2 * bb2 - 4 * aa2 * cc2;
                                                            double delta3 = bb3 * bb3 - 4 * aa3 * cc3;
                                                            if ((a1 == 0 && b1== 0) || ( a2== 0 && b2 ==0))
                                                            {
                                                                Thongbao fr = new Thongbao("Lưu ý, ĐT Δ₁ phải tồn tạii");
                                                                fr.ShowDialog();
                                                            }
                                                            else
                                                            {
                                                                if ((a4 == 0 && b4 == 0) || (a5 == 0 && b5 == 0))
                                                                {
                                                                    Thongbao fr = new Thongbao("Lưu ý, ĐT Δ₂ phải tồn tạii");
                                                                    fr.ShowDialog();
                                                                }
                                                                else
                                                                {
                                                                    textBox9.Text = "TH1 : Xét Δ₁ cắt Δ₂";
                                                                    if (aa1 == 0)
                                                                    {
                                                                        if (bb1 == 0)
                                                                        {
                                                                            if (cc1 == 0)
                                                                            {
                                                                                textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                            }
                                                                            else
                                                                            {
                                                                                textBox9.Text += Environment.NewLine + " - Xảy ra với mọi m";

                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m ≠ " + Math.Round(-cc1 / bb1, 5);

                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (delta1 > 0)
                                                                        {
                                                                            double x11 = (-bb1 + Math.Sqrt(delta1)) / (2 * aa1);
                                                                            double x12 = (-bb1 - Math.Sqrt(delta1)) / (2 * aa1);
                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m ≠ " + Math.Round(x11, 5) + " và m ≠ " + Math.Round(x12, 5);

                                                                        }
                                                                        if (delta1 == 0)
                                                                        {
                                                                            double x11 = (-bb1) / (2 * aa1);
                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m ≠ " + Math.Round(x11, 5);

                                                                        }
                                                                        if ( delta1 < 0)
                                                                        {
                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra với mọi m";

                                                                        }
                                                                    }
                                                                    textBox9.Text += Environment.NewLine + "TH2 : Xét Δ₁ // Δ₂";
                                                                    if (aa1 == 0)
                                                                    {
                                                                        if (bb1 == 0)
                                                                        {
                                                                            if (cc1 == 0)
                                                                            {
                                                                                if (aa2 == 0)
                                                                                {
                                                                                    if (bb2 == 0)
                                                                                    {
                                                                                        if (cc2 == 0)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra với mọi m";

                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi với m ≠ " + Math.Round(-cc2/bb2,5);

                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (delta2 > 0)
                                                                                    {
                                                                                        double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                        double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m ≠ " + Math.Round(x21, 5) + " và m ≠ " + Math.Round(x22, 5);

                                                                                    }
                                                                                    if (delta2 == 0)
                                                                                    {
                                                                                        double x21 = (-bb2) / (2 * aa2);
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m ≠ " + Math.Round(x21, 5);

                                                                                    }
                                                                                    if (delta2 < 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra với mọi m";

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (aa2 == 0)
                                                                            {
                                                                                if (bb2 == 0)
                                                                                {
                                                                                    if (cc2 == 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1/bb1,5);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (-cc1/bb1 != -cc2/bb2)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (delta2 > 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    if (-cc1/bb1 != x21 || -cc1/bb1 != x22)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                   
                                                                                }
                                                                                if (delta2 == 0)
                                                                                {
                                                                                    double x21 = (-bb2) / (2 * aa2);
                                                                                    if (-cc1 / bb1 != x21)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                }
                                                                                if (delta2 < 0)
                                                                                {
                                                                                    textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (delta1 > 0)
                                                                        {
                                                                            double x11 = (-bb1 + Math.Sqrt(delta1)) / (2 * aa1);
                                                                            double x12 = (-bb1 - Math.Sqrt(delta1)) / (2 * aa1);
                                                                            if (aa2 == 0)
                                                                            {
                                                                                if (bb2 == 0)
                                                                                {
                                                                                    if (cc2 == 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12,5);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (x11 != -cc2 / bb2)
                                                                                    {
                                                                                      if (x12 != -cc2/bb2)
                                                                                      {
                                                                                          textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12, 5);

                                                                                      }
                                                                                      else
                                                                                      {
                                                                                          textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                      }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (x12 != -cc2 / bb2)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                        }
                                                                                        
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (delta2 > 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                   if (x11 != x21)
                                                                                   {
                                                                                       if (x11 != x22)
                                                                                       {
                                                                                           if (x12 != x21)
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12, 5);
                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                           }
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           if (x12!= x21)
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                           }
                                                                                       }
                                                                                   }
                                                                                   else
                                                                                   {
                                                                                       if (x12 != x21)
                                                                                       {
                                                                                           if (x12!= x22)
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                           }
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                       }     
                                                                                     
                                                                                   }

                                                                                }
                                                                                if (delta2 == 0)
                                                                                {
                                                                                    double x21 = (-bb2) / (2 * aa2);
                                                                                    if (x11 != x21)
                                                                                    {
                                                                                        if (x12 != x21)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12, 5);

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (x12 != x21)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                        }

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        if (delta1 == 0)
                                                                        {
                                                                            double x11 = (-bb1) / (2 * aa1);
                                                                            if (aa2 == 0)
                                                                            {
                                                                                if (bb2 == 0)
                                                                                {
                                                                                    if (cc2 == 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (x11 != -cc2/bb2)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (delta2 > 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    if (x11 != x21)
                                                                                    {
                                                                                        if (x11!= x22)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                    }
                                                                                }
                                                                                if (delta2 == 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    if (x11 != x21)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                }
                                                                                if (delta2 < 0)
                                                                                {
                                                                                    textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                }
                                                                            }
                                                                        }
                                                                        if (delta1 < 0)
                                                                        {
                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                        }
                                                                    }
                                                                    textBox9.Text += Environment.NewLine + "TH3 : Xét Δ₁ ≡ Δ₂";
                                                                    if (aa1 == 0)
                                                                    {
                                                                        if (bb1 == 0)
                                                                        {
                                                                            if (cc1 == 0)
                                                                            {
                                                                                if (aa2 == 0)
                                                                                {
                                                                                    if (bb2 == 0)
                                                                                    {
                                                                                        if (cc2 == 0)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra với mọi m";

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi với m = " + Math.Round(-cc2/bb2,5);

                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (delta2 > 0)
                                                                                    {
                                                                                        double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                        double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x21, 5) + " và m = " + Math.Round(x22, 5);

                                                                                    }
                                                                                    if (delta2 == 0)
                                                                                    {
                                                                                        double x21 = (-bb2) / (2 * aa2);
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x21, 5);

                                                                                    }
                                                                                    if (delta2 < 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (aa2 == 0)
                                                                            {
                                                                                if (bb2 == 0)
                                                                                {
                                                                                    if (cc2 == 0)
                                                                                    {
                                                                                         textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                       
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (-cc1 / bb1 == -cc2 / bb2)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (delta2 > 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    if (-cc1/bb1 == x21 || -cc1/bb1 == x22)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                   
                                                                                }
                                                                                if (delta2 == 0)
                                                                                {
                                                                                    double x21 = (-bb2) / (2 * aa2);
                                                                                    if (-cc1 / bb1 == x21)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc1 / bb1, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                }
                                                                                if (delta2 < 0)
                                                                                {
                                                                                    textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (delta1 > 0)
                                                                        {
                                                                            double x11 = (-bb1 + Math.Sqrt(delta1)) / (2 * aa1);
                                                                            double x12 = (-bb1 - Math.Sqrt(delta1)) / (2 * aa1);
                                                                            if (aa2 == 0)
                                                                            {
                                                                                if (bb2 == 0)
                                                                                {
                                                                                    if (cc2 == 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12,5);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (x11 == -cc2 / bb2)
                                                                                    {
                                                                                      if (x12 == -cc2/bb2)
                                                                                      {
                                                                                          textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12, 5);

                                                                                      }
                                                                                      else
                                                                                      {
                                                                                          textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                      }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (x12 == -cc2 / bb2)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                        }
                                                                                        
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (delta2 > 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                   if (x11 == x21)
                                                                                   {
                                                                                       if (x11 == x22)
                                                                                       {
                                                                                           if (x12 == x21)
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12, 5);
                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                           }
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           if (x12 == x21)
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                           }
                                                                                       }
                                                                                   }
                                                                                   else
                                                                                   {
                                                                                       if (x12 == x21)
                                                                                       {
                                                                                           if (x12== x22)
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                           }
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                       }     
                                                                                     
                                                                                   }

                                                                                }
                                                                                if (delta2 == 0)
                                                                                {
                                                                                    double x21 = (-bb2) / (2 * aa2);
                                                                                    if (x11 == x21)
                                                                                    {
                                                                                        if (x12 == x21)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5) + " hoặc m = " + Math.Round(x12, 5);

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (x12 == x21)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x12, 5);

                                                                                        }

                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        if (delta1 == 0)
                                                                        {
                                                                            double x11 = (-bb1) / (2 * aa1);
                                                                            if (aa2 == 0)
                                                                            {
                                                                                if (bb2 == 0)
                                                                                {
                                                                                    if (cc2 == 0)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11,5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (x11 == -cc2/bb2)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (delta2 > 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    double x22 = (-bb2 - Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    if (x11 == x21)
                                                                                    {
                                                                                        if (x11 == x22)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (x11 == x22)
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                                        }
                                                                                    }
                                                                                }
                                                                                if (delta2 == 0)
                                                                                {
                                                                                    double x21 = (-bb2 + Math.Sqrt(delta2)) / (2 * aa2);
                                                                                    if (x11 == x21)
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x11, 5);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                    }
                                                                                }
                                                                                if (delta2 < 0)
                                                                                {
                                                                                    textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                                }
                                                                            }
                                                                        }
                                                                        if (delta1 < 0)
                                                                        {
                                                                            textBox9.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage9.Show();
        }

        private void txt13_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelItem10_Click(object sender, EventArgs e)
        {

        }

    }
}