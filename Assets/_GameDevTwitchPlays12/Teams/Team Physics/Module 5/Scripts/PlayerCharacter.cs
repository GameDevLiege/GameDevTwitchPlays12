using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private GameObject m_playerChar;

    public int troupe;
    public bool hasGlasses = false;
    
    #region properties
    /*
    private Color m_PcColor;
    public Color PcColor
    {
        get { return m_PcColor; }
        set { m_PcColor = value; }
    }
    */
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

    //private Color m_FactionColor;
    public Color FactionColor
    {
        get { return m_faction.FactionColor; }
        //set { m_FactionColor = value; }
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

    }

    public void Move(string TypeOfMove)
    {
        float y;
        float x;
        switch (TypeOfMove)
        {
            case "UP":
                y = m_currentTerritory.transform.position.y + 1;//la case au dessus
                if (!(y > m_manager.m_nbrYTerritories - 1))
                {
                    m_playerChar.transform.Translate(0f, 1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y + 1;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                }
                break;
            case "DOWN":
                y = m_currentTerritory.transform.position.y - 1;//la case au dessus
                if (!(y < 0))
                {
                    m_playerChar.transform.Translate(0f, -1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y - 1;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                }
                break;
            case "LEFT":
                x = m_currentTerritory.transform.position.x - 1;//la case au dessus
                if (!(x < 0))
                {
                    m_playerChar.transform.Translate(-1f, 0f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x - 1;
                    float tempy = m_currentTerritory.gameObject.transform.position.y;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                }
                break;
            case "RIGHT":
                x = m_currentTerritory.transform.position.x + 1;//la case au dessus
                if (!(x > m_manager.m_nbrXTerritories - 1))
                {
                    m_playerChar.transform.Translate(1f, 0f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x + 1;
                    float tempy = m_currentTerritory.gameObject.transform.position.y;
                    m_currentTerritory = GameObject.Find("y=" + (int)tempy + "x=" + (int)tempx);
                }
                break;
            case "DIG":
                Dig();
                break;
        }
    }
    #endregion
}
