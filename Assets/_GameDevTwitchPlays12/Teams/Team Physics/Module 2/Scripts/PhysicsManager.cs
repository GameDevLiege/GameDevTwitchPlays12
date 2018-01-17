using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager  : MonoBehaviour
{

    #region Public Members
    public GameObject TEMP_REMOVE_ME_player;

    public List<GameObject> m_listPlayer = new List<GameObject>();

    public int m_incomePerTerritory;
    public float m_timeBetweenPayDay;
    public int m_nbrXTerritories =33;
    public int m_nbrYTerritories =33;
    public GameObject m_TerritoryPrefab;
    public GameObject m_playerCharPrefab;
    public int m_nbrFactions;
    #endregion


    #region Public Void

    public Faction FactionRED
    {
        get { return m_factionRed; }
        set { m_factionRed = value; }
    }
    public Faction FactionBLUE
    {
        get { return m_factionBlue; }
        set { m_factionBlue = value; }
    }
    public Faction FactionGREEN
    {
        get { return m_factionGreen; }
        set { m_factionGreen = value; }
    }
    public Faction FactionYELLOW
    {
        get { return m_factionYellow; }
        set { m_factionYellow = value; }
    }

    #endregion


    #region System
    public void AssignFactionToPlayers(List<string> ListOfPlayerNames)         //JEROME HERE ! Give me a list of player names, or ID in string format, thx buddy ;-)
    {
        foreach(string Pname in ListOfPlayerNames)
        {
            GameObject NewPlayerChar = Instantiate(m_playerCharPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            NewPlayerChar.GetComponent<PlayerCharacter>().PlayerName = Pname;
            m_listPlayer.Add(NewPlayerChar);
        }
        
        FactionRED = gameObject.AddComponent<Faction>();
        FactionRED.FactionColor = Color.red;
        FactionBLUE = gameObject.AddComponent<Faction>();
        FactionBLUE.FactionColor = Color.blue;
        if (ListOfPlayerNames.Count>8)//if 4 factions
        {
            FactionGREEN = gameObject.AddComponent<Faction>();
            FactionGREEN.FactionColor = Color.green;
            FactionYELLOW = gameObject.AddComponent<Faction>();
            FactionYELLOW.FactionColor = Color.yellow;
            int PlayerNum = 0;
            for(int i=0; PlayerNum<m_listPlayer.Count-1; i++)
            {
                if(i==0)
                {
                    m_listPlayer[PlayerNum].GetComponent<PlayerCharacter>().FactionColor = Color.red;
                    m_listPlayer[PlayerNum].transform.position = new Vector3(0f,0f,0f);
                }
                else if (i == 1)
                {
                    m_listPlayer[PlayerNum].GetComponent<PlayerCharacter>().FactionColor = Color.blue;
                    m_listPlayer[PlayerNum].transform.position = new Vector3(m_nbrXTerritories-1, m_nbrYTerritories-1, 0f);
                }
                else if (i == 2)
                {
                    m_listPlayer[PlayerNum].GetComponent<PlayerCharacter>().FactionColor = Color.green;
                    m_listPlayer[PlayerNum].transform.position = new Vector3(m_nbrXTerritories-1, 0f, 0f);
                }
                else if (i == 3)
                {
                    m_listPlayer[PlayerNum].GetComponent<PlayerCharacter>().FactionColor = Color.yellow;
                    m_listPlayer[PlayerNum].transform.position = new Vector3(0f, m_nbrYTerritories - 1, 0f);
                }
                i++;
                if(i>3)
                {
                    i = 0;
                }
                PlayerNum++;
            }
        }
        else//if 2 factions
        {
            int PlayerNum = 0;
            for (int i = 0; PlayerNum < m_listPlayer.Count - 1; i++)
            {
                if (i == 0)
                {
                    m_listPlayer[PlayerNum].GetComponent<PlayerCharacter>().FactionColor = Color.red;
                }
                else if (i == 1)
                {
                    m_listPlayer[PlayerNum].GetComponent<PlayerCharacter>().FactionColor = Color.blue;
                }
                i++;
                if (i > 1)
                {
                    i = 0;
                }
                PlayerNum++;
            }
        }
    }

    void Start () 
    {
		
	}
    void Awake () 
    {
        InitializeBoard();
        TEMP_REMOVE_ME_player.GetComponent<PlayerCharacter>().CurrentTerritory = m_AxeY[0][0].gameObject;
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

    private Faction m_factionRed;
    private Faction m_factionBlue;
    private Faction m_factionGreen;
    private Faction m_factionYellow;


    #endregion

}
