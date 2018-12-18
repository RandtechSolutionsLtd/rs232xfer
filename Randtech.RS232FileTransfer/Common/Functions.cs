using System;
using System.Configuration;
using System.IO;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Common
{
	public class Functions
	{
		public static void ShowStatus()
		{
			
			Console.WriteLine($"Config folder: {Settings.ConfigFolderName}");
			bool isFolderFound = Directory.Exists(Settings.ConfigFolderName.ToString());
			Console.WriteLine($"Folder found? {isFolderFound}");

			Console.WriteLine($"Send config file: {Settings.SendConfigFileName}");
			Console.WriteLine($"Send config path: {Settings.SendConfigPathName}");
			bool isSendFileFound = File.Exists(Settings.SendConfigPathName);
			Console.WriteLine($"Send config file found at path? {isSendFileFound}");


			Console.WriteLine($"Receive config file: {Settings.ReceiveConfigFileName}");
			Console.WriteLine($"Receive config path: {Settings.ReceiveConfigPathName}");
			bool isReceiveFileFound = File.Exists(Settings.ReceiveConfigPathName);
			Console.WriteLine($"Receive config file found at path? {isReceiveFileFound}");

			if (!isFolderFound){
				throw new DirectoryNotFoundException($"Check folder '{Settings.ConfigFolderName}' exists.");
			}

			if (!isSendFileFound)
			{
				throw new FileNotFoundException($"Send config file not found. Check path '{Settings.SendConfigPathName}' exists.");
			}
			if (!isReceiveFileFound)
			{
				throw new FileNotFoundException($"Receive config file not found. Check path '{Settings.ReceiveConfigPathName}' exists.");
			}

			Console.WriteLine($"Send port name: {Settings.SendPortName}");
			Console.WriteLine($"Receive port name: {Settings.ReceivePortName}");

			Console.WriteLine($"Send from: {Settings.SendFileName}");
			Console.WriteLine($"Receive to: {Settings.ReceiveFolderName}");

			Console.WriteLine($"Success message: {Settings.MessageSuccess}");
			Console.WriteLine($"Fail message: {Settings.MessageFail}");
			Console.ReadLine();
		}


		public static SerialPort GetSerialPort(bool isSendPort = true)
		{
			var port = new SerialPort
			{
				BaudRate = Settings.BaudRate,
				DataBits = int.Parse(ConfigurationManager.AppSettings["bits"] ?? "8"),
				PortName = ConfigurationManager.AppSettings[isSendPort ? "portNameForSending" : "portNameForReceiving"],
				StopBits = Settings.StopBits,
				Parity = Settings.Parity,
				Handshake = Handshake.RequestToSend,
				
				//RtsEnable = true,
				//DtrEnable = true,
				//ReadTimeout = -1,
				//WriteTimeout = 4600
			};

			while(port.IsOpen){
				Console.WriteLine($"Port  '{port.PortName}' is already open. Attempting to close.");
				port.Close();
			}

			return port;
		}

		public static byte[] ReadByteArrayFromFile(string fileName)
		{
			byte[] buff = null;
			var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			var br = new BinaryReader(fs);
			long numBytes = new FileInfo(fileName).Length;
			buff = br.ReadBytes((int)numBytes);
			return buff;
		}

		public static void SendTextFile(SerialPort port, string fileName)
		{
			port.Open();
			port.Write(File.OpenText(fileName).ReadToEnd());
			Console.WriteLine("File send completed.");
			port.Close();
		}

		public static void SendBinaryFile(SerialPort port, string fileName)
		{
			port.Open();
			using (FileStream fs = File.OpenRead(fileName))
				port.Write((new BinaryReader(fs)).ReadBytes((int)fs.Length), 0, (int)fs.Length);

			Console.WriteLine("File send completed.");
			port.Close();
		}
	}
}
