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
    public class RandomColorAnimation: Animation
    {
        private Color color;
        private int speed;
        private int currentColorStep = 0;

        public RandomColorAnimation(BloenkDevice device, int ledCount, int speed)
            : base(device, ledCount)
        {
            color = Color.White;
            this.speed = speed;
            currentColorStep = 0;
            Init();
        }

        protected override Color LedColorHook(int step, int led, Color color)
        {

            if (step != currentColorStep) { 
                Random random = new Random();
                this.color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                currentColorStep = step;
            }
            
            return (this.color);
        }

        protected void Init()
        {
            Step step = new Step();
            step.duration = speed;

            for (int a = 0; a < ledCount; a++)
            {
                
                step.SetLedColor(a, Color.White);
                
            }

            AddStep(step);
            AddStep(step);
        }
    }
}
