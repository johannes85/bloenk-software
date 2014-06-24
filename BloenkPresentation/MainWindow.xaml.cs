/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk presentation for X-Make Munich 2013
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using Bloenk;
using Bloenk.Animation;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int ledCount = 3;

        private Dispatcher dispatcher;
        private List<Screen> screens;
        private BloenkDevice device;
        private Thread thread;
        private bool stopping;
        private int currentScreen;

        public MainWindow()
        {
            InitializeComponent();

            dispatcher = Dispatcher.CurrentDispatcher;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                device = new BloenkDevice(5824, 1500);
                device.OpenDevice();
            }
            catch (BloenkDeviceException)
            {
                MessageBox.Show("Could not open connection to Blönk", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            Screen screen;
            screens = new List<Screen>();
            
            screen = new Screen();
            screen.duration = 5000;
            screen.bloenkAnimation = new RandomCircleAnimation(device, ledCount);
            screen.image = "1.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 3000;
            screen.bloenkColor = System.Drawing.Color.Cyan;
            screen.image = "LyncOnlineGreenSmall.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 5000;
            screen.bloenkColor = System.Drawing.Color.Lime;
            screen.image = "LyncOnlineGreen.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 5000;
            screen.bloenkColor = System.Drawing.Color.Yellow;
            screen.image = "LyncOnlineYellow.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 5000;
            screen.bloenkColor = System.Drawing.Color.Red;
            screen.image = "LyncOnlineRed.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 5000;
            screen.bloenkAnimation = new CircleAnimation(device, ledCount, System.Drawing.Color.Red);
            screen.image = "LyncCall.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 5000;
            screen.bloenkAnimation = new BlinkAnimation(device, ledCount, System.Drawing.Color.Red);
            screen.image = "LyncMessenger.png";
            screens.Add(screen);
            
            screen = new Screen();
            screen.duration = 8000;
            screen.bloenkColor = System.Drawing.Color.FromArgb(0, 85, 255);
            screen.image = "Csharp.png";
            screens.Add(screen);

            screen = new Screen();
            screen.duration = 8000;
            screen.bloenkColor = System.Drawing.Color.FromArgb(255, 0, 85);
            screen.image = "Powershell.png";
            screens.Add(screen);
                
            StartPresentation();
        }

        private void SetLedColorAll(System.Drawing.Color color)
        {
            for (int a = 0; a < ledCount; a++)
            {
                device.SetColor(a, color);
            }
            device.Write();
        }

        private bool ThreadIsRunning()
        {
            return (thread != null && thread.IsAlive);
        }

        private void StartPresentation()
        {
            if (screens.Count > 0 && ThreadIsRunning() == false)
            {
                stopping = false;
                currentScreen = 0;
                thread = new Thread(this.DoWork);
                thread.Start();
            }
        }

        private void StopPresentation()
        {
            stopping = true;
            while (thread.IsAlive)
            {

            }
            SetLedColorAll(System.Drawing.Color.Black);
        }

        private void SetImage(String source)
        {
            BitmapImage logo = new BitmapImage();
            if (source != null && !source.Equals(""))
            {
                logo.BeginInit();
                logo.UriSource = new Uri("pack://application:,,,/Demo;component/Resources/" + source);
                logo.EndInit();
            }
            image.Source = logo;
        }

        private void DoWork()
        {
            do
            {
                Screen screen = screens[currentScreen];

                this.Dispatcher.BeginInvoke(new Action<String>(SetImage), screen.image);

                if (screen.bloenkAnimation != null)
                {
                    screen.bloenkAnimation.Run();
                }
                else
                {
                    SetLedColorAll(screen.bloenkColor);
                }

                Thread.Sleep(screen.duration < 50 ? 50 : screen.duration);

                if (screen.bloenkAnimation != null)
                {
                    screen.bloenkAnimation.Stop();
                }

                currentScreen++;
                if (currentScreen >= screens.Count)
                {
                    currentScreen = 0;
                }
            } while (stopping == false);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ThreadIsRunning())
            {
                StopPresentation();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
