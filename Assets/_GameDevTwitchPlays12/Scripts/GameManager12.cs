using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using GameManager;
using DidzNeil.ChatAPI;

public class FakeInput : IInput
{
    public void SendFeedback(ICommand command)
    {
        Debug.LogWarning("Command Feedback: " + command.response);
    }
}

public class GameManager12 : MonoBehaviour
{
    #region Public Members

    public IInput m_input = new FakeInput();

    public IGameEngine m_gameEngine;
    public ICommandManager m_commandManager;

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
        // Either lead to Nothing, Feedback user or influence the game.
        ChatAPI.AddListener(HandleMessage);

        // Item pickups influences the cooldown on the CommandManager
        SpecialAPI.AddListener(HandleEvent);
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
            m_input.SendFeedback(command);

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

    private void HandleEvent(ISpecial special)
    {
        string[] userInfo = special.m_playerCharacter.PlayerName.Split(new char[] { ' ' }, 2);

        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long timestamp = (DateTime.Now.ToUniversalTime() - unixStart).Ticks;

        string state = "";

        switch (special.m_typeSpecial)
        {
            case Special.e_specialType.PEBBLE:
                break;
            case Special.e_specialType.COINCHEST:
                break;
            case Special.e_specialType.GRENADES:
                break;
            case Special.e_specialType.SHOVEL:
                break;
            case Special.e_specialType.PARCHEMENT:
                state = "STUN";
                break;
            case Special.e_specialType.STRAIN:
                state = "STRAIN";
                break;
            case Special.e_specialType.GLASSES:
                break;
            default:
                break;
        }

        state = ((CommandManager)m_commandManager).firstStateCharacter + state;

        m_commandManager.Parse(userInfo[1], Int32.Parse(userInfo[0]), state, timestamp);
    }

    #endregion

    #region Class Methods

    #endregion

    #region Tools Debug and Utility

#if UNITY_EDITOR
    [MenuItem("GDL-Twitch12/Stun Player %t")]
    public static void StunPlayer()
    {
        Special spe = new Special
        {
            m_playerCharacter = new PlayerCharacter() { PlayerName = "0 Neil" },
            m_typeSpecial = Special.e_specialType.PARCHEMENT
        };

        SpecialAPI.NotifyNewSpecial(spe);

        string ticks = "?";
        try
        {
            ticks = GameObject.Find("PManager").GetComponent<CommandManager>().stunMult.ToString();
        }
        catch (Exception) { }

        Debug.LogWarning("Stunning Neil for " + ticks + " ticks!");
    }
#endif

    #endregion

    #region Private and Protected Members

    #endregion
}

