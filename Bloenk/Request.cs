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

namespace Bloenk
{
    public static class Request
    {
        public const byte CUSTOM_RQ_SET_CURRENT_LED = 0;
        public const byte CUSTOM_RQ_SET_COLOR_R = 1;
        public const byte CUSTOM_RQ_SET_COLOR_G = 2;
        public const byte CUSTOM_RQ_SET_COLOR_B = 3;
        public const byte CUSTOM_RQ_WRITE_TO_LEDS = 4;
        public const byte CUSTOM_RQ_SET_LEDCOUNT = 10;
        public const byte CUSTOM_RQ_GET_LEDCOUNT = 20;
    }
}
