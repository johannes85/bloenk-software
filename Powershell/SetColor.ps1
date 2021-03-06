#    ____  _ _   _       _    
#   |  _ \| (_) (_)     | |   
#   | |_) | | ___  _ __ | | __
#   |  _ <| |/ _ \| '_ \| |/ /
#   | |_) | | (_) | | | |   < 
#   |____/|_|\___/|_| |_|_|\_\    
#  
#    Blönk .NET library
#    Powershell example
#    
#  by DomesticHacks
#  http://www.domestichacks.info/
#  http://www.youtube.com/DomesticHacks
# 
#  Author: Johannes Zinnau (johannes@johnimedia.de)
#  
#  License:
#  Creative Commons: Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
#  http://creativecommons.org/licenses/by-nc-sa/3.0/
#

Add-Type -Path '.\LibUsbDotNet.dll'
Add-Type -Path '.\Bloenk.dll'

$led = Read-Host 'LED?'
$color = Read-Host 'Color?'

$bloenkDevice = New-Object Bloenk.BloenkDevice(5824, 1500)
$bloenkDevice.OpenDevice();
$bloenkDevice.SetColor($led, $color);
$bloenkDevice.Write();
$bloenkDevice.CloseDevice();
