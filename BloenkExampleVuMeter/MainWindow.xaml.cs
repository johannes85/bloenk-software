/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk .NET library
 *   Example: VU meter (sound visualizer)
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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using Bloenk;
using NAudio.CoreAudioApi;

namespace BloenkExampleVuMeter
{
    public partial class MainWindow : Window
    {
        private BloenkDevice device;
        private Thread audioVolumeThread;
        private Dispatcher dispatcher;

        private int ledsCount;
        private bool audioThreadStopping;


        public MainWindow()
        {
            InitializeComponent();
            dispatcher = Dispatcher.CurrentDispatcher;

            btnDisconnect.Visibility = System.Windows.Visibility.Hidden;
            ledsCount = 3;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            device = new BloenkDevice(5824, 1500);
        }

        private void Connect()
        {
            device.OpenDevice();
        }

        private void Disconnect()
        {
            if (device.DeviceOpened())
            {
                for (int a = 0; a < ledsCount; a++)
                {
                    device.SetColor(a, System.Drawing.Color.Black);
                }
                device.Write();
                device.CloseDevice();
            }
        }

        private System.Drawing.Color Interpolate(System.Drawing.Color source, System.Drawing.Color target, double percent)
        {
            var r = (byte)(source.R + (target.R - source.R) * percent);
            var g = (byte)(source.G + (target.G - source.G) * percent);
            var b = (byte)(source.B + (target.B - source.B) * percent);

            return System.Drawing.Color.FromArgb(255, r, g, b);
        }

        private void SetAudioLevel(float peakLevel)
        {
            lblPeakLevel.Content = peakLevel.ToString();
            prgVolumeLevel.Value = (int)(1000 * peakLevel);
        }

        private void SetAudioDevice(string deviceName)
        {
            lblSoundDevice.Content = deviceName;
        }

        private void StartAudioThread()
        {
            if (audioVolumeThread == null || !audioVolumeThread.IsAlive)
            {
                audioThreadStopping = false;
                audioVolumeThread = new Thread(DoAudioWork);
                audioVolumeThread.Start();
            }
        }

        private void StopAudioThread()
        {
            if (audioVolumeThread != null && audioVolumeThread.IsAlive)
            {
                audioThreadStopping = true;
                while (audioVolumeThread.IsAlive)
                {

                }
            }
            
        }

        private void DoAudioWork()
        {
            MMDeviceEnumerator devices = new MMDeviceEnumerator();
            MMDevice audioDevice = devices.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            dispatcher.BeginInvoke(new Action<string>(SetAudioDevice), audioDevice.DeviceFriendlyName);

            do
            {
                dispatcher.BeginInvoke(new Action<float>(SetAudioLevel), audioDevice.AudioMeterInformation.MasterPeakValue);

                for (int a = 0; a < ledsCount; a++)
                {
                    device.SetColor(a, Interpolate(System.Drawing.Color.Black, System.Drawing.Color.Green, audioDevice.AudioMeterInformation.MasterPeakValue));
                }
                device.Write();

                Thread.Sleep(1);
            } while (audioThreadStopping == false);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnConnect.IsEnabled = false;
                Connect();
                BloenkDeviceConfiguration config = device.ReadConfiguration();
                ledsCount = config.ledCount;
                StartAudioThread();
                btnConnect.Visibility = System.Windows.Visibility.Hidden;
                btnDisconnect.Visibility = System.Windows.Visibility.Visible;
                btnDisconnect.IsEnabled = true;
            }
            catch (BloenkDeviceException)
            {
                MessageBox.Show("Could not open connection to Blönk", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                btnConnect.IsEnabled = true;
            }
        }

        private void btnDisonnect_Click(object sender, RoutedEventArgs e)
        {
            btnDisconnect.IsEnabled = false;
            StopAudioThread();
            Disconnect();
            SetAudioDevice("-");
            SetAudioLevel(0);
            btnDisconnect.Visibility = System.Windows.Visibility.Hidden;
            btnConnect.Visibility = System.Windows.Visibility.Visible;
            btnConnect.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopAudioThread();
            Disconnect();
        }

    }
}
