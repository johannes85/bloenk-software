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
    public class CircleAnimation: Animation
    {
        private Color color;

        public CircleAnimation(BloenkDevice device, int ledCount, Color color)
            : base(device, ledCount)
        {
            this.color = color;
            Init();
        }

        protected void Init()
        {
            Step step;

            for (int a = 0; a < ledCount; a++)
            {
                step = new Step();
                step.duration = 170;
                for (int b = 0; b < ledCount; b++)
                {
                    if (a == b)
                    {
                        step.SetLedColor(b, color);

                    }
                    else
                    {
                        step.SetLedColor(b, Color.Black);
                    }
                }
                AddStep(step);
            }
        }
    }
}
