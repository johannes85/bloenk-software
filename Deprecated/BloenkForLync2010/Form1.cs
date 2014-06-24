/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk Lync 2010 integration
 *   Deprecated proof of concept
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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Microsoft.Lync.Model;
using Bloenk;

namespace BloenkForLync2010
{
    public partial class Form1 : Form
    {
        private Dispatcher dispatcher;
        private LyncClient lyncClient;
        private BloenkDevice device;
        private int reconnectCount;

        public Form1()
        {
            InitializeComponent();

            dispatcher = Dispatcher.CurrentDispatcher;
            notifyIcon1.Text = this.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.device = new BloenkDevice(5824, 1500);
                this.device.OpenDevice();
            }
            catch (BloenkDeviceException)
            {
                MessageBox.Show("Could not open connection to Blönk", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            connectToLync();
        }

        private void Client_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
            dispatcher.BeginInvoke(new Action<ClientState>(UpdateUserInterface), e.NewState);
        }

        private void SelfContact_ContactInformationChanged(object sender, ContactInformationChangedEventArgs e)
        {
            if (lyncClient.State == ClientState.SignedIn)
            {
                if (e.ChangedContactInformation.Contains(ContactInformationType.Availability))
                {
                    dispatcher.BeginInvoke(new Action(SetAvailability));
                }
            }
        }

        private void connectToLync()
        {
            lblLyncStatus.Text = "Connecting to Lync";
            try
            {
                lyncClient = LyncClient.GetClient();
            }
            catch (ClientNotFoundException)
            {
                //MessageBox.Show("Could not found Lync client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLyncStatus.Text = "Could not found Lync client";
                startReconnect();
                return;
            }
            catch (NotStartedByUserException ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLyncStatus.Text = ex.Message;
                startReconnect();
                return;
            }
            catch (LyncClientException ex)
            {
               // MessageBox.Show("Lync client error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLyncStatus.Text = "Lync client error: " + ex.Message;
                startReconnect();
                return;
            }
            catch (SystemException ex)
            {
                if (IsLyncException(ex))
                {
                    // Log the exception thrown by the Lync Model API.
                    //MessageBox.Show("Lync client error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblLyncStatus.Text = "Lync client error: " + ex.Message;
                    startReconnect();
                    return;
                }
                else
                {
                    //MessageBox.Show("Unknown error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblLyncStatus.Text = "Unknown error: " + ex.Message;
                    startReconnect();
                    throw;
                }
            }


            UpdateUserInterface(lyncClient.State);
            lyncClient.StateChanged +=
                new EventHandler<ClientStateChangedEventArgs>(Client_StateChanged);
        }

        private void UpdateUserInterface(ClientState currentState)
        {
            lblLyncStatus.Text = currentState.ToString();

            if (currentState == ClientState.SignedIn)
            {
                SetAvailability();

                lyncClient.Self.Contact.ContactInformationChanged +=
                    new EventHandler<ContactInformationChangedEventArgs>(SelfContact_ContactInformationChanged);
            }
            else
            {
                Color bloenkColor = Color.Black;
                pnlStatus.BackColor = bloenkColor;
                this.device.SetColor(0, bloenkColor);
                this.device.SetColor(1, bloenkColor);
                this.device.SetColor(2, bloenkColor);
                this.device.Write();
            }
        }

        private void SetAvailability()
        {
            ContactAvailability currentAvailability = 0;
            Color bloenkColor = Color.Black;
            try
            {
                currentAvailability = (ContactAvailability)lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Availability);
            }
            catch (Exception)
            {
                //MessageBox.Show("Error getting Lync state: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (currentAvailability != 0)
            {
                switch (currentAvailability)
                {
                    case ContactAvailability.Away:
                        bloenkColor = Color.Yellow;
                        break;
                    case ContactAvailability.Busy:
                        bloenkColor = Color.Red;
                        break;
                    case ContactAvailability.BusyIdle:
                        bloenkColor = Color.Red;
                        break;
                    case ContactAvailability.DoNotDisturb:
                        bloenkColor = Color.DarkRed;
                        break;
                    case ContactAvailability.Free:
                        bloenkColor = Color.Lime;
                        break;
                    case ContactAvailability.FreeIdle:
                        bloenkColor = Color.Lime;
                        break;
                    case ContactAvailability.Offline:
                        bloenkColor = Color.LightSlateGray;
                        break;
                    case ContactAvailability.TemporarilyAway:
                        bloenkColor = Color.Yellow;
                        break;
                    default:
                        bloenkColor = Color.LightSlateGray;
                        break;
                }
            }
            pnlStatus.BackColor = bloenkColor;
            this.device.SetColor(0, bloenkColor);
            this.device.SetColor(1, bloenkColor);
            this.device.SetColor(2, bloenkColor);
            this.device.Write();
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

        private void startReconnect()
        {
            this.reconnectCount = 10;
            timer1.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                if (this.device.DeviceOpened())
                {
                    Color bloenkColor = Color.Black;
                    pnlStatus.BackColor = bloenkColor;
                    this.device.SetColor(0, bloenkColor);
                    this.device.SetColor(1, bloenkColor);
                    this.device.SetColor(2, bloenkColor);
                    this.device.Write();
                    this.device.CloseDevice();
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            mnuShow_Click(sender, e);
        }

        private void mnuShow_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.reconnectCount--;
            if (this.reconnectCount < 1)
            {
                timer1.Enabled = false;
                connectToLync();
            }
        }
    }
}
