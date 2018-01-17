using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory  : MonoBehaviour
{

    #region Public Members
    

    #endregion


    #region Public Void
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
        if(m_listPlayerCharOnTerritory.Count>1)
        {
            Battle(pc);
        }
        else
        {
            if (m_currentColor != pc.FactionColor)
            {
                ColorChange(pc);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        m_listPlayerCharOnTerritory.Remove(col.GetComponent<PlayerCharacter>());
    }

    private void ColorChange(PlayerCharacter pc)
    {
        
        if (m_currentColor != Color.white)//donc si ça appartient a un autre joueur, on leur vole donc -1 en nombre territoires pour eux
        {
            if (m_currentColor == Color.red)
            {
                GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionRED.NbrTerritories--;
            }
            else if (m_currentColor == Color.blue)
            {
                GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionBLUE.NbrTerritories--;
            }
            else if (m_currentColor == Color.green)
            {
                GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionGREEN.NbrTerritories--;
            }
            else if (m_currentColor == Color.yellow)
            {
                GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionYELLOW.NbrTerritories--;
            }
        }
        m_currentColor = pc.FactionColor;
        if (m_currentColor == Color.red)
        {
            GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionRED.NbrTerritories++;
        }
        else if (m_currentColor == Color.blue)
        {
            GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionBLUE.NbrTerritories++;
        }
        else if (m_currentColor == Color.green)
        {
            GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionGREEN.NbrTerritories++;
        }
        else if (m_currentColor == Color.yellow)
        {
            GameObject.Find("PManager").GetComponent<PhysicsManager>().FactionYELLOW.NbrTerritories++;
        }
    }

    private void Battle(PlayerCharacter pc)
    {

    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private bool m_hasSpecial;
    private bool m_isCenter;
    private bool m_isHQ;
    public Color m_currentColor = Color.white;
    private List<PlayerCharacter> m_listPlayerCharOnTerritory = new List<PlayerCharacter>();
    #endregion

}
