using System;

namespace Randtech.RS232FileTransfer.Upload
{
	class UploadProgram
	{
		static void Main(string[] args)
		{
			Common.Functions.IntroText();

			Common.Functions.SendBinaryFile(new System.IO.Ports.SerialPort("COM1"), @"F:\My Folder\Dev\CR.RS232FileTransfer\Randtech.RS232FileTransfer\Upload\Uploads\UploadFile.txt");
		}
	}
}
