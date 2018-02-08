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

  
    [SerializeField]
    private bool _ignoreMock;
    private bool _ignoreMockPrevious;




    private void Start()
    {
        ChatAPI.AddListener(RedirectMessage);
        ChatAPI.IgnoreMockUp = _ignoreMockPrevious = _ignoreMock;
    }

    private void RedirectMessage(Message message)
    {
        m_onNewMessage.Invoke(message);
    }


    public void OnValidate()
    {
        if(_ignoreMock != _ignoreMockPrevious)
        {
            _ignoreMockPrevious = _ignoreMock;
            ChatAPI.IgnoreMockUp = _ignoreMock;
        }
    }
}
