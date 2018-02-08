using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DidzNeil.ChatAPI;
using System;

public class ChatInput : MonoBehaviour
{
    public Dropdown username;
    public ChatHistory chatHistory;
    InputField inputField;

    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            inputField.text = chatHistory.GetPrevious();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            inputField.text = chatHistory.GetNext();
    }

    public void Send()
    {
        var text = inputField.text;
        if (text == string.Empty) return;

        print("sending: '" + text + "'");

        Message message = new Message(username.options[username.value].text, text, DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).Ticks, Platform.Mockup);

        try
        {
            ChatAPI.NotifyNewMessageToListeners(message);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            inputField.text = string.Empty;
            inputField.ActivateInputField();

            if (chatHistory)
                chatHistory.Add(message);
        }
    }
}
