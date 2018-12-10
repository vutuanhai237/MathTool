using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Net.Mail;
using System.Net;
namespace Math_V1._1
{
    public partial class Gmail : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Gmail()
        {
            InitializeComponent();
        }

        private void Gmail_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Loading fr1 = new Loading();
            fr1.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((txtten.Text == "") || (txttieude.Text == "") || (txtbody.Text ==""))
            {
                send.Enabled = false;
            }
            else
            {
                send.Enabled = true;
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("haimeohung5@gmail.com");
                mail.To.Add("haimeohung@gmail.com");
                mail.Subject = txttieude.Text.ToString();

                textBox1.Text = "Tiêu đề : " + txttieude.Text.ToString() + Environment.NewLine + ". Tên : " + txtten.Text.ToString() + Environment.NewLine + Environment.NewLine + ". Vấn đề : " + vande.Text.ToString() + Environment.NewLine + ". Nội dung : "  + Environment.NewLine + txtbody.Text.ToString();
                mail.Body = textBox1.Text.ToString();
                mail.IsBodyHtml = true;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("haimeohung5" + "@gmail.com", "01274822188");
                this.Text = "Đang gửi mail ...";
                SmtpServer.Send(mail);
                this.Text = "Liên lạc với tôi";
                Thongbao fr = new Thongbao("Gửi mail thành công" + Environment.NewLine + "Mình sẽ phản hồi tới bạn sớm nhất");
                fr.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
    
    
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtten.Text = "";
            txttieude.Text = "";
            vande.Text = "";
            txtbody.Clear();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}