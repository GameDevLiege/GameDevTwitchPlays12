using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using System.Linq;
using System;

public class CommandManager : DualBehaviour, ICommandManager
{
    #region Public Var

    public long cd = 2;
    public long stunMult = 5;
    public long sprainMult = 5;
    public long fightMult = 5;
    public int  maxPlayer = 20;

    public char firstCommmandCharacter  = '!';
    public char firstStateCharacter     = '?';

    public static Command INVALIDCOMMAND = new Command("Votre commande est invalide", true);

    #endregion

    #region Public Func

    public ICommand Parse(string _username, int _plateform, string _message, long _time)
    {
        string userID = _plateform + " " + _username;
        
        if ((!IsACommand(_message)) && (!IsAState(_message)))
        {
            return null;
        }

        _message = _message.ToUpper();

        if ((!CommandIsValid(_message)) && (!StateIsValid(_message)))
        {
            return INVALIDCOMMAND;
        }

        // ???
        if(userDataBase.ContainsKey(userID))
        {
            var c = userDataBase[userID].states.Count;
            Debug.LogWarning(userID + " " + String.Join(" ", (from s in userDataBase[userID].states select s.Key + " " + (_time - s.Value.time)).ToArray<string>()));
        }
        // ???

        if (StateIsValid(_message))
        {
            if (_message.Equals(firstStateCharacter + "STUN"))
            {
                userDataBase[userID].AddStun(_time);
                return null;
            }

            if (_message.Equals(firstStateCharacter + "SPRAIN"))
            {
                userDataBase[userID].AddSprain(_time);
                return null;
            }

            if(_message.Equals(firstStateCharacter + "FIGHT"))
            {
                if (!userDataBase[userID].StateIsActive("FIGHT", _time))
                {
                    userDataBase[userID].AddFight(_time);
                    return null;
                }
                else
                {
                    int randomPos = UnityEngine.Random.Range(0, 3);
                    return new Command(movementCommand[randomPos], false);
                }
            }
        }

        if (CommandIsValid(_message))
        {
            if (_message.Equals(firstCommmandCharacter + "JOIN"))
            {
                if (!userDataBase.ContainsKey(userID))
                {
                    if (userDataBase.Count < maxPlayer)
                    {
                        userDataBase.Add(userID, new PlayerCTRL(userID));
                        return new Command(_message, false);
                    }
                    else
                    {
                        return new Command("Le nombre maximum de joueur est atteint", true);
                    }
                }
                else
                {
                    return new Command("Vous avez déja rejoins la partie", true);
                }
            }

            if (!userDataBase.ContainsKey(userID))
            {
                return new Command("Veuillez d'abord rejoindre la partie a l'aide de la commande " + firstCommmandCharacter + "JOIN", true);
            }

            if (userDataBase[userID].states.ContainsKey("FIGHT"))
            {
                if (userDataBase[userID].StateIsActive("FIGHT", _time))
                {
                    return new Command("Vous etes en train de combattre", true);
                }
            }

            if (userDataBase[userID].states.ContainsKey("STUN"))
            {
                if (userDataBase[userID].StateIsActive("STUN", _time))
                {
                    return new Command("Vous ne pouvez pas effectuer d'action car vous êtes STUN", true);
                }
            }

            if (userDataBase[userID].states.ContainsKey("SPRAIN") && (_message.Equals(firstCommmandCharacter + "DIG")))
            {
                if (userDataBase[userID].StateIsActive("SPRAIN", _time))
                {
                    return new Command("Vous ne pouvez pas creuser car vous vous êtes fais une entorse", true);
                }
            }

            if (!Cooldown(_time, userID)) // && (!_message.Equals(firstCommmandCharacter + "JOIN")))
            {
                return new Command("Le cooldown entre 2 commandes n'est pas terminé", true);
            }

            userDataBase[userID].time = _time;
            _message = ParseCommand(_message);
            return new Command(_message, false);
        }
        throw new System.Exception("[CommandManger] SHOULD NOT BE THERE: " + _username + ":" + _message + "("+_time+")" );
    }

    #endregion

    #region Private Func

    private bool IsACommand(string _message)
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

    private bool IsAState(string _message)
    {
        bool isValid;
        if (_message[0] == firstStateCharacter)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
        return isValid;
    }

    private bool CommandIsValid(string _message)
    {
        bool isValid = false;
        for (int i = 0; i < validCommand.Count; i++)
        {
            if (_message.Equals(firstCommmandCharacter + validCommand[i]))
            {
                isValid = true;
                break;
            }
        }
        return isValid;
    }

    private bool StateIsValid(string _message)
    {
        bool isValid = false;
        for (int i = 0; i < validState.Count; i++)
        {
            if (_message.Equals(firstStateCharacter + validState[i]))
            {
                isValid = true;
                break;
            }
        }
        return isValid;
    }

    private string ParseCommand(string _message)
    {
        string message;
        switch (_message)
        {
            case "!U":
                message = "!UP";
                break;
            case "!D":
                message = "!DOWN";
                break;
            case "!L":
                message = "!LEFT";
                break;
            case "!R":
                message = "!RIGHT";
                break;
            default:
                message = _message;
                break;
        }
        return message;
    }

    private bool Cooldown(long _time, string _name)
    {
        long oldTime;
        if (userDataBase.ContainsKey(_name))
        {
            oldTime = userDataBase[_name].time;
        }
        else
        {
            oldTime = 0;   
        }
        long value = _time - oldTime; 

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
        "UP"    ,
        "DOWN"  ,
        "LEFT"  ,
        "RIGHT" ,
        "DIG"   ,
        "JOIN"  ,
        "U"     ,
        "D"     ,
        "R"     ,
        "L"     ,
    };

    [SerializeField]
    private List<string> movementCommand = new List<string>
    {
        "UP"    ,
        "DOWN"  ,
        "LEFT"  ,
        "RIGHT" ,
    };

    [SerializeField]
    private List<string> validState = new List<string>
    {
        "STUN"      ,
        "SPRAIN"    ,
        "FIGHT"     ,
    };

    private Dictionary<string, PlayerCTRL> _userDataBase = new Dictionary<string, PlayerCTRL>();
    public Dictionary<string, PlayerCTRL> userDataBase
    {
        get { return _userDataBase; }
        set { _userDataBase = value; }
    }

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

[System.Serializable]
public class PlayerCTRL
{
    CommandManager _commandManager = new CommandManager();

    private string _userID;
    public string userID
    {
        get { return _userID; }
        set { _userID = value; }
    }

    private long _time;
    public long time
    {
        get { return _time; }
        set { _time = value; }
    }

    private Dictionary<string, State> _states = new Dictionary<string, State>();
    public Dictionary<string, State> states
    {
        get { return _states; }
        set { _states = value; }
    }

    public PlayerCTRL(string username)
    {
        userID = username;
        time = 0;
    }

    public void AddState(string _name, long _until)
    {
        if (!states.ContainsKey(_name))
        {
            states.Add(_name, new State(_name, _until));
        }
        else
        {
            states[_name].time = _until;
        }
    }

    public void AddStun(long _time)
    {
        AddState("STUN", (_time + (_commandManager.cd * _commandManager.stunMult)));
    }

    public void AddSprain(long _time)
    {
        AddState("SPRAIN", (_time + (_commandManager.cd * _commandManager.sprainMult)));
    }

    public void AddFight(long _time)
    {
        AddState("FIGHT", (_time + (_commandManager.cd * _commandManager.fightMult)));
        RemoveState("SPRAIN");
        RemoveState("STUN");
    }

    public void RemoveState(string _name)
    {
        if (states.ContainsKey(_name))
        {
            states.Remove(_name);
        }
    }

    public bool StateIsActive(string _name, long _time)
    {
        if (states.ContainsKey(_name))
        {
            if (states[_name].time > _time)
            {
                return true;
            }
            else
            {
                RemoveState(_name);
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}

public class State
{
    private string _name;
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    private long _time;
    public long time
    {
        get { return _time; }
        set { _time = value; }
    }

    public State (string stateName, long stateTime)
    {
        name = stateName;
        time = stateTime;
    }
}
