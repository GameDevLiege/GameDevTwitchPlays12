using DidzNeil.ChatAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mockup_SequenceMessages : MonoBehaviour
{

    public float _minTimeSending = 0.1f;
    public float _maxTimeSending = 1f;

    public  List<MessageToSend> _messageToSend;

    [Header("Debug")]
    public int _messageIndex;

    [System.Serializable]
    public class MessageToSend {

        public string _userName;
        public string _message;
    }
    

    IEnumerator Start()
    {

        while (true)
        {
            SendNextMessage();
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minTimeSending, _maxTimeSending));
        }
    }

    private void SendNextMessage()
    {
        if (_messageToSend.Count == 0)
            throw new System.Exception("List must not be null, you damn morron");

        MessageToSend toSend = _messageToSend[_messageIndex];
        _messageIndex++;
        if (_messageIndex >= _messageToSend.Count)
            _messageIndex = 0;

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
}
