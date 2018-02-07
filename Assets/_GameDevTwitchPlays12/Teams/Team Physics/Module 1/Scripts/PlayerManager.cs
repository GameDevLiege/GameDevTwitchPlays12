using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public AudioClip brawlSound;
    public AudioClip popSound;
    public AudioClip diggingSound;
    public AudioClip paperSound;
    public TerritoryManager m_territoryManager;
    public GameObject m_glassesPrefab;
    public GameObject m_levelUpParticlePrefab;
    public GameObject m_holeInTheGround;
    public GameObject m_playerPrefab;
    public int m_goldPerCoinChest = 50;
    public float peebleImpactTime;
    public Dictionary<string, Player> listPlayerByName;
    public Dictionary<int, Player> listPlayerById;
    public int[] m_levelPrices = new int[] { 125, 275, 450, 650, 900, 1200, 1550, 1950, 2400, 3000, 3750 };
    private int numPlayer=0;
    private int lastFaction = 1;
    private bool m_hasJustLostBattle;

    //public int m_nbrFactions;
    //private bool m_gameHasStarted;
    //private bool m_isInitialized;
    #region properties

    #endregion
    #region system
    private void Awake()
    {
        ItemEvent.AddPickupListener(PlayerPickUp);
        listPlayerByName = new Dictionary<string, Player>();
    }

    private void Start()
    {

    }
    #endregion
    #region  class methods
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
        listPlayerByName.Add(name, newPlayer);
        listPlayerById.Add(numPlayer, newPlayer);
        AssignFactionToPlayers(newPlayer);
        return newPlayer;
    }
    public Player GetPlayer(string name)
    {
        Player player;
        listPlayerByName.TryGetValue(name, out player);
        return player;
    }
    private void PlayerPickUp(Item item, Player player)
    {

        if (item.EffectType == Item.e_effectType.INVENTORY)
        {
            int numberItem = player.NumberOfItem(item.ItemType);
            if (numberItem > 0)
            {
                player.Inventory[item.ItemType] = numberItem;
            }
            else
            {
                player.Inventory.Add(item.ItemType, numberItem);
            }

            //Debug.Log(player.Inventory.Count);
        }
        m_territoryManager.eligibleTerritoryItem.Add(player.CurrentTerritory);
    }
    public void DoAction(string TypeOfMove, Player player,int idEnnemy=0)
    {
        if (!player.CurrentTerritory.Locked)//if in territorry locked means if in battle, so not accepting commands
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
                    //some condition based on inventory
                    if (player.NumberOfItem(Item.e_itemType.GRENADES) > 0)
                    {
                        player.Inventory[Item.e_itemType.GRENADES] -= 1;
                        LaunchGrenade(player);
                    }

                    break;
                case "SHOVEL":
                    
                    break;
                case "PEBBLE":
                    if (player.NumberOfItem(Item.e_itemType.PEBBLE) > 0)
                    {
                        player.Inventory[Item.e_itemType.PEBBLE] -= 1;
                        Player ennemy;
                        if (listPlayerById.TryGetValue(idEnnemy, out ennemy)) {
                            Timer timerPeeble = gameObject.AddComponent<Timer>();
                            StartCoroutine(LaunchPebble(ennemy.CurrentTerritory.transform.position,player,ennemy));
                            //LaunchPebble(player,ennemy);
                        };
                        
                    }
                    break;
            }
        }
    }
    public void LaunchGrenade(Player player)
    {
        Territory center = player.CurrentTerritory;
        int centerX = (int)center.transform.position.x;
        int centerY = (int)center.transform.position.y;
        TryPaintTarget(centerX + 1, centerY + 1, player);
        TryPaintTarget(centerX + 1, centerY, player);
        TryPaintTarget(centerX + 1, centerY - 1, player);
        TryPaintTarget(centerX, centerY + 1, player);
        TryPaintTarget(centerX, centerY - 1, player);
        TryPaintTarget(centerX - 1, centerY + 1, player);
        TryPaintTarget(centerX - 1, centerY, player);
        TryPaintTarget(centerX - 1, centerY - 1, player);
    }
    IEnumerator LaunchPebble(Vector3 ennemyPosition,Player player,Player ennemy)
    {
        yield return new WaitForSeconds(peebleImpactTime);
        if (ennemy.CurrentTerritory.transform.position==ennemyPosition) {
            player.Gold += ennemy.Gold / 2;
            ennemy.transform.position = ennemy.Faction.RespawnPosition.transform.position;
        }
    }
    public void TryPaintTarget(int x, int y, Player player)
    {
        if ((x > 0 && x < m_territoryManager.m_nbrXTerritories - 1) && (y > 0 && y < m_territoryManager.m_nbrYTerritories - 1))
        {
            Territory targetT = m_territoryManager.m_battleField[x, y];
            targetT.FactionChange(player);
        }
    }
    public void GetCommandFromPlayer(string PName, string Command)
    {
        throw new System.NotImplementedException();
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
    #endregion


}