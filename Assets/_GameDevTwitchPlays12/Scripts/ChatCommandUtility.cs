using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Us this class to convert string from the chat to usable and valide commande
/// </summary>
public class ChatCommandUtility : MonoBehaviour
{

    public static ChatCommandUtility Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public char firstCommmandCharacter = '!';

    [SerializeField]
    private List<string> m_validCommands = new List<string>
    {
        "UP"    ,
        "DOWN"  ,
        "LEFT"  ,
        "RIGHT" ,
        "DIG"   ,
        "JOIN"  ,
    };


    public bool ConvertMessageToCommand(string _message, out string _commandType) {
        
        _commandType = "";

        if (!IsCommandStartingCharTag(_message)) return false;

        string command = GetCommandFound(_message);
        if (command == null) return false;

        _commandType = command;
        return true;
    }

    private string GetCommandFound(string _message)
    {
        string found = null;
        foreach (string cmd in m_validCommands)
        {
            if (_message.Contains(cmd))
            {
                found = cmd;
                break;
            }
        }
        return found;
    }

    private bool IsCommandStartingCharTag(string _message)
    {
        bool isValid;
        if (_message[0] == firstCommmandCharacter)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
        return isValid;
    }




}
