using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public bool m_debug = true;
    [Header ("materials mole helmet")]
    public Material m_helmetMoleBlue;
    public Material m_helmetMoleRed;
    public Material m_helmetMoleGreen;
    public Material m_helmetMoleYellow;
    [Header("audio")]
    public AudioClip brawlSound;
    public AudioClip popSound;
    public AudioClip diggingSound;
    public AudioClip paperSound;
    public AudioClip hurtSound;
    public AudioClip coinSound;
    public AudioClip grenadeSound;
    [Header("prefabsAnim")]
    public GameObject m_glassesPrefab;
    public GameObject m_levelUpParticlePrefab;
    public GameObject m_holeInTheGround;
    public GameObject m_playerPrefab;
    public GameObject m_starStun;
    public GameObject m_stoneThrow;
    [Header("other")]
    public TerritoryManager m_territoryManager;
    public int m_goldPerCoinChest = 50;
    public int m_costOfGrenade = 50;
    public int m_costOfShovel = 50;
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
        listPlayerById= new Dictionary<int, Player>();
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
        GameObject NewPlayerGameObject = Instantiate(m_playerPrefab.gameObject, new Vector3(-5f, -5f, 0f), Quaternion.Euler(-90,0,0), transform);
        //newPlayer.playerTransform = NewPlayerGameObject.transform;
        NewPlayerGameObject.name = name;
        NewPlayerGameObject.GetComponentInChildren<TextMeshPro>().text = "" + numPlayer;
        newPlayer = NewPlayerGameObject.GetComponent<Player>();
        //newPlayer.playerTransform = NewPlayerGameObject.transform;
        newPlayer.Name = name;
        newPlayer.NumPlayer = numPlayer;
        listPlayerByName.Add(name, newPlayer);
        listPlayerById.Add(numPlayer, newPlayer);
        AssignFactionToPlayers(newPlayer);
        foreach (MeshFilter meshF in NewPlayerGameObject.GetComponentsInChildren<MeshFilter>())
        {
            if (meshF.gameObject.name == "Color")
            {
                switch (newPlayer.Faction.NumFaction)
                {
                    case 1:meshF.gameObject.GetComponent<MeshRenderer>().material = m_helmetMoleRed;break;
                    case 2:meshF.gameObject.GetComponent<MeshRenderer>().material = m_helmetMoleBlue;break;
                    case 3:meshF.gameObject.GetComponent<MeshRenderer>().material = m_helmetMoleGreen;break;
                    case 4:meshF.gameObject.GetComponent<MeshRenderer>().material = m_helmetMoleYellow;break;
                }
            }
        }
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
            int numberItem = player.NumberOfItem((int)item.ItemType);

            if (numberItem > 0)
            {
                player.Inventory[(int)item.ItemType] = numberItem;
            }
            else
            {
                player.Inventory.Add((int)item.ItemType, numberItem);
                player.Inventory[(int)item.ItemType] += 1;
            }

            //Debug.Log(player.Inventory.Count);
        }
        m_territoryManager.eligibleTerritoryItem.Add(player.CurrentTerritory);
    }
    public void RotateMole(GameObject Mole, float angleY)
    {
        foreach (Transform objTransform in Mole.GetComponentsInChildren<Transform>())
        {
            if ((objTransform.gameObject.name == "MontyMole_MontyMole") || (objTransform.gameObject.name == "Helmet"))
            {
                objTransform.localEulerAngles = new Vector3(objTransform.localEulerAngles.x, angleY, objTransform.localEulerAngles.z);
            }
        }
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
                    RotateMole(player.gameObject, 0);
                    y = player.CurrentTerritory.transform.position.y + 1;
                    if (!(y > m_territoryManager.m_nbrYTerritories - 1))
                    {
                        int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x;
                        int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y + 1;
                        if (!m_territoryManager.m_battleField[tempx, tempy].Locked)
                        {
                            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
                            player.transform.Translate(0f, 0f, 1f);
                            player.CurrentTerritory = m_territoryManager.m_battleField[tempx, tempy];
                        }
                    }
                    break;

                case "DOWN":
                    RotateMole(player.gameObject, 180);
                    y = player.CurrentTerritory.transform.position.y - 1;
                    if (!(y < 0))
                    {
                        int tempx = (int)player.CurrentTerritory.gameObject.transform.position.x;
                        int tempy = (int)player.CurrentTerritory.gameObject.transform.position.y - 1;
                        if (!m_territoryManager.m_battleField[tempx, tempy].Locked)
                        {
                            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
                            player.transform.Translate(0f, 0f, -1f);
                            player.CurrentTerritory = m_territoryManager.m_battleField[tempx, tempy];
                        }
                    }
                    break;

                case "LEFT":
                    RotateMole(player.gameObject, -90);
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
                        }
                    }
                    break;

                case "RIGHT":
                    RotateMole(player.gameObject, 90);
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
                        }

                    }
                    break;

                case "DIG":
                    Instantiate(m_holeInTheGround, player.CurrentTerritory.transform);
                    Collider collider = player.CurrentTerritory.GetComponent<Collider>();
                    collider.enabled = false;
                    collider.enabled = true;
                    player.PlayDig();
                    if (player.CurrentTerritory.HasItem)
                    {
                        if (m_debug)
                        {
                            Debug.Log("PlayerManager: " + "player " + player.NumPlayer + " digged an item out");
                        }
                        Item item = player.CurrentTerritory.TerritoryItem;
                        //item.m_PlayerAction = this;

                        if (item.ItemType == Item.e_itemType.COINCHEST)
                        {
                            player.Gold += m_goldPerCoinChest;
                            player.PlayCoin();
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

                        if (item.ItemType == Item.e_itemType.STRAIN)
                        {
                            player.PlayHurt();
                        }
                        else if (item.ItemType == Item.e_itemType.PARCHEMENT)
                        {
                            player.PlayPaper();
                            GameObject parchement = Instantiate(m_starStun);
                            ObjectsFollow.FollowCharacter(parchement.transform, player.transform.position);
                        }
                        else
                        {
                            player.PlayDig();
                        }
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
                        player.Level= player.Level+1;
                        
                        GameObject level = Instantiate(m_levelUpParticlePrefab);
                        ObjectsFollow.FollowCharacter(level.transform, player.transform.position);

                    }
                    break;

                case "GRENADE":
                    //some condition based on inventory

                    if (player.NumberOfItem((int)Item.e_itemType.GRENADES) > 0)
                    {
                        player.Inventory[(int)Item.e_itemType.GRENADES] -= 1;
                        LaunchGrenade(player);
                        player.PlayGrenade();
                        ItemEvent.NotifyItemUse(Item.e_itemType.GRENADES, player);
                    }

                    break;
                case "SHOVEL":
                    if (player.NumberOfItem((int)Item.e_itemType.SHOVEL) > 0)
                    {
                        player.Inventory[(int)Item.e_itemType.SHOVEL] -= 1; ;
                        ItemEvent.NotifyItemUse(Item.e_itemType.GRENADES, player);
                    }
                    else
                    {
                        //some message "no shovel"
                    }
                       
                    //handled by remi&françois
                    break;

                case "BUYGRENADE":

                    if (player.Gold > m_costOfGrenade)
                    {
                        player.Gold -= m_costOfGrenade;
                        if (player.NumberOfItem((int)Item.e_itemType.GRENADES) > 0)
                            player.Inventory.Add((int)Item.e_itemType.GRENADES, 1);
                        else
                            player.Inventory[(int)Item.e_itemType.GRENADES] += 1;
                    }
                    break;
                case "BUYSHOVEL":
                    if (player.Gold > m_costOfShovel)
                    {
                        player.Gold -= m_costOfShovel;
                        if (player.NumberOfItem((int)Item.e_itemType.SHOVEL) > 0)
                            player.Inventory.Add((int)Item.e_itemType.SHOVEL, 1);
                        else
                            player.Inventory[(int)Item.e_itemType.SHOVEL] += 1;
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
            ennemy.Gold -= ennemy.Gold / 2;
            ennemy.Level -= 1;
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
    }
    
    #endregion


}