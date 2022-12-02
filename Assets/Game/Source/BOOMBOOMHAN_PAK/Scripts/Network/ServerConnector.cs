using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ServerConnector : MonoBehaviour
{
	[SerializeField, DisplayName("IP地址")]
	private string ip;

	[SerializeField, DisplayName("端口号")]
	private int port;

	[SerializeField, DisplayName("开始时连接"), Space(10.0f)]
	private bool connectWhenStart;

	private Socket socket;

	public Socket ConnectionSocket
	{
		get => socket;
	}

	public ServerConnector()
	{
		ip = "127.0.0.1";
		port = 3306;
		connectWhenStart = false;
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    if (connectWhenStart)
	    {
		    Connect();
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Connect()
    {
	    socket = await ServerConnectionHelper.ConnectToServer(ip, port);
	    Debug.Log(socket.Connected);
    }

    public void Disconnect()
    {
	    socket.Disconnect(true);
    }
}
