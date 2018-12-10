namespace Math_V1._1
{
    partial class ThongTinDoThi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThongTinDoThi));
            this.lvXAndFX = new System.Windows.Forms.ListView();
            this.chX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbBieuThuc = new System.Windows.Forms.ComboBox();
            this.rbDo = new System.Windows.Forms.RadioButton();
            this.rbRadian = new System.Windows.Forms.RadioButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvXAndFX
            // 
            this.lvXAndFX.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chX,
            this.chFX});
            this.lvXAndFX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvXAndFX.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvXAndFX.FullRowSelect = true;
            this.lvXAndFX.GridLines = true;
            this.lvXAndFX.Location = new System.Drawing.Point(2, 55);
            this.lvXAndFX.Margin = new System.Windows.Forms.Padding(4);
            this.lvXAndFX.Name = "lvXAndFX";
            this.lvXAndFX.Size = new System.Drawing.Size(398, 174);
            this.lvXAndFX.TabIndex = 3;
            this.lvXAndFX.UseCompatibleStateImageBehavior = false;
            this.lvXAndFX.View = System.Windows.Forms.View.Details;
            // 
            // chX
            // 
            this.chX.Text = "x";
            this.chX.Width = 62;
            // 
            // chFX
            // 
            this.chFX.Text = "f(x)";
            this.chFX.Width = 297;
            // 
            // cbBieuThuc
            // 
            this.cbBieuThuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbBieuThuc.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBieuThuc.FormattingEnabled = true;
            this.cbBieuThuc.Location = new System.Drawing.Point(2, 20);
            this.cbBieuThuc.Name = "cbBieuThuc";
            this.cbBieuThuc.Size = new System.Drawing.Size(398, 33);
            this.cbBieuThuc.TabIndex = 4;
            // 
            // rbDo
            // 
            this.rbDo.AutoSize = true;
            this.rbDo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDo.Location = new System.Drawing.Point(407, 127);
            this.rbDo.Name = "rbDo";
            this.rbDo.Size = new System.Drawing.Size(48, 25);
            this.rbDo.TabIndex = 7;
            this.rbDo.TabStop = true;
            this.rbDo.Text = "Độ";
            this.rbDo.UseVisualStyleBackColor = true;
            this.rbDo.CheckedChanged += new System.EventHandler(this.rbDo_CheckedChanged);
            // 
            // rbRadian
            // 
            this.rbRadian.AutoSize = true;
            this.rbRadian.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRadian.Location = new System.Drawing.Point(408, 158);
            this.rbRadian.Name = "rbRadian";
            this.rbRadian.Size = new System.Drawing.Size(46, 25);
            this.rbRadian.TabIndex = 8;
            this.rbRadian.TabStop = true;
            this.rbRadian.Text = "Ra";
            this.rbRadian.UseVisualStyleBackColor = true;
            this.rbRadian.CheckedChanged += new System.EventHandler(this.rbRadian_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cbBieuThuc);
            this.groupControl1.Controls.Add(this.lvXAndFX);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(402, 231);
            this.groupControl1.TabIndex = 9;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(408, 62);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(58, 57);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(408, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(58, 56);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ThongTinDoThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 231);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.rbRadian);
            this.Controls.Add(this.rbDo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThongTinDoThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin đồ thị";
            this.Load += new System.EventHandler(this.ThongTinDoThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvXAndFX;
        private System.Windows.Forms.ColumnHeader chX;
        private System.Windows.Forms.ColumnHeader chFX;
        private System.Windows.Forms.ComboBox cbBieuThuc;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.RadioButton rbDo;
        private System.Windows.Forms.RadioButton rbRadian;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}