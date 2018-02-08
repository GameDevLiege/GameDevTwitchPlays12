using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commande : ICommand
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

    public Commande(string message, int number, bool feedback)
    {
        feedbackUser = feedback;
        numberOfIteration = number;
        response = message;
    }
}


