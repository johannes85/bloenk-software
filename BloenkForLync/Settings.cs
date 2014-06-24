/*
 *   ____  _ _   _       _    
 *  |  _ \| (_) (_)     | |   
 *  | |_) | | ___  _ __ | | __
 *  |  _ <| |/ _ \| '_ \| |/ /
 *  | |_) | | (_) | | | |   < 
 *  |____/|_|\___/|_| |_|_|\_\    
 * 
 *   Blönk Lync 2013 (2010) integration
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

namespace BloenkForLync
{
    public class Settings
    {
        public bool blinkOnMessage { get; set; }
        public bool blinkOnCall { get; set; }

        public Settings()
        {
        }

        public void Save()
        {
            Properties.Settings.Default.BlinkOnMessage = this.blinkOnMessage;
            Properties.Settings.Default.BlinkOnCall = this.blinkOnCall;
            Properties.Settings.Default.Save();
        }

        public void Load()
        {
            this.blinkOnMessage = Properties.Settings.Default.BlinkOnMessage;
            this.blinkOnCall = Properties.Settings.Default.BlinkOnCall;
        }
    }
}
