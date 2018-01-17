using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidzNeil.ChatAPI;
using System;

public class SendToTwitch : MonoBehaviour {

    // Use this for initialization
    IEnumerator Start()
    {
        ChatAPI.AddGameToServerListener(SendMessage);
        ChatAPI.AddGameToServerListener(SendMessageToAll);


        yield return new WaitForSeconds(1);

        ChatAPI.SendMessageToUser("jamscenter", Platform.Twitch, new Message("GameMaster", "bonjour toi", Message.GetCurrentTimeUTC(), Platform.Game));
        ChatAPI.SendMessageToEveryUsers(new Message("GameMaster", "bonjour tous",Message.GetCurrentTimeUTC(),Platform.Game));


    }

    private void SendMessage(string user, Platform platform, Message msg)
    {





    }
    private void SendMessageToAll(Message msg)
    {





    }

    // Update is called once per frame
    void Update () {
		
	}

    
}
