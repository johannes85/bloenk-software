namespace BloenkJenkins
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
            this.components = new System.ComponentModel.Container();
            this.lstJobs = new System.Windows.Forms.ListView();
            this.clmLed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmJob = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddJob = new System.Windows.Forms.Button();
            this.btnRemoveJob = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTimerValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstJobs
            // 
            this.lstJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmLed,
            this.clmServer,
            this.clmJob,
            this.clmColor});
            this.lstJobs.FullRowSelect = true;
            this.lstJobs.Location = new System.Drawing.Point(12, 12);
            this.lstJobs.Name = "lstJobs";
            this.lstJobs.Size = new System.Drawing.Size(524, 205);
            this.lstJobs.TabIndex = 0;
            this.lstJobs.UseCompatibleStateImageBehavior = false;
            this.lstJobs.View = System.Windows.Forms.View.Details;
            // 
            // clmLed
            // 
            this.clmLed.Text = "LED";
            // 
            // clmServer
            // 
            this.clmServer.Text = "Server";
            this.clmServer.Width = 170;
            // 
            // clmJob
            // 
            this.clmJob.Text = "Job";
            this.clmJob.Width = 170;
            // 
            // clmColor
            // 
            this.clmColor.Text = "Color";
            this.clmColor.Width = 90;
            // 
            // btnAddJob
            // 
            this.btnAddJob.Location = new System.Drawing.Point(12, 223);
            this.btnAddJob.Name = "btnAddJob";
            this.btnAddJob.Size = new System.Drawing.Size(64, 23);
            this.btnAddJob.TabIndex = 1;
            this.btnAddJob.Text = "&Add";
            this.btnAddJob.UseVisualStyleBackColor = true;
            this.btnAddJob.Click += new System.EventHandler(this.btnAddJob_Click);
            // 
            // btnRemoveJob
            // 
            this.btnRemoveJob.Location = new System.Drawing.Point(82, 223);
            this.btnRemoveJob.Name = "btnRemoveJob";
            this.btnRemoveJob.Size = new System.Drawing.Size(64, 23);
            this.btnRemoveJob.TabIndex = 2;
            this.btnRemoveJob.Text = "&Remove";
            this.btnRemoveJob.UseVisualStyleBackColor = true;
            this.btnRemoveJob.Click += new System.EventHandler(this.btnRemoveJob_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTimerValue
            // 
            this.lblTimerValue.Location = new System.Drawing.Point(487, 225);
            this.lblTimerValue.Name = "lblTimerValue";
            this.lblTimerValue.Size = new System.Drawing.Size(49, 18);
            this.lblTimerValue.TabIndex = 3;
            this.lblTimerValue.Text = "0";
            this.lblTimerValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 258);
            this.Controls.Add(this.lblTimerValue);
            this.Controls.Add(this.btnRemoveJob);
            this.Controls.Add(this.btnAddJob);
            this.Controls.Add(this.lstJobs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Blönk Jenkins Integration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstJobs;
        private System.Windows.Forms.ColumnHeader clmLed;
        private System.Windows.Forms.ColumnHeader clmServer;
        private System.Windows.Forms.ColumnHeader clmJob;
        private System.Windows.Forms.Button btnAddJob;
        private System.Windows.Forms.Button btnRemoveJob;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColumnHeader clmColor;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTimerValue;
    }
}

