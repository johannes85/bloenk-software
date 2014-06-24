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
    public class Step
    {
        private Dictionary<int, Color> ledColors;
        public int duration;

        public Step(){
            ledColors = new Dictionary<int, Color>();
        }

        public Dictionary<int, Color> GetLedColors()
        {
            return(ledColors);
        }

        public void SetLedColor(int led, Color color)
        {
            if (ledColors.ContainsKey(led))
            {
                ledColors[led] = color;
            }
            else
            {
                ledColors.Add(led, color);
            }
        }
    }
}
