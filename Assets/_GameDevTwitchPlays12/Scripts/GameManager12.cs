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
    public bool m_debug;

    public ICommandManager m_commandManager;
    public PhysicsManager m_physicsManager;

    public TimerGame m_timerGame;

    bool gameIsStarted;
    #endregion

    #region System
    protected void Awake()
    {
        m_commandManager = GetComponent<CommandManager>();

        gameIsStarted = false;
    }

    protected void Start()
    {
        ChatAPI.AddListener(HandleMessage);
        PhysicsManager.AddEndGameTimerListener(TimerFunction);
        ItemEvent.AddPickupListener(HandleEvent);
    }

    private void Update()
    {
        m_timerGame.TimerGameStart(m_physicsManager.timerGame.timer, 60 * 20f);

        m_timerGame.TimerEquipeBLUE(m_physicsManager.timerBlue.timer, 60f);
        m_timerGame.TimerEquipeGREEN(m_physicsManager.timerGreen.timer, 60f);
        m_timerGame.TimerEquipeRED(m_physicsManager.timerRed.timer, 60f);
        m_timerGame.TimerEquipeYELLOW(m_physicsManager.timerYellow.timer, 60f);

        if(m_debug)
        {
            Debug.Log(string.Format("GameManager:Update() => Timers blue:{0} red:{1} yellow:{2} green:{3}", m_physicsManager.timerBlue.timer, m_physicsManager.timerRed.timer, m_physicsManager.timerYellow.timer, m_physicsManager.timerGreen.timer));
        }
    }

    public void TimerFunction(bool victory, Faction faction)
    {
        gameIsStarted = false;
        //TODO prévenir les joeurs par chat que la partie est terminée
        //TODO afficher l'écran de fin de jeu pendant x sec
    }

    public void DoCommand(string username, int platformCode, ICommand command)
    {
        if (command == null)
            return;

        if(m_debug)
        {
            Debug.Log(string.Format("GameManager12:DoCommand() => username:{0} feedback:{1} response:{2}", username, command.feedbackUser, command.response));
        }

        Platform platform = (Platform)platformCode;

        if (command.feedbackUser)
        {
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
        if (m_debug)
        {
            Debug.Log(string.Format("GameManager12:HandleMessage() => timestamp:{0} platform:{1} username:{2} message:{3}", message.GetTimestamp(), message.GetPlatform(), message.GetUserName(), message.GetMessage()));
        }
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
        if (m_debug)
        {
            Debug.Log(string.Format("GameManager12:HandleEvent() \n" +
                "=> ItemType:{0} EffectType:{1} goldValue:{2}\n" +
                "=> Player:{3} - {4}", item.ItemType, item.EffectType, item.goldValue, player.NumPlayer, player.Name));
        }

        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long timestamp = (DateTime.Now.ToUniversalTime() - unixStart).Ticks;

        string state = "";

        //if (item.EffectType == Item.e_effectType.INSTANT) ;

        /*
        switch (item.EffectType)
        {
            case Item.e_effectType.INSTANT:
                break;
            case Item.e_effectType.INVENTORY:
                break;
            default:
                break;
        }
        //*/
        switch (item.ItemType)
        {
            case Item.e_itemType.COINCHEST:
                float goldChest = item.goldValue; 
                //TODO : déjà ajouté à l'or du joueur + son, juste le prévenir dans le chat
                break;
            case Item.e_itemType.GRENADES:
                break;
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
        if(userInfo.Length == 1)
        {
            m_commandManager.Parse(userInfo[0], 0, state, timestamp);
        }
        else
        m_commandManager.Parse(userInfo[1], Int32.Parse(userInfo[0]), state, timestamp);
    }
    #endregion
}

