using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using System.Linq;
using System;
using UnityEngine.UI;

public class CommandManager : DualBehaviour, ICommandManager
{
    #region Public Var

    public GameManager12 gameManager;

    //Test
    public InputField mess;

    public int maxMovement = 5;
    public long cd = 2;
    public long stunMult = 5;
    public long sprainMult = 5;
    public long fightMult = 5;
    public int  maxPlayer = 20;

    public char firstCommmandCharacter  = '!';
    public char firstStateCharacter     = '?';

    #endregion

    #region Public Func

    public void Test()
    {
        mess.text = Parse("test", 42, mess.text, 4242).response;   
    }

    public ICommand Parse(string _username, int _plateform, string _message, long _time)
    {
        string userID = _plateform + " " + _username;
        _message = _message.ToUpper();

        if (StartAsCommand(_message)) 
        {
            if (CommandIsValid(_message))
            {
                string[] splitedMesage = SplitMessage(_message);

                if (_message.Equals(firstCommmandCharacter + "JOIN"))
                {
                    if (!userDataBase.ContainsKey(userID))
                    {
                        if (userDataBase.Count < maxPlayer)
                        {
                            userDataBase.Add(userID, new PlayerCTRL(userID, this));
                            return new Command(_message, 1, false);
                        }
                        else
                        {
                            return new Command("Le nombre maximum de joueur est atteint", 1, true);
                        }
                    }
                    else
                    {
                        return new Command("Vous avez déja rejoins la partie", 1, true);
                    }
                }

                if (!userDataBase.ContainsKey(userID))
                {
                    return new Command("Veuillez d'abord rejoindre la partie a l'aide de la commande " + firstCommmandCharacter + "JOIN", 1, true);
                }

                if (userDataBase[userID].states.ContainsKey("FIGHT"))
                {
                    if (userDataBase[userID].StateIsActive("FIGHT", _time))
                    {
                        return new Command("Vous etes en train de combattre", 1, true);
                    }
                }

                if (userDataBase[userID].states.ContainsKey("STUN"))
                {
                    if (userDataBase[userID].StateIsActive("STUN", _time))
                    {
                        return new Command("Vous ne pouvez pas effectuer d'action car vous êtes STUN", 1, true);
                    }
                }

                if (userDataBase[userID].states.ContainsKey("MOVE"))
                {
                    if (userDataBase[userID].StateIsActive("MOVE", _time))
                    {
                        return new Command("Vous êtes en cours de mouvement", 1, true);
                    }
                }

                if (userDataBase[userID].states.ContainsKey("SPRAIN") && (_message.Equals(firstCommmandCharacter + "DIG")))
                {
                    if (userDataBase[userID].StateIsActive("SPRAIN", _time))
                    {
                        return new Command("Vous ne pouvez pas creuser car vous vous êtes fais une entorse", 1, true);
                    }
                }

                if (!Cooldown(_time, userID)) // && (!_message.Equals(firstCommmandCharacter + "JOIN")))
                {
                    return new Command("Le cooldown entre 2 commandes n'est pas terminé", 1, true);
                }

                if (splitedMesage.Length == 2)
                {
                    if (ArgsIsValid(splitedMesage[1]))
                    {
                        int number;
                        int.TryParse(splitedMesage[1], out number);
                        userDataBase[userID].AddState("MOVE", (_time + (number * cd)));
                        return new Command(splitedMesage[0], number, false);
                    }
                    else
                    {
                        return new Command("Argument invalide", 1, true);
                    }
                }

                userDataBase[userID].time = _time;
                splitedMesage[0] = ParseCommand(splitedMesage[0]);
                return new Command(splitedMesage[0], 1, false);
            }
            else return new Command("Votre commande est invalide", 1, true);
        }

        if (StartAsState(_message))
        {
            if (StateIsValid(_message))
            {
                switch (_message)
                {
                    case "?STUN":
                        userDataBase[userID].AddStun(_time);
                        break;
                    case "?SPRAIN":
                        userDataBase[userID].AddSprain(_time);
                        break;
                    case "?FIGHT":
                        if (!userDataBase[userID].StateIsActive("FIGHT", _time))
                        {
                            userDataBase[userID].AddFight(_time);
                        }
                        else
                        {
                            int randomPos = UnityEngine.Random.Range(0, 3);
                            return new Command(movementCommand[randomPos], 1, false);
                        }
                        break;
                    default:
                        break;
                }
                return null;
            }
            else return new Command("Invalid state input, ignored", 1, false);
        }

        return null;  
    }

    #endregion

    #region Private Func

    private bool ArgsIsValid(string arg)
    {
        bool isValid;
        int number;

        if (int.TryParse(arg, out number))
        {
            if (number > 0 && number <= maxMovement)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
        }
        else
        {
            isValid = false;
        }
        return isValid;
    }

    private string[] SplitMessage(string _message)
    {
        string[] splitedMessage = _message.Split(' ');
        return splitedMessage;
    }

    private bool StartAsState(string _message)
    {
        return _message[0] == firstStateCharacter;
    }

    private bool StartAsCommand(string _message)
    {
        return _message[0] == firstCommmandCharacter;
    }

    private bool CommandIsValid(string _message)
    {
        bool isValid = false;

        if (SplitMessage(_message).Length <= 2)
        { 
            for (int i = 0; i < validCommand.Count; i++)
            {
                if (SplitMessage(_message)[0].Equals(firstCommmandCharacter + validCommand[i]))
                {
                    isValid = true;
                }
            }
        }
        else
        {
            isValid = false;
        }
        return isValid;
    }

    private bool StateIsValid(string _message)
    {

        //return validState.Contains(firstStateCharacter + _message);

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
        "UP"        ,
        "DOWN"      ,
        "LEFT"      ,
        "RIGHT"     ,
        "DIG"       ,
        "JOIN"      ,
        "U"         ,
        "D"         ,
        "R"         ,
        "L"         ,
        "LEVELUP"   ,
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
        "MOVE"      ,
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

    private int _numberOfIteration;
    public int numberOfIteration
    {
        get { return _numberOfIteration; }
        set { _numberOfIteration = value; }
    }

    private string _response;
    public string response
    {
        get { return _response; }
        set { _response = value; }
    }

    public Command(string message, int number, bool feedback)
    {
        feedbackUser = feedback;
        response = message;
        numberOfIteration = number;
    }
}
