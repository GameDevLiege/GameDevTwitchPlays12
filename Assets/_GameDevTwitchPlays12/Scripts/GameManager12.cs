using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameManager;
using DidzNeil.ChatAPI;

public class FakePlayer : IPlayer
{
    public string Username;
    public ITeam Team;

    public override string ToString()
    {
        return String.Format("Player<{0}>", Username);
    }
}

public class FakeInput : IInput
{
    public List<IMessage> GetInput()
    {
        throw new NotImplementedException();
    }
}

public class FakeGameEngine : IGameEngine
{
    public void Do(ICommand command)
    {
        throw new NotImplementedException();
    }

    public void Do(List<ICommand> _commands)
    {
        throw new NotImplementedException();
    }

    public void GenerateMap() { }
}

public class FakeCommandManager : ICommandManager
{
    public ICommand Parse(string _username, string _message, long _timestamp)
    {
        throw new NotImplementedException();
    }
}


public class GameManager12 : MonoBehaviour
{
    #region Public Members

    public IGameEngine GameEngine = new FakeGameEngine();

    public List<IPlayer> Players = new List<IPlayer>();

    public ICommandManager m_commandManager = new FakeCommandManager();
    public Queue<ICommand> m_commandQueue = new Queue<ICommand>();

    #endregion

    #region Public void

    #endregion

    #region System

    protected void Start()
    {
        GameEngine.GenerateMap();

        ChatAPI.AddListener(HandleMessage);

        HandleMessage("");

        StartCoroutine(MainGameLoop());
    }

    private void HandleMessage(Message message)
    {
        ICommand command = m_commandManager.Parse(
            message.GetUserName(),
            message.GetMessage(),
            message.GetTimestamp()
        );

        m_commandQueue.Enqueue(command);
    }

    private IEnumerator MainGameLoop()
    {
        while (m_commandQueue.Count != 0)
        {
            ICommand command = m_commandQueue.Dequeue();

            GameEngine.Do(command);
        }

        yield return new WaitForEndOfFrame();
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

