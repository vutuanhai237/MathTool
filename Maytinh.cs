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
    public partial class Maytinh : DevComponents.DotNetBar.Metro.MetroForm
    {
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
        public Maytinh()
        {
            InitializeComponent();
        }
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

        private void cmdBang_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                Tinhtoan();
                CoTinhdon = true;
            }
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

        private void cmd1_Click(object sender, EventArgs e)
        {
            Nhapso("1");
        }

        private void cmd0_Click(object sender, EventArgs e)
        {
            Nhapso("0");
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
        private void cmdCE_Click(object sender, EventArgs e)
        {
            CoTeliet = false;
            txtOutput.Text = "0";
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!CoTeliet)
            {
                txtOutput.Text = Math.Sqrt((double.Parse(txtOutput.Text))).ToString();
                CoTrangthainhap = true;
            }
        }

        private void Maytinh_Load(object sender, EventArgs e)
        {
            Loading fr1 = new Loading();
            fr1.ShowDialog();
        }


























    }
}