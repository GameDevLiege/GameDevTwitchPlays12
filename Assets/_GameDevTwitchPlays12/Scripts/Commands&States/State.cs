using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public State(string stateName, long stateTime)
    {
        name = stateName;
        time = stateTime;
    }
}

