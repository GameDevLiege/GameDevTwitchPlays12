using DidzNeil.ChatAPI;
using System;
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

    public bool debugPrintAll;

    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        print("Chtululu awakens!");

        s = new TcpClient();

        s.Connect(m_addr, m_port);

        var networkStream = s.GetStream();

        var input = new StreamReader(networkStream);

        inProc = new Thread(() => IRCInputProcedure(input, networkStream));
        inProc.Start();
    }

    private void Update()
    {
        lock (recievedMsgs)
        {
            if (recievedMsgs.Count > 0)
            {
                for (int i = 0; i < recievedMsgs.Count; i++)
                {
                    NotifyChatAPI(recievedMsgs[i]);
                }
                recievedMsgs.Clear();
            }
        }
    }

    #endregion

    #region Class Methods

    private void IRCInputProcedure(TextReader input, NetworkStream networkStream)
    {
        while (!stopThreads)
        {
            if (!networkStream.DataAvailable)
                continue;

            line = input.ReadLine();

            lock (recievedMsgs)
            {
                recievedMsgs.Add(line);
            }

            if (debugPrintAll)
                Debug.Log("> " + line);

            //// Can't do it here because the event is called within the non-main thread
            //// (and Unity related functionality, such as modifying transforms, etc, HATE when you do that)
            //NotifyChatAPI(line);
        }
    }

    private void NotifyChatAPI(string line)
    {
        if (debugPrintAll)
            print("Trying to notify");

        FBMessage fb_msg = JsonUtility.FromJson<FBMessage>(line);

        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long timestamp = (long)(DateTime.Now.ToUniversalTime() - unixStart).Ticks;

        // Linking the two classes together, oops!

        Message msg = new Message(fb_msg.from.name, fb_msg.message, timestamp, Platform.Facebook);
        ChatAPI.NotifyNewMessageToListeners(msg);
    }

    #endregion

    #region Tools Debug and Utility


    public void OnDisable()
    {
        CloseAll();
    }
    public void OnDestroy()
    {
        CloseAll();
    }
    public void OnApplicationQuit()
    {
        CloseAll();
    }

    public void CloseAll() {
        Debug.LogWarning("Close Socket & Thread connection");

        s.Close();
        inProc.Abort();
    }

    #endregion

    #region Private and Protected Members

    //private Socket s;
    private TcpClient s;
    private bool stopThreads = false;
    private Thread inProc;
    private string line = "";
    private List<string> recievedMsgs = new List<string>();


    #endregion
}
