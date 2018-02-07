using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int numPlayer=0;
    public Dictionary<string, Player> players;
    public GameObject m_playerPrefab;
    //public int m_nbrFactions;
    //private bool m_gameHasStarted;
    //private bool m_isInitialized;
    private int lastFaction=1;


    private void Awake()
    {
        players = new Dictionary<string, Player>();
    }
    public void AssignFactionToPlayers(Player player)         
    {

        switch (lastFaction)
        {
            case 1:
                FactionManager.BLUE.AddPlayer(player);
                player.Faction = FactionManager.BLUE;
                player.CurrentTerritory = player.Faction.RespawnPosition;
                player.transform.position = player.Faction.RespawnPosition.transform.position;
                lastFaction++;
                break;
            case 2:
                FactionManager.RED.AddPlayer(player);
                player.Faction = FactionManager.RED;
                player.CurrentTerritory = player.Faction.RespawnPosition;
                player.transform.position = player.Faction.RespawnPosition.transform.position;
                lastFaction++;
                break;
            case 3:
                FactionManager.YELLOW.AddPlayer(player);
                player.Faction = FactionManager.YELLOW;
                player.CurrentTerritory = player.Faction.RespawnPosition;
                player.transform.position = player.Faction.RespawnPosition.transform.position;
                lastFaction++;
                break;
            case 4:
                FactionManager.GREEN.AddPlayer(player);
                player.Faction = FactionManager.GREEN;
                player.CurrentTerritory = player.Faction.RespawnPosition;
                player.transform.position = player.Faction.RespawnPosition.transform.position;
                lastFaction = 1;
                break;

            
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

    public Player CreatePlayer(string name)             
    {
        numPlayer++;
        Player newPlayer;
        GameObject NewPlayerGameObject = Instantiate(m_playerPrefab.gameObject, new Vector3(-5f, -5f, 0f), Quaternion.identity, transform);
        //newPlayer.playerTransform = NewPlayerGameObject.transform;
        NewPlayerGameObject.name = name;
        NewPlayerGameObject.GetComponentInChildren<TextMesh>().text = "" + numPlayer;
        newPlayer = NewPlayerGameObject.GetComponent<Player>();
        //newPlayer.playerTransform = NewPlayerGameObject.transform;
        newPlayer.Name = name;
        newPlayer.NumPlayer = numPlayer;
        players.Add(name,newPlayer);
        AssignFactionToPlayers(newPlayer);
        return newPlayer;
    }
    public Player GetPlayer(string name)
    {
        Player player;
        players.TryGetValue(name, out player);
        return player;
    }
    
    // Use this for initialization
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }


}