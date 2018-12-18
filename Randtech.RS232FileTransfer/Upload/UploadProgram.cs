using System;
using System.Configuration;
using System.IO;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Upload
{
	class UploadProgram
	{
		static void Main(string[] args)
		{
			Common.Functions.ShowStatus();

			SerialPort port = Common.Functions.GetSerialPort();
			
			string fileName = Common.Settings.SendFileName;


			Common.Functions.SendTextFile(port, fileName);
		}
	}
}