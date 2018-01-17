using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class CommandManager : DualBehaviour, ICommandManager
{
    #region Public Var

    public char firstCommmandCharacter;

    public static Command INVALIDCOMMAND = new Command("Votre commande est invalide");

    #endregion

    #region Public Func

    public ICommand Parse(string _username, int _plateform, string _message, long _time)
    {
        string userID = _plateform + " " + _username;
        
        if (!IsACommand(_message))
        {
            return null;
        }
        if (!CommandIsValid(_message))
        {
            return INVALIDCOMMAND;
        }
        userDataBase[userID] = _time;
        return new Command(_message);
    }

    #endregion

    #region Private Func

    private bool IsACommand(string message)
    {
        bool isValid;
        if (message[0] == firstCommmandCharacter)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
        return isValid;
    }

    private bool CommandIsValid(string message)
    {
        bool isValid = false;
        for (int i = 0; i < validCommand.Count; i++)
        {
            if (message.Equals(validCommand[i]))
            {
                isValid = true;
                break;
            }
        }
        return isValid;
    }

    #endregion

    #region Private Var

    [SerializeField]
    private List<string> validCommand = new List<string>
    {
        "command1",
        "command2",
        "command3",
    };

    private Dictionary<string, long> userDataBase = new Dictionary<string, long>();

    #endregion
}

public class Command:ICommand
{
    public string response;

    public Command(string message)
    {
        response = message;
    }
}

