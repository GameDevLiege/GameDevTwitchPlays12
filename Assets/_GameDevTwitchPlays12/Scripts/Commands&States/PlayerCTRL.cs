using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCTRL
{
    CommandManager _commandManager;

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

    public PlayerCTRL(string username, CommandManager manager)
    {
        _commandManager = manager;
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

    public void AddAutoDig(long _time)
    {
        if (_commandManager.debug)
        {
            Debug.Log("durée : " + (_commandManager.cooldown * _commandManager.autoDigMult));
        }
        AddState("AUTODIG", (_time + (_commandManager.cooldown * _commandManager.autoDigMult)));
    }

    public void AddStun(long _time)
    {
        if (_commandManager.debug)
        {
            Debug.Log("durée : " + (_commandManager.cooldown * _commandManager.stunMult));
        }
        AddState("STUN", (_time + (_commandManager.cooldown * _commandManager.stunMult)));
        RemoveState("AUTODIG");
    }

    public void AddSprain(long _time)
    {
        if (_commandManager.debug)
        {
            Debug.Log("durée : " + (_commandManager.cooldown * _commandManager.sprainMult));
        }
        AddState("SPRAIN", (_time + (_commandManager.cooldown * _commandManager.sprainMult)));
        RemoveState("AUTODIG");
    }

    public void AddFight(long _time)
    {
        if (_commandManager.debug)
        {
            Debug.Log("durée : " + (_commandManager.cooldown * _commandManager.fightMult));
        }
        AddState("FIGHT", (_time + (_commandManager.cooldown * _commandManager.fightMult)));
        RemoveState("SPRAIN");
        RemoveState("STUN");
        RemoveState("MOVE");
        RemoveState("AUTODIG");
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
                if (_commandManager.debug)
                {
                    Debug.Log((_name)+" is active");
                }
                return true;
            }
            else
            {
                if (_commandManager.debug)
                {
                    Debug.Log("remove : " + (_name));
                    Debug.Log("buffTime : " + states[_name].time + (" currentTime : " + _time));
                }
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
