using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Collections;
namespace Math_V1._1
{
    public partial class Main : DevComponents.DotNetBar.Metro.MetroForm

    {
        Graph form;
        Color[] colorLevels = { Color.Red,  Color.Green, Color.Blue,  
            Color.Purple, Color.Brown, Color.Orange, Color.Chocolate, 
            Color.Maroon, Color.Navy, Color.YellowGreen };
        string[] strFunctions ={ "abs", "sin", "cos", "tan", "sec", "cosec", "cot", "arcsin", 
            "arccos", "arctan", "exp", "ln", "log", "antilog", "sqrt", "sinh", "cosh", "tanh", 
            "arcsinh", "arccosh", "arctanh" };


        public Main()
        {
            InitializeComponent();
        }
        #region Menu Area
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "sin(";

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "arcsin(";

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "cos(";

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "arccos(";

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "tan(";

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "arctan(";

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "arccot(";

        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "sqrt(";

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "+";

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "-";

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "*";

        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "/";

        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "^";

        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += "(";

        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtExpression.Text += ")";

        }
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
        private void lstExpressions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstExpressions.SelectedIndex != -1)
                this.txtExpression.Text = this.lstExpressions.Items[this.lstExpressions.SelectedIndex].ToString();
        }
        private void txtExpression_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtExpression.Text.Length > 0)
                AddExpression();

            //if a letter is found
            if (char.IsLetter(e.KeyChar))
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
        }
        private void mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mode.SelectedIndex == 1)
                this.sensitivity.Enabled = true;
            else
                this.sensitivity.Enabled = false;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
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
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int index = this.lstExpressions.SelectedIndex;
            this.lstExpressions.Items.Remove(this.lstExpressions.SelectedItem);
            if (index == this.lstExpressions.Items.Count)
                index--;
            if (index != -1)
                this.lstExpressions.SelectedIndex = index;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
           
            this.lstExpressions.Items.Clear();
        }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {          
            this.CheckDuplication();    
        }
        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = this.txtExpression.SelectionStart;
            WriteText(this.txtExpression.Text);
            this.txtExpression.SelectionStart = cursorPosition;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Loading fr1 = new Loading();
            fr1.ShowDialog();
            this.mode.SelectedIndex = 0;
            this.sensitivity.Enabled = false;
            this.lstExpressions.Items.Add("sin(x)");
            this.lstExpressions.Items.Add("cos(x)");
            this.lstExpressions.Items.Add("tan(x)");
            this.lstExpressions.Items.Add("cot(x)");           
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int s = 1;
            s =  s++;
            thoigian.Caption = (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString()) + " " + DateTime.Now.DayOfWeek.ToString() + ", " + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) + "/" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year;
        }
    }
}