using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager  : MonoBehaviour
{

    #region Public Members
    public GameObject TEMP_REMOVE_ME_player;

    public int m_incomePerTerritory;
    public float m_timeBetweenPayDay;
    public int m_nbrXTerritories =33;
    public int m_nbrYTerritories =33;
    public GameObject m_TerritoryPrefab;
    public int m_nbrFactions;
    #endregion


    #region Public Void

    #endregion


    #region System

    void Start () 
    {
		
	}
    void Awake () 
    {
        InitializeBoard();
        TEMP_REMOVE_ME_player.GetComponent<PlayerCharacter>().SetCurrentTerritory(m_AxeY[0][0].gameObject);
    }
	
	void Update () 
    {
		
	}

    #endregion

    #region Private Void
    private void InitializeBoard()
    {
        InstanciateTerritories();
        PlaceCenterZone();
        PlaceFactionHQ();
    }
    private void InstanciateTerritories()
    {
        for (int y = 0; y < m_nbrYTerritories; y++)
        {
            m_AxeX = new List<GameObject>();//obligatoire de faire ds boucle ou Y remplace la même list encore et encore au lieu d'Add -.-'
            for (int x = 0; x < m_nbrXTerritories; x++)
            {
                Vector3 positionOfCell = new Vector3(x * 1f, y * 1f, 0);
                GameObject Ter = Instantiate(m_TerritoryPrefab, positionOfCell, Quaternion.identity);
                Ter.transform.position = positionOfCell;
                Ter.name = "y=" + positionOfCell.y + "x=" + positionOfCell.x;
                m_AxeX.Add(Ter);
            }
            m_AxeY.Add(m_AxeX);
        }
    }
    private void PlaceFactionHQ()
    {
        //LeftBottom
        m_AxeY[1][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[2][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[2][2].gameObject.GetComponent<Territory>().IsHQ = true;
        //RightBottom
        m_AxeY[1][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[2][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[2][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        //LeftTop
        m_AxeY[m_nbrYTerritories - 1][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[m_nbrYTerritories - 1][2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[m_nbrYTerritories - 2][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[m_nbrYTerritories - 2][2].gameObject.GetComponent<Territory>().IsHQ = true;
        //RightTop
        m_AxeY[m_nbrYTerritories - 1][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[m_nbrYTerritories - 1][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[m_nbrYTerritories - 2][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[m_nbrYTerritories - 2][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;

    }
    private void PlaceCenterZone()
    {
        int xTrueCenter = (((m_nbrXTerritories + 2) - 1) / 2);
        int yTrueCenter = (((m_nbrYTerritories + 2) - 1) / 2);
        for (int y = yTrueCenter-1; y <= yTrueCenter +1; y++)
        {
            for (int x = xTrueCenter - 1; x <= xTrueCenter + 1; x++)
            {
                m_AxeY[y][x].gameObject.GetComponent<Territory>().IsCenter = true;
            }
        }
    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private List<GameObject> m_AxeX = new List<GameObject>();
    private List<List<GameObject>> m_AxeY = new List<List<GameObject>>();
    #endregion

}
