using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidzNeil.ChatAPI;
using System;

public class ChatAPIToConsole : MonoBehaviour {

    public bool trackReceivedMessage;
    public bool trackSendingMessageToUser;
    public bool trackSendingMessageToAll;

    // Use this for initialization
    void Start ()
    {
        if(trackReceivedMessage)
            ChatAPI.AddListener(DisplayMessageWhenReceived);
        if (trackSendingMessageToUser)
            ChatAPI.AddGameToServerListener(DisplayMessageWhenSend);
        if (trackSendingMessageToAll)
            ChatAPI.AddGameToServerListener(DisplayMessageWhenSendToAll);
    }

    private void DisplayMessageWhenSend(string user, Platform platform, Message msg)
    {
        Debug.Log("Send, to" + user + ": "+ msg.GetMessage());
    }
    private void DisplayMessageWhenSendToAll(Message message)
    {
        Debug.Log("Send, to all: "+ message.GetMessage());
    }

    private void DisplayMessageWhenReceived(Message message)
    {
        Debug.Log("Received: "+message.GetUserName()+": "+ message.GetMessage());
    }
    
}
