/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk .NET library
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
using System.Drawing;
using LibUsbDotNet;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Main;

namespace Bloenk
{
    public class BloenkDevice
    {
        private bool connectionEnabled;

        private UsbDeviceFinder deviceFinder;
        private UsbDevice device;
        private IDeviceNotifier notifier;

        public event EventHandler OnClosed;
        public event EventHandler OnOpened;

        public BloenkDevice(int vid, int pid)
        {
            connectionEnabled = false;
            deviceFinder = new UsbDeviceFinder(vid, pid);
            notifier = DeviceNotifier.OpenDeviceNotifier();
            notifier.OnDeviceNotify += new EventHandler<DeviceNotifyEventArgs>(notifier_OnDeviceNotify);
        }

        void notifier_OnDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
            if (e.EventType == EventType.DeviceArrival)
            {
                 if (!DeviceOpened() && connectionEnabled)
                 {
                     if (deviceFinder.Vid == e.Device.IdVendor &&
                         deviceFinder.Pid == e.Device.IdProduct)
                     {
                        try
                        {
                            OpenDevice();
                            if (OnOpened != null)
                            {
                                OnOpened(this, e);
                            }
                        }
                        catch (BloenkDeviceException)
                        {

                        }
                    }
                }
            }
            else if (e.EventType == EventType.DeviceRemoveComplete)
            {
                if (device != null)
                {
                    if (device.UsbRegistryInfo.Vid == e.Device.IdVendor &&
                        device.UsbRegistryInfo.Pid == e.Device.IdProduct)
                    {
                        DoCloseDevice();
                        if (OnClosed != null)
                        {
                            OnClosed(this, e);
                        }
                    }
                }
            }
        }

        public bool DeviceOpened()
        {
            if (device == null)
            {
                return (false);
            }

            if (!device.IsOpen)
            {
                DoCloseDevice();
                return (false);
            }

            return (true);
        }

        public void OpenOrWaitForDevice()
        {
            connectionEnabled = true;
            try
            {
                OpenDevice();
            }
            catch (BloenkDeviceException)
            {

            }
        }

        public void OpenDevice()
        {
            if (!DeviceOpened())
            {
 
                UsbRegistry reg = UsbDevice.AllLibUsbDevices.Find(deviceFinder);
                if (reg == null)
                {
                    throw new BloenkDeviceException("Couldn't found device");
                }
                
                device = reg.Device;
                if (device == null)
                {
                    throw new BloenkDeviceException("Couldn't open device");
                }
            }
        }

        public void CloseDevice()
        {
            connectionEnabled = false;
            DoCloseDevice();
        }

        public void SetColor(int ledNumber, Color color)
        {
            if (!DeviceOpened())
            {
                throw new BloenkDeviceException("Device isn't opened");
            }

            Send(Request.CUSTOM_RQ_SET_CURRENT_LED, (byte)ledNumber);
            Send(Request.CUSTOM_RQ_SET_COLOR_R, (byte)color.R);
            Send(Request.CUSTOM_RQ_SET_COLOR_G, (byte)color.G);
            Send(Request.CUSTOM_RQ_SET_COLOR_B, (byte)color.B);
        }

        public void Write()
        {
            if (!DeviceOpened())
            {
                throw new BloenkDeviceException("Device isn't opened");
            }

            Send(Request.CUSTOM_RQ_WRITE_TO_LEDS, 0);
        }

        public void WriteConfiguration(BloenkDeviceConfiguration config)
        {
            if (!DeviceOpened())
            {
                throw new BloenkDeviceException("Device isn't opened");
            }
            
            Send(Request.CUSTOM_RQ_SET_LEDCOUNT, (byte)config.ledCount);
        }

        public BloenkDeviceConfiguration ReadConfiguration()
        {
            if (!DeviceOpened())
            {
                throw new BloenkDeviceException("Device isn't opened");
            }

            try
            {
                BloenkDeviceConfiguration config = new BloenkDeviceConfiguration();

                byte[] ret = ReadBytes(Request.CUSTOM_RQ_GET_LEDCOUNT, 0, 1);
                config.ledCount = ret[0];

                return (config);
            }
            catch (BloenkDeviceException)
            {
                throw new BloenkDeviceException("Couldn't read device config");
            }
        }

        private void DoCloseDevice()
        {
            try
            {
                device.Close();
            }
            catch
            {

            }
            device = null;
        }

        private bool Send(byte request, byte data)
        {
            bool result = false;

            if (DeviceOpened())
            {
                int bytesWritten;
                byte[] writeBuffer = new byte[4];

                UsbSetupPacket writeIOMessagemessk = new UsbSetupPacket(
                    (byte)(UsbCtrlFlags.RequestType_Vendor |
                    UsbCtrlFlags.Recipient_Device |
                    UsbCtrlFlags.Direction_Out),
                    request,
                    data,
                    0,
                    0
                );

                result = device.ControlTransfer(ref writeIOMessagemessk, writeBuffer, 0, out bytesWritten);
            }

            return (result);
        }

        private byte[] ReadBytes(byte request, byte data, int readLength)
        {
            byte[] readBuffer = new byte[readLength];

            if (DeviceOpened())
            {
                int bytesRead;

                UsbSetupPacket readPacket = new UsbSetupPacket(
                    (byte)(UsbCtrlFlags.RequestType_Vendor |
                    UsbCtrlFlags.Recipient_Device |
                    UsbCtrlFlags.Direction_In),
                    request,
                    data,
                    0,
                    0
                );

                bool res = device.ControlTransfer(
                    ref readPacket,
                    readBuffer,
                    readLength,
                    out bytesRead
                );
                
                if (!res || bytesRead != readLength)
                {
                    throw new BloenkDeviceException("Invalid answer from device");
                }
            }

            return (readBuffer);
        }
    }
}
