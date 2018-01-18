using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private GameObject m_playerChar;
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
        m_playerChar = this.gameObject;
    }
    #endregion

    #region  class methods
    public void Dig()
    {
        if (CurrentTerritory.GetComponent<Territory>().HasSpecial)
        {
            CurrentTerritory.GetComponent<Special>().GetItemOrEffect(this);
        }
        else
        {
        }
    }
    public void DoBattle(PlayerCharacter Enemy)
    {
        int temp= this.PlayerCharLVL;
        this.PlayerCharLVL -= Enemy.PlayerCharLVL;
        Enemy.PlayerCharLVL -= temp;
        if(this.PlayerCharLVL<1)
        {
            this.PlayerCharLVL = 1;
            this.gameObject.transform.position = this.Faction.RespawnPosition;
            m_hasJustLostBattle = true;
        }
        if (Enemy.PlayerCharLVL < 1)
        {
            Enemy.PlayerCharLVL = 1;
            Enemy.gameObject.transform.position = this.Faction.RespawnPosition;
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
            Debug.Log("y=" + (int)tempy + "x=" + (int)tempx);
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
            Debug.Log("y=" + (int)tempy + "x=" + (int)tempx);
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
            Debug.Log("y=" + (int)tempy + "x=" + (int)tempx);
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
            Debug.Log("y=" + (int)tempy + "x=" + (int)tempx);
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
                    m_playerChar.transform.Translate(0f, 1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y + 1;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    // TestForNearbyEnnemies();
                }
                break;
            case "DOWN":
                y = m_currentTerritory.transform.position.y - 1;
                if (!(y < 0))
                {
                    m_playerChar.transform.Translate(0f, -1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y - 1;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    // TestForNearbyEnnemies();
                }
                break;
            case "LEFT":
                x = m_currentTerritory.transform.position.x - 1;
                if (!(x < 0))
                {
                    m_playerChar.transform.Translate(-1f, 0f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x - 1;
                    float tempy = m_currentTerritory.gameObject.transform.position.y;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    // TestForNearbyEnnemies();
                }
                break;
            case "RIGHT":
                x = m_currentTerritory.transform.position.x + 1;
                if (!(x > m_manager.m_nbrXTerritories - 1))
                {
                    m_playerChar.transform.Translate(1f, 0f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x + 1;
                    float tempy = m_currentTerritory.gameObject.transform.position.y;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                    // TestForNearbyEnnemies();
                }
                break;
            case "DIG":
                Dig();
                break;
        }
    }
    #endregion

    #region Didi



    #endregion
}
