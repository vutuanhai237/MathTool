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
    public partial class Thuoctinhhinhhoc : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region"maytinh
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
        public Thuoctinhhinhhoc()
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

        private void Thuoctinhhinhhoc_Load(object sender, EventArgs e)
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
            if ((txt10.Text == "") || (txt11.Text == "") || (txt12.Text == "") || (txt13.Text == "") || (txt14.Text == "") || (txt15.Text == "") || (txt16.Text == "") || (txt17.Text == "") || (txt18.Text == ""))
            {
                ve1.Enabled = false;

            }
            else
            {
                ve1.Enabled = true;
            }

            if ((txt4.Text == "") || (txt5.Text == "") || (txt6.Text == "") || (txt7.Text == "") || (txt8.Text == "") || (txt9.Text == ""))
            {
                ve2.Enabled = false;

            }
            else
            {
                ve2.Enabled = true;
            }

            if ((txt1.Text == "") || (txt2.Text == "") )
            {
                ve3.Enabled = false;

            }
            else
            {
                ve3.Enabled = true;
            }

            if ((txt3.Text == ""))
            {
                ve4.Enabled = false;

            }
            else
            {
                ve4.Enabled = true;
            }
        }

        #region"moitem
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage5.Show();
        }
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage6.Show();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage1.Show();
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage3.Show();
        }
        private void simpleButton33_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region"thuoctinhhyberpol
        private void simpleButton32_Click(object sender, EventArgs e)
        {
            txt1.Text = "";
            txt2.Text = "";
            textBox1.Text = "";
        }

        private void simpleButton33_Click_1(object sender, EventArgs e)
        {
            if (txt1.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập aᶻ");
                fr.ShowDialog();
            }
            else
            {
                if (txt2.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập bᶻ");
                    fr.ShowDialog();
                }
                else
                {
                    double a2 = Convert.ToDouble(txt1.Text);
                    double b2 = Convert.ToDouble(txt2.Text);
                    if (a2 <= 0 || b2 <= 0)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, aᶻ và bᶻ > 0");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = Math.Sqrt(a2);
                        double b = Math.Sqrt(b2);
                        double c = Math.Sqrt(a2 + b2);
                        textBox1.Text = " Tâm O (0; 0)";
                        textBox1.Text += Environment.NewLine + "Độ dài trục thực trên Ox : " + Math.Round(2 * a, 5);
                        textBox1.Text += Environment.NewLine + "Độ dài trục ảo trên Oy : " + Math.Round(b * 2, 5);
                        textBox1.Text += Environment.NewLine + "Tiêu điểm trái F1 (" + Math.Round(-c, 5) + "; 0)";
                        textBox1.Text += Environment.NewLine + "Tiêu điểm phải F2 (0; " + Math.Round(c, 5) + "; 0)";
                        textBox1.Text += Environment.NewLine + "Độ dài tiêu cự 2c = " + Math.Round(2 * c, 5);

                        textBox1.Text += Environment.NewLine + "Tọa độ các đỉnh của Hyberpol : A1 (" + Math.Round(-a, 5) + "; 0) và A2 (" + Math.Round(a, 5) + "; 0)";
                        textBox1.Text += Environment.NewLine + "Tâm sai e : " + Math.Round(c / a, 5);
                        textBox1.Text += Environment.NewLine + "Bán kính qua tiêu điểm : MF1 = " + Math.Round(Math.Sqrt(a), 5);
                        if (c / a > 0)
                        {
                            textBox1.Text += " + " + Math.Round(c / a, 5) + "x₀";
                        }
                        if (c / a < 0)
                        {
                            textBox1.Text += " - " + -Math.Round(c / a, 5) + "x₀";
                        }
                        textBox1.Text += Environment.NewLine + "                                    MF2 = " + Math.Round(c / a, 5) + "x₀";
                        if (a > 0)
                        {
                            textBox1.Text += " - " + Math.Round(a, 5);
                        }
                        if (a < 0)
                        {
                            textBox1.Text += " + " + -Math.Round(a, 5);
                        }
                        textBox1.Text += " (x > 0)";
                        textBox1.Text += Environment.NewLine + "                           Hoặc MF1 = -(" + Math.Round(Math.Sqrt(a), 5);
                        if (c / a > 0)
                        {
                            textBox1.Text += " + " + Math.Round(c / a, 5) + "x₀)";
                        }
                        if (c / a < 0)
                        {
                            textBox1.Text += " - " + -Math.Round(c / a, 5) + "x₀)";
                        }
                        textBox1.Text += Environment.NewLine + "                                    MF2 = -(" + Math.Round(c / a, 5) + "x₀";
                        if (a > 0)
                        {
                            textBox1.Text += " - " + Math.Round(a, 5);
                        }
                        if (a < 0)
                        {
                            textBox1.Text += " + " + -Math.Round(a, 5);
                        }
                        textBox1.Text += ") (x < 0)";
                        textBox1.Text += Environment.NewLine + "PT các đường tiệm cận y = |±" + Math.Round(Math.Abs(b / a), 5) + "x|";

                    }
                }
            }
        }

        private void txt1_DoubleClick(object sender, EventArgs e)
        {
            txt1.Text = txtOutput.Text;
        }

        private void txt2_DoubleClick(object sender, EventArgs e)
        {
            txt2.Text = txtOutput.Text;
        }
        #endregion

        #region"thuoctinhparapol
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txt3.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2p");
                fr.ShowDialog();
            }
            else
            {
                double p = Convert.ToDouble(txt3.Text);
                p = p / 2;
                if (p <= 0)
                {
                    Thongbao fr = new Thongbao("Lưu ý, p > 0");
                    fr.ShowDialog();
                }
                else
                {

                    textBox2.Text = " Tâm O (0; 0)";
                    textBox2.Text += Environment.NewLine + "Tham số tiêu p : " + Math.Round(p, 5);
                    textBox2.Text += Environment.NewLine + "Tiêu điểm F : (" + Math.Round(p / 2, 5) + "; 0)";
                    textBox2.Text += Environment.NewLine + "Bán kính qua tiêu điểm : MF = x0 + " + Math.Round(p / 2, 5);
                    textBox2.Text += Environment.NewLine + "Đường chuẩn : x = -" + Math.Round(p / 2, 5);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txt3.Text = "";
            textBox2.Clear();
        }
        private void txt3_DoubleClick(object sender, EventArgs e)
        {
            txt3.Text = txtOutput.Text;
        }

        #endregion

        #region"thuoctinhtamgiac3dinh
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            txt4.Text = "";
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
            txt9.Text = "";
            textBox3.Text = "";

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (txt4.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số xA");
                fr.ShowDialog();
            }
            else
            {
                if (txt5.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số xA");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt6.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số xB");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt7.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số xB");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt8.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số xC");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt9.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số xC");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double xa = Convert.ToDouble(txt4.Text);
                                    double ya = Convert.ToDouble(txt5.Text);
                                    double xb = Convert.ToDouble(txt6.Text);
                                    double yb = Convert.ToDouble(txt7.Text);
                                    double xc = Convert.ToDouble(txt8.Text);
                                    double yc = Convert.ToDouble(txt9.Text);
                                    if (xa == xb && ya == yb)
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, đỉnh A và đỉnh B phải khác nhau");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (xb == xc && yb == yc)
                                        {
                                            Thongbao fr = new Thongbao("Lưu ý, đỉnh B và đỉnh C phải khác nhau");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if (xc == xa && yc == ya)
                                            {
                                                Thongbao fr = new Thongbao("Lưu ý, đỉnh C và đỉnh A phải khác nhau");
                                                fr.ShowDialog();
                                            }
                                            else
                                            {
                                                double a = Math.Sqrt((xc - xb) * (xc - xb) + (yc - yb) * (yc - yb));
                                                double b = Math.Sqrt((xa - xc) * (xa - xc) + (ya - yc) * (ya - yc));
                                                double c = Math.Sqrt((xa - xb) * (xa - xb) + (ya - yb) * (ya - yb));
                                                double p = (a + b + c) / 2;
                                                double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                                                double d = -(xb - xa) * (yc - ya) + (xc - xa) * (yb - ya);
                                                double dx = -((xb - xa) * xc + (yb - ya) * yc) * (yc - ya) + ((xc - xa) * xb + (yc - ya) * yb) * (yb - ya);
                                                double dy = (xb - xa) * ((xc - xa) * xb + (yc - ya) * yb) - (xc - xa) * ((xb - xa) * xc + (yb - ya) * yc);
                                                if (d == 0)
                                                {
                                                    if (dx == 0 && dy == 0)
                                                    {
                                                        Thongbao fr = new Thongbao("Lưu ý, tam giác ABC phải tồn tại");
                                                        fr.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        Thongbao fr = new Thongbao("Lưu ý, tam giác ABC tồn tại vô số");
                                                        fr.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    textBox3.Text = "Phương trình đường thẳng AB : " + Environment.NewLine + "";
                                                    if (yb - ya != 0)
                                                    {
                                                        textBox3.Text += Math.Round(yb - ya, 5) + "x";
                                                    }
                                                    if (xa - xb > 0)
                                                    {
                                                        textBox3.Text += " + " + Math.Round(xa - xb, 5) + "y";
                                                    }
                                                    if (xa - xb < 0)
                                                    {
                                                        textBox3.Text += " - " + -Math.Round(xa - xb, 5) + "y";
                                                    }
                                                    if (-(ya * (xa - xb) + xa * (yb - ya)) > 0)
                                                    {
                                                        textBox3.Text += " + " + -Math.Round((ya * (xa - xb) + xa * (yb - ya)), 5);
                                                    }
                                                    if (-(ya * (xa - xb) + xa * (yb - ya)) < 0)
                                                    {
                                                        textBox3.Text += " - " + Math.Round((ya * (xa - xb) + xa * (yb - ya)), 5);
                                                    }
                                                    textBox3.Text += " = 0, AB = " + Math.Round(c, 5);
                                                    textBox3.Text += Environment.NewLine + "Phương trình đường thẳng BC : " + Environment.NewLine + "";
                                                    if (yc - yb != 0)
                                                    {
                                                        textBox3.Text += Math.Round(yc - yb, 5) + "x";
                                                    }
                                                    if (xb - xc > 0)
                                                    {
                                                        textBox3.Text += " + " + Math.Round(xb - xc, 5) + "y";
                                                    }
                                                    if (xb - xc < 0)
                                                    {
                                                        textBox3.Text += " - " + -Math.Round(xb - xc, 5) + "y";
                                                    }
                                                    if (-(yb * (xb - xc) + xb * (yc - yb)) > 0)
                                                    {
                                                        textBox3.Text += " + " + -Math.Round((yb * (xb - xc) + xb * (yc - yb)), 5);
                                                    }
                                                    if (-(yb * (xb - xc) + xb * (yc - yb)) < 0)
                                                    {
                                                        textBox3.Text += " - " + Math.Round((yb * (xb - xc) + xb * (yc - yb)), 5);
                                                    }
                                                    textBox3.Text += " = 0, BC = " + Math.Round(a, 5);
                                                    textBox3.Text += Environment.NewLine + "Phương trình đường thẳng CA : " + Environment.NewLine + "";
                                                    if (ya - yc != 0)
                                                    {
                                                        textBox3.Text += Math.Round(ya - yc, 5) + "x";
                                                    }
                                                    if (xc - xa > 0)
                                                    {
                                                        textBox3.Text += " + " + Math.Round(xc - xa, 5) + "y";
                                                    }
                                                    if (xc - xa < 0)
                                                    {
                                                        textBox3.Text += " - " + -Math.Round(xc - xa, 5) + "y";
                                                    }
                                                    if (-(yc * (xc - xa) + xc * (ya - yc)) > 0)
                                                    {
                                                        textBox3.Text += " + " + -Math.Round((yc * (xc - xa) + xc * (ya - yc)), 5);
                                                    }
                                                    if (-(yc * (xc - xa) + xc * (ya - yc)) > 0)
                                                    {
                                                        textBox3.Text += " - " + Math.Round((yc * (xc - xa) + xc * (ya - yc)), 5);
                                                    }
                                                    textBox3.Text += " = 0, CA = " + Math.Round(b, 5);
                                                    textBox3.Text += Environment.NewLine + "Chu vi C = " + Math.Round(2 * p, 5) + ", diện tích S = " + Math.Round(s, 5);
                                                    textBox3.Text += Environment.NewLine + "Tọa độ trọng tâm G (" + Math.Round((xa + xb + xc) / 3, 5) + "; " + Math.Round((ya + yb + yc) / 3, 5) + ")";
                                                    textBox3.Text += Environment.NewLine + "Tọa độ trực tâm H (" + Math.Round(dx / d, 5) + "; " + Math.Round(dy / d, 5) + ")";
                                                    textBox3.Text += Environment.NewLine + "Độ dài đường trung tuyến kẻ từ A : " + Math.Round(Math.Sqrt((b * b + c * c) / 2 - (a * a) / 4), 5);
                                                    textBox3.Text += Environment.NewLine + "Độ dài đường trung tuyến kẻ từ B : " + Math.Round(Math.Sqrt((c * c + a * a) / 2 - (b * b) / 4), 5);
                                                    textBox3.Text += Environment.NewLine + "Độ dài đường trung tuyến kẻ từ C : " + Math.Round(Math.Sqrt((a * a + b * b) / 2 - (c * c) / 4), 5);
                                                    textBox3.Text += Environment.NewLine + "Bán kính ĐT' ngoại tiếp R = " + Math.Round((a * b * c) / (4 * s), 5) + ", bán kính ĐT' nội tiếp r = " + Math.Round(s / p, 5);
                                                    textBox3.Text += Environment.NewLine + "Số đo góc A : " + Math.Round(Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / Math.PI, 5) + "⁰";
                                                    textBox3.Text += Environment.NewLine + "Số đo góc B : " + Math.Round(Math.Acos((c * c + a * a - b * b) / (2 * c * a)) * 180 / Math.PI, 5) + "⁰";
                                                    textBox3.Text += Environment.NewLine + "Số đo góc C : " + Math.Round(Math.Acos((a * a + b * b - c * c) / (2 * a * b)) * 180 / Math.PI, 5) + "⁰";
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

        private void txt4_DoubleClick(object sender, EventArgs e)
        {
            txt4.Text = txtOutput.Text.ToString();
        }
        private void txt6_DoubleClick(object sender, EventArgs e)
        {
            txt6.Text = txtOutput.Text.ToString();

        }
        private void txt8_DoubleClick(object sender, EventArgs e)
        {
            txt8.Text = txtOutput.Text.ToString();

        }
        private void txt5_DoubleClick(object sender, EventArgs e)
        {
            txt5.Text = txtOutput.Text.ToString();

        }
        private void txt7_DoubleClick(object sender, EventArgs e)
        {
            txt7.Text = txtOutput.Text.ToString();

        }
        private void txt9_DoubleClick(object sender, EventArgs e)
        {
            txt9.Text = txtOutput.Text.ToString();

        }
        #endregion
        private void simpleButton16_Click(object sender, EventArgs e)
        {
            txt10.Text = "";
            txt11.Text = "";
            txt12.Text = "";
            txt13.Text = "";
            txt14.Text = "";
            txt15.Text = "";
            txt16.Text = "";
            txt17.Text = "";
            txt18.Text = "";
            textBox4.Text = "";

        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if (txt10.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số a₁");
                fr.ShowDialog();
            }
            else
            {
                if (txt11.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số b₁");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt12.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số c₁");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt13.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số a₂");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt14.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số b₂");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt15.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số c₂");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    if (txt16.Text == "")
                                    {
                                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số a₃");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        if (txt17.Text == "")
                                        {
                                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số b₃");
                                            fr.ShowDialog();
                                        }
                                        else
                                        {
                                            if (txt18.Text == "")
                                            {
                                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số c₃");
                                                fr.ShowDialog();
                                            }
                                            else
                                            {
                                                double a1 = Convert.ToDouble(txt10.Text);
                                                double b1 = Convert.ToDouble(txt11.Text);
                                                double c1 = -Convert.ToDouble(txt12.Text);
                                                double a2 = Convert.ToDouble(txt13.Text);
                                                double b2 = Convert.ToDouble(txt14.Text);
                                                double c2 = -Convert.ToDouble(txt15.Text);
                                                double a3 = Convert.ToDouble(txt16.Text);
                                                double b3 = Convert.ToDouble(txt17.Text);
                                                double c3 = -Convert.ToDouble(txt18.Text);
                                                double dab = a1 * b2 - a2 * b1;
                                                double dxab = c1 * b2 - c2 * b1;
                                                double dyab = a1 * c2 - a2 * c1;
                                                double dbc = a2 * b3 - a3 * b2;
                                                double dxbc = c2 * b3 - c3 * b2;
                                                double dybc = a2 * c3 - a3 * c2;
                                                double dca = a3 * b1 - a1 * b3;
                                                double dxca = c3 * b1 - c1 * b3;
                                                double dyca = a3 * c1 - a1 * c3;
                                                if (dab == 0 || dbc == 0 || dca == 0)
                                                {
                                                    Thongbao fr = new Thongbao("Lưu ý, Tam giác ABC phải tồn tại");
                                                    fr.ShowDialog();
                                                }
                                                else
                                                {

                                                    double xa = dxca / dca;
                                                    double ya = dyca / dca;
                                                    double xb = dxab / dab;
                                                    double yb = dyab / dab;
                                                    double xc = dxbc / dbc;
                                                    double yc = dybc / dbc;
                                                    textBox4.Text = "Đỉnh A (" + Math.Round(dxca / dca, 5) + ";" + Math.Round(dyca / dca, 5) + ")";
                                                    textBox4.Text += Environment.NewLine + "Đỉnh B (" + Math.Round(dxab / dab, 5) + ";" + Math.Round(dyab / dab, 5) + ")";
                                                    textBox4.Text += Environment.NewLine + "Đỉnh C (" + Math.Round(dxbc / dbc, 5) + ";" + Math.Round(dybc / dbc, 5) + ")";
                                                    if (xa == xb && ya == yb)
                                                    {
                                                        Thongbao fr = new Thongbao("Lưu ý, điểm A và điểm B phải khác nhau");
                                                        fr.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        if (xb == xc && yb == yc)
                                                        {
                                                            Thongbao fr = new Thongbao("Lưu ý, điểm B và điểm C phải khác nhau");
                                                            fr.ShowDialog();
                                                        }
                                                        else
                                                        {
                                                            if (xc == xa && yc == ya)
                                                            {
                                                                Thongbao fr = new Thongbao("Lưu ý, điểm C và điểm A phải khác nhau");
                                                                fr.ShowDialog();
                                                            }
                                                            else
                                                            {
                                                                double a = Math.Sqrt((xc - xb) * (xc - xb) + (yc - yb) * (yc - yb));
                                                                double b = Math.Sqrt((xa - xc) * (xa - xc) + (ya - yc) * (ya - yc));
                                                                double c = Math.Sqrt((xa - xb) * (xa - xb) + (ya - yb) * (ya - yb));
                                                                double p = (a + b + c) / 2;
                                                                double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                                                                double d = -(xb - xa) * (yc - ya) + (xc - xa) * (yb - ya);
                                                                double dx = -((xb - xa) * xc + (yb - ya) * yc) * (yc - ya) + ((xc - xa) * xb + (yc - ya) * yb) * (yb - ya);
                                                                double dy = (xb - xa) * ((xc - xa) * xb + (yc - ya) * yb) - (xc - xa) * ((xb - xa) * xc + (yb - ya) * yc);
                                                                if (d == 0)
                                                                {
                                                                    if (dx == 0 && dy == 0)
                                                                    {
                                                                        Thongbao fr = new Thongbao("Lưu ý, tam giác ABC phải tồn tại");
                                                                        fr.ShowDialog();
                                                                    }
                                                                    else
                                                                    {
                                                                        Thongbao fr = new Thongbao("Lưu ý, tam giác ABC tồn tại vô số");
                                                                        fr.ShowDialog();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    textBox4.Text = "Đỉnh A (" + Math.Round(dxca / dca, 5) + ";" + Math.Round(dyca / dca, 5) + ")";
                                                                    textBox4.Text += Environment.NewLine + "Đỉnh B (" + Math.Round(dxab / dab, 5) + ";" + Math.Round(dyab / dab, 5) + ")";
                                                                    textBox4.Text += Environment.NewLine + "Đỉnh C (" + Math.Round(dxbc / dbc, 5) + ";" + Math.Round(dybc / dbc, 5) + ")";

                                                                    textBox4.Text += Environment.NewLine + "Chu vi C = " + Math.Round(2 * p, 5) + ", diện tích S = " + Math.Round(s, 5);
                                                                    textBox4.Text += Environment.NewLine + "Tọa độ trọng tâm G (" + Math.Round((xa + xb + xc) / 3, 5) + "; " + Math.Round((ya + yb + yc) / 3, 5) + ")";
                                                                    textBox4.Text += Environment.NewLine + "Tọa độ trực tâm H (" + Math.Round(dx / d, 5) + "; " + Math.Round(dy / d, 5) + ")";
                                                                    textBox4.Text += Environment.NewLine + "Độ dài đường trung tuyến kẻ từ A : " + Math.Round(Math.Sqrt((b * b + c * c) / 2 - (a * a) / 4), 5);
                                                                    textBox4.Text += Environment.NewLine + "Độ dài đường trung tuyến kẻ từ B : " + Math.Round(Math.Sqrt((c * c + a * a) / 2 - (b * b) / 4), 5);
                                                                    textBox4.Text += Environment.NewLine + "Độ dài đường trung tuyến kẻ từ C : " + Math.Round(Math.Sqrt((a * a + b * b) / 2 - (c * c) / 4), 5);
                                                                    textBox4.Text += Environment.NewLine + "Bán kính ĐT' ngoại tiếp R = " + Math.Round((a * b * c) / (4 * s), 5) + ", bán kính ĐT' nội tiếp r = " + Math.Round(s / p, 5);
                                                                    textBox4.Text += Environment.NewLine + "Số đo góc A : " + Math.Round(Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / Math.PI, 5) + "⁰";
                                                                    textBox4.Text += Environment.NewLine + "Số đo góc B : " + Math.Round(Math.Acos((c * c + a * a - b * b) / (2 * c * a)) * 180 / Math.PI, 5) + "⁰";
                                                                    textBox4.Text += Environment.NewLine + "Số đo góc C : " + Math.Round(Math.Acos((a * a + b * b - c * c) / (2 * a * b)) * 180 / Math.PI, 5) + "⁰";
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

        private void xtraTabPage7_Paint(object sender, PaintEventArgs e)
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

        private void xtraTabPage3_Paint(object sender, PaintEventArgs e)
        {

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

        private void simpleButton27_Click(object sender, EventArgs e)
        {
            txt22.Text = "";
            txt23.Text = "";
            txt24.Text = "";
            textBox5.Text = "";
        }

        private void simpleButton38_Click(object sender, EventArgs e)
        {
            txt19.Text = "";
            txt20.Text = "";
            txt21.Text = "";
            textBox6.Text = "";
        }

        private void simpleButton28_Click(object sender, EventArgs e)
        {
            if (txt22.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số a");
                fr.ShowDialog();
            }
            else
            {
                if (txt23.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số b");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt24.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số α⁰ ");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = Convert.ToDouble(txt22.Text);
                        double b = Convert.ToDouble(txt23.Text);
                        double alpha = Convert.ToDouble(txt24.Text);
                        if (alpha > 180 && alpha <= 0)
                        {
                            Thongbao fr = new Thongbao("Lưu ý, số đo góc α⁰ phải thõa mãn");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (a <= 0 || b <= 0)
                            {
                                Thongbao fr = new Thongbao("Lưu ý, a và b > 0");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double c = Math.Sqrt(a * a + b * b - 2 * b * a * Math.Cos(alpha * Math.PI / 180));
                                double A = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / Math.PI;
                                double B = 180 - A - alpha;
                                {
                                    textBox5.Text = "Trong ∆ ABC :";
                                    textBox5.Text += Environment.NewLine + "Số đo góc A = " + Math.Round(A, 5) + "⁰" + ", B = " + Math.Round(B, 5) + "⁰, C = " + Math.Round(alpha, 5) + "⁰";
                                    textBox5.Text += Environment.NewLine + "AB = " + Math.Round(c, 5) + ", BC = " + Math.Round(a, 5) + ", CA = " + Math.Round(b, 5);
                                }
                            }

                        }
                    }
                }
            }
        }

        private void simpleButton39_Click(object sender, EventArgs e)
        {
            if (txt19.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số α₁⁰");
                fr.ShowDialog();
            }
            else
            {
                if (txt20.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số α₂⁰");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt21.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập hệ số a");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double alpha1 = Convert.ToDouble(txt19.Text);
                        double alpha2 = Convert.ToDouble(txt20.Text);
                        double a = Convert.ToDouble(txt21.Text);
                        if (alpha1 > 180 || alpha1 <= 0 || alpha2 > 180 || alpha2 <= 0)
                        {
                            Thongbao fr = new Thongbao("Lưu ý, số đo góc α₁⁰ và α₂⁰ phải thõa mãn");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (a <= 0)
                            {
                                Thongbao fr = new Thongbao("Lưu ý, a > 0");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double alpha3 = 180 - alpha1 - alpha2;
                                double b = a * Math.Sin(alpha1 * Math.PI / 180) / (Math.Sin(alpha3 * Math.PI / 180));
                                double c = a * Math.Sin(alpha2 * Math.PI / 180) / (Math.Sin(alpha3 * Math.PI / 180));
                                {
                                    textBox6.Text = "Trong ∆ ABC :";
                                    textBox6.Text += Environment.NewLine + "Số đo góc A = " + Math.Round(alpha3, 5) + "⁰" + ", B = " + Math.Round(alpha2, 5) + "⁰, C = " + Math.Round(alpha1, 5) + "⁰";
                                    textBox6.Text += Environment.NewLine + "AB = " + Math.Round(c, 5) + ", BC = " + Math.Round(a, 5) + ", CA = " + Math.Round(b, 5);
                                }
                            }

                        }
                    }
                }
            }
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            double a1 = Convert.ToDouble(txt10.Text);
            double b1 = Convert.ToDouble(txt11.Text);
            double c1 = Convert.ToDouble(txt12.Text);
            double a2 = Convert.ToDouble(txt13.Text);
            double b2 = Convert.ToDouble(txt14.Text);
            double c2 = Convert.ToDouble(txt15.Text);
            double a3 = Convert.ToDouble(txt16.Text);
            double b3 = Convert.ToDouble(txt17.Text);
            double c3 = Convert.ToDouble(txt18.Text);

            if (b1 != 0)
            {
                if (-c1 / b1 > 0)
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
                if (-c2 / b2 > 0)
                {
                    txtExpression.Text = Math.Round(-a2 / b2, 5) + "*x+" + Math.Round(-c2 / b2, 5);

                }
                else
                {
                    txtExpression.Text = Math.Round(-a2 / b2, 5) + "*x-" + -Math.Round(-c2 / b2, 5);

                }
            }

            this.CheckDuplication();
            if (b3 != 0)
            {
                if (-c3 / b3 > 0)
                {
                    txtExpression.Text = Math.Round(-a3 / b3, 5) + "*x+" + Math.Round(-c3 / b3, 5);

                }
                else
                {
                    txtExpression.Text = Math.Round(-a3 / b3, 5) + "*x-" + -Math.Round(-c3 / b3, 5);

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
            double xa = Convert.ToDouble(txt4.Text);
            double ya = Convert.ToDouble(txt5.Text);
            double xb = Convert.ToDouble(txt6.Text);
            double yb = Convert.ToDouble(txt7.Text);
            double xc = Convert.ToDouble(txt8.Text);
            double yc = Convert.ToDouble(txt9.Text);
            double a1 = yb - ya;
            double b1 = xa - xb;
            double c1 = -(ya * (xa - xb) + xa * (yb - ya));
            double a2 = yc - yb;
            double b2 = xb - xc;
            double c2 = -(yb * (xb - xc) + xb * (yc - yb));
            double a3 = ya - yc;
            double b3 = xc - xa;
            double c3 = -(yc * (xc - xa) + xc * (ya - yc));

            if (b1 != 0)
            {
                if (-c1 / b1 > 0)
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
                if (-c2 / b2 > 0)
                {
                    txtExpression.Text = Math.Round(-a2 / b2, 5) + "*x+" + Math.Round(-c2 / b2, 5);

                }
                else
                {
                    txtExpression.Text = Math.Round(-a2 / b2, 5) + "*x-" + -Math.Round(-c2 / b2, 5);

                }
            }

            this.CheckDuplication();
            if (b3 != 0)
            {
                if (-c3 / b3 > 0)
                {
                    txtExpression.Text = Math.Round(-a3 / b3, 5) + "*x+" + Math.Round(-c3 / b3, 5);

                }
                else
                {
                    txtExpression.Text = Math.Round(-a3 / b3, 5) + "*x-" + -Math.Round(-c3 / b3, 5);

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
            double a2 = Convert.ToDouble(txt1.Text);
            double b2 = Convert.ToDouble(txt2.Text);
            if (a2 <= b2 || a2 <= 0 || b2 <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý : aᶻ và bᶻ > 0");
                fr.ShowDialog();
            }
            else
            {
                txtExpression.Text = "sqrt(" + Math.Round(b2 / a2, 5) + "*x*x";
                if (-b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(-b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(-b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(b2 / a2, 5) + "*x*x";
                if (-b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(-b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(-b2, 5);
                } this.CheckDuplication();
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

        private void ve4_Click(object sender, EventArgs e)
        {
            double p = Convert.ToDouble(txt3.Text);
            if (p <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý : 2p > 0");
                fr.ShowDialog();
            }
            else
            {
                txtExpression.Text = "sqrt(" + Math.Round(p, 5) + "*x)";
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(p, 5) + "*x";          
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

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage4.Show();
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage7.Show();
        }
    }
}