using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject m_glassesPrefab;
    public GameObject m_levelUpParticlePrefab;
    public GameObject m_holeInTheGround;
    public int m_goldPerCoinChest = 50;
    private bool m_hasJustLostBattle;
    public TerritoryManager m_territoryManager;
    public int[] m_levelPrices = new int[] {125,275,450,650,900,1200,1550,1950,2400,3000,3750};
    #region properties
    
    #endregion

    #region system
    private void Awake()
    {
        ItemEvent.AddPickupListener(PlayerPickUp);
    }
    private void PlayerPickUp(Item item,Player player)
    {
        
        if (item.EffectType==Item.e_effectType.INVENTORY)
        {
            int numberItem = player.NumberOfItem(item);
            if (numberItem > 0)
            {
                player.Inventory[item] = numberItem;
            }
            else
            {
                player.Inventory.Add(item, numberItem);
            }

            //Debug.Log(player.Inventory.Count);
        }
        m_territoryManager.eligibleTerritoryItem.Add(player.CurrentTerritory);
    }
    private void Start()
    {
        
    }
    #endregion

    #region  class methods
    

    

    /*
    public void DoBattle(Player player,Player enemy)
    {
       // GetComponent<AudioSource>().PlayOneShot(brawlSound);
        int temp= player.Level;
        int x;
        int y;
        player.Level -= enemy.Level;
        enemy.Level -= temp;
        Debug.Log(player.Level);
        if(player.Level<1)
        {
            player.Level = 1;
            player.transform.position = player.Faction.RespawnPosition.transform.position;
            y = (int)player.Faction.RespawnPosition.transform.position.y;
            x = (int)player.Faction.RespawnPosition.transform.position.x;
            player.CurrentTerritory = m_territoryManager.m_battleField[x, y];
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
            enemy.transform.position = enemy.Faction.RespawnPosition.transform.position;
            y = (int)enemy.Faction.RespawnPosition.transform.position.y;
            x = (int)enemy.Faction.RespawnPosition.transform.position.x;
            enemy.CurrentTerritory = m_territoryManager.m_battleField[x, y];
            if (enemy.HasGlasses)
            {
                player.HasGlasses = true;
                enemy.HasGlasses = false;
            }
        }
    }
    */
    /*
    public void CheckIfEnnemy(Territory TerritoryToTest, Player player)
    {
        if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
        {
            foreach (Player otherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
            {
                if (!m_hasJustLostBattle)
                {
                    if (player.Faction.NumFaction != otherMole.Faction.NumFaction)
                    {
                        DoBattle(player, otherMole);
                    }
                }
            }
        }
    }*/
    /*
    public void TestForNearbyEnnemies(Player player)
    {
        float y;
        float x;
        Territory TerritoryToTest;
        y = player.CurrentTerritory.transform.position.y + 1;
        if (!(y > m_territoryManager.m_nbrYTerritories - 1))
        {
            int tempx = (int)player.CurrentTerritory.transform.position.x;
            int tempy = (int)player.CurrentTerritory.transform.position.y + 1;
            TerritoryToTest = m_territoryManager.m_battleField[tempx, tempy];
            CheckIfEnnemy(TerritoryToTest, player);
        }
        y = player.CurrentTerritory.transform.position.y - 1;
        if (!(y < 0))
        {
            int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x;
            int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y - 1;
            TerritoryToTest = m_territoryManager.m_battleField[tempx, tempy];
            CheckIfEnnemy(TerritoryToTest, player);
        }
        x = player.CurrentTerritory.transform.position.x - 1;
        if (!(x < 0))
        {
            int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x - 1;
            int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y;
            TerritoryToTest = m_territoryManager.m_battleField[tempx, tempy];
            CheckIfEnnemy(TerritoryToTest, player);
        }
        x = player.CurrentTerritory.transform.position.x + 1;
        if (!(x > m_territoryManager.m_nbrXTerritories - 1))
        {
            int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x + 1;
            int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y;
            TerritoryToTest = m_territoryManager.m_battleField[tempx, tempy];
            CheckIfEnnemy(TerritoryToTest, player);
        }
        m_hasJustLostBattle = false;
    }
    */

    public void DoAction(string TypeOfMove,Player player)
    {
        if(!player.CurrentTerritory.Locked)//if in territorry locked means if in battle, so not accepting commands
        {
            float y;
            float x;

            switch (TypeOfMove)
            {
                case "UP":
                    //player.gameObject.transform.rotation.SetLookRotation(new Vector3(0,0,0));
                    y = player.CurrentTerritory.transform.position.y + 1;
                    if (!(y > m_territoryManager.m_nbrYTerritories - 1))
                    {
                        int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x;
                        int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y + 1;
                        if (!m_territoryManager.m_battleField[tempx, tempy].Locked)
                        {
                            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
                            player.transform.Translate(0f, 1f, 0f);
                            player.CurrentTerritory = m_territoryManager.m_battleField[tempx, tempy];
                            //TestForNearbyEnnemies(player);
                        }
                    }
                    break;
                case "DOWN":
                    //player.gameObject.transform.rotation.SetLookRotation(new Vector3(0, 180, 0));
                    y = player.CurrentTerritory.transform.position.y - 1;
                    if (!(y < 0))
                    {
                        int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x;
                        int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y - 1;
                        if (!m_territoryManager.m_battleField[tempx, tempy].Locked)
                        {
                            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
                            player.transform.Translate(0f, -1f, 0f);
                            player.CurrentTerritory = m_territoryManager.m_battleField[tempx, tempy];
                            //TestForNearbyEnnemies(player);
                        }
                    }
                    break;
                case "LEFT":
                    //player.gameObject.transform.rotation.SetLookRotation(new Vector3(0, -90, 0));
                    x = player.CurrentTerritory.transform.position.x - 1;
                    if (!(x < 0))
                    {
                        int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x - 1;
                        int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y;
                        if (!m_territoryManager.m_battleField[tempx, tempy].Locked)
                        {
                            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
                            player.transform.Translate(-1f, 0f, 0f);
                            player.CurrentTerritory = m_territoryManager.m_battleField[tempx, tempy];
                            //TestForNearbyEnnemies(player);
                        }
                    }
                    break;
                case "RIGHT":
                    //player.gameObject.transform.rotation.SetLookRotation(new Vector3(0, 90, 0));
                    x = player.CurrentTerritory.transform.position.x + 1;
                    if (!(x > m_territoryManager.m_nbrXTerritories - 1))
                    {
                        int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x + 1;
                        int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y;
                        if (!m_territoryManager.m_battleField[tempx, tempy].Locked)
                        {
                            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
                            player.transform.Translate(1f, 0f, 0f);
                            player.CurrentTerritory = m_territoryManager.m_battleField[tempx, tempy];
                            //TestForNearbyEnnemies(player);
                        }

                    }
                    break;
                case "DIG":
                    Instantiate(m_holeInTheGround, player.CurrentTerritory.transform);
                    if (player.CurrentTerritory.HasItem)
                    {
                        Item item = player.CurrentTerritory.TerritoryItem;
                        //item.m_PlayerAction = this;

                        if (item.ItemType == Item.e_itemType.COINCHEST)
                        {
                            player.Gold += m_goldPerCoinChest;

                            //lance animation cedric
                        }
                        if (item.ItemType == Item.e_itemType.GLASSES)
                        {
                            player.HasGlasses = true;
                            GameObject glasses = Instantiate(m_glassesPrefab, player.transform);
                            player.Glasses = glasses;
                            ObjectsFollow.FollowCharacter(glasses.transform, player.transform.position);

                            //active objet glasses cedric
                        }

                        if (item.ItemType == Item.e_itemType.PARCHEMENT)
                            player.PlayPaper();
                        else
                            player.PlayDig();
                        player.CurrentTerritory.HasItem = false;
                        Destroy(player.CurrentTerritory.gameObject.GetComponent("Item"));
                        ItemEvent.NotifyNewItem(item, player);
                        m_territoryManager.RePopSpecial();
                    }
                    else
                    {
                        //message nothing to dig?
                    }
                    break;
                case "LEVELUP":
                    if (player.Gold > m_levelPrices[player.Level])
                    {
                        player.Gold -= m_levelPrices[player.Level];
                        player.Level++;
                        Instantiate(m_levelUpParticlePrefab, player.transform);
                    }
                    break;
                case "GRENADE":

                    break;
                case "SHOVEL":

                    break;
            }
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
}
