namespace kurs_el_och
{
    partial class РасписаниеДоктора
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pn_txt = new System.Windows.Forms.MaskedTextBox();
            this.vt_txt = new System.Windows.Forms.MaskedTextBox();
            this.sr_txt = new System.Windows.Forms.MaskedTextBox();
            this.cht_txt = new System.Windows.Forms.MaskedTextBox();
            this.pt_txt = new System.Windows.Forms.MaskedTextBox();
            this.sb_txt = new System.Windows.Forms.MaskedTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Понедельник";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Вторник";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Среда";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Четверг";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Пятница";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Суббота";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(143, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Изменить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pn_txt
            // 
            this.pn_txt.Location = new System.Drawing.Point(143, 83);
            this.pn_txt.Mask = "00:00-00:00";
            this.pn_txt.Name = "pn_txt";
            this.pn_txt.Size = new System.Drawing.Size(100, 20);
            this.pn_txt.TabIndex = 14;
            this.pn_txt.ValidatingType = typeof(System.DateTime);
            // 
            // vt_txt
            // 
            this.vt_txt.Location = new System.Drawing.Point(143, 109);
            this.vt_txt.Mask = "00:00-00:00";
            this.vt_txt.Name = "vt_txt";
            this.vt_txt.Size = new System.Drawing.Size(100, 20);
            this.vt_txt.TabIndex = 21;
            this.vt_txt.ValidatingType = typeof(System.DateTime);
            // 
            // sr_txt
            // 
            this.sr_txt.Location = new System.Drawing.Point(143, 135);
            this.sr_txt.Mask = "00:00-00:00";
            this.sr_txt.Name = "sr_txt";
            this.sr_txt.Size = new System.Drawing.Size(100, 20);
            this.sr_txt.TabIndex = 22;
            this.sr_txt.ValidatingType = typeof(System.DateTime);
            // 
            // cht_txt
            // 
            this.cht_txt.Location = new System.Drawing.Point(143, 161);
            this.cht_txt.Mask = "00:00-00:00";
            this.cht_txt.Name = "cht_txt";
            this.cht_txt.Size = new System.Drawing.Size(100, 20);
            this.cht_txt.TabIndex = 23;
            this.cht_txt.ValidatingType = typeof(System.DateTime);
            // 
            // pt_txt
            // 
            this.pt_txt.Location = new System.Drawing.Point(143, 187);
            this.pt_txt.Mask = "00:00-00:00";
            this.pt_txt.Name = "pt_txt";
            this.pt_txt.Size = new System.Drawing.Size(100, 20);
            this.pt_txt.TabIndex = 24;
            this.pt_txt.ValidatingType = typeof(System.DateTime);
            // 
            // sb_txt
            // 
            this.sb_txt.Location = new System.Drawing.Point(143, 213);
            this.sb_txt.Mask = "00:00-00:00";
            this.sb_txt.Name = "sb_txt";
            this.sb_txt.Size = new System.Drawing.Size(100, 20);
            this.sb_txt.TabIndex = 25;
            this.sb_txt.ValidatingType = typeof(System.DateTime);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 1;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // РасписаниеДоктора
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 293);
            this.Controls.Add(this.sb_txt);
            this.Controls.Add(this.pt_txt);
            this.Controls.Add(this.cht_txt);
            this.Controls.Add(this.sr_txt);
            this.Controls.Add(this.vt_txt);
            this.Controls.Add(this.pn_txt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "РасписаниеДоктора";
            this.Text = "Расписание Доктора";
            this.Load += new System.EventHandler(this.РасписаниеДоктора_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox pn_txt;
        private System.Windows.Forms.MaskedTextBox vt_txt;
        private System.Windows.Forms.MaskedTextBox sr_txt;
        private System.Windows.Forms.MaskedTextBox cht_txt;
        private System.Windows.Forms.MaskedTextBox pt_txt;
        private System.Windows.Forms.MaskedTextBox sb_txt;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}