using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Net.Sockets;
using System.IO;
using System;

public class TwitchTchatInterface : MonoBehaviour {

    [Tooltip("Action done each time a message is received")]
    [SerializeField]
    public string _userName;
    [Tooltip("Action done each time a message is received")]
    [SerializeField]
    public string _password;



    [Tooltip("Action done each time a message is received")]
    [SerializeField]
    public UnityEvent _onMessageReceivedEvent;


    public delegate void OnMessageReceived(string userName, string message);
    public OnMessageReceived _onMessageReceived;


    public TcpClient _tcpClient;
    public StreamReader _streamReader;
    public StreamWriter _streamWriter;



    void Awake () {

        Reconnect();
	
	}
    void Reconnect() {

        _tcpClient = new TcpClient("irc.twitch.tv", 6667);
        _streamReader = new StreamReader(_tcpClient.GetStream());
        _streamWriter = new StreamWriter(_tcpClient.GetStream());

        _streamWriter.WriteLine("PASS " + _password + Environment.NewLine + "NICK " + _userName + Environment.NewLine + "USER " + _userName + " 8 * :" + _userName);

        _streamWriter.WriteLine("JOIN #JamsCenter");
        _streamWriter.Flush();
    }
	// Update is called once per frame
	void Update () {

        if (!_tcpClient.Connected) {
            Reconnect();
        }

        if (_tcpClient.Available > 0 || _streamReader.Peek() >= 0) {
            string message = _streamReader.ReadLine();
            Debug.Log("M:" + message);

        }
	}
}
