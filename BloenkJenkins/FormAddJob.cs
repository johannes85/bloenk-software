using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JenkinsApi;

namespace BloenkJenkins
{
    public partial class FormAddJob : Form
    {
        private Job job;
        private Settings settings;

        public Job Job {
            get
            {
                return job;
            }
            set
            {
                btnOk.Enabled = value != null;
                job = value;
            }
        }

        public FormAddJob(Settings settings, int ledCount)
        {
            InitializeComponent();
            this.settings = settings;
            numLedNumber.Minimum = 1;
            numLedNumber.Value = 1;
            numLedNumber.Maximum = ledCount;
        }

        private void FormAddJob_Load(object sender, EventArgs e)
        {
            Job = null;
            pnlServerDetails.Enabled = false;
            btnOk.Enabled = false;
            txtServer.Text = settings.JenkinsLastServer;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Job = null;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbAllLeds.Checked)
            {
                Job.Led = 0;
            }
            else
            {
                Job.Led = (int)numLedNumber.Value;
            }
            Close();
        }

        private void btnLoadServer_Click(object sender, EventArgs e)
        {
            settings.JenkinsLastServer = txtServer.Text;
            settings.Save();
            pnlServerDetails.Enabled = false;
            pnlServer.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstJobs.SelectedItem != null)
            {
                Job = (Job)lstJobs.SelectedItem;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Jenkins jenkins = new Jenkins(txtServer.Text);
            e.Result = jenkins.GetServerDetails();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(
                    String.Format("{0}:\n{1}", e.Error.Message, e.Error.InnerException.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                pnlServer.Enabled = true;
            }
            else
            {
                ServerDetails jenkinsServer = (ServerDetails)e.Result;
                foreach (JenkinsApi.Job job in jenkinsServer.Jobs)
                {
                    lstJobs.Items.Add(new Job(0, txtServer.Text, job.Name));
                }
                if (lstJobs.Items.Count > 0)
                {
                    lstJobs.SelectedIndex = 0;
                }
                pnlServerDetails.Enabled = true;
                pnlServer.Enabled = true;
            }
        }

        private void rbAllLeds_CheckedChanged(object sender, EventArgs e)
        {
            numLedNumber.Enabled = false;
        }

        private void rbSpecificLed_CheckedChanged(object sender, EventArgs e)
        {
            numLedNumber.Enabled = true;
        }

    }
}
