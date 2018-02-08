using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction 
{
    public int NumFaction { get; set; }
    private int m_nbrTerritories;
    private Color m_factionColor;
    
    private int m_goldReserves;
    private bool m_hasMiddle = false;
    private static bool canLaunchTeamTimer = false;

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
    /*
    public void UpdateInterfaceUI(string faction, InterfaceManager IUI)
    {
        for(int i=0; i<m_listPlayer.Count;i++)
        {
            Player p = m_listPlayer[i];
            IUI._placementPlayer(faction, i + 1, p.NumPlayer, p.Name, p.Level, p.Gold);
        }
    }*/
    
    void StartCountDown()
    {

    }
   
   
}
