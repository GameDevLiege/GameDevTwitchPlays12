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

    public AffichageInventaire m_InventoryDisplay;
    public TimerGame m_timerGame;

    bool gameIsStarted;
    public float TimeBeforeRestart = 10f;
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
        PhysicsManager.AddEndGameTimerListener(DoEndGame);
        ItemEvent.AddPickupListener(HandleEvent);
        ItemEvent.AddUseListener(HandleUse);
    }

    private void Update()
    {
        m_timerGame.TimerGameStart(m_physicsManager.timerGame.timer, 60 * 20f);

        switch(m_physicsManager.CurrentTeamTimer)
        {
            case 1: m_timerGame.TimerEquipeRED(m_physicsManager.timerRed.timer, 60f); break;
            case 2: m_timerGame.TimerEquipeBLUE(m_physicsManager.timerBlue.timer, 60f); break;
            case 3: m_timerGame.TimerEquipeGREEN(m_physicsManager.timerGreen.timer, 60f); break;
            case 4: m_timerGame.TimerEquipeYELLOW(m_physicsManager.timerYellow.timer, 60f); break;
        }    
    }

    public UIWins UIWinInterfaceScript;
    public void DoEndGame(bool victory, Faction faction)
    {
        gameIsStarted = false;
        UIWinInterfaceScript.ActiveUIWins(true);
        UIWinInterfaceScript.SetInfo(GetFactionStringFromFaction(faction), faction.FactionColor);
        //TODO prévenir les joeurs par chat que la partie est terminée
        //TODO afficher l'écran de fin de jeu pendant x sec

        Invoke("RestartGame", TimeBeforeRestart);
    }
    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
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

    public void HandleUse(Item.e_itemType item, Player player)
    {
        if (m_debug)
        {
            Debug.Log(string.Format("GameManager12:HandleUse() => ItemType:{0} Player:{3} - {4}", item, player.NumPlayer, player.Name));
        }

        int platformCode;
        string playerName;
        SplitPlayerID(player.Name, out platformCode, out playerName);

        string faction = GetFactionStringFromFaction(player.Faction);

        switch (item)
        {
            case Item.e_itemType.GRENADES:
                m_InventoryDisplay.RetireInventaire(faction, player.Faction.ListPlayer.IndexOf(player) + 1, "GRENADE");
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous avez utilisé une grenade!");
                break;
            case Item.e_itemType.SHOVEL:
                m_InventoryDisplay.RetireInventaire(faction, player.Faction.ListPlayer.IndexOf(player) + 1, "PELLE");
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous utilisez maintenant une pelle pour creuser plus vite.");
                SendCommand("AUTODIG", player.Name);
                break;
            default:
                Debug.LogWarning(string.Format("GameManager12: HandleUse() => default switch ({0} received)", item.ToString()));
                break;
        }
    }

    private void HandleEvent(Item item, Player player)
    {
        if (m_debug)
        {
            Debug.Log(string.Format("GameManager12:HandleEvent() \n" +
                "=> ItemType:{0} EffectType:{1} goldValue:{2}\n" +
                "=> Player:{3} - {4}", item.ItemType, item.EffectType, item.goldValue, player.NumPlayer, player.Name));
        }

        string faction = GetFactionStringFromFaction(player.Faction);

        int platformCode;
        string playerName;
        SplitPlayerID(player.Name, out platformCode, out playerName);

        string state = "";  
        switch (item.ItemType)
        {
            case Item.e_itemType.COINCHEST:
                float goldChest = item.goldValue;
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous avez découvert un coffre de pièces!");
                break;
            case Item.e_itemType.GRENADES:
                m_InventoryDisplay.AjoutInventaire(faction, player.Faction.ListPlayer.IndexOf(player) + 1, "GRENADE");
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous avez rammassé une grenade!");
                break;
            case Item.e_itemType.SHOVEL:
                m_InventoryDisplay.AjoutInventaire(faction, player.Faction.ListPlayer.IndexOf(player) + 1, "PELLE");
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous avez trouvé une pelle!");
                break;
            case Item.e_itemType.PARCHEMENT:
                state = "STUN";
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous vous êtes fait une entorse.");
                break;
            case Item.e_itemType.STRAIN:
                state = "STRAIN";
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous vous êtes fait une entorse.");
                break;
            case Item.e_itemType.GLASSES:
                SendMessageToPlayer((Platform)platformCode, playerName, "Vous avez les lunettes!");
                break;
            default:
                break;
        }

        SendCommand(state, player.Name);
    }

    private long GetTimestamp()
    {
        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return (DateTime.Now.ToUniversalTime() - unixStart).Ticks;
    }

    private void SendCommand(string state, string playerId)
    {
        state = ((CommandManager)m_commandManager).firstStateCharacter + state;

        int platformCode;
        string playerName;
        SplitPlayerID(playerId, out platformCode, out playerName);

        m_commandManager.Parse(playerName, platformCode, state, GetTimestamp());
        /*
        string[] userInfo = playerId.Split(' ');
        if (userInfo.Length == 1)
        {
            m_commandManager.Parse(userInfo[0], 0, state, timestamp);
        }
        else
            m_commandManager.Parse(userInfo[1], Int32.Parse(userInfo[0]), state, timestamp);
            */
    }

    private string GetFactionStringFromFaction(Faction faction)
    {
        string factionString = "";
        switch (faction.NumFaction)
        {
            case 1: factionString = "RED"; break;
            case 2: factionString = "BLUE"; break;
            case 3: factionString = "GREEN"; break;
            case 4: factionString = "YELLOW"; break;
        }
        return factionString;
    }
    
    private void SendMessageToPlayer(Platform platform, string playerName, string message)
    {
        Message msg = new Message(playerName, message, GetTimestamp(), platform);
        ChatAPI.SendMessageToUser(playerName, platform, msg);
    }
    

    private void SplitPlayerID(string playerId, out int platformCode, out string playerName)
    {
        string[] userInfo = playerId.Split(' ');

        platformCode = Int32.Parse(userInfo[0]);
        playerName = userInfo[1];
    }
    /*
    private void SendMessageToPlayer(Player player, string message)
    {

        int platformCode;
        string playerName;
        
        SplitPlayerID(player.Name, out platformCode, playerName);


        Message msg = new Message(playerName, message, GetTimestamp(), platform);
        ChatAPI.SendMessageToUser(playerName, platform, msg);
    }
*/
    #endregion
}

