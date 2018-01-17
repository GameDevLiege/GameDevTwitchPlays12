using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DidzNeil.ChatAPI;
using System;

[System.Serializable]
public class MessageEvent : UnityEvent<Message> { }


public class ChatAPIMono : MonoBehaviour
{
    public MessageEvent m_onNewMessage;


    private void Start()
    {
        ChatAPI.AddListener(RedirectMessage);
        //ChatAPI.IgnoreMockUp
    }

    private void RedirectMessage(Message message)
    {
        m_onNewMessage.Invoke(message);
    }

}
