using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour {
    public static Faction RED { get; set; }
    public static Faction BLUE { get; set; }
    public static Faction GREEN { get; set; }
    public static Faction YELLOW { get; set; }
    private bool m_timerFinished = true;
    // Use this for initialization
    void Start () {
        RED = new Faction();
        BLUE = new Faction();
        GREEN = new Faction();
        YELLOW = new Faction();
        
        
    }
	
	// Update is called once per frame
	void Update () {
        // if (m_timerFinished)
        //     StartCoroutine(TimerPayDay());
    }
    /*
    public void DispatchMoney()
    {
        if (m_listPlayer.Count != 0)
        {
            int part = m_goldReserves / m_listPlayer.Count;
            foreach (Player player in m_listPlayer)
            {
                player.Gold += part;
            }
            m_goldReserves = m_goldReserves % m_listPlayer.Count;
        }
    }
    IEnumerator TimerPayDay()
    {
        m_timerFinished = false;
        yield return new WaitForSeconds(m_timeBetweenPayDay);
        GoldReserves += NbrTerritories * m_incomePerTerritory;
        m_timerFinished = true;
        DispatchMoney();
    }*/
}
