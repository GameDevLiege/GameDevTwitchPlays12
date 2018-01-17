using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class CommandManager : DualBehaviour, ICommandManager
{
    #region Public Var

    public long cd;

    public char firstCommmandCharacter;

    public static Command INVALIDCOMMAND = new Command("Votre commande est invalide", true);

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
        if (!Cooldown(_time, userID))
        {
            return new Command("Le cooldown entre 2 commandes n'est pas terminé", true);
        }
        userDataBase[userID] = _time;
        return new Command(_message, false);
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
            if (message.Equals(firstCommmandCharacter + validCommand[i]))
            {
                isValid = true;
                break;
            }
        }
        return isValid;
    }

    private bool Cooldown(long time, string name)
    {
        long oldTime;
        if (userDataBase.ContainsKey(name))
        {
            oldTime = userDataBase[name];
        }
        else
        {
            oldTime = 0;   
        }
        long value = time - oldTime; 

        if (value < cd)
        {
            return false;
        }
        return true;
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
    private bool _feedbackUser;

    public bool feedbackUser
    {
        get { return _feedbackUser; }
        set { _feedbackUser = value; }
    }

    private string _response;

    public string response
    {
        get { return _response; }
        set { _response = value; }
    }

    public Command(string message, bool feedback)
    {
        feedbackUser = feedback;
        response = message;

    }
}

