using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bloenk;
using Bloenk.Animation;

namespace BloenkTest
{
    public partial class Form1 : Form
    {
        private Boolean isSelecting;

        private Bitmap pickerBitmap;
        private BloenkDevice device;
        private Animation animation;

        public Form1()
        {
            InitializeComponent();

            isSelecting = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pickerBitmap = (Bitmap)pnlPicker.BackgroundImage;

            device = new BloenkDevice(5824, 1500);
            device.OnClosed += device_OnClosed;
            device.OnOpened += device_OnOpened;
            device.OpenOrWaitForDevice();

            if (device.DeviceOpened())
            {
                pnlMain.Enabled = true;
                readDeviceConfig();
            }
            else
            {
                pnlMain.Enabled = false;
            }
        }

        private void readDeviceConfig()
        {
            BloenkDeviceConfiguration config;
            txtLog.AppendText("Read device config\n");
            try
            {
                config = device.ReadConfiguration();
            }
            catch (BloenkDeviceException)
            {
                config = new BloenkDeviceConfiguration();
                config.ledCount = 8;
                txtLog.AppendText(" Couldn't read device config, using default values\n");
            }
            numericLedCount.Value = config.ledCount;
            txtLog.AppendText(" Number of LEDs = " + config.ledCount.ToString() + "\n");
        }

        private void pickColor(int x, int y)
        {
            if (isSelecting &&
                x >= 0 && y >= 0 &&
                x < pickerBitmap.Width && y < pickerBitmap.Height)
            { 
                Color pickerColor = pickerBitmap.GetPixel(x, y);

                if (device.DeviceOpened())
                {
                    if (rbAllLeds.Checked)
                    {
                        for (int a = 0; a < numericLedCount.Value; a++)
                        {
                            device.SetColor(a, pickerColor);
                        }
                    }
                    else
                    {
                        device.SetColor((int)numericLed.Value, pickerColor);
                    }
                    device.Write();
                }
            }
        }

        void device_OnOpened(object sender, EventArgs e)
        {
            txtLog.AppendText("Device opened\n");

            pnlMain.Enabled = true;
            readDeviceConfig();

            if (animation != null && !animation.IsRunning())
            {
                animation.Run();
            }
        }

        void device_OnClosed(object sender, EventArgs e)
        {
            txtLog.AppendText("Device closed\n");

            pnlMain.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();

            device.SetColor((int)numericLed.Value, colorDialog1.Color);
            device.Write();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();

            for (int a = 0; a < numericLedCount.Value; a++)
            {
                device.SetColor(a, colorDialog1.Color);
            }
            device.Write();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (animation != null && animation.IsRunning())
            {
                animation.Stop();
            }
            animation = new BlinkAnimation(device, (int)numericLedCount.Value, Color.Red);
            animation.Run();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            animation.Stop();
            animation = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (animation != null && animation.IsRunning())
            {
                animation.Stop();
            }
            animation = new CircleAnimation(device, (int)numericLedCount.Value, Color.Lime);
            animation.Run();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (animation != null && animation.IsRunning())
            {
                animation.Stop();
            }
            animation = new RandomColorAnimation(device, (int)numericLedCount.Value, 200);
            animation.Run();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (animation != null && animation.IsRunning())
            {
                animation.Stop();
            }
            animation = new RandomCircleAnimation(device, (int)numericLedCount.Value);
            animation.Run();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (animation != null && animation.IsRunning())
            {
                animation.Stop();
            }
            animation = new RandomFadeAnimation(device, (int)numericLedCount.Value, 100);
            animation.Run();
        }

        private void pnlPicker_MouseDown(object sender, MouseEventArgs e)
        {
            isSelecting = true;
            pickColor(e.X, e.Y); 
        }

        private void pnlPicker_MouseUp(object sender, MouseEventArgs e)
        {
            isSelecting = false;
        }

        private void pnlPicker_MouseMove(object sender, MouseEventArgs e)
        {
            pickColor(e.X, e.Y);   
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device.DeviceOpened())
            {
                if (animation != null && animation.IsRunning())
                {
                    animation.Stop();
                }
                device.CloseDevice();
            }
        }

    }
}
