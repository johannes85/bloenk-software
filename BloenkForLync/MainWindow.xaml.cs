/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk Lync 2013 (2010) integration
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
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
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
// using System.Windows.Forms;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Bloenk;
using Bloenk.Animation;

namespace BloenkForLync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Dispatcher dispatcher;
        private LyncClient lyncClient;
        private BloenkDevice device;
        private Settings settings;
        private int ledCount = 0;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer animationTimer;
        private int reconnectCount;
        private System.Windows.Forms.NotifyIcon notifyIcon;

        private System.Drawing.Color currentStatusColor;
        private Animation currentAnimation;

        public MainWindow()
        {
            InitializeComponent();

            dispatcher = Dispatcher.CurrentDispatcher;

            lblBloenkConnected.Visibility = System.Windows.Visibility.Hidden;

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Text = this.Title;
            Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/BloenkForLync;component/Resources/Icons/icon.ico")).Stream;
            notifyIcon.Icon = new System.Drawing.Icon(iconStream);
            iconStream.Close();
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            notifyIcon.Visible = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Tick += animationTimer_Tick;

            settings = new Settings();
            settings.Load();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            device = new BloenkDevice(5824, 1500);
            device.OnClosed += device_OnClosed;
            device.OnOpened += device_OnOpened;
            device.OpenOrWaitForDevice();
            if (device.DeviceOpened())
            {
                lblBloenkConnected.Visibility = System.Windows.Visibility.Visible;
                ReadLedCount();

                for (int a = 0; a < ledCount; a++)
                {
                    device.SetColor(a, System.Drawing.Color.Black);
                }
                device.Write();
            }
            ConnectToLync();
        }

        private void ConnectToLync()
        {
            lblLyncStatus.Content = "Connecting to Lync";
            try
            {
                lyncClient = LyncClient.GetClient();
            }
            catch (ClientNotFoundException)
            {
                //MessageBox.Show("Could not found Lync client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLyncStatus.Content = "Could not found Lync client";
                StartReconnect();
                return;
            }
            catch (NotStartedByUserException ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLyncStatus.Content = ex.Message;
                StartReconnect();
                return;
            }
            catch (LyncClientException ex)
            {
                // MessageBox.Show("Lync client error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLyncStatus.Content = "Lync client error: " + ex.Message;
                StartReconnect();
                return;
            }
            catch (SystemException ex)
            {
                if (IsLyncException(ex))
                {
                    // Log the exception thrown by the Lync Model API.
                    //MessageBox.Show("Lync client error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblLyncStatus.Content = "Lync client error: " + ex.Message;
                    StartReconnect();
                    return;
                }
                else
                {
                    //MessageBox.Show("Unknown error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblLyncStatus.Content = "Unknown error: " + ex.Message;
                    StartReconnect();
                    throw;
                }
            }
            
            UpdateUserInterface(lyncClient.State);
            lyncClient.StateChanged += lyncClient_StateChanged;
            lyncClient.ConversationManager.ConversationAdded += ConversationManager_ConversationAdded;
            lyncClient.ConversationManager.ConversationRemoved +=ConversationManager_ConversationRemoved;
        }

        private void UpdateUserInterface(ClientState currentState)
        {
            Debug.WriteLine("UpdateUserInterface: State: " + currentState.ToString());
           
            lblLyncStatus.Content = currentState.ToString();

            switch (currentState)
            {
                case ClientState.SignedIn:
                    SetAvailability();
                    StopAnimation();
                    SetAvatar();

                    lyncClient.Self.Contact.ContactInformationChanged += Contact_ContactInformationChanged;
                    break;
                case ClientState.SigningIn:
                    RunAnimation(new RandomCircleAnimation(this.device, ledCount));
                    break;
                case ClientState.SigningOut:
                    RunAnimation(new CircleAnimation(this.device, ledCount, System.Drawing.Color.DarkBlue));
                    break;
                case ClientState.SignedOut:
                case ClientState.Invalid:
                    SetAvailability();
                    StopAnimation();
                    break;
                default:
                    break;
            }
        }

        private void SetAvatar()
        {
            try
            {
                using (Stream photoStream = lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Photo) as Stream)
                {
                    if (photoStream != null)
                    {
                        BitmapImage bm = new BitmapImage();
                        bm.BeginInit();
                        bm.StreamSource = photoStream;
                        bm.EndInit();
                        imageAvatar.Source = bm;
                    }
                }
            }
            catch (ItemNotFoundException)
            {
                Debug.WriteLine("Set Avatar: No avatar");
            }
            catch (NotReadyException)
            {
                Debug.WriteLine("Set Avatar: Not ready");
            }
            catch (LyncClientException ex)
            {
                Debug.WriteLine("Set Avatar: Error: " + ex);
            }
            catch (SystemException ex)
            {
                if (IsLyncException(ex))
                {
                    Debug.WriteLine("Set Avatar: Error: " + ex);
                }
                else
                {
                    throw;
                }
            }
        }

        private void SetAvailability()
        {
            Debug.Write("SetAvailability: Capabilities: ");
            Debug.WriteLine(lyncClient.Capabilities);

            ContactAvailability currentAvailability = 0;
            System.Windows.Media.SolidColorBrush statusColor = System.Windows.Media.Brushes.Black;
            try
            {
                if (lyncClient.Capabilities != 0 && lyncClient.Capabilities != LyncClientCapabilityTypes.Invalid) {
                    currentAvailability = (ContactAvailability)lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Availability);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (currentAvailability != 0)
            {
                switch (currentAvailability)
                {
                    case ContactAvailability.Away:
                        statusColor = System.Windows.Media.Brushes.Yellow;
                        break;
                    case ContactAvailability.Busy:
                        statusColor = System.Windows.Media.Brushes.Red;
                        break;
                    case ContactAvailability.BusyIdle:
                        statusColor = System.Windows.Media.Brushes.Red;
                        break;
                    case ContactAvailability.DoNotDisturb:
                        statusColor = System.Windows.Media.Brushes.DarkRed;
                        break;
                    case ContactAvailability.Free:
                        statusColor = System.Windows.Media.Brushes.Lime;
                        break;
                    case ContactAvailability.FreeIdle:
                        statusColor = System.Windows.Media.Brushes.Lime;
                        break;
                    case ContactAvailability.Offline:
                        statusColor = System.Windows.Media.Brushes.LightSlateGray;
                        break;
                    case ContactAvailability.TemporarilyAway:
                        statusColor = System.Windows.Media.Brushes.Yellow;
                        break;
                    default:
                        statusColor = System.Windows.Media.Brushes.LightSlateGray;
                        break;
                }
            }

            SetCurrentStatusColor(statusColor);
        }

        private void SetCurrentStatusColor(System.Windows.Media.SolidColorBrush statusColor)
        {
            borderState.Background = statusColor;
            currentStatusColor = System.Drawing.Color.FromArgb(statusColor.Color.R, statusColor.Color.G, statusColor.Color.B);

            if (!AnimationIsRunning())
            {
                SetDeviceColor();
            }
        }

        private void SetDeviceColor()
        {
            if (device.DeviceOpened())
            {
                for (int a = 0; a < ledCount; a++)
                {
                    device.SetColor(a, currentStatusColor);
                }
                device.Write();
            }
        }

        private void ReadLedCount()
        {
            if (device.DeviceOpened())
            {
                BloenkDeviceConfiguration config;
                try
                {
                    config = device.ReadConfiguration();
                }
                catch (BloenkDeviceException)
                {
                    config = new BloenkDeviceConfiguration();
                    config.ledCount = 8;
                }
                
                ledCount = config.ledCount;
            }
        }

        private void StartReconnect()
        {
            this.reconnectCount = 10;
            timer.Enabled = true;
        }

        private void RunAnimation(Animation animation, int length)
        {
            if (AnimationIsRunning())
            {
                if (animationTimer.Enabled == true)
                {
                    animationTimer.Enabled = false;
                }
                currentAnimation.Stop();
            }
            
            currentAnimation = animation;
            animation.Run();

            if (length > 0)
            {
                animationTimer.Interval = length;
                animationTimer.Enabled = true;
            }
        }

        private void RunAnimation(Animation animation)
        {
            RunAnimation(animation, 0);
        }

        private void StopAnimation()
        {
            if (AnimationIsRunning())
            {
                currentAnimation.Stop();
            }
            SetDeviceColor();
        }

        private bool AnimationIsRunning()
        {
            return (currentAnimation != null && currentAnimation.IsRunning());
        }

        private bool IsLyncException(SystemException ex)
        {
            return
                ex is NotImplementedException ||
                ex is ArgumentException ||
                ex is NullReferenceException ||
                ex is NotSupportedException ||
                ex is ArgumentOutOfRangeException ||
                ex is IndexOutOfRangeException ||
                ex is InvalidOperationException ||
                ex is TypeLoadException ||
                ex is TypeInitializationException ||
                //ex is InvalidComObjectException ||
                ex is InvalidCastException;
        }

        private void lyncClient_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
            dispatcher.BeginInvoke(new Action<ClientState>(UpdateUserInterface), e.NewState);
        }

        void ConversationManager_ConversationAdded(object sender, Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs e)
        {
            /*
             * Call livecycle
             *   Added Notified
             *   Changed Notified
             *   Changed Joining
             *   Changed Connected
             *   Changed Disconnected
             *   Removed Disconnected
             */

            Debug.WriteLine("ConversationAdded: Added Call");
            Debug.WriteLine(e.Conversation.Modalities[ModalityTypes.AudioVideo].State);

            if (e.Conversation.Modalities[ModalityTypes.AudioVideo].State == ModalityState.Notified && settings.blinkOnCall)
            {
                e.Conversation.Modalities[ModalityTypes.AudioVideo].ModalityStateChanged += MainWindow_CallModalityStateChanged;
            }

            Debug.WriteLine("ConversationAdded: Added Message");
            Debug.WriteLine(e.Conversation.Modalities[ModalityTypes.InstantMessage].State);

            if (e.Conversation.Modalities[ModalityTypes.InstantMessage].State == ModalityState.Notified && settings.blinkOnMessage)
            {
                Animation animation = new BlinkAnimation(device, ledCount, currentStatusColor);
                dispatcher.BeginInvoke(new Action<Animation, int>(RunAnimation), animation, 1500);
            }
        }

        void MainWindow_CallModalityStateChanged(object sender, ModalityStateChangedEventArgs e)
        {
            Debug.WriteLine("CallModalityStateChanged: Changed Call");
            Debug.WriteLine(e.NewState);

            if (e.NewState == ModalityState.Notified)
            {
                Animation animation = new CircleAnimation(device, ledCount, currentStatusColor);
                dispatcher.BeginInvoke(new Action<Animation>(RunAnimation), animation);
            }
            else
            {
                dispatcher.BeginInvoke(new Action(StopAnimation));
            }
        }

        void ConversationManager_ConversationRemoved(object sender, ConversationManagerEventArgs e)
        {
            Debug.WriteLine("ConversationRemoved: Removed Call");
            Debug.WriteLine(e.Conversation.Modalities[ModalityTypes.AudioVideo].State);
            Debug.WriteLine("ConversationRemoved: Removed Message");
            Debug.WriteLine(e.Conversation.Modalities[ModalityTypes.InstantMessage].State);
            dispatcher.BeginInvoke(new Action(StopAnimation));
        }

        private void Contact_ContactInformationChanged(object sender, ContactInformationChangedEventArgs e)
        {
            Debug.WriteLine("ContactInformationChanged");
            try
            {
                if (lyncClient.State == ClientState.SignedIn)
                {
                    if (e.ChangedContactInformation.Contains(ContactInformationType.Availability))
                    {
                        Debug.WriteLine("ContactInformationChanged: Availability");
                        dispatcher.BeginInvoke(new Action(SetAvailability));
                    }
                    if (e.ChangedContactInformation.Contains(ContactInformationType.Photo))
                    {
                        Debug.WriteLine("ContactInformationChanged: Photo");
                        dispatcher.BeginInvoke(new Action(SetAvatar));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void device_OnOpened(object sender, EventArgs e)
        {
            lblBloenkConnected.Visibility = System.Windows.Visibility.Visible;
            ReadLedCount();
            SetDeviceColor();
        }

        void device_OnClosed(object sender, EventArgs e)
        {
            lblBloenkConnected.Visibility = System.Windows.Visibility.Hidden;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            reconnectCount--;
            if (reconnectCount < 1)
            {
                timer.Enabled = false;
                ConnectToLync();
            }
        }

        void animationTimer_Tick(object sender, EventArgs e)
        {
            animationTimer.Enabled = false;
            if (AnimationIsRunning())
            {
                StopAnimation();
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            notifyIcon.Visible = false;

            if (device.DeviceOpened())
            {
                if (AnimationIsRunning())
                {
                    currentAnimation.Stop();
                }

                System.Drawing.Color bloenkColor = System.Drawing.Color.Black;
                for (int a = 0; a < ledCount; a++)
                {
                    device.SetColor(a, bloenkColor);
                }
                device.Write();
                device.CloseDevice();
            }
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow(settings);
            window.OnSaved += settingsWindow_OnSaved;
            window.ShowDialog();
        }

        void settingsWindow_OnSaved(object sender, EventArgs e)
        {
            SetDeviceColor();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.ShowDialog();
        }

        void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
            this.ShowInTaskbar = true;
        }
    }
}
