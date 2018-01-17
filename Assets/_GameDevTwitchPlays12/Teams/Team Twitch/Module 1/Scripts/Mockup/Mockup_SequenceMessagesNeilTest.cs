/*using DidzNeil.ChatAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mockup_SequenceMessagesNeilTest : MonoBehaviour {

    public  List<MessageToSend> _messageToSend;

    [Header("Debug")]
    public int _messageIndex;

    [System.Serializable]
    public class MessageToSend
    {
        public string _userName;
        public string _message;
    }

    private void OnValidate()
    {
        MyInputMessage();
    }

    public void MyInputMessage()
    {
        MessageToSend toSend = _messageToSend[_messageIndex];
        _messageIndex++;

        Message msg = new Message(
            toSend._userName, toSend._message
            , GetTimestamp(DateTime.Now), Platform.Mockup
            );
        ChatAPI.NotifyNewMessageToListeners(msg);
    }

    public static long GetTimestamp(DateTime dateTime)

    {

        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return (dateTime.ToUniversalTime() - unixStart).Ticks;

    }


}*/
