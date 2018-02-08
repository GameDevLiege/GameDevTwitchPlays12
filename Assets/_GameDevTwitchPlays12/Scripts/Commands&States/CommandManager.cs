using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using System.Linq;
using System;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour, ICommandManager
{
    #region Public Var

    public GameManager12 gameManager;

    public bool debug = false;

    public int maxMovement = 5;
    public long cooldown = 2;
    public long stunMult = 5;
    public long sprainMult = 5;
    public long fightMult = 5;
    public int  maxPlayer = 20;

    public char firstCommmandCharacter  = '!';
    public char firstStateCharacter     = '?';

    #endregion

    #region Public Func

    private void Awake()
    {
        cooldown = cooldown * 10000000;
    }

    public void Parse(string _username, int _plateform, string _message, long _time)
    {
        string userID = _plateform + " " + _username;
        _message = _message.ToUpper().Trim();

        if (!string.IsNullOrEmpty(_message))
        {
            if (StartAsCommand(_message))
            {
                if (CommandIsValid(_message))
                {
                    string[] splitedMessage = SplitMessage(_message);
                    splitedMessage[0] = ParseCommand(splitedMessage[0]);

                    if (_message.Equals(firstCommmandCharacter + "JOIN"))
                    {
                        if (!userDataBase.ContainsKey(userID))
                        {
                            if (userDataBase.Count < maxPlayer)
                            {
                                userDataBase.Add(userID, new PlayerCTRL(userID, this));
                                gameManager.DoCommand(_username, _plateform, new Command(_message, false));
                            }
                            else
                            {
                                if (debug)
                                {
                                    Debug.Log("Maximum number of players reached!");
                                }                               
                                gameManager.DoCommand(_username, _plateform, new Command("Maximum number of players reached!", true));
                            }
                        }
                        else
                        {
                            if (debug)
                            {
                                Debug.Log("You already joined the game");
                            }
                            gameManager.DoCommand(_username, _plateform, new Command("You already joined the game", true));
                        }
                    }
                    else if (!userDataBase.ContainsKey(userID))
                    {
                        if (debug)
                        {
                            Debug.Log("Please first join the game using \"" + firstCommmandCharacter + "JOIN\" command.");
                        }
                        gameManager.DoCommand(_username, _plateform, new Command("Please first join the game using \"" + firstCommmandCharacter + "JOIN\" command.", true));
                    }
                    else if (userDataBase[userID].states.ContainsKey("FIGHT"))
                    {
                        if (userDataBase[userID].StateIsActive("FIGHT", _time))
                        {
                            if (debug)
                            {
                                Debug.Log("You're busy fighting right now!");
                            }
                            gameManager.DoCommand(_username, _plateform, new Command("You're busy fighting right now!", true));
                        }
                    }
                    else if (userDataBase[userID].states.ContainsKey("STUN"))
                    {
                        if (userDataBase[userID].StateIsActive("STUN", _time))
                        {
                            if (debug)
                            {
                                Debug.Log("You can't do anything right now because you're still STUN.");
                            }
                            gameManager.DoCommand(_username, _plateform, new Command("You can't do anything right now because you're still STUN.", true));
                        }
                    }
                    else if (userDataBase[userID].states.ContainsKey("MOVE"))
                    {
                        if (userDataBase[userID].StateIsActive("MOVE", _time))
                        {
                            if (debug)
                            {
                                Debug.Log("You're busy moving right now!");
                            }
                            gameManager.DoCommand(_username, _plateform, new Command("You're busy moving right now!", true));
                        }
                    }
                    else if (userDataBase[userID].states.ContainsKey("SPRAIN") && (_message.Equals(firstCommmandCharacter + "DIG")))
                    {
                        if (userDataBase[userID].StateIsActive("SPRAIN", _time))
                        {
                            if (debug)
                            {
                                Debug.Log("You can't move now because you have a sprain!");
                            }
                            gameManager.DoCommand(_username, _plateform, new Command("You can't move now because you have a sprain!", true));
                        }
                    }
                    else if (!Cooldown(_time, userID))
                    {
                        if (debug)
                        {
                            Debug.Log("Please wait for your cooldown to be over!");
                        }
                        gameManager.DoCommand(_username, _plateform, new Command("Please wait for your cooldown to be over!", true));
                    }
                    else if (splitedMessage.Length == 2)
                    {
                        if (ArgsIsValid(splitedMessage[1]))
                        {
                            int number;
                            int.TryParse(splitedMessage[1], out number);
                            userDataBase[userID].AddState("MOVE", (_time + (cooldown)));

                            StartCoroutine(Iteration(_username, _plateform, new Command(splitedMessage[0], false), number, userID));
                        }
                        else
                        {
                            if (debug)
                            {
                                Debug.Log("Invalid argument");
                            }
                            gameManager.DoCommand(_username, _plateform, new Command("Invalid argument", true));
                        }
                    }
                    else
                    {
                        userDataBase[userID].time = _time;
                        gameManager.DoCommand(_username, _plateform, new Command(splitedMessage[0], false));
                    }
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("Your command is invalid!");
                    }
                    gameManager.DoCommand(_username, _plateform, new Command("Your command is invalid!", true));
                }
            }
            else if (StartAsState(_message))
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
                                gameManager.DoCommand(_username, _plateform, new Command(movementCommand[randomPos], false));
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("Invalid state input, ignored");
                    }
                    gameManager.DoCommand(_username, _plateform, new Command("Invalid state input, ignored", false));
                }
            }
        } 
    }
    #endregion

    #region Private Func

    private IEnumerator Iteration(string _username, int _plateform, ICommand _command, int number, string userID)
    {
        for (int i = 0; i < number; i++)
        {
            if ((userDataBase[userID].states.ContainsKey("MOVE")))
            {
                gameManager.DoCommand(_username, _plateform, _command);
                yield return new WaitForSeconds((float)cooldown/10000000f);
                userDataBase[userID].states["MOVE"].time += cooldown;
            }
        }
        userDataBase[userID].RemoveState("MOVE");
    }

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

    private bool MessageIsNull(string _message)
    {
        if (_message == null)
        {
            return true;
        }
        return false;
    }

    private bool CommandIsValid(string _message)
    {
        bool isValid = false;

        string[] splitedMessage = SplitMessage(_message);

        if (splitedMessage.Length <= 2)
        {
            for (int i = 0; i < validCommand.Count; i++)
            {
                if (splitedMessage[0].Equals(firstCommmandCharacter + validCommand[i]))
                {
                    isValid = true;
                    break;
                }
                else
                {
                    isValid = false;
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
                if (debug)
                {
                    Debug.Log("message : "+ _message);
                }
                break;
            case "!D":
                message = "!DOWN";
                if (debug)
                {
                    Debug.Log("message : " + _message);
                }
                break;
            case "!L":
                message = "!LEFT";
                if (debug)
                {
                    Debug.Log("message : " + _message);
                }
                break;
            case "!R":
                message = "!RIGHT";
                if (debug)
                {
                    Debug.Log("message : " + _message);
                }
                break;
            default:
                message = _message;
                if (debug)
                {
                    Debug.Log("message : " + _message);
                }
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

        if (value < cooldown)
        {
            return false;
        }
        return true;
    }

    #endregion

    #region Private Var

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
        "L"         ,
        "R"         ,
        "LEVELUP"   ,
        "SHOVEL"    ,
        "GRENADE"   ,
    };

    private List<string> movementCommand = new List<string>
    {
        "UP"    ,
        "DOWN"  ,
        "LEFT"  ,
        "RIGHT" ,
    };

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
