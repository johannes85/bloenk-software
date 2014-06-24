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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BloenkForLync
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        private Settings settings;

        public event EventHandler OnSaved;
        public event EventHandler OnBeforeSaved;

        public SettingsWindow(Settings settings)
        {
            InitializeComponent();

            this.settings = settings;
            cbBlinkOnMessage.IsChecked = settings.blinkOnMessage;
            cbBlinkOnCall.IsChecked = settings.blinkOnCall;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (OnBeforeSaved != null)
            {
                OnBeforeSaved(this, e);
            }

            settings.blinkOnCall = (bool)cbBlinkOnCall.IsChecked;
            settings.blinkOnMessage = (bool)cbBlinkOnMessage.IsChecked;
            settings.Save();

            if (OnSaved != null)
            {
                OnSaved(this, e);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
