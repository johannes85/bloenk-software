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
using System.Threading;

namespace Bloenk.Animation
{
    public class RandomFadeAnimation : Animation
    {
        private Color sourceColor;
        private Color targetColor;
        private int interpolationProgress;
        private int speed;

        public RandomFadeAnimation(BloenkDevice device, int ledCount, int speed)
            : base(device, ledCount)
        {
            // Adding dummy step, so the parent class can start the animation
            Step step = new Step();
            AddStep(step);

            this.speed = speed;
        }

        private void SetColor(Color color)
        {
            for (int a = 0; a < ledCount; a++)
            {
                device.SetColor(a, color);
            }
            device.Write();
        }

        protected override void DoWork()
        {
            try
            {
                interpolationProgress = 0;
                do
                {
                    sourceColor = GetRandomColor();
                } while (EqualColor(sourceColor, Color.Black));
                do
                {
                    targetColor = GetRandomColor();
                } while (EqualColor(sourceColor, targetColor) || EqualColor(targetColor, Color.Black) || EqualColor(targetColor, Color.White));
                do
                {
                    SetColor(Interpolate(sourceColor, targetColor, 1.0 / 100 * interpolationProgress));

                    if (interpolationProgress >= 100)
                    {
                        interpolationProgress = 0;
                        sourceColor = targetColor;
                        do
                        {
                            targetColor = GetRandomColor();
                        } while (EqualColor(sourceColor, targetColor) || EqualColor(targetColor, Color.Black));
                    }

                    interpolationProgress++;

                    Thread.Sleep(speed);
                } while (stopping == false);
            }
            catch (BloenkDeviceException)
            {

            }
        }

        private Color GetRandomColor()
        {
            Random random = new Random();
            return (Color.FromArgb(
                random.Next(2) == 0 ? 0 : 255,
                random.Next(2) == 0 ? 0 : 255, 
                random.Next(2) == 0 ? 0 : 255
            ));
        }

        private bool EqualColor(Color color1, Color color2)
        {
            return(color1.ToArgb() == color2.ToArgb());
        }

        private Color Interpolate(Color source, Color target, double percent)
        {
            int r = (int)(source.R + (target.R - source.R) * percent);
            int g = (int)(source.G + (target.G - source.G) * percent);
            int b = (int)(source.B + (target.B - source.B) * percent);

            return System.Drawing.Color.FromArgb(255, r, g, b);
        }
    }
}
