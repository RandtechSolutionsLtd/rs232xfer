using System;
using System.Configuration;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Download
{
	class DownloadProgram
	{

		// Create the serial port with basic settings 
		private static SerialPort port = Common.Functions.GetSerialPort(false);

		[STAThread]
		static void Main(string[] args)
		{
			//Common.Functions.ShowStatus();

			new DownloadProgram();
		}



		private DownloadProgram()
		{
			
			int maxTries = int.Parse(ConfigurationManager.AppSettings["maxTries"] ?? "20");
			int count = 0;
			port.Open();
			Console.WriteLine($"Port '{port.PortName}' is now open and awaiting file...");

			while (maxTries > count)
			{
				try
				{
					port.DataReceived += port_DataReceived;
					port.ErrorReceived += Port_ErrorReceived;
				}
				catch (Exception ex)
				{
					count++;
					Console.WriteLine(ex.ToString());
					
					if(maxTries > count){
						Console.WriteLine(Common.Settings.MessageFail);
					}
				}
			}
		}

		private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			Console.WriteLine("Error received.");
		}

		private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			// Show all the incoming data in the port's buffer
			Console.WriteLine("Incoming Data:");

			Console.WriteLine(port.ReadExisting());
		}
	}
}
