using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DidzNeil.ChatAPI;
using UnityEngine.UI;

public class ChatHistory : MonoBehaviour
{
    public Text textBox;
    ScrollRect scrollRect;
    List<string> history = new List<string>();
    int index = 0;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void Add(Message message)
    {
        history.Add(message.GetMessage());
        index = history.Count;

        textBox.text += message.GetUserName() + ": " + message.GetMessage() + "\n";
        scrollRect.verticalScrollbar.value = 0;
    }

    public string GetPrevious()
    {
        if (index > 0)
            index--;

        return history[index];
    }

    public string GetNext()
    {
        if (index < history.Count)
            index++;

        return index < history.Count ? history[index] : string.Empty;
    }
}
