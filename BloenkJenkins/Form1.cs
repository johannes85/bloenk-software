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
using Bloenk;

namespace BloenkJenkins
{
    public partial class Form1 : Form
    {
        private BloenkDevice device;
        private BloenkDeviceConfiguration deviceConfig;
        private Settings settings;
        private List<Job> jobs;
        private bool refreshing;
        private int refreshTime;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshTime = 60;
            refreshing = false;
            settings = new Settings();
            settings.Reload();
            device = new BloenkDevice(5824, 1500);
            device.OnOpened += device_OnOpened;
            device.OnClosed += device_OnClosed;
            device.OpenOrWaitForDevice();
            setStateByDevice();
            switchOffLeds();
            loadJobsToList();
            refreshJobs();
        }

        void device_OnClosed(object sender, EventArgs e)
        {
            setStateByDevice();
        }

        void device_OnOpened(object sender, EventArgs e)
        {
            deviceConfig = device.ReadConfiguration();
            setStateByDevice();
        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            FormAddJob form = new FormAddJob(settings, deviceConfig.ledCount);
            form.ShowDialog();
            if (form.Job != null)
            {
                if (form.Job.Led == 0)
                {
                    settings.Jobs.Clear();
                }
                else
                {
                    if (
                        settings.Jobs.Count == 1 &&
                        settings.Jobs[0].Led == 0
                    ) {
                        settings.Jobs.Clear();
                    }
                }
                settings.Jobs.Add(form.Job);
                settings.Save();
                switchOffLeds();
                loadJobsToList();
                refreshJobs();
            }
        }

        private void btnRemoveJob_Click(object sender, EventArgs e)
        {
            if (lstJobs.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in lstJobs.SelectedItems) {
                    Job jobItem = (Job)item.Tag;
                    settings.Jobs.Remove(jobItem);
                    settings.Save();
                    lstJobs.Items.Remove(item);
                    switchOffLeds();
                    refreshJobs();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Job job in settings.Jobs)
            {
                try
                {
                    Jenkins jenkins = new Jenkins(job.Server);
                    JenkinsApi.Job jenkinsJob = jenkins.GetJob(job.Project);
                    job.Color = jenkinsJob.Color.ToString();
                }
                catch (Exception)
                {
                    job.Color = "error";
                }
                Color ledColor = Color.Black;
                switch (job.Color)
                {
                    case "blue":
                    case "blue_anime":
                        ledColor = Color.Green;
                        break;
                    case "yellow":
                    case "yellow_anime":
                        ledColor = Color.Yellow;
                        break;
                    case "red":
                    case "red_anime":
                        ledColor = Color.Red;
                        break;
                    case "notbuild":
                    case "notbuild_anime":
                        ledColor = Color.Blue;
                        break;
                    case "disabled":
                    case "disabled_anime":
                    case "grey":
                    case "grey_anime":
                        ledColor = Color.DarkGray;
                        break;
                    case "error":
                        ledColor = Color.Black;
                        break;
                }
                setLedColor(job.Led - 1, ledColor);
            }
            writeToDevice();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            settings.Save();
            loadJobsToList();
            Enabled = true;
            refreshing = false; 
            if (device.DeviceOpened()) {
                refreshTime = 60;
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshTime--;
            lblTimerValue.Text = refreshTime.ToString();
            if (refreshTime < 1)
            {
                refreshJobs();
            }
        }


        private void loadJobsToList()
        {
            lstJobs.Items.Clear();
            foreach (Job job in settings.Jobs)
            {
                string ledNumber = job.Led == 0 ? "all" : job.Led.ToString();
                ListViewItem item = new ListViewItem(ledNumber);
                item.Tag = job;
                item.SubItems.Add(job.Server);
                item.SubItems.Add(job.Project);
                item.SubItems.Add(job.Color);
                lstJobs.Items.Add(item);
            }
        }

        private void refreshJobs()
        {
            timer1.Enabled = false;
            refreshing = true;
            Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void setStateByDevice()
        {
            if (device.DeviceOpened())
            {
                deviceConfig = device.ReadConfiguration();
                btnAddJob.Enabled = true;
                if (refreshing == false) {
                    timer1.Enabled = true;
                }
            }
            else
            {
                btnAddJob.Enabled = false;
                timer1.Enabled = false;
            }
        }

        private void switchOffLeds()
        {
            setLedColor(-1, Color.Black);
            writeToDevice();
        }

        private void setLedColor(int led, Color color)
        {
            if (device.DeviceOpened() && deviceConfig != null)
            {
                if (led == -1)
                {
                    for (int a = 0; a < deviceConfig.ledCount; a++)
                    {
                        device.SetColor(a, color);
                    }
                }
                else
                {
                    device.SetColor(led, color);
                }
            }
        }

        private void writeToDevice()
        {
            if (device.DeviceOpened())
            {
                device.Write();
            }
        }

    }
}
