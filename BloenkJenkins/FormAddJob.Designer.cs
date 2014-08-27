namespace BloenkJenkins
{
    partial class FormAddJob
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadServer = new System.Windows.Forms.Button();
            this.lstJobs = new System.Windows.Forms.ComboBox();
            this.pnlServerDetails = new System.Windows.Forms.Panel();
            this.numLedNumber = new System.Windows.Forms.NumericUpDown();
            this.rbAllLeds = new System.Windows.Forms.RadioButton();
            this.rbSpecificLed = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlServer = new System.Windows.Forms.Panel();
            this.pnlServerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLedNumber)).BeginInit();
            this.pnlServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.Location = new System.Drawing.Point(44, 0);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(327, 20);
            this.txtServer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Job:";
            // 
            // btnLoadServer
            // 
            this.btnLoadServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadServer.Location = new System.Drawing.Point(378, 0);
            this.btnLoadServer.Name = "btnLoadServer";
            this.btnLoadServer.Size = new System.Drawing.Size(75, 20);
            this.btnLoadServer.TabIndex = 4;
            this.btnLoadServer.Text = "Load Jobs";
            this.btnLoadServer.UseVisualStyleBackColor = true;
            this.btnLoadServer.Click += new System.EventHandler(this.btnLoadServer_Click);
            // 
            // lstJobs
            // 
            this.lstJobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstJobs.FormattingEnabled = true;
            this.lstJobs.Location = new System.Drawing.Point(44, 0);
            this.lstJobs.Name = "lstJobs";
            this.lstJobs.Size = new System.Drawing.Size(409, 21);
            this.lstJobs.TabIndex = 5;
            this.lstJobs.SelectedIndexChanged += new System.EventHandler(this.lstJobs_SelectedIndexChanged);
            // 
            // pnlServerDetails
            // 
            this.pnlServerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServerDetails.Controls.Add(this.numLedNumber);
            this.pnlServerDetails.Controls.Add(this.rbAllLeds);
            this.pnlServerDetails.Controls.Add(this.rbSpecificLed);
            this.pnlServerDetails.Controls.Add(this.label3);
            this.pnlServerDetails.Controls.Add(this.lstJobs);
            this.pnlServerDetails.Controls.Add(this.label2);
            this.pnlServerDetails.Location = new System.Drawing.Point(12, 38);
            this.pnlServerDetails.Name = "pnlServerDetails";
            this.pnlServerDetails.Size = new System.Drawing.Size(453, 72);
            this.pnlServerDetails.TabIndex = 7;
            // 
            // numLedNumber
            // 
            this.numLedNumber.Location = new System.Drawing.Point(137, 27);
            this.numLedNumber.Name = "numLedNumber";
            this.numLedNumber.Size = new System.Drawing.Size(67, 20);
            this.numLedNumber.TabIndex = 9;
            // 
            // rbAllLeds
            // 
            this.rbAllLeds.AutoSize = true;
            this.rbAllLeds.Location = new System.Drawing.Point(44, 50);
            this.rbAllLeds.Name = "rbAllLeds";
            this.rbAllLeds.Size = new System.Drawing.Size(210, 17);
            this.rbAllLeds.TabIndex = 8;
            this.rbAllLeds.Text = "All LEDs (All other jobs will be removed)";
            this.rbAllLeds.UseVisualStyleBackColor = true;
            this.rbAllLeds.CheckedChanged += new System.EventHandler(this.rbAllLeds_CheckedChanged);
            // 
            // rbSpecificLed
            // 
            this.rbSpecificLed.AutoSize = true;
            this.rbSpecificLed.Checked = true;
            this.rbSpecificLed.Location = new System.Drawing.Point(44, 27);
            this.rbSpecificLed.Name = "rbSpecificLed";
            this.rbSpecificLed.Size = new System.Drawing.Size(87, 17);
            this.rbSpecificLed.TabIndex = 7;
            this.rbSpecificLed.TabStop = true;
            this.rbSpecificLed.Text = "Specific LED";
            this.rbSpecificLed.UseVisualStyleBackColor = true;
            this.rbSpecificLed.CheckedChanged += new System.EventHandler(this.rbSpecificLed_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Led:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(390, 117);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(309, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // pnlServer
            // 
            this.pnlServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServer.Controls.Add(this.btnLoadServer);
            this.pnlServer.Controls.Add(this.label1);
            this.pnlServer.Controls.Add(this.txtServer);
            this.pnlServer.Location = new System.Drawing.Point(12, 12);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(453, 25);
            this.pnlServer.TabIndex = 10;
            // 
            // FormAddJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 152);
            this.Controls.Add(this.pnlServer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pnlServerDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddJob";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Jenkins Job";
            this.Load += new System.EventHandler(this.FormAddJob_Load);
            this.pnlServerDetails.ResumeLayout(false);
            this.pnlServerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLedNumber)).EndInit();
            this.pnlServer.ResumeLayout(false);
            this.pnlServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadServer;
        private System.Windows.Forms.ComboBox lstJobs;
        private System.Windows.Forms.Panel pnlServerDetails;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbAllLeds;
        private System.Windows.Forms.RadioButton rbSpecificLed;
        private System.Windows.Forms.NumericUpDown numLedNumber;
    }
}