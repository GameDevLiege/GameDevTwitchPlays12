using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Territory : MonoBehaviour
{

    #region Public Members
    public GameObject m_particleFightPrefab;
    #endregion
    public bool Locked { get; set; }
    private bool playerDied = false;
    #region Public Void
    #endregion
    #region Propertie (GET,SET)
    [SerializeField]
    private string m_territoryID;
    public string TerritoryID
    {
        get { return m_territoryID; }
        set { m_territoryID = value; }
    }
    [Header("Used GameObject")]
    [SerializeField]
    private MeshRenderer m_territoryMeshRenderer;

    public MeshRenderer TerritoryMeshRenderer
    {
        get { return m_territoryMeshRenderer; }
        set { m_territoryMeshRenderer = value; }
    }
    [Header("Item territory properties")]
    [SerializeField]
    private bool m_hasItem;
    public bool HasItem
    {
        get { return m_hasItem; }
        set { m_hasItem = value; }
    }

    private Item m_territoryItem;
    public Item TerritoryItem
    {
        get { return m_territoryItem; }
        set { m_territoryItem = value; }
    }
    [Header("Headquarter")]
    [SerializeField]
    private bool m_isHQ;
    public bool IsHQ
    {
        get { return m_isHQ; }
        set { m_isHQ = value; }
    }
    [Header("Central Zone")]
    [SerializeField]
    private bool m_isCenter;
    public bool IsCenter
    {
        get { return m_isCenter; }
        set { m_isCenter = value; }
    }
    [Header("Territory Color")]
    [SerializeField]
    private Color m_currentColor = Color.white;
    public Color CurrentColor
    {
        get { return m_currentColor; }
        set { m_currentColor = value; }
    }
    public int FactionNum {get;set;}

    private List<Player> m_listPlayerCharOnTerritory = new List<Player>();
    public List<Player> GetListOfPlayerOnThisTerritory()
    {
        return m_listPlayerCharOnTerritory;
    }

    public int GetPlayerNumOnTerritory()
    {
    return m_listPlayerCharOnTerritory.Count;
    }
    private TerritoryManager m_territoryManager; 

    #endregion


    #region System

    void Awake () 
    {
        m_territoryManager = FindObjectOfType<TerritoryManager>();
        m_territoryMeshRenderer = GetComponentInChildren<MeshRenderer>();
        FactionNum = 0;
        Locked = false;
        m_particleFightPrefab = m_territoryManager.m_particleFightPrefab;
    }

    #endregion

    #region Private Void
    IEnumerator UnlockAfterFight(Player player, Player potentialEnnemy)
    {
        player.PlayBrawl();
        Instantiate(m_particleFightPrefab, player.transform);
        yield return new WaitForSeconds(2*m_territoryManager.m_durationOfTick);
        EnterBattle(player, potentialEnnemy);
        Locked = false;
    }

    private void CheckForEnnemies(Player player)
    {
        if (player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Count > 1)
        {
            for (int i = 0; i < player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Count; i++)
            {
                Player potentialEnnemy = player.CurrentTerritory.GetListOfPlayerOnThisTerritory()[i];
                if (player.Faction.NumFaction != potentialEnnemy.Faction.NumFaction)
                {
                    Debug.Log("test 000");
                    Locked = true;
                    StartCoroutine(UnlockAfterFight(player, potentialEnnemy));
                }
            }
        }
    }
    private void EnterBattle(Player player, Player enemy)
    {
        int temp = player.Level;
        int x;
        int y;
        player.Level -= enemy.Level;
        enemy.Level -= temp;
        if (player.Level < 1)
        {
            player.Level = 1;
            player.transform.position = player.Faction.RespawnPosition.transform.position;
            y = (int)player.Faction.RespawnPosition.transform.position.y;
            x = (int)player.Faction.RespawnPosition.transform.position.x;
            playerDied = true;
            player.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(player);
            player.CurrentTerritory = m_territoryManager.m_battleField[x, y];
            if (player.HasGlasses)
            {
                player.HasGlasses = false;
                enemy.HasGlasses = true;
                enemy.Glasses = player.Glasses;
                ObjectsFollow.FollowCharacter(enemy.Glasses.transform, enemy.transform.position);
            }
        }
        if (enemy.Level < 1)
        {
            enemy.Level = 1;
            enemy.transform.position = enemy.Faction.RespawnPosition.transform.position;
            y = (int)enemy.Faction.RespawnPosition.transform.position.y;
            x = (int)enemy.Faction.RespawnPosition.transform.position.x;
            enemy.CurrentTerritory.GetListOfPlayerOnThisTerritory().Remove(enemy);
            enemy.CurrentTerritory = m_territoryManager.m_battleField[x, y];
            if (enemy.HasGlasses)
            {
                player.HasGlasses = true;
                enemy.HasGlasses = false;
                player.Glasses = enemy.Glasses;
                ObjectsFollow.FollowCharacter(player.Glasses.transform, player.transform.position);
            }
        }
        player.PlayPop();
    }

    private void OnTriggerEnter(Collider col)
    {
        playerDied = false;
        Player p = col.GetComponent<Player>();
        m_listPlayerCharOnTerritory.Add(p);
        if(m_listPlayerCharOnTerritory.Count>1)
        {
            CheckForEnnemies(p);
        }
        if (!playerDied && (p != null && p.Faction!=null & FactionNum != p.Faction.NumFaction)&&(!IsHQ) )
        {
            FactionChange(p);
        }
    }
    public void ColorChange(Color color) {
        
        m_territoryMeshRenderer.material.color = color;

    }
    public void FactionChange(Player p)
    {
        //previous territory owner looses Nbrterritory

        if (FactionNum != 0)
        {
            if (FactionNum == 1)
            {
                FactionManager.RED.NbrTerritories--;
                ColorChange(Color.red);
            }
            else if (FactionNum == 2)
            {
                FactionManager.BLUE.NbrTerritories--;
                ColorChange(Color.blue);
            }
            else if (FactionNum == 3)
            {
                FactionManager.GREEN.NbrTerritories--;
                ColorChange(Color.green);
            }
            else if (FactionNum == 4)
            {
                FactionManager.YELLOW.NbrTerritories--;
                ColorChange(Color.yellow);
            }
        }
        m_currentColor = p.Faction.FactionColor;
        FactionNum = p.Faction.NumFaction;
        Color col = TerritoryMeshRenderer.material.color;
        col = p.Faction.FactionColor;
        col.a = 100f;
        TerritoryMeshRenderer.material.color = col;
        //new territory owner gains Nbrterritory
        if (FactionNum == 1)
        {
            FactionManager.RED.NbrTerritories++;
        }
        else if (FactionNum == 2)
        {
            FactionManager.BLUE.NbrTerritories++;
        }
        else if (FactionNum == 3)
        {
            FactionManager.GREEN.NbrTerritories++;
        }
        else if (FactionNum == 4)
        {
            FactionManager.YELLOW.NbrTerritories++;
        }
    }

    private void Battle()
    {

    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    #endregion

}
