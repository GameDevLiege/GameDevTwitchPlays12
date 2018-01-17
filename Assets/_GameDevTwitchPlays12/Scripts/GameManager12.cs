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
    public ICommand Parse(string _username, int _platform, string _message, long _timestamp)
    {
        return null;
    }
}


public class GameManager12 : MonoBehaviour
{
    #region Public Members

    public IGameEngine GameEngine = new FakeGameEngine();

    public ICommandManager m_commandManager;

    private Queue<ICommand> m_commandQueue = new Queue<ICommand>();

    #endregion

    #region Public void

    #endregion

    #region System

    protected void Awake()
    {
        m_commandManager = GetComponent<CommandManager>();
    }

    protected void Start()
    {
        GameEngine.GenerateMap();

        ChatAPI.AddListener(HandleMessage);

        // HandleMessage("");

        StartCoroutine(MainGameLoop());
    }

    private void HandleMessage(Message message)
    {
        ICommand command = m_commandManager.Parse(
            message.GetUserName(),
            (int)message.GetPlatform(),
            message.GetMessage(),
            message.GetTimestamp()
        );

        // [TODO] Cleanup
        if(command != null && command != CommandManager.INVALIDCOMMAND)
            m_commandQueue.Enqueue(command);
    }

    private IEnumerator MainGameLoop()
    {
        while (m_commandQueue.Count != 0)
        {
            ICommand command = m_commandQueue.Dequeue();

            Debug.Log(command.response);

            GameEngine.Do(command);
        }

        yield return new WaitForEndOfFrame();
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}

