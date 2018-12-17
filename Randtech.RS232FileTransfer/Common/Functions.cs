using System;
using System.IO;
using System.IO.Ports;

namespace Randtech.RS232FileTransfer.Common
{
	public class Functions
	{
		public static void IntroText()
		{
			Console.WriteLine("RS232 File Transfer Application");
			Console.WriteLine("");
			Console.WriteLine("2018 (c) Randtech Solutions Limited");
			Console.WriteLine("Developed by Chris Randle");
			Console.WriteLine("");
			Console.WriteLine("Press a key to continue");
			Console.ReadLine();

			return;
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
		}

		public static void SendBinaryFile(SerialPort port, string fileName)
		{
			port.Open();
			using (FileStream fs = File.OpenRead(fileName))
				port.Write((new BinaryReader(fs)).ReadBytes((int)fs.Length), 0, (int)fs.Length);
		}
	}
}
