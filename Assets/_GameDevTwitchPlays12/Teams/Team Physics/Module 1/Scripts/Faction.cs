using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction 
{
    public float m_timeBetweenPayDay = 1;
    public int m_incomePerTerritory = 1;
    private int m_nbrTerritories;
    private Color m_factionColor;
    private int m_goldReserves;
    private bool m_hasMiddle = false;

    private List<Player> m_listPlayer = new List<Player>();
    public List<Player> ListPlayer
    {
        get { return m_listPlayer; }
        set { m_listPlayer = value; }
    }

    public void AddPlayer(Player player)
    {
        m_listPlayer.Add(player);
    }
    /*
     * TODO
    public bool RemovePlayer()
    {
        if(m_listPlayerChar.Contains()
    }*/

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
    public Territory RespawnPosition { get; set; }

    void StartCountDown()
    {

    }
   
   
}
