﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int numPlayer=0;
    public Dictionary<string, Player> players = new Dictionary<string, Player>();
    public GameObject m_playerCharPrefab;
    public int m_nbrFactions;
    private bool m_gameHasStarted;
    private bool m_isInitialized;
    private PlayerAction playerAction = new PlayerAction();



  

    public void DispatchTeam(Faction faction1, Faction faction2, Faction faction3, Faction faction4, int playerLimit)
    {

    }
    public void AssignFactionToPlayers(Player player)         
    {

        int countRed;
        int countBlue;
        int countYellow;
        int countGreen;
        
        
        countBlue = Faction.BLUE.ListPlayer.Count;
        countRed = Faction.GREEN.ListPlayer.Count;
        countYellow = Faction.YELLOW.ListPlayer.Count;
        countGreen = Faction.RED.ListPlayer.Count;
        int playerLimit = (countBlue + countGreen + countRed + countYellow) / 4;
        Debug.Log(playerLimit);
        if (countBlue >= playerLimit && playerLimit != 0)
        {
            Faction.BLUE.AddPlayer(player);
            player.Faction = Faction.BLUE;
            player.CurrentTerritory = Faction.BLUE.RespawnPosition;
            player.CurrentTerritory.TerritoryTransform.position = Faction.BLUE.RespawnPosition.TerritoryTransform.position;
            player.gameObject.transform.position = Faction.BLUE.RespawnPosition.TerritoryTransform.position;

        }
        else if (countRed >= playerLimit)
        {
            Faction.RED.AddPlayer(player);
            player.Faction = Faction.RED;
            player.CurrentTerritory = Faction.RED.RespawnPosition;
            player.CurrentTerritory.TerritoryTransform.position = Faction.RED.RespawnPosition.TerritoryTransform.position;
            player.gameObject.transform.position = Faction.RED.RespawnPosition.TerritoryTransform.position;
        }
        else if (countYellow >= playerLimit)
        {
            Faction.YELLOW.AddPlayer(player);
            player.Faction = Faction.YELLOW;
            player.CurrentTerritory = Faction.YELLOW.RespawnPosition;
            player.CurrentTerritory.TerritoryTransform.position = Faction.YELLOW.RespawnPosition.TerritoryTransform.position;
            player.gameObject.transform.position = Faction.YELLOW.RespawnPosition.TerritoryTransform.position;
        }
        else if (countGreen >= playerLimit)
        {
            Faction.GREEN.AddPlayer(player);
            player.Faction = Faction.GREEN;
            player.CurrentTerritory = Faction.GREEN.RespawnPosition;
            player.CurrentTerritory.TerritoryTransform.position = Faction.GREEN.RespawnPosition.TerritoryTransform.position;
            player.gameObject.transform.position = Faction.GREEN.RespawnPosition.TerritoryTransform.position;
        }

        /*
        int PlayerNum = 0;
        for(int faction = 0; PlayerNum< ListOfPlayerNames.Count; PlayerNum++)
        {
            GameObject NewPlayerGameObject = Instantiate(m_playerCharPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
            NewPlayerGameObject.name = ListOfPlayerNames[PlayerNum];
            NewPlayerGameObject.GetComponentInChildren<TextMesh>().text = "" + PlayerNum;
            Player NewPlayerScript = NewPlayerGameObject.GetComponent<Player>();
           // NewPlayerScript.MyManager = this;
            NewPlayerScript.NumPlayer = PlayerNum;
            NewPlayerScript.name = ListOfPlayerNames[PlayerNum];
            if (faction == 0)

            {
                NewPlayerScript.PlayerFaction = FactionRED;
                FactionRED.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionRED.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[0][0].gameObject;
            }
            else if (faction == 1)
            {
                NewPlayerScript.PlayerFaction = FactionBLUE;
                FactionBLUE.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionBLUE.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[m_nbrXTerritories - 1][m_nbrYTerritories - 1].gameObject;
            }
            else if (faction == 2)
            {
                NewPlayerScript.PlayerFaction = FactionGREEN;
                FactionGREEN.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionGREEN.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[m_nbrXTerritories - 1][0].gameObject;
            }
            else if (faction == 3)
            {
                NewPlayerScript.PlayerFaction = FactionYELLOW;
                FactionYELLOW.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionYELLOW.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[0][m_nbrYTerritories - 1].gameObject;
            }
            faction++;
            if (ListOfPlayerNames.Count > 8 )
            {
                if(faction > 3)
                {
                    faction = 0;
                }
            }
            else if(faction > 1)
            {
                faction = 0;
            }
           // NewPlayerScript.MyManager = this;
            m_listPlayer.Add(NewPlayerScript);
        }*/

    }

    public void CreatePlayer(string name)             
    {
        GameObject NewPlayerGameObject = Instantiate(m_playerCharPrefab, new Vector3(-5f, -5f, 0f), Quaternion.identity, transform);
        Player newPlayer = NewPlayerGameObject.GetComponent<Player>();
        newPlayer.Name = name;
        newPlayer.NumPlayer = ++numPlayer;
        NewPlayerGameObject.name = newPlayer.name;
        NewPlayerGameObject.GetComponentInChildren<TextMesh>().text = "" + numPlayer;
        AssignFactionToPlayers(newPlayer);
    }
    public Player GetPlayer(string name)
    {
        Player player;
        players.TryGetValue(name, out player);
        return player;
    }
    public void GetCommandFromPlayer(string name, string command)              //JEROME HERE ! Give me a player names and the command he sends ;-)       
    {
        var p = GetPlayer(name);
        if (p==null) {
            CreatePlayer(name);
        }
        else
        {

            playerAction.DoAction(command, p);
        }
    }
    // Use this for initialization
    void Start()
    {
        GetCommandFromPlayer("Oho", "");
        GetCommandFromPlayer("Oho2", "");
        GetCommandFromPlayer("Oho3", "");
        GetCommandFromPlayer("Oho4", "");
        GetCommandFromPlayer("Oho5", "");
        GetCommandFromPlayer("Oho6", "");
        GetCommandFromPlayer("Oho7", "");/*
        GetCommandFromPlayer("Oho", "UP");
        GetCommandFromPlayer("Oho", "UP");
        GetCommandFromPlayer("Oho", "LEFT");
        GetCommandFromPlayer("Oho", "LEFT");*/

    }

    // Update is called once per frame
    void Update()
    {

    }


}