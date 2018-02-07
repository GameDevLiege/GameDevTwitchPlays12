using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour {
    public float m_timeBetweenPayDay = 1;
    public int m_incomePerTerritory = 1;
    public float goldAmountRED;
    public float goldAmountBLUE;
    public float goldAmountYELLOW;
    public float goldAmountGREEN;
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

        //Ne fonctionne pas car problème avec les couleurs voir Territory pour plus d'info
         if (m_timerFinished)
             StartCoroutine(TimerPayDay());
        goldAmountRED=RED.GoldReserves;
        goldAmountBLUE=BLUE.GoldReserves;
        goldAmountGREEN=GREEN.GoldReserves;
        goldAmountYELLOW = YELLOW.GoldReserves;
        
}
 
    public void DispatchMoney(Faction faction)
    {
        faction.GoldReserves += faction.NbrTerritories * m_incomePerTerritory;
        //Debug.Log("test gold------->"+faction.GoldReserves);
        if (faction.ListPlayer.Count != 0)
        {
            int part = faction.GoldReserves / faction.ListPlayer.Count;
            foreach (Player player in faction.ListPlayer)
            {
                player.Gold += part;
            }
            faction.GoldReserves = faction.GoldReserves % faction.ListPlayer.Count;
        }
    }
    IEnumerator TimerPayDay()
    {
        m_timerFinished = false;
        yield return new WaitForSeconds(m_timeBetweenPayDay);
        DispatchMoney(RED);
        DispatchMoney(BLUE);
        DispatchMoney(GREEN);
        DispatchMoney(YELLOW);
        m_timerFinished = true;
    }
}
