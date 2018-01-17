using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidzNeil.ChatAPI;
using System;

public class ChatAPIToConsole : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ChatAPI.AddListener(DisplayMessageWhenReceived);
	}

    private void DisplayMessageWhenReceived(Message message)
    {
        Debug.Log(message.GetUserName()+": "+ message.GetMessage());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
