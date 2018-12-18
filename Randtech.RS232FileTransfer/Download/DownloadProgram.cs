using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Download
{
	class DownloadProgram
	{
		public SerialPort Port { get; set; }
		public string Buffer { get; set; }

		[STAThread]
		static void Main(string[] args)
		{
#if (DEBUG)
			Common.Functions.ShowStatusDebug();
#endif
			new DownloadProgram(Common.Functions.GetSerialPort(false), "");
		}


		public DownloadProgram(SerialPort port, string buffer)
		{
			Port = port;
			Buffer = buffer;
			int maxTries = Common.Settings.MaximumRetries;
			int count = 0;

			Debug.WriteLine($"Port '{Port.PortName}' is now open and awaiting file...");

			while (maxTries > count)
			{
				try
				{
					Debug.WriteLine($"Port '{Port.PortName}' is now open and awaiting file...");

					Port.DataReceived += Port_DataReceived;
					Port.ErrorReceived += Port_ErrorReceived;
					Buffer = string.Empty;
					if(!Port.IsOpen){
						Port.Open();
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

					Port.Close();
				}
			}
		}
		

		private static void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			Debug.WriteLine("Error received.");
		}

		private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				// Read File
				Buffer += Port.ReadExisting();

				//test for termination character in buffer
				if (Buffer.Contains("\r\n"))
				{
					//run code on data received from serial port
					// Save file
					string path = Path.Combine(Common.Settings.ReceiveFolderName, "incoming.txt"); // todo get fle name from incoming				

					if (File.Exists(path))
					{
						File.Delete(path);
					}
					string createText = Buffer + Environment.NewLine;
					File.WriteAllText(path, createText);

					Console.WriteLine(Common.Settings.MessageSuccess);
				}
			}
			catch (Exception)
			{
				Console.WriteLine(Common.Settings.MessageFail);
#if (DEBUG)
				throw;
#endif

			}
		}
	}
}