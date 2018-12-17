using System;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Download
{
	class DownloadProgram
	{

		// Create the serial port with basic settings 
		private static SerialPort port = new SerialPort("COM1",
		  9600, Parity.None, 8, StopBits.One);

		[STAThread]
		static void Main(string[] args)
		{
			Common.Functions.IntroText();

			new DownloadProgram();
		}



		private DownloadProgram()
		{
			Console.WriteLine("Incoming Data:");
			// Attach a method to be called when there
			// is data waiting in the port's buffer 
			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
			// Begin communications 
			port.Open();



			while(true)
			{
				
			}

		}

		private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			// Show all the incoming data in the port's buffer
			Console.WriteLine(port.ReadExisting());
		}
	}
}
