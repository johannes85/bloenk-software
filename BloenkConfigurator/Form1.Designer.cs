namespace BloenkConfigurator
{
    partial class Form1
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
            this.pnlSettings = new System.Windows.Forms.GroupBox();
            this.btnWriteSettings = new System.Windows.Forms.Button();
            this.txtLedCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSettings.Controls.Add(this.button1);
            this.pnlSettings.Controls.Add(this.btnWriteSettings);
            this.pnlSettings.Controls.Add(this.txtLedCount);
            this.pnlSettings.Controls.Add(this.label1);
            this.pnlSettings.Location = new System.Drawing.Point(12, 12);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(364, 106);
            this.pnlSettings.TabIndex = 3;
            this.pnlSettings.TabStop = false;
            this.pnlSettings.Text = "Settings";
            // 
            // btnWriteSettings
            // 
            this.btnWriteSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteSettings.Location = new System.Drawing.Point(283, 77);
            this.btnWriteSettings.Name = "btnWriteSettings";
            this.btnWriteSettings.Size = new System.Drawing.Size(75, 23);
            this.btnWriteSettings.TabIndex = 5;
            this.btnWriteSettings.Text = "&write";
            this.btnWriteSettings.UseVisualStyleBackColor = true;
            this.btnWriteSettings.Click += new System.EventHandler(this.btnWriteSettings_Click);
            // 
            // txtLedCount
            // 
            this.txtLedCount.Location = new System.Drawing.Point(100, 26);
            this.txtLedCount.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.txtLedCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLedCount.Name = "txtLedCount";
            this.txtLedCount.Size = new System.Drawing.Size(49, 20);
            this.txtLedCount.TabIndex = 3;
            this.txtLedCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of LEDs:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(202, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "&read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 130);
            this.Controls.Add(this.pnlSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Blönk Configurator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox pnlSettings;
        private System.Windows.Forms.NumericUpDown txtLedCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWriteSettings;
        private System.Windows.Forms.Button button1;
    }
}

