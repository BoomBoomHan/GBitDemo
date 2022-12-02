using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using MySqlConnector;



public class NetworkCore : MonoBehaviour 
{
    public string serverAddress = "127.0.0.1";
    public int serverPort = 5000;
    public string DatabaseName;
    public string username = "chen";
    public string password = "123";
    public string ChartName;
    public string MemberName;

    private MySqlConnection connection;

    public string ConnectString(string server, string dbName, string uid, string pswd) =>
	    $"Server={server};Database={dbName};Uid={uid};Password={pswd};";

    void Start()
    {
	    var con = ConnectString(serverAddress, DatabaseName, username, password);
	    Debug.Log(con);
	    connection = new MySqlConnection(con);
	    //Debug.Assert(connection != null);
	    
    }

    public async Task<List<KeyValuePair<string, short>>> LoadFromDb()
    {
	    await connection.OpenAsync();
	    //MySqlCommand command = new MySqlCommand($"SELECT * FROM {ChartName};", connection);
	    MySqlCommand command = new MySqlCommand($"SELECT {ChartName}.name, score FROM {ChartName};", connection);
	    MySqlDataReader reader = command.ExecuteReader();

	    var res = new List<KeyValuePair<string, short>>();
	    while (reader.Read())
	    {
		    res.Add(new KeyValuePair<string, short>(reader.GetString("name"), reader.GetInt16("score")));
		    Debug.Log($"name={reader["name"]}, name={reader["score"]}");
	    }
	    connection.Close();
	    return res;
    }
}