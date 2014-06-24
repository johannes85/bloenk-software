/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk device configuration tool
 *   
 * by DomesticHacks
 * http://www.domestichacks.info/
 * http://www.youtube.com/DomesticHacks
 *
 * Author: Johannes Zinnau (johannes@johnimedia.de)
 * 
 * License:
 * Creative Commons: Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
 * http://creativecommons.org/licenses/by-nc-sa/3.0/
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bloenk;

namespace BloenkConfigurator
{
    public partial class Form1 : Form
    {
        private BloenkDevice device;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = new BloenkDevice(5824, 1500);
            device.OnClosed += device_OnClosed;
            device.OnOpened += device_OnOpened;
            device.OpenOrWaitForDevice();
            if (device.DeviceOpened())
            {
                readSettings();
                pnlSettings.Enabled = true;
            }
            else
            {
                pnlSettings.Enabled = false;
            }
        }

        void device_OnOpened(object sender, EventArgs e)
        {
            readSettings();
            pnlSettings.Enabled = true;
        }

        void device_OnClosed(object sender, EventArgs e)
        {
            pnlSettings.Enabled = false;
        }

        private void readSettings()
        {
            BloenkDeviceConfiguration config = device.ReadConfiguration();
            txtLedCount.Value = config.ledCount;
        }

        private void writeSettings()
        {
            BloenkDeviceConfiguration config = new BloenkDeviceConfiguration();
            config.ledCount = (int)txtLedCount.Value;
            device.WriteConfiguration(config);

            MessageBox.Show("The device configuration has been successfully written to the device.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnWriteSettings_Click(object sender, EventArgs e)
        {
            writeSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readSettings();
        }
    }
}
