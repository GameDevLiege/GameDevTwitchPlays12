using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PhysicsManager m_manager;
    public int troupe;
    public bool hasGlasses = false;
    private void Awake()
    {
        m_playerChar = this.gameObject;
    }
    public PhysicsManager MyManager
    {
        get { return m_manager; }
        set { m_manager = value; }
    }
    public int NumPlayer
    {
        get { return m_numPlayer; }
        set { m_numPlayer = value; }
    }


    public Color FactionColor
    {
        get { return m_FactionColor; }
        set { m_FactionColor = value; }
    }
    public Color PcColor
    {
        get { return m_PcColor; }
        set { m_PcColor = value; }
    }

    public GameObject CurrentTerritory
    {
        get { return m_currentTerritory; }
        set { m_currentTerritory = value; }
    }
    public string PlayerName
    {
        get { return m_playerName; }
        set { m_playerName = value; }
    }

    public void Dig()
    {
        if (CurrentTerritory.GetComponent<Territory>().HasSpecial)
        {

        }
        else
        {
            //message nothing to dig?
        }
    }
    public void Move(string TypeOfMove)
    {
        float y;
        float x;
        switch(TypeOfMove)
        {
            case "UP":
                y = m_currentTerritory.transform.position.y+1;//la case au dessus
                if (!(y > m_manager.m_nbrYTerritories - 1))
                {
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y + 1;
                    m_playerChar.transform.Translate(0f, 1f, 0f);
                    m_currentTerritory = GameObject.Find("y="+ (int)tempy+"x="+(int)tempx);
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
    
    private GameObject m_playerChar;
    private GameObject m_currentTerritory;
    private Color m_PcColor;
    private Color m_FactionColor;
    private string m_playerName;
    private int m_numPlayer;
}
