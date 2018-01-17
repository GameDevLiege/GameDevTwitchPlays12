using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory  : MonoBehaviour
{

    #region Public Members
    
    
    //List<Player> m_listPlayerOnTerritory;
    

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
    private void OnTriggerEnter(Collider other)
    {
        
    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private bool m_hasSpecial;
    private bool m_isCenter;
    private bool m_isHQ;
    private Color m_currentColor;
    #endregion

}
