using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidzNeil.ChatAPI;
public class ListenToTwitchIRC : MonoBehaviour {


    public TwitchIRC m_ircManager;
    

	// Use this for initialization
	void Start () {

        m_ircManager.messageRecievedEvent.AddListener(MessageReceived);
	}

    private void MessageReceived(string message)
    {
        int indexOfMessageStart = message.IndexOf("PRIVMSG #");
        //NOT RESPECTING TWITCH STANDARD
        if (indexOfMessageStart < 0) return;



        string userMessageRaw = message.Substring(indexOfMessageStart + 9);
        string[] tokens = userMessageRaw.Split(':');
       //NOT MESSSAGE DETECTED
        if (tokens.Length < 2) return;

        string msg = tokens[1];
        string pseudo = tokens[0];
        GenerateAndSendMessage(pseudo, msg);
    }


    // Update is called once per frame
    void GenerateAndSendMessage (string pseudo, string message) {

        Message msg = new Message(pseudo,message, GetTime(), Platform.Twitch);
        ChatAPI.NotifyNewMessageToListeners(msg);
		
	}

    private long GetTime()
    {
        return Message.GetCurrentTimeUTC();
    }
}
