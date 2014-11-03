using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using WiFo.Data;

namespace WiFoUI.Logic
{
	public class FetchEventArgs : EventArgs
	{
		public FetchEventArgs(List<Record> records)
		{
			this.records = records;
		}

		public List<Record> Records
		{
			get
			{
				return records;
			}
		}

		private List<Record> records;
	}

	public delegate void FetchEventHandler(object sender, FetchEventArgs e);

	public class Client
	{
		public event FetchEventHandler FetchComplete;
		public event EventHandler Connected, Disconnected;

		public Client(string ip, int port)
		{
			serverIP = ip;
			serverPort = port;
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public bool IsStopped
		{
			get
			{
				return stopped;
			}
		}

		public void Start()
		{
			if (stopped)
			{
				IPAddress ipAddress = IPAddress.Parse(serverIP);
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, serverPort);
				client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), null);
			}
		}

		public void Stop()
		{
			stopped = true;
		}

		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				client.EndConnect(ar);
				stopped = false;
				new Thread(new ThreadStart(Run)).Start();
				OnConnected();
			}
			catch
			{
				stopped = true;
				OnDisconnected();
			}
		}

		private void OnConnected()
		{
			if (Connected != null)
				((System.Windows.Forms.Form)Connected.Target).Invoke(Connected, this, new EventArgs());
		}

		private void OnDisconnected()
		{
			try
			{
				if (Disconnected != null && !((System.Windows.Forms.Form)Connected.Target).IsDisposed)
					((System.Windows.Forms.Form)Connected.Target).Invoke(Disconnected, this, new EventArgs());
			}
			catch { }
		}

		private void Run()
		{
			List<Record> records = new List<Record>(1080);
			byte[] buffer = new byte[8 * 1080];

			while (!stopped)
			{
				int len = client.Receive(buffer);

				for (int i = 0; i < buffer.Length; i += 8)
				{
					uint time = BitConverter.ToUInt32(buffer, i);
					uint state = BitConverter.ToUInt32(buffer, i + 4);
					records.Add(new Record(time, state));
				}

				if (records.Count > 1 && FetchComplete != null)
				{
					FetchComplete(this, new FetchEventArgs(records));
					records.Clear();
				}
			}

			client.Close();
			OnDisconnected();
		}

		private string serverIP;
		private int serverPort;
		private bool stopped = true;
		private Socket client;
	}
}
