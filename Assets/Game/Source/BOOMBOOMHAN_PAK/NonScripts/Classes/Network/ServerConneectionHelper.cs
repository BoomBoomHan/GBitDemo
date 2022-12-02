using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class ServerConnectionHelper
{
	public static async Task<Socket> ConnectToServer(string ip, int port)
	{
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
		await socket.ConnectAsync(ipe);
		return socket;
	}
}
