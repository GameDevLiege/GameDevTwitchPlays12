using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public AudioClip brawlSound;
    public AudioClip popSound;
    public AudioClip diggingSound;
    public AudioClip paperSound;

    public int m_goldPerCoinChest = 50;
    public int m_priceOfLevel = 50;
    private bool m_hasJustLostBattle;

    private int m_playerCharLVL=1;
    public int PlayerCharLVL
    {
        get { return m_playerCharLVL; }
        set { m_playerCharLVL = value; }
    }

    public bool hasGlasses = false;
    
    #region properties
    public PhysicsManager m_manager;
    public PhysicsManager MyManager
    {
        get { return m_manager; }
        set { m_manager = value; }
    }

    private int m_numPlayer;
    public int NumPlayer
    {
        get { return m_numPlayer; }
        set { m_numPlayer = value; }
    }
    
    public Color FactionColor
    {
        get { return m_faction.FactionColor; }
    }

    private Faction m_faction;
    public Faction Faction
    {
        get { return m_faction; }
        set { m_faction = value; }
    }

    private GameObject m_currentTerritory;
    public GameObject CurrentTerritory
    {
        get { return m_currentTerritory; }
        set {
            m_currentTerritory = value;
        }
    }

    private string m_playerName;
    public string PlayerName
    {
        get { return m_playerName; }
        set { m_playerName = value; }
    }

    private int m_goldmoney;
    public int Gold
    {
        get { return m_goldmoney; }
        set { m_goldmoney = value; }
    }
    #endregion

    #region system
    private void Awake()
    {
        m_goldmoney = 0;
    }
    #endregion
    
    #region  class methods
    public void DoBattle(PlayerCharacter Enemy)
    {
        GetComponent<AudioSource>().PlayOneShot(brawlSound);


        Vector3 PlayerRespawnPosition = new Vector3();
        Vector3 EnemyRespawnPosition = new Vector3();
        int temp= this.PlayerCharLVL;
        this.PlayerCharLVL -= Enemy.PlayerCharLVL;
        Enemy.PlayerCharLVL -= temp;
        if(this.PlayerCharLVL<1)
        {
            this.PlayerCharLVL = 1;
            PlayerRespawnPosition = this.Faction.RespawnPosition;
            transform.position = PlayerRespawnPosition;
            m_currentTerritory = GameObject.Find("y=" + PlayerRespawnPosition.y + "x=" + PlayerRespawnPosition.x);
            m_hasJustLostBattle = true;
            if(hasGlasses)
            {
                Territory.NotifyIsNotCentral(this.Faction);
                hasGlasses = false;
                Enemy.hasGlasses = true;
            }
        }
        if (Enemy.PlayerCharLVL < 1)
        {
            Enemy.PlayerCharLVL = 1;
            EnemyRespawnPosition = Enemy.Faction.RespawnPosition;
            Enemy.transform.position = EnemyRespawnPosition;
            Enemy.m_currentTerritory = GameObject.Find("y=" + EnemyRespawnPosition.y + "x=" + EnemyRespawnPosition.x);
            if (Enemy.hasGlasses)
            {
                Territory.NotifyIsNotCentral(Enemy.Faction);
                hasGlasses = true;
                Enemy.hasGlasses = false;
            }
        }
    }

    public void TestForNearbyEnnemies()
    {
        float y;
        float x;
        Territory TerritoryToTest;
        y = m_currentTerritory.transform.position.y + 1;
        if (!(y > m_manager.m_nbrYTerritories - 1))
        {
            float tempx = m_currentTerritory.gameObject.transform.position.x;
            float tempy = m_currentTerritory.gameObject.transform.position.y + 1;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if(TerritoryToTest.GetPlayerNumOnTerritory()>0)
            {
                foreach(PlayerCharacter OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {

                    if(!m_hasJustLostBattle)
                    {
                        if(this.Faction!=OtherMole.Faction)
                        {
                            DoBattle(OtherMole);
                        }
                    }
                }
            }
        }
        y = m_currentTerritory.transform.position.y - 1;
        if (!(y < 0))
        {
            float tempx = m_currentTerritory.gameObject.transform.position.x;
            float tempy = m_currentTerritory.gameObject.transform.position.y - 1;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
            {
                foreach (PlayerCharacter OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {
                    if (!m_hasJustLostBattle)
                    {
                        if (this.Faction != OtherMole.Faction)
                        {
                            DoBattle(OtherMole);
                        }
                    }
                        
                }
            }
        }
        x = m_currentTerritory.transform.position.x - 1;
        if (!(x < 0))
        {
            float tempx = m_currentTerritory.gameObject.transform.position.x - 1;
            float tempy = m_currentTerritory.gameObject.transform.position.y;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
            {
                foreach (PlayerCharacter OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {
                    if (!m_hasJustLostBattle)
                    {
                        if (this.Faction != OtherMole.Faction)
                        {
                            DoBattle(OtherMole);
                        }
                    }
                        
                }
            }
        }
        x = m_currentTerritory.transform.position.x + 1;
        if (!(x > m_manager.m_nbrXTerritories - 1))
        {
            float tempx = m_currentTerritory.gameObject.transform.position.x + 1;
            float tempy = m_currentTerritory.gameObject.transform.position.y;
            TerritoryToTest = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx).GetComponent<Territory>();
            if (TerritoryToTest.GetPlayerNumOnTerritory() > 0)
            {
                foreach (PlayerCharacter OtherMole in TerritoryToTest.GetListOfPlayerOnThisTerritory())
                {
                    if (!m_hasJustLostBattle)
                    {
                        if (this.Faction != OtherMole.Faction)
                        {
                            DoBattle(OtherMole);
                        }
                    }
                        
                }
            }
        }
        m_hasJustLostBattle = false;
    }

    public void Move(string TypeOfMove)
    {
        float y;
        float x;
        switch (TypeOfMove)
        {
            case "UP":
                y = m_currentTerritory.transform.position.y + 1;
                if (!(y > m_manager.m_nbrYTerritories - 1))
                {
                    this.gameObject.transform.Translate(0f, 1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y + 1;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies();
                }
                break;
            case "DOWN":
                y = m_currentTerritory.transform.position.y - 1;
                if (!(y < 0))
                {
                    this.gameObject.transform.Translate(0f, -1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y - 1;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies();
                }
                break;
            case "LEFT":
                x = m_currentTerritory.transform.position.x - 1;
                if (!(x < 0))
                {
                    this.gameObject.transform.Translate(-1f, 0f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x - 1;
                    float tempy = m_currentTerritory.gameObject.transform.position.y;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies();
                }
                break;
            case "RIGHT":
                x = m_currentTerritory.transform.position.x + 1;
                if (!(x > m_manager.m_nbrXTerritories - 1))
                {
                    this.gameObject.transform.Translate(1f, 0f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x + 1;
                    float tempy = m_currentTerritory.gameObject.transform.position.y;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    TestForNearbyEnnemies();
                }
                break;
            case "DIG":
                

                if (CurrentTerritory.GetComponent<Territory>().HasSpecial)
                {
                    Special special = m_currentTerritory.GetComponent<Special>();
                    special.m_playerCharacter = this;
                    SpecialAPI.NotifyNewSpecial(special);
                    if(special.m_typeSpecial==Special.e_specialType.COINCHEST)
                    { m_goldmoney += m_goldPerCoinChest; }
                    if (special.m_typeSpecial == Special.e_specialType.GLASSES)
                    { hasGlasses=true; }

                    if (special.m_typeSpecial == Special.e_specialType.PARCHEMENT)
                        GetComponent<AudioSource>().PlayOneShot(paperSound);
                    else
                        GetComponent<AudioSource>().PlayOneShot(diggingSound);


                    CurrentTerritory.GetComponent<Territory>().HasSpecial = false;
                    Destroy(CurrentTerritory.GetComponent("Special"));
                    m_manager.RePopSpecial();
                }
                else
                {
                    //message nothing to dig?
                }
                break;
            case "!LEVEL":
                if(m_goldmoney>m_priceOfLevel)
                {
                    m_goldmoney -= m_priceOfLevel;
                    m_playerCharLVL++;
                }
                break;
        }

    }
    #endregion

    #region Didi



    #endregion
}
