using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using GameManager;
using DidzNeil.ChatAPI;

public class GameManager12 : MonoBehaviour
{
    #region Public Members    
    public bool m_debug = false;

    public ICommandManager m_commandManager;
    public PhysicsManager m_physicsManager;

    bool gameIsStarted;
    #endregion

    #region Public void

    #endregion

    #region System

    protected void Awake()
    {
        m_commandManager = GetComponent<CommandManager>();
        // m_physicsManager = GetComponent<PhysicsManager>(); //find ?

        gameIsStarted = false;
    }

    protected void Start()
    {
        // Either lead to Nothing, Feedback user or influence the game.
        ChatAPI.AddListener(HandleMessage);

        // Item pickups influences the cooldown on the CommandManager
        //SpecialAPI.AddListener(HandleEvent);

        ItemEvent.AddPickupListener(HandleEvent);

        //ItemEvent.AddUseListener(); //Pour UI
    }

    public void DoCommand(string username, int platformCode, ICommand command)
    {
        if (command == null)
            return;

        Platform platform = (Platform)platformCode;

        if (command.feedbackUser)
        {
            if (m_debug) Debug.LogWarning("Command Feedback: " + command.response);

            Message msg = new Message("Game Admin", command.response, Message.GetCurrentTimeUTC(), Platform.Game);
            ChatAPI.SendMessageToUser(username, platform, msg);
        }
        else
        {
            if (command.response == "!START" && !gameIsStarted)
            {
                gameIsStarted = true;
                m_physicsManager.StartGame();
            }
            string userId = platformCode + " " + username;
            string formattedCommand = command.response.Substring(1).ToUpper();

            m_physicsManager.SetCommandFromPlayer(userId, formattedCommand);
        }
    }

    private void HandleMessage(Message message)
    {
        m_commandManager.Parse(
            message.GetUserName(),
            (int)message.GetPlatform(),
            message.GetMessage(),
            message.GetTimestamp()
        );
    }

    public void ResetGame()
    {
        gameIsStarted = false;
    }

    private void HandleEvent(Item item, Player player)
    {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long timestamp = (DateTime.Now.ToUniversalTime() - unixStart).Ticks;

        string state = "";

        //if (item.EffectType == Item.e_effectType.INSTANT) ;

        switch (item.ItemType)
        {
        //    case Item.e_itemType.PEBBLE:
        //        break;
            case Item.e_itemType.COINCHEST:
                float goldChest = item.goldValue; //TODO
                break;
        //    case Item.e_itemType.GRENADES:
        //        break;
            case Item.e_itemType.SHOVEL:
                break;
            case Item.e_itemType.PARCHEMENT:
                state = "STUN";
                break;
            case Item.e_itemType.STRAIN:
                state = "STRAIN";
                break;
            case Item.e_itemType.GLASSES:
                break;
            default:
                break;
        }

        state = ((CommandManager)m_commandManager).firstStateCharacter + state;
        string[] userInfo = player.Name.Split(' ');
        if(m_debug)
        {
            Debug.Log(player.Name);
            foreach (string s in userInfo) Debug.Log(s);
        }
        m_commandManager.Parse(userInfo[1], Int32.Parse(userInfo[0]), state, timestamp);
    }
    #endregion

    #region Tools Debug and Utility
    /*
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
    */
    #endregion
}

