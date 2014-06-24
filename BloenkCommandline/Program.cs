/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk commandline tool
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
using System.Reflection;
using System.Drawing;
using Bloenk;

namespace BloenkCommandline
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                SetColors(args);
            }
            else
            {
                DisplayHelp();
            }
        }

        private static void SetColors(string[] args)
        {
            try
            {
                BloenkDevice device = new BloenkDevice(5824, 1500);
                device.OpenDevice();

                foreach (String arg in args)
                {
                    String[] argParsed = arg.Split(',');
                    int ledNum;
                    int ledColorR;
                    int ledColorG;
                    int ledColorB;
                    Color ledColor;

                    if (argParsed.Length != 4)
                    {
                        throw new Exception("Invalid argument (" + arg + ")");
                    }

                    try
                    {
                        ledNum = int.Parse(argParsed[0]);
                        if (ledNum < 0)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Invalid led number (" + arg + ")");
                    }

                    try
                    {
                        ledColorR = int.Parse(argParsed[1]);
                        ledColorG = int.Parse(argParsed[2]);
                        ledColorB = int.Parse(argParsed[3]);
                        if (ledColorR < 0 || ledColorR > 255 || ledColorG < 0 || ledColorG > 255 || ledColorB < 0 || ledColorB > 255)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Invalid led color (" + arg + ")");
                    }

                    ledColor = Color.FromArgb(ledColorR, ledColorG, ledColorB);
                    device.SetColor(ledNum, ledColor);
                }

                device.Write();
            }
            catch (BloenkDeviceException ex)
            {
                DisplayHeader();
                Console.Error.WriteLine("Device error: \n" + ex.Message);
            }
            catch (Exception ex)
            {
                DisplayHeader();
                Console.Error.WriteLine("Error: \n" + ex.Message);
            }
        }

        private static void DisplayHelp()
        {
            DisplayHeader();
            Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " [lednum,r,g,b] [lednum,r,g,b] ...");
        }

        private static void DisplayHeader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StringBuilder sout = new StringBuilder();
            sout.AppendLine("Bloenk Commandline Tool");
            sout.AppendLine("v" + assembly.GetName().Version.ToString());
            sout.AppendLine("");

            Console.WriteLine(sout);
        }

    }
}
