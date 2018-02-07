using System.Collections.Generic;
using UnityEngine;
public class Territory : MonoBehaviour
{

    #region Public Members
    #endregion


    #region Public Void
    #endregion
    #region Propertie (GET,SET)
    [SerializeField]
    private string m_territoryID;
    public string TerritoryID
    {
        get { return m_territoryID; }
        set { m_territoryID = value; }
    }
    [Header("Used GameObject")]
    [SerializeField]
    private MeshRenderer m_territoryMeshRenderer;

    public MeshRenderer TerritoryMeshRenderer
    {
        get { return m_territoryMeshRenderer; }
        set { m_territoryMeshRenderer = value; }
    }
    [Header("Item territory properties")]
    [SerializeField]
    private bool m_hasItem;
    public bool HasItem
    {
        get { return m_hasItem; }
        set { m_hasItem = value; }
    }

    private Item m_territoryItem;
    public Item TerritoryItem
    {
        get { return m_territoryItem; }
        set { m_territoryItem = value; }
    }
    [Header("Headquarter")]
    [SerializeField]
    private bool m_isHQ;
    public bool IsHQ
    {
        get { return m_isHQ; }
        set { m_isHQ = value; }
    }
    [Header("Central Zone")]
    [SerializeField]
    private bool m_isCenter;
    public bool IsCenter
    {
        get { return m_isCenter; }
        set { m_isCenter = value; }
    }
    [Header("Territory Color")]
    [SerializeField]
    private Color m_currentColor = Color.white;
    public Color CurrentColor
    {
        get { return m_currentColor; }
        set { m_currentColor = value; }
    }
    public int FactionNum {get;set;}

    private List<Player> m_listPlayerCharOnTerritory = new List<Player>();
    public List<Player> GetListOfPlayerOnThisTerritory()
    {
        return m_listPlayerCharOnTerritory;
    }

    public int GetPlayerNumOnTerritory()
    {
    return m_listPlayerCharOnTerritory.Count;
    }


    #endregion


    #region System

    void Awake () 
    {
        m_territoryMeshRenderer = GetComponent<MeshRenderer>();
        FactionNum = 0;

    }

    #endregion

    #region Private Void
    // à déplacer
    private void OnTriggerEnter(Collider col)
    {
        Player p = col.GetComponent<Player>();
        m_listPlayerCharOnTerritory.Add(p);
        if ((p != null && p.Faction!=null && FactionNum != p.Faction.NumFaction)&&(!IsHQ) )
        {
            FactionChange(p);
        }
    }
    public void ColorChange(Color color) {
        
        m_territoryMeshRenderer.material.color = color;

    }
    public void FactionChange(Player p)
    {
        //previous territory owner looses Nbrterritory

        if (FactionNum != 0)
        {
            if (FactionNum == 1)
            {
                FactionManager.RED.NbrTerritories--;
            }
            else if (FactionNum == 2)
            {
                FactionManager.BLUE.NbrTerritories--;
            }
            else if (FactionNum == 3)
            {
                FactionManager.GREEN.NbrTerritories--;
            }
            else if (FactionNum == 4)
            {
                FactionManager.YELLOW.NbrTerritories--;
            }
        }
        m_currentColor = p.Faction.FactionColor;
        FactionNum = p.Faction.NumFaction;
        Color col = TerritoryMeshRenderer.material.color;
        col = p.Faction.FactionColor;
        col.a = 100f;
        TerritoryMeshRenderer.material.color = col;
        //new territory owner gains Nbrterritory
        Debug.Log("Faction color ="  );
        Debug.Log(p.Faction.FactionColor.linear + " ==" + Color.red.linear +" "+ (p.Faction.FactionColor.linear==Color.red.linear));
        if (FactionNum == 1)
        {
            FactionManager.RED.NbrTerritories++;
            Debug.Log("ici--------****" + FactionManager.RED.NbrTerritories);
        }
        else if (FactionNum == 2)
        {
            FactionManager.BLUE.NbrTerritories++;
        }
        else if (FactionNum == 3)
        {
            FactionManager.GREEN.NbrTerritories++;
        }
        else if (FactionNum == 4)
        {
            FactionManager.YELLOW.NbrTerritories++;
        }
    }

    private void Battle()
    {

    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    #endregion

}
