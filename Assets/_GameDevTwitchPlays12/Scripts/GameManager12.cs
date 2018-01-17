using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : IPlayer
{
    public string username
    {
        get { return _username; }
        set { _username = value; }
    }

    public ITeam team
    {
        get { throw new System.NotImplementedException(); }
        set { throw new System.NotImplementedException(); }
    }

    private string _username;
}

public class GameManager12 : MonoBehaviour
{
    #region Public Members


    public IInput Input;
    public IGameEngine GameEngine;

    public List<IPlayer> Players = new List<IPlayer>();

    #endregion

    #region Public void

    #endregion

    #region System

    protected IEnumerator Start()
    {
        GenerateMap();

        yield return WaitingForPlayers();

        StartCoroutine(MainGameLoop());
    }

    private IEnumerator MainGameLoop()
    {
        GameEngine.SendInputs(Input.GetInputs());

        yield return new WaitForEndOfFrame();
    }

    private void GenerateMap()
    {
        // throw new NotImplementedException();
    }

    private IEnumerator WaitingForPlayers()
    {
        RetrievePlayers();

        while (Players.Count == 0)
        {
            Debug.Log("Waiting for players…");

            yield return new WaitForSeconds(2);

            RetrievePlayers();
        }
    }

    private void RetrievePlayers()
    {
        Players.Add(new FakePlayer());
    }

    private void Update()
    {
        
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
