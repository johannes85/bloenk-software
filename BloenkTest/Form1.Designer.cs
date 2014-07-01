namespace BloenkTest
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericLed = new System.Windows.Forms.NumericUpDown();
            this.cbLedNumber = new System.Windows.Forms.RadioButton();
            this.rbAllLeds = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numericLedCount = new System.Windows.Forms.NumericUpDown();
            this.pnlPicker = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLedCount)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(245, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 26);
            this.button1.TabIndex = 5;
            this.button1.Text = "select color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(245, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(246, 26);
            this.button2.TabIndex = 8;
            this.button2.Text = "select color all";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(245, 130);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(249, 26);
            this.button3.TabIndex = 9;
            this.button3.Text = "start blink animation";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(245, 290);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(249, 26);
            this.button4.TabIndex = 10;
            this.button4.Text = "stop animation";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(245, 162);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(249, 26);
            this.button5.TabIndex = 11;
            this.button5.Text = "start circle animation";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 343);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(494, 191);
            this.txtLog.TabIndex = 12;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.button8);
            this.pnlMain.Controls.Add(this.button7);
            this.pnlMain.Controls.Add(this.button6);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.numericLedCount);
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Controls.Add(this.button2);
            this.pnlMain.Controls.Add(this.button5);
            this.pnlMain.Controls.Add(this.button4);
            this.pnlMain.Controls.Add(this.pnlPicker);
            this.pnlMain.Controls.Add(this.button3);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(495, 323);
            this.pnlMain.TabIndex = 15;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(245, 258);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(249, 26);
            this.button8.TabIndex = 24;
            this.button8.Text = "start random fade animation";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(245, 194);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(249, 26);
            this.button7.TabIndex = 23;
            this.button7.Text = "start random circle animation";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(245, 226);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(249, 26);
            this.button6.TabIndex = 22;
            this.button6.Text = "start random color animation";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericLed);
            this.panel1.Controls.Add(this.cbLedNumber);
            this.panel1.Controls.Add(this.rbAllLeds);
            this.panel1.Location = new System.Drawing.Point(0, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 45);
            this.panel1.TabIndex = 21;
            // 
            // numericLed
            // 
            this.numericLed.Location = new System.Drawing.Point(93, 23);
            this.numericLed.Maximum = new decimal(new int[] {
            253,
            0,
            0,
            0});
            this.numericLed.Name = "numericLed";
            this.numericLed.Size = new System.Drawing.Size(146, 20);
            this.numericLed.TabIndex = 21;
            // 
            // cbLedNumber
            // 
            this.cbLedNumber.AutoSize = true;
            this.cbLedNumber.Location = new System.Drawing.Point(0, 23);
            this.cbLedNumber.Name = "cbLedNumber";
            this.cbLedNumber.Size = new System.Drawing.Size(87, 17);
            this.cbLedNumber.TabIndex = 20;
            this.cbLedNumber.Text = "LED number:";
            this.cbLedNumber.UseVisualStyleBackColor = true;
            // 
            // rbAllLeds
            // 
            this.rbAllLeds.AutoSize = true;
            this.rbAllLeds.Checked = true;
            this.rbAllLeds.Location = new System.Drawing.Point(0, 0);
            this.rbAllLeds.Name = "rbAllLeds";
            this.rbAllLeds.Size = new System.Drawing.Size(65, 17);
            this.rbAllLeds.TabIndex = 19;
            this.rbAllLeds.TabStop = true;
            this.rbAllLeds.Text = "All LEDs";
            this.rbAllLeds.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Number of LEDs:";
            // 
            // numericLedCount
            // 
            this.numericLedCount.Location = new System.Drawing.Point(345, 3);
            this.numericLedCount.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numericLedCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericLedCount.Name = "numericLedCount";
            this.numericLedCount.Size = new System.Drawing.Size(146, 20);
            this.numericLedCount.TabIndex = 19;
            this.numericLedCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // pnlPicker
            // 
            this.pnlPicker.BackgroundImage = global::BloenkTest.Properties.Resources.colorpicker;
            this.pnlPicker.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pnlPicker.Location = new System.Drawing.Point(0, 0);
            this.pnlPicker.Name = "pnlPicker";
            this.pnlPicker.Size = new System.Drawing.Size(239, 220);
            this.pnlPicker.TabIndex = 15;
            this.pnlPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPicker_MouseDown);
            this.pnlPicker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlPicker_MouseMove);
            this.pnlPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlPicker_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 546);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.txtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Blönk Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLedCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericLedCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericLed;
        private System.Windows.Forms.RadioButton cbLedNumber;
        private System.Windows.Forms.RadioButton rbAllLeds;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}

