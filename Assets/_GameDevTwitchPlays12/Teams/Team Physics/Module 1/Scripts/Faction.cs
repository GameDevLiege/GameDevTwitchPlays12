using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    public static Faction RED { get; set; }
    public static Faction BLUE { get; set; } 
    public static Faction GREEN { get; set; }
    public static Faction YELLOW { get; set; }




    public float m_timeBetweenPayDay = 1;
    private bool m_timerFinished = true;
    public int m_incomePerTerritory = 1;
    private List<Player> m_listPlayer;
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


    IEnumerator TimerPayDay()
    {
        m_timerFinished = false;
        yield return new WaitForSeconds(m_timeBetweenPayDay);
        GoldReserves += NbrTerritories * m_incomePerTerritory;
        m_timerFinished = true;
        DispatchMoney();
    }


    void StartCountDown()
    {

    }
    private void Awake()
    {
        m_listPlayer = new List<Player>();
    }

    void Update ()
    {
        if(m_timerFinished)
            StartCoroutine(TimerPayDay());
    }

    public void DispatchMoney()
    {
        if(m_listPlayer.Count != 0)
        {
            int part = m_goldReserves / m_listPlayer.Count;
            foreach(Player player in m_listPlayer)
            {
                player.Gold += part;
            }
            m_goldReserves = m_goldReserves % m_listPlayer.Count;
        }        
    }
   
    private int m_nbrTerritories;
    private Color m_factionColor;
    private int m_goldReserves;
    private bool m_hasMiddle = false;
    private Vector3 m_respawnPosition;
  
}
