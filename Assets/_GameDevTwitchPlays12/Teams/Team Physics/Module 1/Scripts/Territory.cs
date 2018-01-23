using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homebrew;

public class Territory  : MonoBehaviour
{

    #region Public Members


    #endregion


    #region Public Void
    public delegate void IsCentral(bool end,Faction faction);
    private static IsCentral m_isCentral;

    public bool HasSpecial
    {
        get { return m_hasSpecial; }
        set { m_hasSpecial = value; }
    }

    public bool IsHQ
    {
        get { return m_isHQ; }
        set { m_isHQ = value; }
    }
    public bool IsCenter
    {
        get { return m_isCenter; }
        set { m_isCenter = value; }
    }
    public Color CurrentColor
    {
        get { return m_currentColor; }
        set { m_currentColor = value; }
    }
    public PhysicsManager Manager
    {
        get { return m_manager; }
        set { m_manager = value; }
    }

    public List<PlayerCharacter> GetListOfPlayerOnThisTerritory()
    {
        return m_listPlayerCharOnTerritory;
    }

    public int GetPlayerNumOnTerritory()
    {
    return m_listPlayerCharOnTerritory.Count;
    }
    public Timer m_timer;
    public float centralTime=60;
    public Faction m_playerFaction= new Faction();
    #endregion


    #region System

    void Awake () 
    {
		
	}

    #endregion

    #region Private Void
    private void OnTriggerEnter(Collider col)
    {
        PlayerCharacter pc = col.GetComponent<PlayerCharacter>();
        m_listPlayerCharOnTerritory.Add(pc);
        if ((m_currentColor != pc.Faction.FactionColor)&&(!IsHQ))
        {
            ColorChange(pc);
        }
        if(IsCenter)
        {
            if(pc.hasGlasses)
            {
                NotifyIsCentral(pc.Faction);
            }
        }
        else if(pc.hasGlasses && !IsCenter)
        {
            NotifyIsNotCentral(pc.Faction);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        m_listPlayerCharOnTerritory.Remove(col.GetComponent<PlayerCharacter>());
      

    }

    private void ColorChange(PlayerCharacter pc)
    {
        //previous territory owner looses Nbrterritory
        if (m_currentColor != Color.white)
        {
            if (m_currentColor == Color.red)
            {
                m_manager.FactionRED.NbrTerritories--;
            }
            else if (m_currentColor == Color.blue)
            {
                m_manager.FactionBLUE.NbrTerritories--;
            }
            else if (m_currentColor == Color.green)
            {
                m_manager.FactionGREEN.NbrTerritories--;
            }
            else if (m_currentColor == Color.yellow)
            {
                m_manager.FactionYELLOW.NbrTerritories--;
            }
        }
        m_currentColor = pc.Faction.FactionColor;
        Color col = gameObject.GetComponentInChildren<MeshRenderer>().material.color;
        col = pc.Faction.FactionColor;
        col.a = 100f;
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = col;
        //new territory owner gains Nbrterritory
        if (m_currentColor == Color.red)
        {
            m_manager.FactionRED.NbrTerritories++;
        }
        else if (m_currentColor == Color.blue)
        {
            m_manager.FactionBLUE.NbrTerritories++;
        }
        else if (m_currentColor == Color.green)
        {
            m_manager.FactionGREEN.NbrTerritories++;
        }
        else if (m_currentColor == Color.yellow)
        {
            m_manager.FactionYELLOW.NbrTerritories++;
        }
    }

    private void Battle()
    {

    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private bool m_hasSpecial;
    private bool m_isCenter;
    private bool m_isHQ;
    private Color m_currentColor = Color.white;
    private List<PlayerCharacter> m_listPlayerCharOnTerritory = new List<PlayerCharacter>();
    private PhysicsManager m_manager;
    #endregion

    private void TimerOnCentral(bool bol) {
        //Partie gagnée
        StartCoroutine("GameOver");
    }
    IEnumerator GameOver()
    {

        yield return new WaitForSeconds(10);
        
    }

    public static void AddListener(IsCentral isCentral)
    {
        m_isCentral += isCentral;

    }
    public static void RemoveListener(IsCentral isCentral)
    {
        m_isCentral -= isCentral;

    }


    public static void NotifyIsCentral(Faction faction)
    {
        m_isCentral(true, faction );
    }
    public static void NotifyIsNotCentral(Faction faction)
    {
        m_isCentral(false,faction);
    }
}
