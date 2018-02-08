using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidzNeil.ChatAPI;
using System.Text.RegularExpressions;
public class ListenToTwitchIRC : MonoBehaviour {


    public TwitchIRC m_ircManager;
    

	// Use this for initialization
	void Start () {

        m_ircManager.messageRecievedEvent.AddListener(MessageReceived);
	}
    //TODO manque le channel reçu du message (comment on l'implémente?)
    private void MessageReceived(string message)
    {
       /* Match match = Regex.Match(message, "(?<=\\#).*");
        string myInfo= match.Success ? match.Groups[0].Value : "";
        Match matchUser = Regex.Match(myInfo, ".*(?=\\s\\:)");
        Match matchMsg= Regex.Match(myInfo, "(?<=\\:).*");
        string pseudo = matchUser.Success ? matchUser.Groups[0].Value:"";
        string msg= matchMsg.Success ? matchMsg.Groups[0].Value:"";*/


         int indexOfMessageStart = message.IndexOf("PRIVMSG #");
         //NOT RESPECTING TWITCH STANDARD
         if (indexOfMessageStart < 0) return;

        string pseudo = "";
        int pseudoStart = message.IndexOf('!')+1;
        int pseudoEnd = message.IndexOf('@');
        pseudo = message.Substring(pseudoStart, pseudoEnd - pseudoStart);

        Debug.LogWarning("0");

        string userMessageRaw = message.Substring(indexOfMessageStart + 9);
         string[] tokens = userMessageRaw.Split(':');
        //NOT MESSSAGE DETECTED
         if (tokens.Length < 2) return;

         
         string channel = tokens[0];

        if (string.IsNullOrEmpty(pseudo))
            return;
        string msg = userMessageRaw.Substring(channel.Length+1);
        if (string.IsNullOrEmpty(msg))
            return;


        Debug.LogWarning("1");

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
