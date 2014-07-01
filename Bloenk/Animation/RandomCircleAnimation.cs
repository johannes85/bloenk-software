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
using System.Threading.Tasks;
using System.Drawing;

namespace Bloenk.Animation
{
    // Exampla animation, using the "LedColorHook" event to override the color for every step.
    public class RandomCircleAnimation: Animation
    {
        private Color color;
        private bool onlyFullColors;

        public RandomCircleAnimation(BloenkDevice device, int ledCount, bool onlyFullColors)
            : base(device, ledCount)
        {
            color = Color.White;
            this.onlyFullColors = onlyFullColors;
            Init();
        }

        public RandomCircleAnimation(BloenkDevice device, int ledCount)
            : base(device, ledCount)
        {
            color = Color.White;
            this.onlyFullColors = false;
            Init();
        }

        protected override Color LedColorHook(int step, int led, Color color)
        {
            if (step == 0 && led == 0)
            {
                this.color = GetRandomColor();
            }
            if (color == Color.White)
            {
                
                return(this.color);
            }
            else
            {
                return (color);
            }
        }

        protected void Init()
        {
            Step step;

            for (int a = 0; a < ledCount; a++)
            {
                step = new Step();
                step.duration = 180;
                for (int b = 0; b < ledCount; b++)
                {
                    if (a == b)
                    {
                        step.SetLedColor(b, Color.White);

                    }
                    else
                    {
                        step.SetLedColor(b, Color.Black);
                    }
                }
                AddStep(step);
            }
        }

        private Color GetRandomColor()
        {
            Color color;
            Random random = new Random();

            if (onlyFullColors)
            {
                do {
                    color = Color.FromArgb(
                        random.Next(2) == 0 ? 0 : 255,
                        random.Next(2) == 0 ? 0 : 255,
                        random.Next(2) == 0 ? 0 : 255
                    );
                } while (EqualColor(color, Color.White) || EqualColor(color, Color.Black));
            }
            else
            {
                color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }
            
            return (color);
        }

        private bool EqualColor(Color color1, Color color2)
        {
            return (color1.ToArgb() == color2.ToArgb());
        }
    }
}
