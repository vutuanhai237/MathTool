namespace Math_V1._1
{
    partial class Gmail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gmail));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtten = new DevExpress.XtraEditors.TextEdit();
            this.txttieude = new DevExpress.XtraEditors.TextEdit();
            this.vande = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.txtbody = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.send = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtten.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttieude.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(136, 21);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên, địa chỉ liên lạc :";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 21);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Tiêu đề :";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 89);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 21);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Vấn đề :";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(12, 124);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(114, 21);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Nội dụng chính :";
            // 
            // txtten
            // 
            this.txtten.Location = new System.Drawing.Point(160, 13);
            this.txtten.Name = "txtten";
            this.txtten.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtten.Properties.Appearance.Options.UseFont = true;
            this.txtten.Size = new System.Drawing.Size(292, 28);
            this.txtten.TabIndex = 4;
            // 
            // txttieude
            // 
            this.txttieude.Location = new System.Drawing.Point(160, 47);
            this.txttieude.Name = "txttieude";
            this.txttieude.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttieude.Properties.Appearance.Options.UseFont = true;
            this.txttieude.Size = new System.Drawing.Size(292, 28);
            this.txttieude.TabIndex = 5;
            // 
            // vande
            // 
            this.vande.DisplayMember = "Text";
            this.vande.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.vande.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vande.ForeColor = System.Drawing.Color.Black;
            this.vande.FormattingEnabled = true;
            this.vande.ItemHeight = 23;
            this.vande.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5});
            this.vande.Location = new System.Drawing.Point(160, 81);
            this.vande.Name = "vande";
            this.vande.Size = new System.Drawing.Size(292, 29);
            this.vande.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.vande.TabIndex = 8;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "Phần mềm hoạt động không ổn định";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Các dạng toán cho ra kết quả sai";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "Đồ họa không được khởi tạo";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "Giao diện không đẹp";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "Vấn đề nhạy cảm khác";
            // 
            // txtbody
            // 
            this.txtbody.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbody.Location = new System.Drawing.Point(160, 116);
            this.txtbody.Multiline = true;
            this.txtbody.Name = "txtbody";
            this.txtbody.Size = new System.Drawing.Size(292, 215);
            this.txtbody.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton3.Location = new System.Drawing.Point(12, 275);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(137, 56);
            this.simpleButton3.TabIndex = 12;
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(12, 213);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(137, 56);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // send
            // 
            this.send.Enabled = false;
            this.send.Image = ((System.Drawing.Image)(resources.GetObject("send.Image")));
            this.send.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.send.Location = new System.Drawing.Point(12, 151);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(137, 56);
            this.send.TabIndex = 10;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(343, 267);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(20, 17);
            this.textBox1.TabIndex = 13;
            this.textBox1.Visible = false;
            // 
            // Gmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 334);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.send);
            this.Controls.Add(this.txtbody);
            this.Controls.Add(this.vande);
            this.Controls.Add(this.txttieude);
            this.Controls.Add(this.txtten);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Gmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liên lạc với tôi";
            this.Load += new System.EventHandler(this.Gmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtten.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttieude.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtten;
        private DevExpress.XtraEditors.TextEdit txttieude;
        private DevComponents.DotNetBar.Controls.ComboBoxEx vande;
        private System.Windows.Forms.TextBox txtbody;
        private DevExpress.XtraEditors.SimpleButton send;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private System.Windows.Forms.TextBox textBox1;
    }
}