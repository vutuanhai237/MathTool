using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Collections.Generic;
namespace Math_V1._1
{
    public partial class Phepbienhinh : DevComponents.DotNetBar.Metro.MetroForm
    {
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
        public Phepbienhinh()
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
        private void Duongtron_Load(object sender, EventArgs e)
        {
            Loading fr1 = new Loading();
            fr1.ShowDialog();
            txtOutput.Text = "0";
            lblTime.Caption = "";
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            lblTime.Caption = (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;

        }
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage3.Show();
        }
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage4.Show();
        }
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage5.Show();
        }
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage7.Show();
        }
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage8.Show();
        }
        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage9.Show();
        }
        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage10.Show();
        }
        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage11.Show();
        }
        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage12.Show();
        }
        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage13.Show();
        }

        // tinh tien diem
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            if (txt1.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt2.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt3.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập i");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt4.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập j");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double xa = Convert.ToDouble(txt1.Text);
                            double ya = Convert.ToDouble(txt2.Text);
                            double xu = Convert.ToDouble(txt3.Text);
                            double yu = Convert.ToDouble(txt4.Text);
                            if (xu == 0 && yu == 0)
                            {
                                textBox1.Text = "Ảnh của A là A' có tọa độ : " + Environment.NewLine + " (" + Math.Round(xa,5) + "; " + Math.Round(ya,5) + ") ( Phép đồng nhất);";
                            }
                            else
                            {
                                textBox1.Text = "Ảnh của A là A' có tọa độ : "  + Environment.NewLine + "(" + Math.Round(xa + xu,5) + "; " + Math.Round(ya + yu,5) + ")";

                            }
                        }
                    }
                }
            }
        }
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
                textBox1.Text = "";
        }
        private void txt1_DoubleClick(object sender, EventArgs e)
        {
            txt1.Text = txtOutput.Text.ToString();

        }
        private void txt2_DoubleClick(object sender, EventArgs e)
        {
            txt2.Text = txtOutput.Text.ToString();


        }
        private void txt3_DoubleClick(object sender, EventArgs e)
        {
            txt3.Text = txtOutput.Text.ToString();

        }
        private void txt4_DoubleClick(object sender, EventArgs e)
        {
            txt4.Text = txtOutput.Text.ToString();

        }
        // tinh tien duong thang
        private void simpleButton20_Click(object sender, EventArgs e)
        {
            if (txt5.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                fr.ShowDialog();
            }
            else
            {
                if (txt6.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt7.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt8.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập i");
                            fr.ShowDialog();
                        }
                        else
                        {
                             if (txt9.Text == "")
                             {
                                 Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập j");
                                  fr.ShowDialog();
                             }
                             else
                             {
                                 double a = Convert.ToDouble(txt5.Text);
                                 double b = Convert.ToDouble(txt6.Text);
                                 double c = Convert.ToDouble(txt7.Text);
                                 double xu = Convert.ToDouble(txt8.Text);
                                 double yu = Convert.ToDouble(txt9.Text);
                                 if (a == 0 && b == 0 )
                                 {
                                     Thongbao fr = new Thongbao("Lưu ý, PT ĐT Δ phải tồn tại");
                                     fr.ShowDialog();
                                 }
                                 else 
                                 {
                                     if (xu == 0 && yu == 0)
                                     {
                                         textBox2.Text = "Ảnh của Δ là Δ' có dạng : " + Environment.NewLine + "";
                                         if (a != 0)
                                         {
                                             textBox2.Text += Math.Round(a,5) + "x";
                                         }
                                         if (b > 0)
                                         {
                                             textBox2.Text += " + " + Math.Round(b,5) + "y" ;
                                         }
                                         if (b < 0)
                                         {
                                             textBox2.Text += " - " + -Math.Round(b, 5) + "y";
                                         }
                                         if (c > 0)
                                         {
                                             textBox2.Text += " + " + Math.Round(c, 5);
                                         }
                                         if (c < 0)
                                         {
                                             textBox2.Text += " - " + -Math.Round(c, 5);
                                         }
                                         textBox2.Text += " = 0 (Phép đồng nhất)";
                                     }
                                     else
                                     {
                                         textBox2.Text = "Ảnh của Δ là Δ' có dạng : " + Environment.NewLine + "";
                                         if (a != 0)
                                         {
                                             textBox2.Text += Math.Round(a, 5) + "x";
                                         }
                                         if (b > 0)
                                         {
                                             textBox2.Text += " + " + Math.Round(b, 5) + "y";
                                         }
                                         if (b < 0)
                                         {
                                             textBox2.Text += " - " + -Math.Round(b, 5) + "y";
                                         }
                                         if (c - a * xu - b * yu > 0)
                                         {
                                             textBox2.Text += " + " + Math.Round(c - a * xu - b * yu, 5);
                                         }
                                         if (c - a * xu - b * yu < 0)
                                         {
                                             textBox2.Text += " - " + -Math.Round(c - a * xu - b * yu, 5);
                                         }
                                         textBox2.Text += " = 0";
                                     }
                                 }
                               
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton19_Click(object sender, EventArgs e)
        {
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
            txt9.Text = "";
            textBox2.Text = "";
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
        private void txt9_DoubleClick(object sender, EventArgs e)
        {
            txt9.Text = txtOutput.Text.ToString();

        }
        // tinh tien duong tron
        private void simpleButton33_Click(object sender, EventArgs e)
        {
            if (txt10.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                fr.ShowDialog();
            }
            else
            {
                if (txt11.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt12.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt13.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập i");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt14.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập j");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xi = Convert.ToDouble(txt10.Text);
                                double yi = Convert.ToDouble(txt11.Text);
                                double r = Convert.ToDouble(txt12.Text);
                                double xu = Convert.ToDouble(txt13.Text);
                                double yu = Convert.ToDouble(txt14.Text);
                                if (r < 0)
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, R > 0");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (xu == 0 && yu == 0)
                                    {
                                        textBox6.Text = "Ảnh của (C) là (C)' có tọa độ tâm : " + Environment.NewLine + "(" + Math.Round(xi, 5) + "; " + Math.Round(yi, 5) + ") và bán kính R = " + Math.Round(r, 5) + " (Phép đồng nhất)";
                                    }
                                    else
                                    {
                                        textBox6.Text = "Ảnh của (C) là (C)' có tọa độ tâm : " + Environment.NewLine + "(" + Math.Round(xi + xu, 5) + "; " + Math.Round(yi + yu, 5) + ") và bán kính R = " + Math.Round(r, 5);
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
            txt10.Text = "";
            txt11.Text = "";
            txt12.Text = "";
            txt13.Text = "";
            txt14.Text = "";
            textBox6.Text = "";
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
        private void txt13_DoubleClick(object sender, EventArgs e)
        {
            txt13.Text = txtOutput.Text.ToString();
        }
        private void txt14_DoubleClick(object sender, EventArgs e)
        {
            txt14.Text = txtOutput.Text.ToString();
        }
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage6.Show();
        }
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage1.Show();
        }
        // doi xung tam 1 diem
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (txt15.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt16.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt17.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt18.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double xa = Convert.ToDouble(txt15.Text);
                            double ya = Convert.ToDouble(txt16.Text);
                            double xo = Convert.ToDouble(txt17.Text);
                            double yo = Convert.ToDouble(txt18.Text);
                            if ( xa == xo && ya == yo)
                            {
                                textBox4.Text = "Ảnh của A là A' có tọa độ : " + Environment.NewLine + "(" + Math.Round(2 * xo - xa, 5) + "; " + Math.Round(2 * yo - ya, 5) + ") (Phép đồng nhất)";

                            }
                            else
                            {
                                textBox4.Text = "Ảnh của A là A' có tọa độ : " + Environment.NewLine + "(" + Math.Round(2 * xo - xa, 5) + "; " + Math.Round(2 * yo - ya, 5) + ")";
                            }

                        }
                    }
                }
            }
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            txt15.Text = "";
            txt16.Text = "";
            txt17.Text = "";
            txt18.Text = "";
            textBox4.Text = "";
        }
        private void txt15_DoubleClick(object sender, EventArgs e)
        {
            txt15.Text = txtOutput.Text.ToString();
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
        // doi xung tam 1 duong thang
        private void simpleButton25_Click(object sender, EventArgs e)
        {
            if (txt19.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                fr.ShowDialog();
            }
            else
            {
                if (txt20.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt21.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt22.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt23.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double a = Convert.ToDouble(txt19.Text);
                                double b = Convert.ToDouble(txt20.Text);
                                double c = Convert.ToDouble(txt21.Text);
                                double xo = Convert.ToDouble(txt22.Text);
                                double yo = Convert.ToDouble(txt23.Text);
                                if (a == 0 && b == 0)
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, PT ĐT Δ phải tồn tại");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if ((a * xo + b * yo + c) == 0)
                                    {
                                        textBox3.Text = "Ảnh của Δ là Δ' có dạng : " + Environment.NewLine + Math.Round(a, 5) + "x + " + Math.Round(b, 5) + "y + " + Math.Round(c, 5) + " = 0 (Phép đồng nhất)";

                                    }
                                    else
                                    {
                                        if ( a == 0)
                                        {

                                            textBox3.Text = "Ảnh của Δ là Δ' có dạng : ";
                                            if (a != 0)
                                            {
                                                textBox3.Text += Math.Round(a, 5) + "x";
                                            }
                                            if (b > 0)
                                            {
                                                textBox3.Text += " + " + Math.Round(b, 5) + "y";
                                            }
                                            if (b < 0)
                                            {
                                                textBox3.Text += " - " + -Math.Round(b, 5) + "y";
                                            }
                                            if (-a * (2 * xo - 1) - b * (2 * yo + c / b) > 0)
                                            {
                                                textBox3.Text += " + " + Math.Round(-a * (2 * xo - 1) - b * (2 * yo + c / b), 5);
                                            }
                                            if (-a * (2 * xo - 1) - b * (2 * yo + c / b) < 0)
                                            {
                                                textBox3.Text += " - " + -Math.Round(-a * (2 * xo - 1) - b * (2 * yo + c / b), 5);
                                            }
                                            textBox3.Text += " = 0";
                                        }
                                        if ( b == 0 )
                                        {
                                            textBox3.Text = "Ảnh của Δ là Δ' có dạng : ";
                                            if (a != 0)
                                            {
                                                textBox3.Text += Math.Round(a, 5) + "x";
                                            }
                                            if (b > 0)
                                            {
                                                textBox3.Text += " + " + Math.Round(b, 5) + "y";
                                            }
                                            if (b < 0)
                                            {
                                                textBox3.Text += " - " + -Math.Round(b, 5) + "y";
                                            }
                                            if (-b * (2 * yo - 1) - a * (2 * xo + c / a) > 0)
                                            {
                                                textBox3.Text += " + " + Math.Round(-b * (2 * yo - 1) - a * (2 * xo + c / a), 5);
                                            }
                                            if (-b * (2 * yo - 1) - a * (2 * xo + c / a) < 0)
                                            {
                                                textBox3.Text += " - " + -Math.Round(-b * (2 * yo - 1) - a * (2 * xo + c / a), 5);
                                            }
                                            textBox3.Text += " = 0";
                                        }
                                        if ( a != 0 && b != 0 )
                                        {
                                            textBox3.Text = "Ảnh của Δ là Δ' có dạng : ";
                                            if (a != 0)
                                            {
                                                textBox3.Text += Math.Round(a, 5) + "x";
                                            }
                                            if (b > 0)
                                            {
                                                textBox3.Text += " + " + Math.Round(b, 5) + "y";
                                            }
                                            if (b < 0)
                                            {
                                                textBox3.Text += " - " + -Math.Round(b, 5) + "y";
                                            }
                                            if (-a * (2 * xo) - b * (2 * yo + c / b) > 0)
                                            {
                                                textBox3.Text += " + " + Math.Round(-a * (2 * xo) - b * (2 * yo + c / b), 5);
                                            }
                                            if (-a * (2 * xo) - b * (2 * yo + c / b) < 0)
                                            {
                                                textBox3.Text += " - " + -Math.Round(-a * (2 * xo) - b * (2 * yo + c / b), 5);
                                            }
                                            textBox3.Text += " = 0";
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        private void simpleButton21_Click(object sender, EventArgs e)
        {
            txt19.Text = "";
            txt20.Text = "";
            txt21.Text = "";
            txt22.Text = "";
            txt23.Text = "";
            textBox3.Text = "";
        }
        private void txt19_DoubleClick(object sender, EventArgs e)
        {
            txt19.Text = txtOutput.Text.ToString();
        }
        private void txt20_DoubleClick(object sender, EventArgs e)
        {
            txt20.Text = txtOutput.Text.ToString();
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
        // doi xung tam duong tron
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (txt24.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                fr.ShowDialog();
            }
            else
            {
                if (txt25.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt26.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt27.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt28.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xi = Convert.ToDouble(txt24.Text);
                                double yi = Convert.ToDouble(txt25.Text);
                                double r = Convert.ToDouble(txt26.Text);
                                double xo = Convert.ToDouble(txt27.Text);
                                double yo = Convert.ToDouble(txt28.Text);
                                if ( r < 0)
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, R > 0");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (xi == xo && yi == yo)
                                    {
                                        textBox5.Text = "Ảnh của (C) là (C)' có tọa độ tâm : " + Environment.NewLine + "(" + Math.Round(xi,5) + "; " + Math.Round(yi, 5)  + ") và bán kính R = " + Math.Round(r,5) + " (Phép đồng nhất)";

                                    }
                                    else
                                    {
                                        textBox5.Text = "Ảnh của (C) là (C)' có tọa độ tâm : " + Environment.NewLine + "(" + Math.Round(2 * xo - xi, 5) + "; " + Math.Round(2 * yo -yi, 5) + ") và bán kính R = " + Math.Round(r,5);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            txt24.Text = "";
            txt25.Text = "";
            txt26.Text = "";
            txt27.Text = "";
            txt28.Text = "";
            textBox5.Text = "";
        }
        private void txt24_DoubleClick(object sender, EventArgs e)
        {
            txt24.Text = txtOutput.Text.ToString();

        }
        private void txt25_DoubleClick(object sender, EventArgs e)
        {
            txt25.Text = txtOutput.Text.ToString();

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
        // doi xung truc diem
        private void simpleButton48_Click(object sender, EventArgs e)
        {
            if (txt29.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt30.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt31.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập A");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt32.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập B");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt33.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập C");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xa = Convert.ToDouble(txt29.Text);
                                double ya = Convert.ToDouble(txt30.Text);
                                double a = Convert.ToDouble(txt31.Text);
                                double b = Convert.ToDouble(txt32.Text);
                                double c = Convert.ToDouble(txt33.Text);
                                double d = a * a + b * b;
                                double dx = -c * a + (b * xa - a * ya) * b;
                                double dy = a * (a * ya - b * xa) - c * b;
                                if (a == 0 && b == 0)
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, PT trục đối xứng phải tồn tại");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if ( (a*xa + b * ya + c) == 0 )
                                    {
                                        textBox7.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round(xa, 5) + "; " + Math.Round(ya, 5) + ") (Phép đồng nhất)";
                                    }
                                    else
                                    {
                                        textBox7.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round(2 * (dx / d) - xa, 5) + "; " + Math.Round(2 * dy / d - ya, 5) + ")";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton47_Click(object sender, EventArgs e)
        {
            txt29.Text = "";
            txt30.Text = "";
            txt31.Text = "";
            txt32.Text = "";
            txt33.Text = "";
            textBox7.Text = "";
        }
        private void txt29_DoubleClick(object sender, EventArgs e)
        {
            txt29.Text = txtOutput.Text.ToString();
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
        private void txt33_DoubleClick(object sender, EventArgs e)
        {
            txt33.Text = txtOutput.Text.ToString();
        }
        // doi xung truc duong thang
        private void simpleButton56_Click(object sender, EventArgs e)
        {
            if (txt34.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                fr.ShowDialog();
            }
            else
            {
                if (txt35.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt36.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt37.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập A");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt38.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập B");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt39.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập C");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a = Convert.ToDouble(txt34.Text);
                                    double b = Convert.ToDouble(txt35.Text);
                                    double c = Convert.ToDouble(txt36.Text);
                                    double A = Convert.ToDouble(txt37.Text);
                                    double B = Convert.ToDouble(txt38.Text);
                                    double C = Convert.ToDouble(txt39.Text);
                                    if ((A == 0 && B ==0))
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, PT trục đối xứng phải tồn tại");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (a == 0 && b == 0)
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            double d = a * B - A * b;
                                            double dx = -c * B + C * b;
                                            double dy = -a * C + A * c;
                                            if (d != 0)
                                            {
                                               
                                                double x1 = dx / d;
                                                double y1 = dy / d;
                                                double xb = 1;
                                                double yb = -(c + 1) / b;
                                                d = a * a + b * b;
                                                dx = -a * c + b * (b * xb - a * yb);
                                                dy = a * (a * yb - b * xb) - b * c;
                                                double x2 = 2 * (dx / d) - xb;
                                                double y2 = 2 * (dy / d) - yb;
                                                double aa = y2 - y1;
                                                double bb = x1 - x2;
                                                double cc = y1 * (x1 - x2) + x1 * (y2 - y1);
                                                
                                                textBox8.Text = "Ảnh của Δ là Δ' có phương trình : ";
                                                if (aa != 0)
                                                {
                                                    textBox8.Text += Math.Round(aa, 5) + "x";
                                                }
                                                if (bb > 0)
                                                {
                                                    textBox8.Text += " - " + Math.Round(bb, 5) + "y";
                                                }
                                                if (bb < 0)
                                                {
                                                    textBox8.Text += " + " + -Math.Round(bb, 5) + "y";
                                                }
                                                if (cc > 0)
                                                {
                                                    textBox8.Text += " + " + Math.Round(cc, 5);
                                                }
                                                if (cc < 0)
                                                {
                                                    textBox8.Text += " - " + -Math.Round(cc, 5);
                                                }
                                                textBox8.Text += " = 0";

                                            }
                                            else
                                            {
                                                if (dx == 0 && dy == 0)
                                                {
                                                    textBox8.Text = "Ảnh của Δ là Δ' có phương trình : ";
                                                    if (a != 0)
                                                    {
                                                        textBox8.Text += Math.Round(a, 5) + "x";
                                                    }
                                                    if (b > 0)
                                                    {
                                                        textBox8.Text += " + " + Math.Round(b, 5) + "y";
                                                    }
                                                    if (b < 0)
                                                    {
                                                        textBox8.Text += " - " + -Math.Round(b, 5) + "y";
                                                    }
                                                    if (c > 0)
                                                    {
                                                        textBox8.Text += " + " + Math.Round(c, 5);
                                                    }
                                                    if (c < 0)
                                                    {
                                                        textBox8.Text += " - " + -Math.Round(c, 5);
                                                    }
                                                    textBox8.Text += " = 0 (Phép đồng nhất)";
                                                }
                                                else
                                                {
                                                    double xa = 1;
                                                    double xb = 1;
                                                    double ya = -(c + 1) / b;
                                                    double yb = -(C + 1) / B;
                                                    double xo = 2*xb - xa;
                                                    double yo = 2*yb - ya;
                                                    double aa = a;
                                                    double bb = b;
                                                    double cc = -(a*xo +b*yo);
                                                    textBox8.Text = "Ảnh của Δ là Δ' có phương trình : ";
                                                    if (aa != 0)
                                                    {
                                                        textBox8.Text += Math.Round(aa, 5) + "x";
                                                    }
                                                    if (bb > 0)
                                                    {
                                                        textBox8.Text += " + " + Math.Round(bb, 5) + "y";
                                                    }
                                                    if (bb < 0)
                                                    {
                                                        textBox8.Text += " - " + -Math.Round(bb, 5) + "y";
                                                    }
                                                    if (cc > 0)
                                                    {
                                                        textBox8.Text += " + " + Math.Round(cc, 5);
                                                    }
                                                    if (cc < 0)
                                                    {
                                                        textBox8.Text += " - " + -Math.Round(cc, 5);
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
        }
        private void simpleButton55_Click(object sender, EventArgs e)
        {
            txt34.Text = "";
            txt35.Text = "";
            txt36.Text = "";
            txt37.Text = "";
            txt38.Text = "";
            txt39.Text = "";
            textBox8.Text = "";
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
        private void txt39_DoubleClick(object sender, EventArgs e)
        {
            txt39.Text = txtOutput.Text.ToString();

        }
            // doi xung truc duong tron
        private void simpleButton64_Click(object sender, EventArgs e)
        {
            if (txt40.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                fr.ShowDialog();
            }
            else
            {
                if (txt41.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt42.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt43.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập A");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt44.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập B");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt45.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập C");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double xi = Convert.ToDouble(txt40.Text);
                                    double yi = Convert.ToDouble(txt41.Text);
                                    double r = Convert.ToDouble(txt42.Text);
                                    double a = Convert.ToDouble(txt43.Text);
                                    double b = Convert.ToDouble(txt44.Text);
                                    double c = Convert.ToDouble(txt45.Text);
                                    double d = a * a + b * b;
                                    double dx = -c * a + (b * xi - a * yi) * b;
                                    double dy = a * (a * yi - b * xi) - c * b;
                                    if (a == 0 && b == 0 && c != 0)
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, PT trục đối xứng phải tồn tại");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (r <= 0)
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, R > 0");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if ((a * xi + b * yi + c) == 0)
                                            {
                                                textBox9.Text = "Ảnh của (C) là (C)' có tọa độ tâm : (" + Math.Round(xi, 5) + "; " + Math.Round(yi, 5) + ") và bán kính R = " + Math.Round(r, 5) + " (Phép đồng nhất)";
                                            }
                                            else
                                            {
                                                textBox9.Text = "Ảnh của (C) là (C)' có tọa độ tâm : (" + Math.Round(2 * (dx / d) - xi, 5) + "; " + Math.Round(2 * dy / d - yi, 5) + ") và bán kính R = " + Math.Round(r, 5); ;
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
        private void simpleButton63_Click(object sender, EventArgs e)
        {
            txt40.Text = "";
            txt41.Text = "";
            txt42.Text = "";
            txt43.Text = "";
            txt44.Text = "";
            txt45.Text = "";
            textBox9.Text = "";
        }
        private void txt40_DoubleClick(object sender, EventArgs e)
        {
            txt40.Text = txtOutput.Text.ToString();

        }
        private void txt41_DoubleClick(object sender, EventArgs e)
        {
            txt41.Text = txtOutput.Text.ToString();

        }
        private void txt42_DoubleClick(object sender, EventArgs e)
        {
            txt42.Text = txtOutput.Text.ToString();

        }
        private void txt43_DoubleClick(object sender, EventArgs e)
        {
            txt43.Text = txtOutput.Text.ToString();

        }
        private void txt44_DoubleClick(object sender, EventArgs e)
        {
            txt44.Text = txtOutput.Text.ToString();

        }
        private void txt45_DoubleClick(object sender, EventArgs e)
        {
            txt45.Text = txtOutput.Text.ToString();

        }
        private void cmdMod_Click(object sender, EventArgs e)
        {

        }
        private void cmdMongoac_Click(object sender, EventArgs e)
        {

        }
        #region"phepvitu
        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage14.Show();
        }
        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage15.Show();

        }
        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage16.Show();

        }
        private void simpleButton40_Click(object sender, EventArgs e)
        {
            txt50.Text = "";
            txt51.Text = "";
            txt52.Text = "";
            txt53.Text = "";
            txt54.Text = "";
            textBox13.Text = "";

        }
        private void simpleButton98_Click(object sender, EventArgs e)
        {
            txt55.Text = "";
            txt56.Text = "";
            txt57.Text = "";
            txt58.Text = "";
            txt59.Text = "";
            txt60.Text = "";
            textBox14.Text = "";
        }
        private void simpleButton106_Click(object sender, EventArgs e)
        {
            txt61.Text = "";
            txt62.Text = "";
            txt63.Text = "";
            txt64.Text = "";
            txt65.Text = "";
            txt66.Text = "";
            textBox15.Text = "";
        }
     
        private void txt50_DoubleClick(object sender, EventArgs e)
        {
            txt50.Text = txtOutput.Text;
        }
        private void txt51_DoubleClick(object sender, EventArgs e)
        {
            txt51.Text = txtOutput.Text;

        }
        private void txt52_DoubleClick(object sender, EventArgs e)
        {
            txt52.Text = txtOutput.Text;

        }
        private void txt53_DoubleClick(object sender, EventArgs e)
        {
            txt53.Text = txtOutput.Text;

        }
        private void txt54_DoubleClick(object sender, EventArgs e)
        {
            txt54.Text = txtOutput.Text;

        }
        private void txt55_DoubleClick(object sender, EventArgs e)
        {
            txt55.Text = txtOutput.Text;

        }
        private void txt56_DoubleClick(object sender, EventArgs e)
        {
            txt56.Text = txtOutput.Text;

        }
        private void txt57_DoubleClick(object sender, EventArgs e)
        {
            txt57.Text = txtOutput.Text;

        }
        private void txt58_DoubleClick(object sender, EventArgs e)
        {
            txt58.Text = txtOutput.Text;

        }
        private void txt59_DoubleClick(object sender, EventArgs e)
        {
            txt59.Text = txtOutput.Text;

        }
        private void txt60_DoubleClick(object sender, EventArgs e)
        {
            txt60.Text = txtOutput.Text;

        }
        private void txt61_DoubleClick(object sender, EventArgs e)
        {
            txt61.Text = txtOutput.Text;

        }
        private void txt62_DoubleClick(object sender, EventArgs e)
        {
            txt62.Text = txtOutput.Text;

        }
        private void txt63_DoubleClick(object sender, EventArgs e)
        {
            txt63.Text = txtOutput.Text;

        }
        private void txt64_DoubleClick(object sender, EventArgs e)
        {
            txt64.Text = txtOutput.Text;

        }
        private void txt65_DoubleClick(object sender, EventArgs e)
        {
            txt65.Text = txtOutput.Text;


        }
        private void txt66_DoubleClick(object sender, EventArgs e)
        {
            txt66.Text = txtOutput.Text;

        }
        #endregion

        private void simpleButton41_Click(object sender, EventArgs e)
        {
            if (txt50.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt51.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt52.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt53.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt54.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập k");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xa = Convert.ToDouble(txt50.Text);
                                double ya = Convert.ToDouble(txt51.Text);
                                double xo = Convert.ToDouble(txt52.Text);
                                double yo = Convert.ToDouble(txt53.Text);
                                double k = Convert.ToDouble(txt54.Text);
                                if (xa== xo && ya == yo)
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, Tọa độ điểm phải khác với tọa độ tâm vị tự");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (k == 0)
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, k phải khác 0");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (k == 1)
                                        {
                                            textBox13.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round(xa, 5) + "; " + Math.Round(ya, 5) + ") (Phép đồng nhất)";
                                        }
                                        else
                                        {
                                            textBox13.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round(k * (xa - xo) + xo, 5) + "; " + Math.Round(k * (ya - yo) + yo, 5) + ")";
                                        }
                                    }
                                   
                                }
                              
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton99_Click(object sender, EventArgs e)
        {
            if (txt55.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                fr.ShowDialog();
            }
            else
            {
                if (txt56.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt57.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt58.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt59.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt60.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập k");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a = Convert.ToDouble(txt55.Text);
                                    double b = Convert.ToDouble(txt56.Text);
                                    double c = Convert.ToDouble(txt57.Text);
                                    double xo = Convert.ToDouble(txt58.Text);
                                    double yo = Convert.ToDouble(txt59.Text);
                                    double k = Convert.ToDouble(txt60.Text);
                                    if (a == 0 && b == 0)
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if ( k == 0 )
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, k phải khác 0");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if (k == 1)
                                            {
                                                textBox14.Text = "Ảnh của Δ là Δ' có dạng : " ;
                                                 if (a != 0)
                                                 {
                                                     textBox14.Text += Math.Round(a, 5) + "x";
                                                 }
                                                 if (b > 0)
                                                 {
                                                     textBox14.Text += " + " + Math.Round(b, 5) + "y";
                                                 }
                                                 if (b < 0)
                                                 {
                                                     textBox14.Text += " - " + -Math.Round(b, 5) + "y";
                                                 }
                                                 if (c > 0)
                                                 {
                                                     textBox14.Text += " + " + Math.Round(c, 5);
                                                 }
                                                 if (c < 0)
                                                 {
                                                     textBox14.Text += " - " + -Math.Round(c, 5);
                                                 }
                                                 textBox3.Text += " = 0 (Phép đồng nhất)";
                                            }
                                            else
                                            {
                                                if (a == 0)
                                                {

                                                    textBox14.Text = "Ảnh của Δ là Δ' có dạng : ";
                                                    if (a != 0)
                                                    {
                                                        textBox14.Text += Math.Round(a, 5) + "x";
                                                    }
                                                    if (b > 0)
                                                    {
                                                        textBox14.Text += " + " + Math.Round(b, 5) + "y";
                                                    }
                                                    if (b < 0)
                                                    {
                                                        textBox14.Text += " - " + -Math.Round(b, 5) + "y";
                                                    }
                                                    if (c > 0)
                                                    {
                                                        textBox14.Text += " + " + Math.Round(c, 5);
                                                    }
                                                    if (c < 0)
                                                    {
                                                        textBox14.Text += " - " + -Math.Round(c, 5);
                                                    }
                                                    textBox14.Text += " = 0 (Phép đồng nhất)";
                                                }
                                                else
                                                {
                                                    if (b == 0)
                                                    {
                                                        textBox14.Text = "Ảnh của Δ là Δ' có dạng : " + Math.Round(a, 5) + "x";
                                                      
                                                        if (c > 0)
                                                        {
                                                            textBox14.Text += " + " + Math.Round(c, 5);
                                                        }
                                                        if (c < 0)
                                                        {
                                                            textBox14.Text += " - " + -Math.Round(c, 5);
                                                        }
                                                        textBox14.Text += " = 0 (Phép đồng nhất)";
                                                    }
                                                    else
                                                    {
                                                        double xc = k * (-xo) + xo;
                                                        double yc = k * (-c / b - yo) + yo;
                                                        double xd = k * (-c / a - xo) + xo;
                                                        double yd = k * (-yo) + yo;
                                                        textBox14.Text = "Ảnh của Δ là Δ' có dạng : ";
                                                        if (yd - yc != 0)
                                                        {
                                                            textBox14.Text += Math.Round(yd - yc, 5) + "x";
                                                        }
                                                        if (xc - xd > 0)
                                                        {
                                                            textBox14.Text += " + " + Math.Round(xc - xd, 5) + "y";
                                                        }
                                                        if (xc - xd < 0)
                                                        {
                                                            textBox14.Text += " - " + -Math.Round(xc - xd, 5) + "y";
                                                        }
                                                        if (yc * (xc - xd) + xc * (yd - yc) > 0)
                                                        {
                                                            textBox14.Text += " + " + Math.Round(yc * (xc - xd) + xc * (yd - yc), 5);
                                                        }
                                                        if (yc * (xc - xd) + xc * (yd - yc) < 0)
                                                        {
                                                            textBox14.Text += " - " + -Math.Round(yc * (xc - xd) + xc * (yd - yc), 5);
                                                        }
                                                        textBox14.Text += " = 0";
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
        private void simpleButton107_Click(object sender, EventArgs e)
        {
            if (txt61.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                fr.ShowDialog();
            }
            else
            {
                if (txt62.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt63.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt64.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt65.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt66.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập k");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double xi = Convert.ToDouble(txt61.Text);
                                    double yi = Convert.ToDouble(txt62.Text);
                                    double r = Convert.ToDouble(txt63.Text);
                                    double xo = Convert.ToDouble(txt64.Text);
                                    double yo = Convert.ToDouble(txt65.Text);
                                    double k = Convert.ToDouble(txt66.Text);
                                    if (r <= 0)
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, R phải lớn hơn 0");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (k == 0)
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, k phải khác 0");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if (k == 1)
                                            {
                                                textBox15.Text = "Ảnh của (C) là (C') có tọa độ tâm : (" + Math.Round(xi, 5) + "; " + Math.Round(yi, 5) + ") và bán kính R = " + Math.Round(r, 5) + " (Phép đồng nhất)";

                                            }
                                            else
                                            {
                                                textBox15.Text = "Ảnh của (C) là (C') có tọa độ tâm : (" + Math.Round(k * (xi - xo) + xo, 5) + "; " + Math.Round(k * (yi - yo) + yo, 5) + ") và bán kính R = " + Math.Round(Math.Abs(k) * r, 5);
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
        private void cmdSqrt_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Sqrt((double.Parse(txtOutput.Text))).ToString();
                CoTrangthainhap = true;
            }
        }
        private void txt67_DoubleClick(object sender, EventArgs e)
        {
            txt67.Text = txtOutput.Text.ToString();

        }
        private void txt68_DoubleClick(object sender, EventArgs e)
        {
            txt68.Text = txtOutput.Text.ToString();

        }  
        private void txt70_DoubleClick(object sender, EventArgs e)
        {
            txt70.Text = txtOutput.Text.ToString();

        }
        private void txt71_DoubleClick(object sender, EventArgs e)
        {
            txt71.Text = txtOutput.Text.ToString();

        }
        private void txt72_DoubleClick(object sender, EventArgs e)
        {
            txt72.Text = txtOutput.Text.ToString();

        }
        private void txt74_DoubleClick(object sender, EventArgs e)
        {
            txt74.Text = txtOutput.Text.ToString();

        }
        private void txt75_DoubleClick(object sender, EventArgs e)
        {
            txt75.Text = txtOutput.Text.ToString();

        }
        private void txt76_DoubleClick(object sender, EventArgs e)
        {
            txt76.Text = txtOutput.Text.ToString();

        }
        private void simpleButton71_Click(object sender, EventArgs e)
        {
            txt67.Text = "";
            txt68.Text = "";
           
            textBox10.Text = "";

        }
        private void simpleButton79_Click(object sender, EventArgs e)
        {
            txt70.Text = "";
            txt71.Text = "";
            txt72.Text = "";
            textBox11.Text = "";
        }
        private void simpleButton87_Click(object sender, EventArgs e)
        {
            txt74.Text = "";
            txt75.Text = "";
            txt76.Text = "";
            textBox12.Text = "";
        }
        private void simpleButton72_Click(object sender, EventArgs e)
        {
            if (txt67.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt68.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {

                    double xa = Convert.ToDouble(txt67.Text);
                    double ya = Convert.ToDouble(txt68.Text);
                      if (xa == 0 && ya == 0)
                      {
                          Thongbao fr = new Thongbao("Lưu ý, tọa độ điểm A phải khác điểm O (0; 0)");
                          fr.ShowDialog();
                      }
                      else
                      {
                          if (comboBoxEx1.Text == "90⁰")
                          {
                              textBox10.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round(-ya, 5) + "; " + Math.Round(xa, 5) + ")";
                          }
                          if (comboBoxEx1.Text == "-90⁰")
                          {
                              textBox10.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round(ya, 5) + "; " + Math.Round(-xa, 5) +")";
                          }
                          if (comboBoxEx1.Text == "45⁰")
                          {
                              textBox10.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round((xa - ya) / 2, 5) + "; " + Math.Round((ya + xa) / 2, 5) + ")";
                          }
                          if (comboBoxEx1.Text == "-45⁰")
                          {
                              textBox10.Text = "Ảnh của A là A' có tọa độ : (" + Math.Round((xa + ya) / 2, 5) + "; " + Math.Round((ya - xa) / 2, 5) + ")";
                          } 
                      }
                }
            }
        }
        private void simpleButton80_Click(object sender, EventArgs e)
        {
            if (txt70.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a");
                fr.ShowDialog();
            }
            else
            {
                if (txt71.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt72.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập c");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = Convert.ToDouble(txt70.Text);
                        double b = Convert.ToDouble(txt71.Text);
                        double c = Convert.ToDouble(txt72.Text);
                        if (a == 0 && b == 0)
                        {
                            Thongbao fr = new Thongbao("Lưu ý, PT ĐT phải tồn tại");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (a == 0)
                            {
                                if (comboBoxEx2.Text == "90⁰")
                                {

                                    textBox11.Text = "Ảnh của Δ là Δ' có có dạng : x = " + Math.Round(c / b, 5);
                                }
                                if (comboBoxEx2.Text == "-90⁰")
                                {
                                    textBox11.Text = "Ảnh của Δ là Δ' có có dạng : x = " + Math.Round(-c / b, 5);
                                }
                               
                            }
                            if (b == 0)
                            {
                                if (comboBoxEx2.Text == "90⁰")
                                {

                                    textBox11.Text = "Ảnh của Δ là Δ' có có dạng : y = " + Math.Round(c / b, 5);
                                }
                                if (comboBoxEx2.Text == "-90⁰")
                                {
                                    textBox11.Text = "Ảnh của Δ là Δ' có có dạng : y = " + Math.Round(-c / b, 5);
                                }
                            }
                            if (a != 0 && b != 0)
                            {
                                if (comboBoxEx2.Text == "90⁰")
                                {
                                    textBox11.Text = "Ảnh của Δ là Δ' có có dạng : " ;
                                    if (b != 0)
                                    {
                                        textBox11.Text += Math.Round(b, 5) + "x";
                                    }
                                    if (-a > 0)
                                    {
                                        textBox11.Text += " + " + Math.Round(-a, 5) + "y";
                                    }
                                    if (-a < 0)
                                    {
                                        textBox11.Text += " - " + -Math.Round(-a, 5) + "y";
                                    }
                                    if (c > 0)
                                    {
                                        textBox11.Text += " + " + Math.Round(c, 5);
                                    }
                                    if (c < 0)
                                    {
                                        textBox11.Text += " - " + -Math.Round(c, 5);
                                    }
                                    textBox11.Text += " = 0";
                                }
                                if (comboBoxEx2.Text == "-90⁰")
                                {
                                    textBox11.Text = "Ảnh của Δ là Δ' có có dạng : ";
                                    if (b != 0)
                                    {
                                        textBox11.Text += Math.Round(b, 5) + "x";
                                    }
                                    if (-a > 0)
                                    {
                                        textBox11.Text += " + " + Math.Round(-a, 5) + "y";
                                    }
                                    if (-a < 0)
                                    {
                                        textBox11.Text += " - " + -Math.Round(-a, 5) + "y";
                                    }
                                    if (c > 0)
                                    {
                                        textBox11.Text += " + " + Math.Round(c, 5);
                                    }
                                    if (c < 0)
                                    {
                                        textBox11.Text += " - " + -Math.Round(c, 5);
                                    }
                                    textBox11.Text += " = 0";
                                }
                            }
                           
                        }
                    }
                }
            }
        }
        private void simpleButton88_Click(object sender, EventArgs e)
        {
            if (txt74.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                fr.ShowDialog();
            }
            else
            {
                if (txt75.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt76.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double xi = Convert.ToDouble(txt74.Text);
                        double yi = Convert.ToDouble(txt75.Text);
                        double r = Convert.ToDouble(txt76.Text);
                        if (r <= 0)
                        {
                            Thongbao fr = new Thongbao("Lưu ý, R > 0");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (xi == 0 && yi == 0)
                            {
                                Thongbao fr = new Thongbao("Lưu ý, tọa độ điểm I phải khác điểm O (0; 0)");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (comboBoxEx3.Text == "90⁰")
                                {
                                    textBox12.Text = "Ảnh của (C) là (C)' có tọa độ tâm : (" + Math.Round(-yi, 5) + "; " + Math.Round(xi, 5) + ") và bán kính R = " + Math.Round(r, 5);
                                }
                                if (comboBoxEx3.Text == "-90⁰")
                                {
                                    textBox12.Text = "Ảnh của (C) là (C)' có tọa độ tâm : (" + Math.Round(yi, 5) + "; " + Math.Round(-xi, 5) + ") và bán kính R = " + Math.Round(r, 5);
                                }
                                if (comboBoxEx3.Text == "45⁰")
                                {
                                    textBox12.Text = "Ảnh của (C) là (C)' có tọa độ tâm : (" + Math.Round((xi - yi) / 2, 5) + "; " + Math.Round((yi + xi) / 2, 5) + ") và bán kính R = " + Math.Round(r, 5);
                                }
                                if (comboBoxEx3.Text == "-45⁰")
                                {
                                    textBox12.Text = "Ảnh của (C) là (C)' có tọa độ tâm : (" + Math.Round((xi + yi) / 2, 5) + "; " + Math.Round((yi - xi) / 2, 5) + ") và bán kính R = " + Math.Round(r, 5);
                                }
                            }
                        }
                      
                    }
                }
            }
        }

        private void txt1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabPage6_Paint(object sender, PaintEventArgs e)
        {

        }
     
    }
}