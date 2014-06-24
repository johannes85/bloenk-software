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
using System.Threading.Tasks;
using System.Threading;

namespace Bloenk.Animation
{
    public class Animation
    {
        protected int ledCount;
        private List<Step> steps;
        private int currentStep;
        private BloenkDevice device;
        private Thread thread;
        private bool stopping;

        public Animation(BloenkDevice device, int ledCount)
        {
            this.device = device;
            this.ledCount = ledCount;
            steps = new List<Step>();
        }

        public void AddStep(Step step)
        {
            steps.Add(step);
        }

        public bool IsRunning()
        {
            return (thread != null && thread.IsAlive);
        }

        public void Run()
        {
            if (IsRunning() == false && steps.Count > 0)
            {
                stopping = false;
                currentStep = 0;
                thread = new Thread(this.DoWork);
                thread.Start();
            }
        }

        public void Stop()
        {
            if (IsRunning() == true)
            {
                stopping = true;
                while (thread.IsAlive)
                {

                }
            }
        }

        private void DoWork()
        {
            try
            {
                do
                {
                    Dictionary<int, Color> colors = steps[currentStep].GetLedColors();

                    foreach (KeyValuePair<int, Color> ledColor in colors)
                    {
                        device.SetColor(ledColor.Key, LedColorHook(currentStep, ledColor.Key, ledColor.Value));
                    }
                    device.Write();

                    Thread.Sleep(steps[currentStep].duration);

                    currentStep++;
                    if (currentStep >= steps.Count)
                    {
                        currentStep = 0;
                    }
                } while (stopping == false);
            }
            catch (BloenkDeviceException)
            {

            }
        }

        protected virtual Color LedColorHook(int step, int led, Color color)
        {
            return(color);
        }
    }
}
