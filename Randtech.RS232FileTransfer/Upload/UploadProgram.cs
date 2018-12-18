using System;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Upload
{
	class UploadProgram
	{
		public SerialPort Port { get; set; }

		static void Main(string[] args)
		{
#if (DEBUG)
			Common.Functions.ShowStatusDebug(); 
#endif

			new UploadProgram(Common.Functions.GetSerialPort(true), Common.Settings.SendFileName);
		}


		private UploadProgram(SerialPort _port, string fileName)
		{
			Port = _port;
			try
			{
				Common.Functions.SendTextFile(Port, fileName);

				Console.WriteLine(Common.Settings.MessageSuccess);
			}
			catch 
			{
				Console.WriteLine(Common.Settings.MessageFail);

#if (DEBUG)
				throw;
#endif
			}
		}
	}
}