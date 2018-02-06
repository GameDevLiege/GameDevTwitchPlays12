using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public AudioClip brawlSound;
    public AudioClip popSound;
    public AudioClip diggingSound;
    public AudioClip paperSound;
    public int m_goldPerCoinChest = 50;
    public int m_priceOfLevel = 50;
    private bool m_hasJustLostBattle;
    public TerritoryManager m_territoryManager;
    
    #region properties
    
    #endregion

    #region system
    private void Awake()
    {
    }
    private void Start()
    {
        
    }
    #endregion

    #region  class methods
    public void DoBattle(Player player,Player enemy)
    {
        GetComponent<AudioSource>().PlayOneShot(brawlSound);
        int temp= player.Level;
        player.Level -= enemy.Level;
        enemy.Level -= temp;
        if(player.Level<1)
        {
            player.Level = 1;
            player.CurrentTerritory.TerritoryTransform.position = player.Faction.RespawnPosition.TerritoryTransform.position;
            player.CurrentTerritory.TerritoryGameObject = GameObject.Find("y=" + player.Faction.RespawnPosition.TerritoryTransform.position.y + "x=" + player.Faction.RespawnPosition.TerritoryTransform.position.x);
            m_hasJustLostBattle = true;
            if(player.HasGlasses)
            {
                player.HasGlasses = false;
                enemy.HasGlasses = true;
            }
        }
        if (enemy.Level < 1)
        {
            enemy.Level = 1;
            enemy.CurrentTerritory.TerritoryTransform.position = enemy.Faction.RespawnPosition.TerritoryTransform.position;
            enemy.CurrentTerritory.TerritoryGameObject = GameObject.Find("y=" + enemy.Faction.RespawnPosition.TerritoryTransform.position.y + "x=" + enemy.Faction.RespawnPosition.TerritoryTransform.position.x);
            if (enemy.HasGlasses)
            {
                player.HasGlasses = true;
                enemy.HasGlasses = false;
            }
        }
    }

    public void TestForNearbyEnnemies(Player player)
    {
        
        float y;
        float x;
        Territory TerritoryToTest;
        y = player.CurrentTerritory.transform.position.y + 1;
        if (!(y > m_territoryManager.m_nbrYTerritories - 1))
        {
            float tempx = player.CurrentTerritory.gameObject.transform.position.x;
            float tempy = player.CurrentTerritory.gameObject.transform.position.y + 1;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if(TerritoryToTest.GetPlayerNumOnTerritory()>0)
            {
                foreach(Player OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {

                    if(!m_hasJustLostBattle)
                    {
                        if(player.Faction.FactionColor!=OtherMole.Faction.FactionColor)
                        {
                            DoBattle(player,OtherMole);
                        }
                    }
                }
            }
        }
        y = player.CurrentTerritory.transform.position.y - 1;
        if (!(y < 0))
        {
            float tempx = player.CurrentTerritory.gameObject.transform.position.x;
            float tempy = player.CurrentTerritory.gameObject.transform.position.y - 1;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
            {
                foreach (Player OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {
                    if (!m_hasJustLostBattle)
                    {
                        if (player.Faction.FactionColor != OtherMole.Faction.FactionColor)
                        {
                            DoBattle(player, OtherMole);
                        }
                    }
                        
                }
            }
        }
        x = player.CurrentTerritory.transform.position.x - 1;
        if (!(x < 0))
        {
            float tempx = player.CurrentTerritory.gameObject.transform.position.x - 1;
            float tempy = player.CurrentTerritory.gameObject.transform.position.y;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
            {
                foreach (Player OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {
                    if (!m_hasJustLostBattle)
                    {
                        if (player.Faction.FactionColor != OtherMole.Faction.FactionColor)
                        {
                            DoBattle(player,OtherMole);
                        }
                    }
                        
                }
            }
        }
        x = player.CurrentTerritory.transform.position.x + 1;
        if (!(x > m_territoryManager.m_nbrXTerritories - 1))
        {
            float tempx = player.CurrentTerritory.gameObject.transform.position.x + 1;
            float tempy = player.CurrentTerritory.gameObject.transform.position.y;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
            {
                foreach (Player OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {
                    if (!m_hasJustLostBattle)
                    {
                        if (player.Faction.FactionColor != OtherMole.Faction.FactionColor)
                        {
                            DoBattle(player,OtherMole);
                        }
                    }
                        
                }
            }
        }
        m_hasJustLostBattle = false;
    }

    public void DoAction(string TypeOfMove,Player player)
    {

        float y;
        float x;
        
        switch (TypeOfMove)
        {
            case "UP":
                y = player.CurrentTerritory.transform.position.y + 1;
                if (!(y > m_territoryManager.m_nbrYTerritories - 1))
                {
                    player.playerTransform.Translate(0f, 1f, 0f);
                    float tempx = player.CurrentTerritory.gameObject.transform.position.x;
                    float tempy = player.CurrentTerritory.gameObject.transform.position.y + 1;
                    player.CurrentTerritory.TerritoryGameObject = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies(player);
                }
                break;
            case "DOWN":
                y = player.CurrentTerritory.transform.position.y - 1;
                if (!(y < 0))
                {
                    player.playerTransform.Translate(0f, -1f, 0f);
                    float tempx = player.CurrentTerritory.gameObject.transform.position.x;
                    float tempy = player.CurrentTerritory.gameObject.transform.position.y - 1;
                    player.CurrentTerritory.TerritoryGameObject = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies(player);
                }
                break;
            case "LEFT":
                x = player.CurrentTerritory.transform.position.x - 1;
                if (!(x < 0))
                {
                    player.playerTransform.Translate(-1f, 0f, 0f);
                    float tempx = player.CurrentTerritory.gameObject.transform.position.x - 1;
                    float tempy = player.CurrentTerritory.gameObject.transform.position.y;
                    player.CurrentTerritory.TerritoryGameObject = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies(player);
                }
                break;
            case "RIGHT":
                x = player.CurrentTerritory.transform.position.x + 1;
                if (!(x > m_territoryManager.m_nbrXTerritories - 1))
                {
                    player.playerTransform.Translate(1f, 0f, 0f);
                    float tempx = player.CurrentTerritory.gameObject.transform.position.x + 1;
                    float tempy = player.CurrentTerritory.gameObject.transform.position.y;
                    player.CurrentTerritory.TerritoryGameObject = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies(player);
                }
                break;
            case "DIG":
                

                if (player.CurrentTerritory.HasItem)
                {
                    Item item = player.CurrentTerritory.TerritoryItem;
                    //item.m_PlayerAction = this;
                   // SpecialAPI.NotifyNewSpecial(special);
                    if(item.ItemType==Item.e_itemType.COINCHEST)
                    { player.Gold += m_goldPerCoinChest; }
                    if (item.ItemType == Item.e_itemType.GLASSES)
                    { player.HasGlasses=true; }

                    if (item.ItemType == Item.e_itemType.PARCHEMENT)
                        GetComponent<AudioSource>().PlayOneShot(paperSound);
                    else
                        GetComponent<AudioSource>().PlayOneShot(diggingSound);


                    player.CurrentTerritory.HasItem = false;
                    Destroy(player.CurrentTerritory.TerritoryGameObject.GetComponent("Item"));
                    m_territoryManager.RePopSpecial();
                }
                else
                {
                    //message nothing to dig?
                }
                break;
            case "!Level":
                if(player.Gold>m_priceOfLevel)
                {
                    player.Gold -= m_priceOfLevel;
                    player.Gold++;
                }
                break;
           
        }

    }

  
    public void GetCommandFromPlayer(string PName, string Command)
    {
        throw new System.NotImplementedException();
    }

    public void AssignFactionToPlayers(List<Player> ListOfPlayerNames)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Didi



    #endregion
}
