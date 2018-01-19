using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

//Primary code by Grahnz on GitHub: https://github.com/Grahnz/TwitchIRC-Unity

public class TwitchIRC : MonoBehaviour
{
	[Tooltip("oAuth Token for Twitch Account")]
    public string oauth;
	[Tooltip("Twitch Account Username")]
    public string nickName;
    private string server = "irc.chat.twitch.tv";
    private int port = 6667;

    //event(buffer).
    public class MsgEvent : UnityEngine.Events.UnityEvent<string> { }
    public MsgEvent messageRecievedEvent = new MsgEvent();

    private string buffer = string.Empty;
    private bool stopThreads = false;
    private Queue<string> commandQueue = new Queue<string>();
    private List<string> recievedMsgs = new List<string>();
    private System.Threading.Thread inProc, outProc;

	public didConnect Connected;
	public delegate void didConnect();
    public UnityEvent _onModeratorConnectedEvent;
	public didDisconnect Disconnected;
	public delegate void didDisconnect();
    public UnityEvent _onModeratorDisconnectedEvent;

    private bool hasBeenConnected;
    private bool hasBeenDisconnected;

    private System.Net.Sockets.TcpClient sock;
    public bool debugPrintAll;

    private void StartIRC()
    {
    	//Connect to Twitch Server
        sock = new System.Net.Sockets.TcpClient();
        sock.Connect(server, port);
        if (!sock.Connected)
        {
            Debug.LogWarning("Failed to connect!");
            return;
        }
        stopThreads = false;
        var networkStream = sock.GetStream();
        var input = new System.IO.StreamReader(networkStream);
       	var output = new System.IO.StreamWriter(networkStream);

        //Send username and password
       	output.WriteLine("PASS " + oauth);
        output.WriteLine("NICK " + nickName.ToLower());
        output.Flush();

        //output proc
       	outProc = new System.Threading.Thread(() => IRCOutputProcedure(output));
        outProc.Start();
        //input proc
        inProc = new System.Threading.Thread(() => IRCInputProcedure(input, networkStream));
        inProc.Start();
    }

    private void IRCInputProcedure(System.IO.TextReader input, System.Net.Sockets.NetworkStream networkStream)
    {
        while (!stopThreads)
        {

            if (!networkStream.DataAvailable)
                continue;


            //Debug.Log("[DEBUG:TwitchIRC] ?? ???");

            buffer = input.ReadLine();
            if (debugPrintAll)
            Debug.Log("> " + buffer);

            //was message?
            if (buffer.Contains("PRIVMSG #"))
            {
                lock (recievedMsgs)
                {

                    recievedMsgs.Add(buffer);
                }
            }

            //Send pong reply to any ping messages
            if (buffer.StartsWith("PING "))
            {
                SendCommand(buffer.Replace("PING", "PONG"));
            }


            //After server sends 001 command, we can join a channel
            if (buffer.Split(' ')[1] == "001")
            {
                SendCommand("JOIN #" + nickName.ToLower());
                if(Connected !=null)
                   Connected();
                hasBeenConnected = true;
            }
        }
    }
    private void IRCOutputProcedure(System.IO.TextWriter output)
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        while (!stopThreads)
        {
            lock (commandQueue)
            {
                if (commandQueue.Count > 0) //do we have any commands to send?
                {
                    // https://github.com/justintv/Twitch-API/blob/master/IRC.md#command--message-limit
                    if (stopWatch.ElapsedMilliseconds > 1750)
                    {
                        string topeek = commandQueue.Peek();
                        //send msg.
                        output.WriteLine(topeek);
                        output.Flush();
                        //remove msg from queue.
                        commandQueue.Dequeue();
                        //restart stopwatch.
                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                }
            }
        }
    }

    public void SendCommand(string cmd)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue(cmd);
        }
    }

    public void SendMsg(string msg)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue("PRIVMSG #" + nickName.ToLower() + " :" + msg);
        }
    }

    void Start()
    {
    	DontDestroyOnLoad(gameObject);
    	//if(oauth.Length > 0 && nickName.Length > 0)
    	//	StartIRC();
    }

    void OnEnable()
    {
        stopThreads = false;
    }

    public void Login(string user, string oAuth)
    {
    	oauth = oAuth;
    	nickName = user;
    	StartIRC();
    }

    void OnDisable()
    {
        stopThreads = true;
    }
    void OnDestroy()
    {
        stopThreads = true;
    }

    public void Disconnect()
    {
    	
    	stopThreads = true;
		inProc.Interrupt();
		outProc.Abort();
    	inProc.Abort();
    	outProc.Abort();
    	sock.Close();
		commandQueue = new Queue<string>();
		recievedMsgs = new List<string>();
		buffer = string.Empty;
    	if(GameObject.FindObjectOfType<TwitchLogin>() != null)
    	{
    		TwitchLogin[] l = GameObject.FindObjectsOfType<TwitchLogin>();
    		foreach(var L in l)
    		{
    			L.Disconnect();
                hasBeenDisconnected = true;
            }
    			
    	}
		Debug.Log("Chat Disconnected");
    }


    void Update()
    {
        if (hasBeenConnected)
        {
            _onModeratorConnectedEvent.Invoke();
            hasBeenConnected = false;
        }
        if (hasBeenDisconnected)
        {
            _onModeratorDisconnectedEvent.Invoke();
            hasBeenDisconnected = false;
        }
        lock (recievedMsgs)
        {
            if (recievedMsgs.Count > 0)
            {
                for (int i = 0; i < recievedMsgs.Count; i++)
                {
                    messageRecievedEvent.Invoke(recievedMsgs[i]);
                }
                recievedMsgs.Clear();
            }
        }
    }
}
