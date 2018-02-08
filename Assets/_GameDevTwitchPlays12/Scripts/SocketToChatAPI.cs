using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class SocketToChatAPI : DualBehaviour
{
    #region Public Members

    public string m_addr;
    public int m_port;

    public didConnect Connected;
    public delegate void didConnect();

    public string nickName;

    public bool debugPrintAll;

    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        // s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s = new TcpClient();

        s.Connect(m_addr, m_port);

        stopThreads = false;

        var networkStream = s.GetStream();

        var input = new StreamReader(networkStream);
        // var output = new System.IO.StreamWriter(networkStream);

        //input proc
        inProc = new Thread(() => IRCInputProcedure(input, networkStream));
        inProc.Start();
    }

    #endregion

    #region Class Methods

    private void IRCInputProcedure(TextReader input, NetworkStream networkStream)
    {
        while (!stopThreads)
        {
            //if (!networkStream.DataAvailable)
            //    continue;

            buffer = input.ReadLine();

            if (debugPrintAll)
                Debug.Log("> " + buffer);
        }
    }

    public void SendCommand(string cmd)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue(cmd);
        }
    }

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    //private Socket s;
    private TcpClient s;
    private bool stopThreads = false;
    private Thread inProc, outProc;
    private string buffer = "";
    private List<string> recievedMsgs = new List<string>();
    private bool hasBeenConnected;
    private Queue<string> commandQueue = new Queue<string>();


    #endregion
}
