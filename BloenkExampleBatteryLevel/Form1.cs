/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk .NET library
 *   Example: Battery indicator
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
using System.Management;
using System.Diagnostics;
using Bloenk;

namespace BloenkExampleBatteryLevel
{
    public partial class Form1 : Form
    {
        private BloenkDevice device;
        private int ledCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = new BloenkDevice(5824, 1500);
            device.OnOpened += device_OnOpened;
            device.OpenOrWaitForDevice();
            ReadLedCount();
            GetBatteryLevel();
        }

        void device_OnOpened(object sender, EventArgs e)
        {
            ReadLedCount();
            GetBatteryLevel();
        }

        private void ReadLedCount()
        {
            if (device.DeviceOpened())
            {
                BloenkDeviceConfiguration config = device.ReadConfiguration();
                ledCount = config.ledCount;
            }
        }

        private void GetBatteryLevel()
        {
            float batterylevel = SystemInformation.PowerStatus.BatteryLifePercent;
            int batteryPercentLevel = (int)Math.Round(batterylevel * 100, 0);
            lblBatteryLevel.Text = batteryPercentLevel.ToString() + "%";
            Color bloenkColor = Interpolate(Color.Red, Color.Green, batterylevel);
            pnlBloenkColor.BackColor = bloenkColor;

            if (device.DeviceOpened())
            {
                for (int a = 0; a < ledCount; a++)
                {
                    device.SetColor(a, bloenkColor);
                }
                device.Write();
            }
        }

        private Color Interpolate(Color source, Color target, double percent)
        {
            int r = (int)(source.R + (target.R - source.R) * percent);
            int g = (int)(source.G + (target.G - source.G) * percent);
            int b = (int)(source.B + (target.B - source.B) * percent);

            return System.Drawing.Color.FromArgb(255, r, g, b);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetBatteryLevel();
        }
    }
}
