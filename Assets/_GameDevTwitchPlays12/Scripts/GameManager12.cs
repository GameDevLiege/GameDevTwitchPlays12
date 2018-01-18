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
    public void SendFeedback(ICommand command)
    {
        Debug.LogWarning("Command Feedback: " + command.response);
    }
}

//public class FakeGameEngine : IGameEngine
//{
//    public void Do(ICommand command)
//    {
//        throw new NotImplementedException();
//    }

//    public void Do(List<ICommand> _commands)
//    {
//        throw new NotImplementedException();
//    }

//    public void GenerateMap() { }
//}

//public class FakeCommandManager : ICommandManager
//{
//    public ICommand Parse(string _username, int _platform, string _message, long _timestamp)
//    {
//        return null;
//    }
//}


public class GameManager12 : MonoBehaviour
{
    #region Public Members

    public IGameEngine m_gameEngine;

    public IInput m_input = new FakeInput();

    public ICommandManager m_commandManager;

    private Queue<ICommand> m_commandQueue = new Queue<ICommand>();

    #endregion

    #region Public void

    #endregion

    #region System

    protected void Awake()
    {
        m_commandManager = GetComponent<CommandManager>();
        m_gameEngine = GetComponent<PhysicsManager>();
    }

    protected void Start()
    {
        // m_gameEngine.GenerateMap();

        ChatAPI.AddListener(HandleMessage);
    }

    private void HandleMessage(Message message)
    {
        ICommand command = m_commandManager.Parse(
            message.GetUserName(),
            (int)message.GetPlatform(),
            message.GetMessage(),
            message.GetTimestamp()
        );

        if (command == null)
            return;

        if (command.feedbackUser)
        {
            Message msg = new Message("Game Admin", command.response, Message.GetCurrentTimeUTC(), Platform.Game);

            //ChatAPI.SendMessageToEveryUsers(msg);
            ChatAPI.SendMessageToUser(message.GetUserName(), message.GetPlatform(), msg);
        }
        else
        {
            if (command.response == "!START")
            {
                List<string> playerList = new List<string>(m_commandManager.userDataBase.Keys);
                m_gameEngine.AssignFactionToPlayers(playerList);
            }

            string userId = (int)message.GetPlatform() + " " + message.GetUserName();
            string formattedCommand = command.response.Substring(1).ToUpper();

            m_gameEngine.GetCommandFromPlayer(userId, formattedCommand);
        }
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

