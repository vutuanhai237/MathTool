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
    public partial class DuongElip : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region'vl
        private void simpleButton45_Click(object sender, EventArgs e)
        {
            txt22.Text = "";
            txt24.Text = "";
            txt26.Text = "";
            txt23.Text = "";
            txt25.Text = "";
            txt27.Text = "";
            textBox11.Text = "";
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
        public DuongElip()
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
        #region'
        private void timer1_Tick(object sender, EventArgs e)
        {

            lblTime.Caption = (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;
            if ((txt1.Text == "") || (txt2.Text == "") )
            {
                ve1.Enabled = false;

            }
            else
            {
                ve1.Enabled = true;
            }
            if ((txt3.Text == "") || (txt4.Text == ""))
            {
                ve2.Enabled = false;

            }
            else
            {
                ve2.Enabled = true;
            }
            if ((txt5.Text == "") || (txt6.Text == ""))
            {
                ve3.Enabled = false;

            }
            else
            {
                ve3.Enabled = true;
            }
            if ((txt7.Text == "") || (txt8.Text == ""))
            {
                ve4.Enabled = false;

            }
            else
            {
                ve4.Enabled = true;
            }
            if ((txt9.Text == "") || (txt10.Text == ""))
            {
                ve5.Enabled = false;

            }
            else
            {
                ve5.Enabled = true;
            }
            if ((txt11.Text == "") || (txt12.Text == "") )
            {
                ve6.Enabled = false;

            }
            else
            {
                ve6.Enabled = true;
            }
            if ((txt13.Text == "") || (txt14.Text == ""))
            {
                ve7.Enabled = false;

            }
            else
            {
                ve7.Enabled = true;
            }
            if ((txt15.Text == "") || (txt16.Text == "") || (txt17.Text == "") || (txt18.Text == ""))
            {
                ve8.Enabled = false;

            }
            else
            {
                ve8.Enabled = true;
            }
            if ((txt19.Text == "") || (txt20.Text == "") || (txt21.Text == ""))
            {
                ve9.Enabled = false;

            }
            else
            {
                ve9.Enabled = true;
            }
            if ((txt22.Text == "") || (txt23.Text == "") || (txt24.Text == "") || (txt25.Text == "") || (txt26.Text == "") || (txt27.Text == ""))
            {
               
            }
            else
            {
            }
        }
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage3.Show();
        }
      
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage4.Show();
        }
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage6.Show();
        }
        private void navBarItem2_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage1.Show();
        }
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage5.Show();
        }
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage7.Show();
        }
        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage8.Show();
        }
        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage9.Show();
        }
       
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage11.Show();
        }
        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtraTabPage12.Show();
        }
        #endregion
#endregion
        private void simpleButton14_Click(object sender, EventArgs e)
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
                    if ( a2 <= b2 || a2 <= 0 || b2 <=0)
                    {
                        Thongbao fr = new Thongbao("Lưu ý : aᶻ > bᶻ > 0");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = Math.Sqrt(a2);
                        double b = Math.Sqrt(b2);
                        textBox1.Text = "Đường Elip : xᶻ / " + a2 + " + yᶻ / " + b2 + " = 1";
                        textBox1.Text += Environment.NewLine + " Tâm O (0, 0)";
                        textBox1.Text += Environment.NewLine + " Độ dài trục lớn : " + Math.Round(2 * a, 5);
                        textBox1.Text += Environment.NewLine + " Độ dài trục bé : " + Math.Round(2 * b, 5);
                        double c = Math.Sqrt(a2 - b2);
                        textBox1.Text += Environment.NewLine + " Tiêu điểm trái F1 : " + Math.Round(c, 5) + "; 0)";
                        textBox1.Text += Environment.NewLine + " Tiêu điểm trái F2 : " + Math.Round(-c, 5) + "; 0)";
                        textBox1.Text += Environment.NewLine + " Tiêu cự : " + Math.Round(c * 2,5);
                        textBox1.Text += Environment.NewLine + " Tâm sai : " + Math.Round(c /a, 5);
                        textBox1.Text += Environment.NewLine + " Tọa độ các HCN cơ sở : A1 (" + Math.Round(-a, 5) + "; 0), A2 (" + Math.Round(a, 5) + "; 0), B1 (0; " + Math.Round(-b, 5) + "), B2 (0; " + Math.Round(b, 5) + ")";
                        textBox1.Text += Environment.NewLine + " Chu vi HCN cơ sở : " + Math.Round((a + b) * 4, 5);
                        textBox1.Text += Environment.NewLine + " Diện tích HCN cơ sở : " + Math.Round(4*a*b, 5);
                        textBox1.Text += Environment.NewLine + " Bán kính qua tiêu điểm MF1 = " + Math.Round(a2, 5) + " + " + Math.Round(c/a,5) + "x, MF2 = " + Math.Round(a2,5) + " - " + Math.Round(c/a,5) + "x";
                        textBox1.Text += Environment.NewLine + " Đường chuẩn x = ± " + Math.Round(a2 / c, 5);
                        textBox1.Text += Environment.NewLine + " Khoảng cách 2 đường chuẩn 2d = " + Math.Round(a2 * 2 / c, 5);




                    }
                }
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            txt1.Text = "";
            txt2.Text = "";
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

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            if (txt3.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2a");
                fr.ShowDialog();
            }
            else
            {
                if (txt4.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2b");
                    fr.ShowDialog();
                }
                else
                {
                    double a = Convert.ToDouble(txt3.Text);
                    a = a /2;
                    double b = Convert.ToDouble(txt4.Text);
                    b = b / 2;
                    if (a <= b || a <= 0 || b <= 0)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, 2a > 2b > 0");
                        fr.ShowDialog();
                    }
                    else
                    {
                        textBox4.Text = "Đường Elip : xᶻ / " + Math.Round(a * a, 5) + " + yᶻ / " + Math.Round(b * b, 5) + " = 1";
                    }
                }
            }
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            txt3.Text = "";
            txt4.Text = "";
            textBox4.Text = "";
        }
        private void simpleButton33_Click(object sender, EventArgs e)
        {
            if (txt5.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2a");
                fr.ShowDialog();
            }
            else
            {
                if (txt6.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2c");
                    fr.ShowDialog();
                }
                else
                {
                    double xa = Convert.ToDouble(txt5.Text);
                    double xb = Convert.ToDouble(txt6.Text);
                    if (xb <=0 || xa <= 0 || xa <=xb)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, 2a > 2c > 0");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = xa /2;
                        double c = xb / 2;
                        double b = Math.Sqrt(a * a - c * c);
                        textBox6.Text = "Đường Elip : xᶻ / " + Math.Round(a * a, 5) + " + yᶻ / " + Math.Round(b * b, 5) + " = 1";
                    }
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txt7.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2a");
                fr.ShowDialog();
            }
            else
            {
                if (txt8.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập e");
                    fr.ShowDialog();
                }
                else
                {
                    double xa = Convert.ToDouble(txt7.Text);
                    double xb = Convert.ToDouble(txt8.Text);
                    if (xa <= 0 || xb <= 0 || xb >= 1)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, 2a > 0 và 0 < e < 1");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = xa / 2;
                        double c = xb * a;
                        double b = Math.Sqrt(a * a - c * c);
                        textBox2.Text = "Đường Elip : xᶻ / " + Math.Round(a * a, 5) + " + yᶻ / " + Math.Round(b * b, 5) + " = 1";
                    }
                }
            }
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (txt9.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2a");
                fr.ShowDialog();
            }
            else
            {
                if (txt10.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập ± a/e");
                    fr.ShowDialog();
                }
                else
                {
                    double xa = Convert.ToDouble(txt9.Text);
                    double d = Convert.ToDouble(txt10.Text);
                    if (xa <= 0 )
                    {
                        Thongbao fr = new Thongbao("Lưu ý, 2a > 0");
                        fr.ShowDialog();
                    }
                    else
                    {
                       
                        double a = xa / 2;
                        double b2 = a*a- (a*a/d)*(a*a/d);
                        if (d <= a )
                        {
                            Thongbao fr = new Thongbao("Lưu ý, ± a/e > a");
                            fr.ShowDialog();
                        }
                        else
                        {
                            textBox3.Text = "Đường Elip : xᶻ / " + Math.Round(a * a, 5) + " + yᶻ / " + Math.Round(b2, 5) + " = 1";

                        }
                    }
                }
            }
        }
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (txt11.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2c");
                fr.ShowDialog();
            }
            else
            {
                if (txt12.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập e");
                    fr.ShowDialog();
                }
                else
                {
                    double c = Convert.ToDouble(txt11.Text);
                    c = c / 2;
                    double e1 = Convert.ToDouble(txt12.Text);
                    if (c <= 0 || e1 <= 0 || e1 >= 1)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, c > 0 và 0 < e < 1");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = c / e1;
                        double b = Math.Sqrt(a * a - c * c);
                        textBox5.Text = "Đường Elip : xᶻ / " + Math.Round(a * a, 5) + " + yᶻ / " + Math.Round(b * b, 5) + " = 1";
                    }
                }
            }
        }
        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if (txt13.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập e");
                fr.ShowDialog();
            }
            else
            {
                if (txt14.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập 2d");
                    fr.ShowDialog();
                }
                else
                {
                    double e1 = Convert.ToDouble(txt13.Text);
                    double d = Convert.ToDouble(txt14.Text);
                    if (d <= 0 || 0 >= e1 || e1 >= 1)
                    {
                        Thongbao fr = new Thongbao("Lưu ý, 0 < e < 1 và 2d > 0");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a = e1 * d / 2;
                        double c = 2 * a * a / d;
                        double b = Math.Sqrt(a * a - c * c);
                        textBox7.Text = "Đường Elip : xᶻ / " + Math.Round(a * a, 5) + " + yᶻ / " + Math.Round(b * b, 5) + " = 1";
                    }
                }
            }
        }
        private void simpleButton26_Click(object sender, EventArgs e)
        {
            if (txt15.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xA");
                fr.ShowDialog();
            }
            else
            {
                if (txt16.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yA");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt17.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập xB");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt18.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập yB");
                            fr.ShowDialog();
                        }
                        else
                        {
                           
                            double xa = Convert.ToDouble(txt15.Text);
                            double ya = Convert.ToDouble(txt16.Text);
                            double xb = Convert.ToDouble(txt17.Text);
                            double yb = Convert.ToDouble(txt18.Text);
                            if ((xa*xa == xb*xb) || (ya*ya == yb*yb) )
                            {
                                Thongbao fr = new Thongbao("Lưu ý, xA phải khác xB, yA phải khác yB");
                                fr.ShowDialog();
                            }
                            else
                            {
                                double a2 = (xa * xa * yb * yb - ya * ya * xb * xb) / (yb * yb - ya * ya);
                                if (a2 - xa * xa != 0)
                                {
                                    double b2 = (a2 * ya * ya) / (a2 - xa * xa);
                                    if (a2 <= b2 || a2 <= 0 || b2 <= 0)
                                    {
                                        Thongbao fr = new Thongbao("Không tồn tại đường Elip thõa mãn điều kiện");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        textBox8.Text = "Đường Elip : xᶻ / " + Math.Round(a2, 5) + " + yᶻ / " + Math.Round(b2, 5) + " = 1";
               
                                    }
                                   
                                }
                                else
                                {
                                    double b2 = (a2 * yb * yb) / (a2 - xb * xb);
                                    if (a2 <= b2 || a2 <= 0 || b2 <= 0)
                                    {
                                        Thongbao fr = new Thongbao("Không tồn tại đường Elip thõa mãn điều kiện");
                                        fr.ShowDialog();
                                    }
                                    else
                                    {
                                        textBox8.Text = "Đường Elip : xᶻ / " + Math.Round(a2, 5) + " + yᶻ / " + Math.Round(b2, 5) + " = 1";
               
                                    }
                                   
                                }

                                
                            }
                        }
                    }
                }
            }
        }
        private void simpleButton42_Click(object sender, EventArgs e)
        {
            if (txt19.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập aᶻ");
                fr.ShowDialog();
            }
            else
            {
                if (txt20.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập bᶻ");
                    fr.ShowDialog();
                }
                else
                {
                    if ( txt21.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập α");
                        fr.ShowDialog();
                    }
                    else
                    {
                        double a2 = Convert.ToDouble(txt19.Text);
                        double b2 = Convert.ToDouble(txt20.Text);
                        double alpha = Convert.ToDouble(txt21.Text);
                        while (alpha >= 360)
                        {
                            alpha = alpha - 360;
                        }
                        double a = Math.Sqrt(a2);
                        double b = Math.Sqrt(b2);
                        double c = Math.Sqrt(a2 - b2);
                        if (a2 <= 0 || b2<= 0 || a2<= b2 || alpha <= 0 || alpha >= 90)
                        {
                            Thongbao fr = new Thongbao("Lưu ý, aᶻ > bᶻ > 0 và 0⁰ < α < 90⁰");
                            fr.ShowDialog();
                        }
                        else
                        {
                            double xm = Math.Sqrt(((4 * a2 - 4 * b2) - (2 * a2 - 2 * a2 * Math.Cos((alpha * Math.PI / 180)))) / (2 * (a2 - b2) / a2 * (1 + Math.Cos(alpha * Math.PI / 180))));
                            double ym = (a2 * b2 - b2 * xm * xm) / a2;
                            textBox10.Text = "Gọi M là điểm cần tìm,";
                            textBox10.Text = "Theo đề bài, ta có :";
                            textBox10.Text += Environment.NewLine + " MF₁ = " + Math.Round(a, 5) + " + " + Math.Round(c / a, 5) + "x; MF₂ = " + Math.Round(a, 5) + " - " + Math.Round(c / a, 5) + "x và F₁F₂ᶻ = 4cᶻ = " + Math.Round(4 * a2 - 4 * b2, 5);
                            
                            textBox10.Text += Environment.NewLine + " Áp dụng định lí Cosin trong ∆ F1MF2 :";
                            textBox10.Text += Environment.NewLine + " => F₁F₂ᶻ = MF₁ᶻ + MF₂ᶻ - 2MF₁. MF₂. Cos(α⁰)";
                            textBox10.Text += Environment.NewLine + "<=> " + Math.Round((4 * a2 - 4 * b2), 5) + " = (" + Math.Round(a, 5) + " + " + Math.Round(c / a, 5) + "x)ᶻ + (" + Math.Round(a, 5) + " - " + Math.Round(c / a, 5) + "x)ᶻ - 2(" + Math.Round(a, 5) + " + " + Math.Round(c / a, 5) + "x)(" + Math.Round(a, 5) + " - " + Math.Round(c / a, 5) + "x)" + ". Cos(" + Math.Round(alpha, 5) + "⁰)";
                            textBox10.Text += Environment.NewLine + "<=> " + Math.Round((4 * a2 - 4 * b2), 5) + " = " + Math.Round(2 * (a2 - b2) / a2 * (1 + Math.Cos(alpha * Math.PI / 180)), 5) + "xᶻ + " + Math.Round(2 * a2 - 2 * a2 * Math.Cos((alpha * Math.PI / 180)), 5);
                            textBox10.Text += Environment.NewLine + "<=> xᶻ = " + Math.Round(xm, 5);
                            textBox10.Text += Environment.NewLine + "  => yᶻ = " + Math.Round(ym, 5);
                            textBox10.Text += Environment.NewLine + "  KL. Tập M bao gồm 4 điểm : ";
                            textBox10.Text += Environment.NewLine + " M₁(" + Math.Round(Math.Sqrt(xm), 5) + "; " + Math.Round(Math.Sqrt(ym), 5) + ")";
                            textBox10.Text += Environment.NewLine + " M₂(" + Math.Round(Math.Sqrt(xm), 5) + "; " + Math.Round(-Math.Sqrt(ym), 5) + ")";
                            textBox10.Text += Environment.NewLine + " M₃(" + Math.Round(-Math.Sqrt(xm), 5) + "; " + Math.Round(Math.Sqrt(ym), 5) + ")";
                            textBox10.Text += Environment.NewLine + " M₄(" + Math.Round(-Math.Sqrt(xm), 5) + "; " + Math.Round(-Math.Sqrt(ym), 5) + ")";
                        }
                    }        
                }
            }
        }
        private void xtraTabControl1_DoubleClick(object sender, EventArgs e)
        {

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
            txt22.Text = txtOutput.Text.ToString();

        }
        private void txt23_DoubleClick(object sender, EventArgs e)
        {
            txt24.Text = txtOutput.Text.ToString();

        }
        private void txt24_DoubleClick(object sender, EventArgs e)
        {
            txt26.Text = txtOutput.Text.ToString();

        }
        private void txt25_DoubleClick(object sender, EventArgs e)
        {
            txt23.Text = txtOutput.Text.ToString();

        }
        private void txt26_DoubleClick(object sender, EventArgs e)
        {
            txt25.Text = txtOutput.Text.ToString();

        }
        private void txt27_DoubleClick(object sender, EventArgs e)
        {
            txt27.Text = txtOutput.Text.ToString();
        }
        private void simpleButton32_Click(object sender, EventArgs e)
        {
            txt5.Text = "";
            txt6.Text = "";
            textBox6.Text = "";

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txt7.Text = "";
            txt8.Text = "";
            textBox2.Text = "";

        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            txt9.Text = "";
            txt10.Text = "";
            textBox3.Text = "";
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            txt11.Text = "";
            txt12.Text = "";
            textBox5.Text = "";
        }
        private void simpleButton15_Click(object sender, EventArgs e)
        {
            txt13.Text = "";
            txt14.Text = "";
            textBox7.Text = "";
        }
        private void simpleButton25_Click(object sender, EventArgs e)
        {
            txt15.Text = "";
            txt16.Text = "";
            txt17.Text = "";
            txt18.Text = "";
            textBox8.Text = "";
        }
        private void simpleButton41_Click(object sender, EventArgs e)
        {
            txt19.Text = "";
            txt20.Text = "";
            txt21.Text = "";
          
            textBox10.Text = "";
        }
        private void simpleButton34_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Sqrt((double.Parse(txtOutput.Text))).ToString();
                CoTrangthainhap = true;
            }
        }

        private void simpleButton46_Click(object sender, EventArgs e)
        {
            if (txt22.Text == "")
            {
                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a1");
                fr.ShowDialog();
            }
            else
            {
                if (txt23.Text == "")
                {
                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b1");
                    fr.ShowDialog();
                }
                else
                {
                    if (txt24.Text == "")
                    {
                        Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập a2");
                        fr.ShowDialog();
                    }
                    else
                    {
                        if (txt25.Text == "")
                        {
                            Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập b2");
                            fr.ShowDialog();
                        }
                        else
                        {
                            if (txt26.Text == "")
                            {
                                Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập bᶻ");
                                fr.ShowDialog();
                            }
                            else
                            {
                                if (txt27.Text == "")
                                {
                                    Thongbao fr = new Thongbao("Thiếu dữ liệu ở ô nhập α");
                                    fr.ShowDialog();
                                }
                                else
                                {
                                    double a = Convert.ToDouble(txt22.Text);
                                    double b = Convert.ToDouble(txt23.Text);
                                    double a1 = Convert.ToDouble(txt24.Text);
                                    double b1 = Convert.ToDouble(txt25.Text);
                                    double a2 = Convert.ToDouble(txt26.Text);
                                    double b2 = Convert.ToDouble(txt27.Text);
                                    double aa = (a1 * b) * (a1 * b) + (a * a2) * (a * a2);
                                    double bb = 2 * a1 * b1 * b * b + 2 * a2 * b2 * a * a;
                                    double cc = ((b * b1) * (b * b1)) + ((a * b2) * (a * b2)) - ((a * b) * (a * b));
                                    double delta = bb * bb - 4 * aa * cc;
                                    if (a2 <= 0 || b2 <= 0 || a2 <= b2 )
                                    {
                                        Thongbao fr = new Thongbao("Lưu ý, aᶻ > bᶻ > 0");
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
                                                  textBox11.Text = " - Điểm A thuộc Elip với mọi m";
                                              }
                                              else
                                              {
                                                  if (cc > 0)
                                                  {
                                                      textBox11.Text = " - Điểm A nằm ngoài Elip với mọi m";
                                                  }
                                                  else
                                                  {
                                                      textBox11.Text = " - Điểm A nằm trong Elip với mọi m";
                                                  }
                                                  
                                              }
                                          }
                                          else
                                          {
                                              double x1 = -cc / bb;
                                              if (bb > 0)
                                              {
                                                  textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m > " + Math.Round(x1,5);
                                                  textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1, 5);
                                                  textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(x1, 5);

                                              }
                                              else
                                              {
                                                  textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(x1, 5);
                                                  textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1, 5);
                                                  textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m > " + Math.Round(x1, 5);

                                              }
                                          }
                                      }
                                      else
                                      {
                                         
                                          if (delta < 0)
                                          {
                                              if (aa > 0)
                                              {
                                                  textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra với mọi m ";
                                                  textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                                  textBox11.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                  textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                                  textBox11.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                              }
                                              else
                                              {
                                                  textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                                  textBox11.Text += Environment.NewLine + " - Không xảy ra với mọi m ";
                                                  textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                                  textBox11.Text += Environment.NewLine + " - Không xảy ra với mọi m";
                                                  textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra với mọi m";

                                              }
                                          }
                                          if (delta == 0)
                                          {
                                              double x1 = (-bb) / (2 * aa);
                                              textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                              textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m ≠ " + Math.Round(x1,5);
                                              textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                              textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1,5);
                                              textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                              textBox11.Text += Environment.NewLine + " - Không xảy ra với mọi m";

                                          }
                                          if (delta > 0)
                                          {
                                              double x1 = (-bb + Math.Sqrt(delta)) / (2 * aa);
                                              double x2 = (-bb - Math.Sqrt(delta)) / (2 * aa);
                                              if (aa > 0)
                                              {
                                                  textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(x2, 5) + " và m > " + Math.Round(x1,5);
                                                  textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1, 5) + " hoặc m = " + Math.Round(x2,5);
                                                  textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi " + Math.Round(x2,5) + " < m < " + Math.Round(x1,5);
                                              }
                                              else
                                              {
                                                  textBox11.Text = "TH1 : Xét A nằm ngoài Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi " + Math.Round(x2, 5) + " < m < " + Math.Round(x1, 5);
               
                                                  textBox11.Text += Environment.NewLine + "TH2 : Xét A nằm trên Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m = " + Math.Round(x1, 5) + " hoặc m = " + Math.Round(x2, 5);
                                                  textBox11.Text += Environment.NewLine + "TH3 : Xét A nằm trong Elip";
                                                  textBox11.Text += Environment.NewLine + " - Xảy ra khi và chỉ khi m < " + Math.Round(x2, 5) + " và m > " + Math.Round(x1, 5);
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

        private void ve1_Click(object sender, EventArgs e)
        {
            double a2 = Convert.ToDouble(txt1.Text);
            double b2 = Convert.ToDouble(txt2.Text);
            if (a2 <= b2 || a2 <= 0 || b2 <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý : aᶻ > bᶻ > 0");
                fr.ShowDialog();
            }
            else
            {
                txtExpression.Text = "sqrt(" + Math.Round(-b2/a2,5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
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

        private void ve2_Click(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(txt3.Text);
            a = a / 2;
            double b = Convert.ToDouble(txt4.Text);
            b = b / 2;
            double a2 = a * a;
            double b2 = b * b;
            if (a <= b || a <= 0 || b <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, 2a > 2b > 0");
                fr.ShowDialog();
            }
            else
            {
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
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
            double xa = Convert.ToDouble(txt5.Text);
            double xb = Convert.ToDouble(txt6.Text);
            if (xb <= 0 || xa <= 0 || xa <= xb)
            {
                Thongbao fr = new Thongbao("Lưu ý, 2a > 2c > 0");
                fr.ShowDialog();
            }
            else
            {
                double a = xa / 2;
                double c = xb / 2;
                double b = Math.Sqrt(a * a - c * c);
                double a2 = a * a;
                double b2 = b * b;
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
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

        private void ve4_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt7.Text);
            double xb = Convert.ToDouble(txt8.Text);
            if (xa <= 0 || xb <= 0 || xb >= 1)
            {
                Thongbao fr = new Thongbao("Lưu ý, 2a > 0 và 0 < e < 1");
                fr.ShowDialog();
            }
            else
            {
                double a = xa / 2;
                double c = xb * a;
                double b = Math.Sqrt(a * a - c * c);
                double a2 = a * a;
                double b2 = b * b;
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
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

        private void ve5_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt9.Text);
            double d = Convert.ToDouble(txt10.Text);
            if (xa <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý, 2a > 0");
                fr.ShowDialog();
            }
            else
            {
                double a = xa / 2;
                double b2 = a * a - (a * a / d) * (a * a / d);
                double a2 = a * a;
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
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

        private void ve6_Click(object sender, EventArgs e)
        {
            double c = Convert.ToDouble(txt11.Text);
            c = c / 2;
            double e1 = Convert.ToDouble(txt12.Text);
            if (c <= 0 || e1 <= 0 || e1 >= 1)
            {
                Thongbao fr = new Thongbao("Lưu ý, c > 0 và 0 < e < 1");
                fr.ShowDialog();
            }
            else
            {
                double a = c / e1;
                double b = Math.Sqrt(a * a - c * c);
                double a2 = a * a;
                double b2 = b * b;
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
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

        private void ve7_Click(object sender, EventArgs e)
        {
            double e1 = Convert.ToDouble(txt13.Text);
            double d = Convert.ToDouble(txt14.Text);
            if (d <= 0 || 0 >= e1 || e1 >= 1)
            {
                Thongbao fr = new Thongbao("Lưu ý, 0 < e < 1 và 2d > 0");
                fr.ShowDialog();
            }
            else
            {
                double a = e1 * d / 2;
                double c = 2 * a * a / d;
                double b = Math.Sqrt(a * a - c * c);
                double a2 = a * a;
                double b2 = b * b;
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
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

        private void ve8_Click(object sender, EventArgs e)
        {
            double xa = Convert.ToDouble(txt15.Text);
            double ya = Convert.ToDouble(txt16.Text);
            double xb = Convert.ToDouble(txt17.Text);
            double yb = Convert.ToDouble(txt18.Text);
            if ((xa * xa == xb * xb) || (ya * ya == yb * yb))
            {
                Thongbao fr = new Thongbao("Lưu ý, xA phải khác xB, yA phải khác yB");
                fr.ShowDialog();
            }
            else
            {
                double a2 = (xa * xa * yb * yb - ya * ya * xb * xb) / (yb * yb - ya * ya);
                if (a2 - xa * xa != 0)
                {
                    double b2 = (a2 * ya * ya) / (a2 - xa * xa);
                    if (a2 <= b2 || a2 <= 0 || b2 <= 0)
                    {
                        Thongbao fr = new Thongbao("Không tồn tại đường Elip thõa mãn điều kiện");
                        fr.ShowDialog();
                    }
                    else
                    {
                        textBox8.Text = "Đường Elip : xᶻ / " + Math.Round(a2, 5) + " + yᶻ / " + Math.Round(b2, 5) + " = 1";
                        txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                        if (b2 > 0)
                        {
                            txtExpression.Text += "+" + Math.Round(b2, 5);
                        }
                        else
                        {
                            txtExpression.Text += "-" + -Math.Round(b2, 5);
                        }
                        this.CheckDuplication();
                        txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                        if (b2 > 0)
                        {
                            txtExpression.Text += "+" + Math.Round(b2, 5);
                        }
                        else
                        {
                            txtExpression.Text += "-" + -Math.Round(b2, 5);
                        }
                        this.CheckDuplication();
                    }

                }
                else
                {
                    double b2 = (a2 * yb * yb) / (a2 - xb * xb);
                    if (a2 <= b2 || a2 <= 0 || b2 <= 0)
                    {
                        Thongbao fr = new Thongbao("Không tồn tại đường Elip thõa mãn điều kiện");
                        fr.ShowDialog();
                    }
                    else
                    {
                        textBox8.Text = "Đường Elip : xᶻ / " + Math.Round(a2, 5) + " + yᶻ / " + Math.Round(b2, 5) + " = 1";
                        txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                        if (b2 > 0)
                        {
                            txtExpression.Text += "+" + Math.Round(b2, 5);
                        }
                        else
                        {
                            txtExpression.Text += "-" + -Math.Round(b2, 5);
                        }
                        this.CheckDuplication();
                        txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                        if (b2 > 0)
                        {
                            txtExpression.Text += "+" + Math.Round(b2, 5);
                        }
                        else
                        {
                            txtExpression.Text += "-" + -Math.Round(b2, 5);
                        }
                        this.CheckDuplication();
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

        private void ve9_Click(object sender, EventArgs e)
        {
            double a2 = Convert.ToDouble(txt19.Text);
            double b2 = Convert.ToDouble(txt20.Text);
            double alpha = Convert.ToDouble(txt21.Text);
            while (alpha >= 360)
            {
                alpha = alpha - 360;
            }
            double a = Math.Sqrt(a2);
            double b = Math.Sqrt(b2);
            double c = Math.Sqrt(a2 - b2);
            double xm = Math.Sqrt(((4 * a2 - 4 * b2) - (2 * a2 - 2 * a2 * Math.Cos((alpha * Math.PI / 180)))) / (2 * (a2 - b2) / a2 * (1 + Math.Cos(alpha * Math.PI / 180))));
            double ym = (a2 * b2 - b2 * xm * xm) / a2;
            if (a2 <= b2 || a2 <= 0 || b2 <= 0)
            {
                Thongbao fr = new Thongbao("Lưu ý : aᶻ > bᶻ > 0");
                fr.ShowDialog();
            }
            else
            {
                txtExpression.Text = "sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                }
                this.CheckDuplication();
                txtExpression.Text = "-sqrt(" + Math.Round(-b2 / a2, 5) + "*x*x";
                if (b2 > 0)
                {
                    txtExpression.Text += "+" + Math.Round(b2, 5);
                }
                else
                {
                    txtExpression.Text += "-" + -Math.Round(b2, 5);
                } this.CheckDuplication();
            }
            txtExpression.Text = Math.Round(ym/(c-ym),5) + "*x";
            if (c*ym/(c-ym) > 0)
            {
                txtExpression.Text = "+" + Math.Round(c*ym/(c-ym),5);
            }
            else
            {
                txtExpression.Text = "-" + -Math.Round(c * ym / (c - ym), 5);

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

        private void ve10_Click(object sender, EventArgs e)
        {
           
        }

        private void xtraTabPage4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xtraTabPage12_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}