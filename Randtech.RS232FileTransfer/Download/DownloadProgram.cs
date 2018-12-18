using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Download
{
	class DownloadProgram
	{
		private static SerialPort port = Common.Functions.GetSerialPort(false);
		private static string buffer;


		[STAThread]
		static void Main(string[] args)
		{
#if(DEBUG)
			Common.Functions.ShowStatusDebug();
#endif

			int maxTries = Common.Settings.MaximumRetries;
			int count = 0;

			while (maxTries > count)
			{
				try
				{
					Debug.WriteLine($"Port '{port.PortName}' is now open and awaiting file...");

					port.DataReceived += Port_DataReceived;
					port.ErrorReceived += Port_ErrorReceived;
					buffer = string.Empty;
					if(!port.IsOpen){
						port.Open();
					}

				}
				catch (Exception ex)
				{
					count++;

					if (maxTries > count){
						Console.WriteLine(Common.Settings.MessageFail);
#if(DEBUG)
						Console.WriteLine(ex.ToString());
#endif
					}

					port.Close();
				}
			}
		}



		private static void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			Debug.WriteLine("Error received.");
		}

		private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			// Read File
			buffer += port.ReadExisting();

			//test for termination character in buffer
			if (buffer.Contains("\r\n"))
			{
				//run code on data received from serial port
				// Save file
				string path = Path.Combine(Common.Settings.ReceiveFolderName, "incoming.txt"); // todo get fle name from incoming				

				if(File.Exists(path)){
					File.Delete(path);
				}
				string createText =  buffer + Environment.NewLine;
				File.WriteAllText(path, createText);
				
			}
		}
	}
}