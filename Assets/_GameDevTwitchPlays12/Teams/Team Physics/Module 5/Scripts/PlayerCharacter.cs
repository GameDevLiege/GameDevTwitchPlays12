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


    //TESING ONLY
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Move("UP");
        }/*
        if (Input.GetButtonDown("Fire2"))
        {
            Move("DOWN");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Move("LEFT");
        }*/
        if (Input.GetButtonDown("Fire2"))
        {
            Move("RIGHT");
        }
    }
    //-------------



    public GameObject CurrentTerritory
    {
        get { return m_currentTerritory; }
        set { m_currentTerritory = value; }
    }
    public void SetCurrentTerritory(GameObject objTerritory)
    {
        m_currentTerritory = objTerritory;
    }

    public void Dig()
    {
        
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
                    m_playerChar.transform.Translate(0f, 1f, 0f);
                    float tempx = m_currentTerritory.gameObject.transform.position.x;
                    float tempy = m_currentTerritory.gameObject.transform.position.y+1;
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
}
