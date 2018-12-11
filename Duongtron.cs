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
    public partial class Duongtron : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region
        #region
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
        public Duongtron()
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
        #endregion
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
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage3.Show();
        }
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage4.Show();
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Sqrt((double.Parse(txtOutput.Text))).ToString();
                CoTrangthainhap = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            lblTime.Caption = (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;
            if ((txt1.Text == "") || (txt2.Text == "") || (txt3.Text == "") || (txt4.Text == "") || (txt5.Text == "") || (txt6.Text == ""))
            {
                ve1.Enabled = false;

            }
            else
            {
                ve1.Enabled = true;
            }
            if ((txt7.Text == "") || (txt8.Text == "") || (txt9.Text == "") || (txt10.Text == "") || (txt11.Text == "") || (txt12.Text == ""))
            {
                
            }
            else
            {
                
            }
            if ((txt13.Text == "") || (txt14.Text == "") || (txt15.Text == "") || (txt16.Text == "") || (txt17.Text == "") || (txt18.Text == ""))
            {
                
            }
            else
            {
                
            }
            if ((txt19.Text == "") || (txt20.Text == "") || (txt21.Text == "") || (txt22.Text == "") || (txt23.Text == ""))
            {
                ve4.Enabled = false;

            }
            else
            {
                ve4.Enabled = true;
            }
            if ((txt24.Text == "") || (txt25.Text == "") || (txt26.Text == "") || (txt27.Text == "") || (txt28.Text == "") || (txt29.Text == "") || (txt30.Text == "") || (txt31.Text == "") || (txt32.Text == ""))
            {
                

            }
            else
            {
            }
            if ((txt33.Text == "") || (txt34.Text == "") || (txt35.Text == ""))
            {
                ve6.Enabled = false;

            }
            else
            {
                ve6.Enabled = true;
            }
            if ((txt36.Text == "") || (txt37.Text == "") || (txt38.Text == "") || (txt39.Text == ""))
            {
                ve7.Enabled = false;

            }
            else
            {
                ve7.Enabled = true;
            }
           
        }
        #endregion

        #region'hehe

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            txt7.Text = "";
            txt8.Text = "";
            txt9.Text = "";
            txt10.Text = "";
            txt11.Text = "";
            txt12.Text = "";
            textBox4.Text = "";
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

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            txt13.Text = "";
            txt14.Text = "";
            txt15.Text = "";
            txt16.Text = "";
            txt17.Text = "";
            txt18.Text = "";
            textBox3.Text = "";
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
        private void txt21_DoubleClick(object sender, EventArgs e)
        {
            txt21.Text = txtOutput.Text.ToString();
        }
        private void txt22_DoubleClick(object sender, EventArgs e)
        {
            txt23.Text = txtOutput.Text.ToString();
        }

        private void txt23_DoubleClick(object sender, EventArgs e)
        {
            txt22.Text = txtOutput.Text.ToString();
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
        #endregion

        private void simpleButton30_Click(object sender, EventArgs e)
        {
            txt19.Text = "";
            txt20.Text = "";
            txt21.Text = "";
            txt22.Text = "";
            txt23.Text = "";
            textBox2.Text = "";

        }
        private void simpleButton35_Click(object sender, EventArgs e)
        {
            txt24.Text = "";
            txt24.Text = "";
            txt25.Text = "";
            txt26.Text = "";
            txt27.Text = "";
            txt28.Text = "";
            txt29.Text = "";
            txt30.Text = "";
            txt31.Text = "";
            txt32.Text = "";
            textBox5.Text = "";
        }
        private void simpleButton47_Click(object sender, EventArgs e)
        {
            txt33.Text = "";
            txt34.Text = "";
            txt35.Text = "";
            textBox6.Text = "";
        }
        private void simpleButton48_Click(object sender, EventArgs e)
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
                        double a = Convert.ToDouble(txt33.Text); a = a / (-2);
                        double b = Convert.ToDouble(txt34.Text); b = b / (-2);
                        double c = Convert.ToDouble(txt35.Text);
                       if (a*a + b*b-c <= 0)
                       {
                           Thongbao fr = new Thongbao("Lưu ý, PT ĐT' phải tồn tại");
                           fr.ShowDialog();
                       }
                       else
                       {
                           textBox6.Text = "PT ĐT' (C) có tọa độ tâm I (" + Math.Round(a, 5) + "; " + Math.Round(b, 5) + ") và bán kính R = " + Math.Round(Math.Sqrt(a * a + b * b - c), 5);
                       }
                      

                    }
                }
            }
        }
        private void simpleButton45_Click_1(object sender, EventArgs e)
        {
            txt36.Text = "";
            txt37.Text = "";
            txt38.Text = "";
            txt39.Text = "";
            textBox7.Text = "";
        }
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage7.Show();
        }
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage1.Show();
        }
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage5.Show();
        }
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage6.Show();
        }
        private void simpleButton14_Click(object sender, EventArgs e)
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
                            if (txt5.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xC");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt6.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yC");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double xa = Convert.ToDouble(txt1.Text);
                                    double ya = Convert.ToDouble(txt2.Text);
                                    double xb = Convert.ToDouble(txt3.Text);
                                    double yb = Convert.ToDouble(txt4.Text);
                                    double xc = Convert.ToDouble(txt5.Text);
                                    double yc = Convert.ToDouble(txt6.Text);
                                    double aa1 = 2*(xa - xb);
                                    double aa2 = 2*(xa - xc);
                                    double bb1 = 2*(ya - yb);
                                    double bb2 = 2*(ya - yc);
                                    double cc1 = xa * xa - xb * xb + ya * ya - yb * yb;
                                    double cc2 = xa * xa - xc * xc + ya * ya - yc * yc;
                                    double d = aa1 * bb2 - aa2 * bb1;
                                    double dx = cc1 * bb2 - cc2 * bb1;
                                    double dy = aa1 * cc2 - aa2 * cc1;
                                    if ((xa == xb && ya == yb) && (xc == xa && yc == ya) && (xc == xb && yc == yb))
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, 3 điểm vừa nhập phải khác nhau");
                                        fr.ShowDialog();

                                    }
                                    else
                                    {
                                        if (d != 0)
                                        {
                                            
                                            double a = dx / d;
                                            double b = dy / d;
                                            double r = (a-xa)*(a-xa) + (b-ya)*(b-ya);
                                            textBox1.Text = "PT ĐT' cần tìm có dạng : (x" ;
                                            if (a > 0)
                                            {
                                                textBox1.Text += " - " + Math.Round(a, 5);
                                            }
                                            if (a < 0)
                                            {
                                                textBox1.Text += " + " + -Math.Round(a, 5);
                                            }
                                            textBox1.Text += ")ᶻ + (y";
                                            if (b > 0)
                                            {
                                                textBox1.Text += " - " + Math.Round(b, 5);
                                            }
                                            if (b < 0)
                                            {
                                                textBox1.Text += " + " + -Math.Round(b, 5);
                                            }
                                            textBox1.Text += ")ᶻ = " + Math.Round(r, 5);
                                        
                                        
                                        }
                                        else
                                        {
                                            if (dx == 0 && dy == 0)
                                            {
                                                textBox1.Text = "Tồn tại vô số ĐT' thõa mãn điều kiện";

                                            }
                                            else
                                            {
                                                textBox1.Text = "Không tồn tại ĐT' thõa mãn điều kiện";

                                            }
                                        }
                                    }
                                    if (xa == xb && ya == yb)
                                    {
                                        textBox1.Text = "Họ ĐT' có tâm I thuộc đường thẳng : ";
                                        if (yc - ya != 0)
                                        {
                                            textBox1.Text += Math.Round(yc - ya, 5) + "x";
                                        }
                                        if (xa - xc > 0)
                                        {
                                            textBox1.Text += " + " + Math.Round(xa - xc, 5) + "y";
                                        }
                                        if (xa - xc < 0)
                                        {
                                            textBox1.Text += " - " + -Math.Round(xa - xc, 5) + "y";

                                        }
                                        if (-(xa * (yc - ya) + ya * (xa - xc)) > 0)
                                        {
                                            textBox1.Text += " + " + Math.Round(-(xa * (yc - ya) + ya * (xa - xc)), 5);
                                        }
                                        if (-(xa * (yc - ya) + ya * (xa - xc)) < 0)
                                        {
                                            textBox1.Text += " - " + -Math.Round(-(xa * (yc - ya) + ya * (xa - xc)), 5);
                                        }
                                        textBox1.Text += " = 0";
                                        
                                    }
                                    if (xb == xc && yb == yc)
                                    {
                                       textBox1.Text = "Họ ĐT' có tâm I thuộc đường thẳng : ";
                                        if (yc - ya != 0)
                                        {
                                            textBox1.Text += Math.Round(yc - ya, 5) + "x";
                                        }
                                        if (xa - xc > 0)
                                        {
                                            textBox1.Text += " + " + Math.Round(xa - xc, 5) + "y";
                                        }
                                        if (xa - xc < 0)
                                        {
                                            textBox1.Text += " - " + -Math.Round(xa - xc, 5) + "y";

                                        }
                                        if (-(xa * (yc - ya) + ya * (xa - xc)) > 0)
                                        {
                                            textBox1.Text += " + " + Math.Round(-(xa * (yc - ya) + ya * (xa - xc)), 5);
                                        }
                                        if (-(xa * (yc - ya) + ya * (xa - xc)) < 0)
                                        {
                                            textBox1.Text += " - " + -Math.Round(-(xa * (yc - ya) + ya * (xa - xc)), 5);
                                        }
                                        textBox1.Text += " = 0";
                                    }
                                    if (xc == xa && yc == ya)
                                    {
                                        textBox1.Text = "Họ ĐT' có tâm I thuộc đường thẳng : ";
                                        if (yc - yb != 0)
                                        {
                                            textBox1.Text += Math.Round(yc - yb, 5) + "x";
                                        }
                                        if (xb - xc > 0)
                                        {
                                            textBox1.Text += " + " + Math.Round(xb - xc, 5) + "y";
                                        }
                                        if (xb - xc < 0)
                                        {
                                            textBox1.Text += " - " + -Math.Round(xb - xc, 5) + "y";

                                        }
                                         if (-(xb * (yc - yb) + yb * (xb - xc)) > 0)
                                        {
                                            textBox1.Text += " + " + Math.Round(-(xb * (yc - yb) + yb * (xb - xc)), 5) ;
                                        }
                                        if (-(xb * (yc - yb) + yb * (xb - xc)) < 0)
                                        {
                                            textBox1.Text += " - " + -Math.Round(-(xb * (yc - yb) + yb * (xb - xc)), 5);
                                        }
                                        textBox1.Text += " = 0";
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            if (txt7.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₁");
                fr.ShowDialog();
            }
            else
            {
                if (txt10.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt8.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₂");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt11.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt9.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₃");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt12.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₃");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a1 = Convert.ToDouble(txt7.Text);
                                    double a2 = Convert.ToDouble(txt8.Text);
                                    double a3 = Convert.ToDouble(txt9.Text);
                                    double b1 = Convert.ToDouble(txt10.Text);
                                    double b2 = Convert.ToDouble(txt11.Text);
                                    double b3 = Convert.ToDouble(txt12.Text);
                                    double aa = (a1*a1+a2*a2);                               
                                    double bb = (2*a1*b1+2*a2*b2-a3);
                                    double cc = (b1*b1+b2*b2-b3);
                                    double delta = bb * bb - 4 * aa * cc;
                                    if (aa == 0)
                                    {
                                        if (bb == 0)
                                        {
                                            if (cc == 0)
                                            {
                                                textBox4.Text = "Họ ĐT' không tồn tại với mọi m";
                                            }
                                            else
                                            {
                                                if ( cc > 0)
                                                {
                                                    textBox4.Text = "Họ ĐT' tồn tại với mọi m";
                                                }
                                                else
                                                {
                                                    textBox4.Text = "Họ ĐT' không tồn tại với mọi m";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (bb > 0)
                                            {
                                                textBox4.Text = "Họ ĐT' tồn tại với mọi m > " + Math.Round(-cc / bb, 5);
                                            }
                                            else
                                            {
                                                textBox4.Text = "Họ ĐT' tồn tại với mọi m < " + Math.Round(-cc / bb, 5);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (delta > 0)
                                        {
                                            double x1 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                                            double x2 = (-bb - Math.Sqrt(delta)) / (2 * aa);
                                            if (aa > 0)
                                            {
                                                textBox4.Text = "Họ ĐT' tồn tại với mọi m > " + Math.Round(x1,5) + " và m < " + Math.Round(x2,5);
                                            }
                                            else
                                            {
                                                textBox4.Text = "Họ ĐT' tồn tại với mọi " + Math.Round(x2,5) + " < m < " + Math.Round(x1,5);

                                            }
                                        }
                                        if (delta == 0)
                                        {
                                            double x1 = (-bb) / (2 * aa);
                                            textBox4.Text = "Họ ĐT' tồn tại với mọi m ≠ " + Math.Round(x1, 5);
                                              

                                        }
                                        if (delta < 0)
                                        {
                                            if ( aa > 0)
                                            {
                                                textBox4.Text = "Họ ĐT' tồn tại với mọi m";
                                            }
                                            else
                                            {
                                                textBox4.Text = "Họ ĐT' không tồn tại với mọi m";
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
        private void simpleButton31_Click(object sender, EventArgs e)
        {
            if (txt19.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₀");
                fr.ShowDialog();
            }
            else
            {
                if (txt20.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₀");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt21.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt22.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt23.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double xo = Convert.ToDouble(txt19.Text);
                                double yo = Convert.ToDouble(txt20.Text);
                                double a = Convert.ToDouble(txt21.Text);
                                double b = Convert.ToDouble(txt22.Text);
                                double r = Convert.ToDouble(txt23.Text);

                                // double bb = chua biet;
                                // double cc = chua biet;
                                if (r <= 0)
                                {
                                    Thongbao fr = new Thongbao("Lưu ý, R > 0");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    textBox2.Text = "ĐT' (C) có tâm I (" + Math.Round(a, 5) + "; " + Math.Round(b, 5) + ") và bán kính R = " + Math.Round(r, 5);
                                    textBox2.Text += Environment.NewLine + "ĐT Δ qua M (" + Math.Round(xo, 5) + "; " + Math.Round(yo, 5) + ") có PT :";
                                    textBox2.Text += Environment.NewLine + "     A(x";
                                    if (xo > 0)
                                    {
                                        textBox2.Text += " - " + Math.Round(xo,5);
                                    }
                                    if (xo < 0)
                                    {
                                        textBox2.Text += " + " + -Math.Round(xo, 5);
                                    }
                                    textBox2.Text += ") + B(y";
                                    if (yo > 0)
                                    {
                                        textBox2.Text += " - " + Math.Round(yo, 5);
                                    }
                                    if (yo < 0)
                                    {
                                        textBox2.Text += " + " + -Math.Round(yo, 5);
                                    }
                                    textBox2.Text += ") = 0 (Aᶻ + Bᶻ ≠ 0)";
                                    textBox2.Text += Environment.NewLine + "Khoảng cách từ I (" + Math.Round(a, 5) + "; " + Math.Round(b, 5) + ") tới ĐT Δ là :";
                                    textBox2.Text += Environment.NewLine + "     d(I; Δ) = |";
                                    if ((a-xo) != 0)
                                    {
                                        textBox2.Text += Math.Round(a-xo,5) + "A";
                                    }
                                    if ((b - yo) > 0)
                                    {
                                        textBox2.Text += " + " + Math.Round(b - yo, 5) + "B";
                                    }
                                    if ((b - yo) < 0)
                                    {
                                        textBox2.Text += " - " + -Math.Round(b - yo, 5) + "B";
                                        
                                    }
                                    textBox2.Text += "| / √(Aᶻ + Bᶻ) = " + Math.Round(r, 5);
                                    textBox2.Text += Environment.NewLine + " <=> |";
                                    if ((a - xo) != 0)
                                    {
                                        textBox2.Text += Math.Round(a - xo, 5) + "A";
                                    }
                                    if ((b - xo) > 0)
                                    {
                                        textBox2.Text += " + " + Math.Round(b - yo, 5) + "B";
                                    }
                                    if ((b - yo) < 0)
                                    {
                                        textBox2.Text += " - " + -Math.Round(b - yo, 5) + "B";
                                    }
                                    textBox2.Text += "| = " + Math.Round(r, 5) + "√(Aᶻ + Bᶻ)";
                                    textBox2.Text += Environment.NewLine + " <=> ";
                                    if ((a-xo) != 0)
                                    {
                                        textBox2.Text += Math.Round((a - xo) * (a - xo), 5) + "Aᶻ";
                                    }
                                    if ((b - yo) != 0)
                                    {
                                        textBox2.Text += " + " + Math.Round((b - yo) * (b - yo), 5) + "Bᶻ";
                                    }
                                    if (2 * (a - xo) * (b - yo) > 0)
                                    {
                                        textBox2.Text += " + " + 2 * (a - xo) * (b - yo) + "AB";
                                    }
                                    if (2 * (a - xo) * (b - yo) < 0)
                                    {
                                        textBox2.Text += " - " + -2 * (a - xo) * (b - yo) + "AB";
                                    }
                                    textBox2.Text += " = " + Math.Round(r * r, 5) + "(Aᶻ + Bᶻ)";
                                    textBox2.Text += Environment.NewLine + " <=> ";
                                    if (((a - xo)* (a-xo)-r*r) != 0)
                                    {
                                        textBox2.Text += Math.Round((a - xo) * (a - xo) - r * r, 5) + "Aᶻ";
                                    }
                                    if ((b - yo) * (b - yo) - r * r > 0)
                                    {
                                        textBox2.Text += " + " + Math.Round((b - yo) * (b - yo) - r * r, 5) + "Bᶻ";
                                    }
                                    if ((b - yo) * (b - yo) - r * r < 0)
                                    {
                                        textBox2.Text += " - " + -Math.Round((b - yo) * (b - yo) - r * r, 5) + "Bᶻ";
                                    }
                                    if (2 * (a - xo) * (b - yo) > 0)
                                    {
                                        textBox2.Text += " + " + Math.Round(2 * (a - xo) * (b - yo),5) + "AB";
                                    }
                                    if (2 * (a - xo) * (b - yo) < 0)
                                    {
                                        textBox2.Text += " - " + -Math.Round(2 * (a - xo) * (b - yo), 5) + "AB";
                                    }
                                    textBox2.Text += " = 0";
                                   
                                    int  g = 2;
                                    double cc = g * g * ((a - xo) * (a - xo) - r * r);
                                    double bb = 2 * g * (a - xo) * (b - yo);
                                    double aa = ((b - yo) * (b - yo) - r * r);
                                    double delta = bb * bb - 4 * aa * cc;
                                    textBox2.Text += Environment.NewLine + "Chọn A = " + g;
                                    if (aa == 0)
                                    {
                                        if (bb == 0)
                                        {
                                            if (cc == 0)
                                            {
                                                textBox2.Text += Environment.NewLine + g + "x + By + C = 0 (B, C thuộc R)";
                                            }
                                            else
                                            {
                                                textBox2.Text = "PT ĐT không tồn tại";
                                            }
                                        }
                                        else
                                        {
                                            textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ : " + Environment.NewLine + g + "x";
                                            if (-cc/bb > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(-cc / bb, 5) + "y";

                                            }
                                            if (-cc / bb < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(-cc / bb, 5) + "y";

                                            }
                                            if (g * -xo - (-cc / bb) * yo > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(g * -xo - (-cc / bb) * yo, 5) ;

                                            }
                                            if (g * -xo - (-cc / bb) * yo < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(g * -xo - (-cc / bb) * yo, 5) ;

                                            }
                                            textBox2.Text += " = 0";
                                        }
                                    }
                                    else
                                    {
                                        if (delta > 0)
                                        {
                                            double x1 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                                            double x2 = (-bb - Math.Sqrt(delta)) / (2 * aa);
                                            textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ₁ : " + Environment.NewLine + g + "x";
                                            if (x1 > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(x1, 5) + "y";

                                            }
                                            if (x1 < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(x1, 5) + "y";

                                            }
                                            if (g * -xo - x1 * yo > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(g * -xo - x1 * yo, 5);

                                            }
                                            if (g * -xo - x1 * yo < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(g * -xo - x1 * yo, 5);

                                            }
                                            textBox2.Text += " = 0";
                                            textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ₂ : " + Environment.NewLine + g + "x";
                                            if (x2 > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(x2, 5) + "y";

                                            }
                                            if (x2 < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(x2, 5) + "y";

                                            }
                                            if (g * -xo - x2 * yo > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(g * -xo - x2 * yo, 5);

                                            }
                                            if (g * -xo - x2 * yo < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(g * -xo - x2 * yo, 5);

                                            }
                                            textBox2.Text += " = 0";

                                        }
                                        if (delta == 0)
                                        {
                                            double x1 = (-bb) / (2 * aa);
                                            textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ : " + Environment.NewLine + g + "x";
                                            if (x1 > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(x1, 5) + "y";

                                            }
                                            if (x1 < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(x1, 5) + "y";

                                            }
                                            if (g * -xo - x1 * yo > 0)
                                            {
                                                textBox2.Text += " + " + Math.Round(g * -xo - x1 * yo, 5);

                                            }
                                            if (g * -xo - x1 * yo < 0)
                                            {
                                                textBox2.Text += " - " + -Math.Round(g * -xo - x1 * yo, 5);

                                            }
                                            textBox2.Text += " = 0";

                                        }
                                        if (delta < 0)
                                        {
                                            textBox2.Text = "PT ĐT không tồn tại";

                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton37_Click(object sender, EventArgs e)
        {
            if (txt24.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₁");
                fr.ShowDialog();
            }
            else
            {
                if (txt25.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt26.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₂");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt27.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt28.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₃");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt29.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₃");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (txt30.Text == "")
                                    {
                                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xI");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (txt31.Text == "")
                                        {
                                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yI");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if (txt32.Text == "")
                                            {
                                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập R");
                                                fr.ShowDialog();
                                            }
                                            else
                                            {
                                                double a1 = Convert.ToDouble(txt24.Text);
                                                double b1 = Convert.ToDouble(txt25.Text);
                                                double a2 = Convert.ToDouble(txt26.Text);
                                                double b2 = Convert.ToDouble(txt27.Text);
                                                double a3 = Convert.ToDouble(txt28.Text);
                                                double b3 = Convert.ToDouble(txt29.Text);
                                                double a = Convert.ToDouble(txt30.Text);
                                                double b = Convert.ToDouble(txt31.Text);
                                                double r = Convert.ToDouble(txt32.Text);
                                                if (r <= 0)
                                                {
                                                    Thongbao fr = new Thongbao("Lưu ý, R > 0");
                                                    fr.ShowDialog();
                                                }
                                                else
                                                {
                                                    double aa = (a1 * a1 + a2 * a2) * (a * a - r * r);
                                                    double bb = (2 * a * a1 * b1 + 2 * b * a2 * b2 - r * r * (2 * a1 * b1 + 2 * a2 * b2));
                                                    double cc = (a * a * b1 * b1 + b * b * b2 * b2 - r * r * (b1 * b1 + b2 * b2));
                                                    double delta = bb * bb - 4 * aa * cc;
                                                    if (aa == 0)
                                                    {
                                                        if (bb == 0)
                                                        {
                                                            if (cc == 0)
                                                            {
                                                                textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);

                                                                textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                            }
                                                            else
                                                            {
                                                                if (cc > 0)
                                                                {
                                                                    textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                                    textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                    textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                    textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                                    textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                    textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                }
                                                                else
                                                                {
                                                                    textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                                    textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                    textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                    textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                    textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                    textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                            textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(-cc / bb, 5);
                                                            if (bb > 0)
                                                            {
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m > " + Math.Round(-cc / bb, 5);
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(-cc / bb, 5);
                                                            }
                                                            else
                                                            {
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(-cc / bb, 5);
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m > " + Math.Round(-cc / bb, 5);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (delta < 0)
                                                        {
                                                            if (aa > 0)
                                                            {
                                                                textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                            }
                                                            else
                                                            {
                                                                textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                            }

                                                        }
                                                        if (delta == 0)
                                                        {
                                                            double x1 = (-bb) / (2 * aa);
                                                            textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                            textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1, 5);
                                                            textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                            textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                            textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                            textBox5.Text += Environment.NewLine + " - Xảy ra với mọi m";
                                                        }
                                                        if (delta > 0)
                                                        {
                                                            double x1 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                                                            double x2 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                                                            textBox5.Text = "TH1 : Δ là tiếp tuyến của (C) hay d(I; Δ) = " + Math.Round(r, 5);
                                                            textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1, 5) + " hoặc m = " + Math.Round(x2, 5);
                                                            if (aa > 0)
                                                            {
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(x2, 5) + " và m > " + Math.Round(x1, 5);
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi " + Math.Round(x2, 5) + " < m < " + Math.Round(x1, 5);

                                                            }
                                                            else
                                                            {
                                                                textBox5.Text += Environment.NewLine + "TH2 : Δ không cắt (C) hay d(I; Δ) > " + Math.Round(r, 5);

                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi " + Math.Round(x2, 5) + " < m < " + Math.Round(x1, 5);
                                                                textBox5.Text += Environment.NewLine + "TH3 : Δ cắt (C) tại 2 điểm phân biệt hay d(I; Δ) < " + Math.Round(r, 5);
                                                                textBox5.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(x2, 5) + " và m > " + Math.Round(x1, 5);
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
       private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (txt13.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₁");
                fr.ShowDialog();
            }
            else
            {
                if (txt14.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt15.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₂");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt16.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt17.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a₃");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt18.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b₃");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a1 = Convert.ToDouble(txt13.Text);
                                    double b1 = Convert.ToDouble(txt14.Text);
                                    double a2 = Convert.ToDouble(txt15.Text);
                                    double b2 = Convert.ToDouble(txt16.Text);
                                    double a3 = Convert.ToDouble(txt17.Text);
                                    double b3 = Convert.ToDouble(txt18.Text);
                                    double aa = a2*a2+a1*a1;
                                    double bb = 2 * a2 * a3 - a1 * a2 * b1 + a1 * a1 * b2;
                                    double cc = a3 * a3 - a1 * a3 * b1 + a1 * a1 * b3;
                                    double delta = bb * bb - 4 * aa * cc;
                                    if (a1 == a2 && a2 == a3 && a3 == 0 && (b1 * b1) / 4 + (b2 * b2) / 4 - b3 <= 0)
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, PT ĐT' phải tồn tại");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (aa == 0)
                                        {
                                            if ( bb == 0)
                                            {
                                                if (cc == 0)
                                                {
                                                    textBox3.Text = "Tập hợp tất cả các điểm cố định thuộc PT ĐT'";
                                                }
                                                else
                                                {
                                                    textBox3.Text = "Họ ĐT' không tồn tại điểm cố định";
                                                }

                                            }
                                            else
                                            {
                                                double x1 = -cc / bb;
                                                textBox3.Text = "Họ ĐT' tồn tại điểm cố định M (" + Math.Round((-a3 - a2 * x1) / a1,5) + "; " + Math.Round(x1,5) + ")" ;
                                            }
                                        }
                                        else
                                        {
                                            if (delta > 0)
                                            {
                                                double x1 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                                                double x2 = (-bb - Math.Sqrt(delta)) / (2 * aa);
                                                textBox3.Text = "Họ ĐT' tồn tại hai điểm cố định";
                                                textBox3.Text += Environment.NewLine + " M (" + Math.Round((-a3 - a2 * x1) / a1, 5) + "; " + Math.Round(x1, 5) + ")";
                                                textBox3.Text += Environment.NewLine + " N (" + Math.Round((-a3 - a2 * x2) / a1, 5) + "; " + Math.Round(x2, 5) + ")";

                                            }
                                            if ( delta == 0)
                                            {
                                                double x1 = -bb / ( 2*aa);
                                                textBox3.Text = "Họ ĐT' tồn tại điểm cố định M (" + Math.Round((-a3 - a2 * x1) / a1, 5) + "; " + Math.Round(x1, 5) + ")";
                                            }
                                            if ( delta < 0)
                                            {
                                                textBox3.Text = "Họ ĐT' không tồn tại điểm cố định";
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
        private void simpleButton51_Click(object sender, EventArgs e)
        {
            if (txt36.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₁");
                fr.ShowDialog();
            }
            else
            {
                if (txt37.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt38.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập x₂");
                        fr.ShowDialog();
                    }
                    else
                    {

                        if (txt39.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập y₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double x1 = Convert.ToDouble(txt36.Text);
                            double y1 = Convert.ToDouble(txt37.Text);
                            double x2 = Convert.ToDouble(txt38.Text);
                            double y2 = Convert.ToDouble(txt39.Text);
                            if (x1 == x2 && y1 == y2)
                            {
                                Thongbao fr = new Thongbao("Lưu ý, tọa độ 2 tâm I phải khác nhau");
                                fr.ShowDialog();
                            }
                            else
                            {
                                textBox7.Text = "Khoảng cách giữa I₁ và I₂ là : I₁I₂ = " + Math.Round(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (-y2 - y1)), 5);
                            }
                        }
                    }
                }
            }
        }

        private void ve6_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt1.Text);
            double ya = Convert.ToDouble(txt2.Text);
            double xb = Convert.ToDouble(txt3.Text);
            double yb = Convert.ToDouble(txt4.Text);
            double xc = Convert.ToDouble(txt5.Text);
            double yc = Convert.ToDouble(txt6.Text);
            double aa1 = 2 * (xa - xb);
            double aa2 = 2 * (xa - xc);
            double bb1 = 2 * (ya - yb);
            double bb2 = 2 * (ya - yc);
            double cc1 = xa * xa - xb * xb + ya * ya - yb * yb;
            double cc2 = xa * xa - xc * xc + ya * ya - yc * yc;
            double d = aa1 * bb2 - aa2 * bb1;
            double dx = cc1 * bb2 - cc2 * bb1;
            double dy = aa1 * cc2 - aa2 * cc1;
            if ((xa == xb && ya == yb) && (xc == xa && yc == ya) && (xc == xb && yc == yb))
            {
                Thongbao fr = new Thongbao("Lưu ý, 3 điểm vừa nhập phải khác nhau");
                fr.ShowDialog();

            }
            else
            {
                if (d != 0)
                {

                    double a = dx / d;
                    double b = dy / d;
                    double r = (a - xa) * (a - xa) + (b - ya) * (b - ya);
                    txtExpression.Text = "sqrt(-x*x";
                    if (a > 0)
                    {
                        txtExpression.Text += "+" + Math.Round(2*a, 5);
                    }
                    else
                    {
                        txtExpression.Text += "-" + -Math.Round(a, 5);
                    }
                    txtExpression.Text += "*x";
                    if (r-a*a > 0)
                    {
                        txtExpression.Text += "+" + Math.Round(r - a * a, 5) + ")";

                    }
                    else
                    {
                        txtExpression.Text += "-" + -Math.Round(r - a * a, 5) + ")";
                    }
                    if (b > 0)
                    {
                        txtExpression.Text += "+" + Math.Round(b, 5);
                    }
                    else
                    {
                        txtExpression.Text += "-" + -Math.Round(b, 5);
                    }
                    this.CheckDuplication();
                    txtExpression.Text = "-sqrt(-x*x";
                    if (a > 0)
                    {
                        txtExpression.Text += "+" + Math.Round(2 * a, 5);
                    }
                    else
                    {
                        txtExpression.Text += "-" + -Math.Round(a, 5);
                    }
                    txtExpression.Text += "*x";
                    if (r - a * a > 0)
                    {
                        txtExpression.Text += "+" + Math.Round(r - a * a, 5) + ")";

                    }
                    else
                    {
                        txtExpression.Text += "-" + -Math.Round(r - a * a, 5) + ")";
                    }
                    if (b > 0)
                    {
                        txtExpression.Text += "+" + Math.Round(b, 5);
                    }
                    else
                    {
                        txtExpression.Text += "-" + -Math.Round(b, 5);
                    }
                    this.CheckDuplication();
                }

            }                
           
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

        }

        private void ve3_Click(object sender, EventArgs e)
        {

        }

        private void ve4_Click(object sender, EventArgs e)
        {
             double xo = Convert.ToDouble(txt19.Text);
             double yo = Convert.ToDouble(txt20.Text);
             double a = Convert.ToDouble(txt21.Text);
             double b = Convert.ToDouble(txt22.Text);
             double r = Convert.ToDouble(txt23.Text);

             // double bb = chua biet;
             // double cc = chua biet;
             if (r <= 0)
             {
                 Thongbao fr = new Thongbao("Lưu ý, R > 0");
                 fr.ShowDialog();
             }
             else
             {
                 textBox2.Text = "ĐT' (C) có tâm I (" + Math.Round(a, 5) + "; " + Math.Round(b, 5) + ") và bán kính R = " + Math.Round(r, 5);
                 textBox2.Text += Environment.NewLine + "ĐT Δ qua M (" + Math.Round(xo, 5) + "; " + Math.Round(yo, 5) + ") có PT :";
                 textBox2.Text += Environment.NewLine + "     A(x";
                 if (xo > 0)
                 {
                     textBox2.Text += " - " + Math.Round(xo, 5);
                 }
                 if (xo < 0)
                 {
                     textBox2.Text += " + " + -Math.Round(xo, 5);
                 }
                 textBox2.Text += ") + B(y";
                 if (yo > 0)
                 {
                     textBox2.Text += " - " + Math.Round(yo, 5);
                 }
                 if (yo < 0)
                 {
                     textBox2.Text += " + " + -Math.Round(yo, 5);
                 }
                 textBox2.Text += ") = 0 (Aᶻ + Bᶻ ≠ 0)";
                 textBox2.Text += Environment.NewLine + "Khoảng cách từ I (" + Math.Round(a, 5) + "; " + Math.Round(b, 5) + ") tới ĐT Δ là :";
                 textBox2.Text += Environment.NewLine + "     d(I; Δ) = |";
                 if ((a - xo) != 0)
                 {
                     textBox2.Text += Math.Round(a - xo, 5) + "A";
                 }
                 if ((b - yo) > 0)
                 {
                     textBox2.Text += " + " + Math.Round(b - yo, 5) + "B";
                 }
                 if ((b - yo) < 0)
                 {
                     textBox2.Text += " - " + -Math.Round(b - yo, 5) + "B";

                 }
                 textBox2.Text += "| / √(Aᶻ + Bᶻ) = " + Math.Round(r, 5);
                 textBox2.Text += Environment.NewLine + " <=> |";
                 if ((a - xo) != 0)
                 {
                     textBox2.Text += Math.Round(a - xo, 5) + "A";
                 }
                 if ((b - xo) > 0)
                 {
                     textBox2.Text += " + " + Math.Round(b - yo, 5) + "B";
                 }
                 if ((b - yo) < 0)
                 {
                     textBox2.Text += " - " + -Math.Round(b - yo, 5) + "B";
                 }
                 textBox2.Text += "| = " + Math.Round(r, 5) + "√(Aᶻ + Bᶻ)";
                 textBox2.Text += Environment.NewLine + " <=> ";
                 if ((a - xo) != 0)
                 {
                     textBox2.Text += Math.Round((a - xo) * (a - xo), 5) + "Aᶻ";
                 }
                 if ((b - yo) != 0)
                 {
                     textBox2.Text += " + " + Math.Round((b - yo) * (b - yo), 5) + "Bᶻ";
                 }
                 if (2 * (a - xo) * (b - yo) > 0)
                 {
                     textBox2.Text += " + " + 2 * (a - xo) * (b - yo) + "AB";
                 }
                 if (2 * (a - xo) * (b - yo) < 0)
                 {
                     textBox2.Text += " - " + -2 * (a - xo) * (b - yo) + "AB";
                 }
                 textBox2.Text += " = " + Math.Round(r * r, 5) + "(Aᶻ + Bᶻ)";
                 textBox2.Text += Environment.NewLine + " <=> ";
                 if (((a - xo) * (a - xo) - r * r) != 0)
                 {
                     textBox2.Text += Math.Round((a - xo) * (a - xo) - r * r, 5) + "Aᶻ";
                 }
                 if ((b - yo) * (b - yo) - r * r > 0)
                 {
                     textBox2.Text += " + " + Math.Round((b - yo) * (b - yo) - r * r, 5) + "Bᶻ";
                 }
                 if ((b - yo) * (b - yo) - r * r < 0)
                 {
                     textBox2.Text += " - " + -Math.Round((b - yo) * (b - yo) - r * r, 5) + "Bᶻ";
                 }
                 if (2 * (a - xo) * (b - yo) > 0)
                 {
                     textBox2.Text += " + " + Math.Round(2 * (a - xo) * (b - yo), 5) + "AB";
                 }
                 if (2 * (a - xo) * (b - yo) < 0)
                 {
                     textBox2.Text += " - " + -Math.Round(2 * (a - xo) * (b - yo), 5) + "AB";
                 }
                 textBox2.Text += " = 0";


                 // ve duongtron
                 
                 txtExpression.Text = "sqrt(-x*x";
                 if (a > 0)
                 {
                     txtExpression.Text += "+" + Math.Round(2 * a, 5);
                 }
                 else
                 {
                     txtExpression.Text += "-" + -Math.Round(2* a, 5);
                 }
                 txtExpression.Text += "*x";
                 if (r - a * a > 0)
                 {
                     txtExpression.Text += "+" + Math.Round(r - a * a, 5) + ")";

                 }
                 else
                 {
                     txtExpression.Text += "-" + -Math.Round(r - a * a, 5) + ")";
                 }
                 if (b > 0)
                 {
                     txtExpression.Text += "+" + Math.Round(b, 5);
                 }
                 else
                 {
                     txtExpression.Text += "-" + -Math.Round(b, 5);
                 }
                 this.CheckDuplication();
                 txtExpression.Text = "-sqrt(-x*x";
                 if (a > 0)
                 {
                     txtExpression.Text += "+" + Math.Round(2 * a, 5);
                 }
                 else
                 {
                     txtExpression.Text += "-" + -Math.Round(2 * a, 5);
                 }
                 txtExpression.Text += "*x";
                 if (r - a * a > 0)
                 {
                     txtExpression.Text += "+" + Math.Round(r - a * a, 5) + ")";

                 }
                 else
                 {
                     txtExpression.Text += "-" + -Math.Round(r - a * a, 5) + ")";
                 }
                 if (b > 0)
                 {
                     txtExpression.Text += "+" + Math.Round(b, 5);
                 }
                 else
                 {
                     txtExpression.Text += "-" + -Math.Round(b, 5);
                 }
                 this.CheckDuplication();





                 int g = 2;
                 double cc = g * g * ((a - xo) * (a - xo) - r * r);
                 double bb = 2 * g * (a - xo) * (b - yo);
                 double aa = ((b - yo) * (b - yo) - r * r);
                 double delta = bb * bb - 4 * aa * cc;
                 textBox2.Text += Environment.NewLine + "Chọn A = " + g;
                 if (aa == 0)
                 {
                     if (bb == 0)
                     {
                         if (cc == 0)
                         {
                             textBox2.Text += Environment.NewLine + g + "x + By + C = 0 (B, C thuộc R)";
                         }
                         else
                         {
                             textBox2.Text = "PT ĐT không tồn tại";
                         }
                     }
                     else
                     {
                         textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ : " + Environment.NewLine + g + "x";
                         if (-cc / bb > 0)
                         {
                             textBox2.Text += " + " + Math.Round(-cc / bb, 5) + "y";

                         }
                         if (-cc / bb < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(-cc / bb, 5) + "y";

                         }
                         if (g * -xo - (-cc / bb) * yo > 0)
                         {
                             textBox2.Text += " + " + Math.Round(g * -xo - (-cc / bb) * yo, 5);

                         }
                         if (g * -xo - (-cc / bb) * yo < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(g * -xo - (-cc / bb) * yo, 5);

                         }
                         textBox2.Text += " = 0";
                         if (cc / bb != 0)
                         {
                             txtExpression.Text = Math.Round(-g*bb/cc,5) + "*x";
                             if ((g * -xo - (-cc / bb) * yo) / (-cc / bb) > 0)
                             {
                                 txtExpression.Text += "+" + Math.Round((g * -xo - (-cc / bb) * yo)/(-cc/bb),5);
                             }
                             if ((g * -xo - (-cc / bb) * yo) / (-cc / bb) < 0)
                             {
                                 txtExpression.Text += "-" + -Math.Round((g * -xo - (-cc / bb) * yo)/(-cc/bb), 5);
                             }
                             this.CheckDuplication();
                         }
                     }
                 }
                 else
                 {
                     if (delta > 0)
                     {
                         double x1 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                         double x2 = (-bb - Math.Sqrt(delta)) / (2 * aa);
                         textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ₁ : " + Environment.NewLine + g + "x";
                         if (x1 > 0)
                         {
                             textBox2.Text += " + " + Math.Round(x1, 5) + "y";

                         }
                         if (x1 < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(x1, 5) + "y";

                         }
                         if ((g * -xo - (-cc / bb) * yo) / (x1) > 0)
                         {
                             textBox2.Text += " + " + Math.Round((g * -xo - (-cc / bb) * yo) / (x1), 5);

                         }
                         if ((g * -xo - (-cc / bb) * yo) / (x1) < 0)
                         {
                             textBox2.Text += " - " + -Math.Round((g * -xo - (-cc / bb) * yo) / (x1), 5);

                         }
                         textBox2.Text += " = 0";
                         // ve dt thu nhat
                         if (x1 != 0)
                         {
                             txtExpression.Text = Math.Round(-g / x1, 5) + "*x";
                             if ((g * -xo - (x2) * yo) / (x2) > 0)
                             {
                                 txtExpression.Text += "-" + Math.Round((g * -xo - (x2) * yo) / (x2), 5);
                             }
                             if ((g * -xo - (x2) * yo) / (x2)  < 0)
                             {
                                 txtExpression.Text += "+" + -Math.Round((g * -xo - (x2) * yo) / (x2), 5);
                             }
                             this.CheckDuplication();
                         }

















                         textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ₂ : " + Environment.NewLine + g + "x";
                         if (x2 > 0)
                         {
                             textBox2.Text += " + " + Math.Round(x2, 5) + "y";

                         }
                         if (x2 < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(x2, 5) + "y";

                         }
                         if (g * -xo - x2 * yo > 0)
                         {
                             textBox2.Text += " + " + Math.Round(g * -xo - x2 * yo, 5);

                         }
                         if (g * -xo - x2 * yo < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(g * -xo - x2 * yo, 5);

                         }
                         textBox2.Text += " = 0";
                         // ve dt thu nhat
                         if (x2 != 0)
                         {
                             txtExpression.Text = Math.Round(-g / x2, 5) + "*x";
                             if ((g * -xo - (x1) * yo) / (x1) > 0)
                             {
                                 txtExpression.Text += "-" + Math.Round((g * -xo - (x1) * yo) / (x1), 5);
                             }
                             if ((g * -xo - (x1) * yo) / (x1) < 0)
                             {
                                 txtExpression.Text += "+" + -Math.Round((g * -xo - (x1) * yo) / (x1), 5);
                             }
                             this.CheckDuplication();
                         }


                     }
                     if (delta == 0)
                     {
                         double x1 = (-bb) / (2 * aa);
                         textBox2.Text += Environment.NewLine + "Ta được tiếp tuyến Δ : " + Environment.NewLine + g + "x";
                         if (x1 > 0)
                         {
                             textBox2.Text += " + " + Math.Round(x1, 5) + "y";

                         }
                         if (x1 < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(x1, 5) + "y";

                         }
                         if (g * -xo - x1 * yo > 0)
                         {
                             textBox2.Text += " + " + Math.Round(g * -xo - x1 * yo, 5);

                         }
                         if (g * -xo - x1 * yo < 0)
                         {
                             textBox2.Text += " - " + -Math.Round(g * -xo - x1 * yo, 5);

                         }
                         textBox2.Text += " = 0";
                         // ve dt thu nhat
                         if (x1 != 0)
                         {
                             txtExpression.Text = Math.Round(-g / x1, 5) + "*x";
                             if ((g * -xo - (x1) * yo) / (x1) > 0)
                             {
                                 txtExpression.Text += "-" + Math.Round((g * -xo - (x1) * yo) / (x1), 5);
                             }
                             if ((g * -xo - (x1) * yo) / (x1) < 0)
                             {
                                 txtExpression.Text += "+" + -Math.Round((g * -xo - (x1) * yo) / (x1), 5);
                             }
                             this.CheckDuplication();
                         }
                     }
                     if (delta < 0)
                     {
                         textBox2.Text = "PT ĐT không tồn tại";


                     }
                 }
             }
                 
           
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

        }

        private void ve6_Click_1(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(txt33.Text); a = a / (-2);
            double b = Convert.ToDouble(txt34.Text); b = b / (-2);
            double c = Convert.ToDouble(txt35.Text);
            if (a * a + b * b - c <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, PT ĐT' phải tồn tại");
                fr.ShowDialog();
            }
            else
            {
                double r = Math.Sqrt(a * a + b * b - c);
                txtExpression.Text = "sqrt(-x*x";
                if (a > 0)
                {
                    txtExpression.Text += "+" + Math.Round(2 * a, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(2 * a, 5);
                }
                txtExpression.Text += "*x";
                if (r - a * a > 0)
                {
                    txtExpression.Text += "+" + Math.Round(r - a * a, 5) + ")";

                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(r - a * a, 5) + ")";
                }
                if (b > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(-x*x";
                if (a > 0)
                {
                    txtExpression.Text += "+" + Math.Round(2 * a, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(2 * a, 5);
                }
                txtExpression.Text += "*x";
                if (r - a * a > 0)
                {
                    txtExpression.Text += "+" + Math.Round(r - a * a, 5) + ")";

                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(r - a * a, 5) + ")";
                }
                if (b > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b, 5);
                }
                this.CheckDuplication();
            }
                      

                    

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
            double xa = Convert.ToDouble(txt36.Text);
            double ya = Convert.ToDouble(txt37.Text);
            double xb = Convert.ToDouble(txt38.Text);
            double yb = Convert.ToDouble(txt39.Text);
            if (xa == xb && ya == yb)
            {
                Thongbao fr = new Thongbao("Lưu ý, tọa độ 2 tâm I phải khác nhau");
                fr.ShowDialog();
            }
            else
            {
                
                if (xa == xb)
                {
                    if (ya == yb)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, tọa độ 2 tâm I phải khác nhau");
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
                        txtExpression.Text = Math.Round(ya, 5).ToString();
                    }
                    else
                    {
                        if (Math.Round((ya * (xa - xb) + xa * (yb - ya)) / (xa - xb), 5) > 0)
                        {
                            txtExpression.Text = -Math.Round((yb - ya) / (xa - xb), 5) + "*x+" + Math.Round((ya * (xa - xb) + xa * (yb - ya)) / (xa - xb), 5);

                        }
                        else
                        {
                            txtExpression.Text = -Math.Round((yb - ya) / (xa - xb), 5) + "*x-" + -Math.Round((ya * (xa - xb) + xa * (yb - ya)) / (xa - xb), 5);

                        }
                    }
                }


                this.CheckDuplication();   
            }
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

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
            txt6.Text = "";
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

        private void txt5_DoubleClick(object sender, EventArgs e)
        {
            txt5.Text = txtOutput.Text.ToString();
        }

        private void txt6_DoubleClick(object sender, EventArgs e)
        {
            txt6.Text = txtOutput.Text.ToString();
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage8.Show();
        }
    }
}