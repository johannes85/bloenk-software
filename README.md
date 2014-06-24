Blönk USB Notifier
=========
.NET (C#) Library and Applications

About
--
Blönk is an universal RGB light attached to the USB port. Originally designt as notifier for the Microsoft Lync software, Blönk now can be used for many other applications.

Flexibility
--
You can ether integrate Blönk into your own projects by using the .NET Library (or commandline software) or you can use Blönk with the already build software as Microsoft Lync notifier.

Hardware
--
You can find the full schematic, PCB layouts, firmware and the Windows drivers in a separate repository:  
https://github.com/johannes85/bloenk-hardware  
I also published more details about the project on my YouTube channel and website (in German): 
http://www.domestichacks.info/projekte/bloenk-usb-licht-fuer-lync/
https://www.youtube.com/domestichacks

C# Projects in this Repository
--
* Bloenk: Library to use Blönk in your own .NET projects or even Powershell
* BloenkApiExample: A short example how to use the library
* BloenkCommandline: Commandline software to control the hardware (change the LED colors)
* BloenkConfigurator: This Application is required to configure the Blönk hardware after building it (setting the number of LEDs used)
* BloenkExampleBatteryLevel: Short example about how to use Blönk as battery indicator for a laptop
* BloenkExampleVuMeter: Short example about how to use Blönk to visualize music
* BloenkForLync: Integration of Blönk in the Microsoft Lync client (status display and call/message notifier)
* BloenkPresentation: A application I used to demonstrate Blönk
* BloenkTest: Application demonstrating many features of Blönk
