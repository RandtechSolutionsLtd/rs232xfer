using System;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Upload
{
	class UploadProgram
	{
		static void Main(string[] args)
		{
			Common.Functions.ShowStatusDebug();

			try
			{
				SerialPort port = Common.Functions.GetSerialPort();
				string fileName = Common.Settings.SendFileName;
				Common.Functions.SendTextFile(port, fileName);

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