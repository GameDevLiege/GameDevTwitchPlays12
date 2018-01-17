using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{

    
   // public List<PlayerCharacter> m_listPlayerChar;
    
    public Color FactionColor
    {
        get { return m_factionColor; }
        set { m_factionColor = value; }
    }
    public int NbrTerritories
    {
        get { return m_nbrTerritories; }
        set { m_nbrTerritories = value; }
    }
    public bool HasMiddle
    {
        get { return m_hasMiddle; }
        set { m_hasMiddle = value; }
    }
    public int GoldReserves
    {
        get { return m_goldReserves; }
        set { m_goldReserves = value; }
    }


    void StartCountDown()
    {

    }
    private void Start()
    {
        
    }

    void Update ()
    {
    }
    private int m_nbrTerritories;
    private Color m_factionColor;
    private int m_goldReserves;
    private bool m_hasMiddle = false;
}
