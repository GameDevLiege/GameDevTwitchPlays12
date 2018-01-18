using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager  : MonoBehaviour, IGameEngine
{

    #region Public Members
    public List<GameObject> m_listPlayer = new List<GameObject>();
    public int m_sizeOfDiceSpecial = 5;
    public int m_incomePerTerritory = 1;
    public float m_timeBetweenPayDay = 1;
    public int m_nbrXTerritories =33;
    public int m_nbrYTerritories =33;
    public GameObject m_territoryPrefab;
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


    public void AssignFactionToPlayers(List<string> ListOfPlayerNames)         //JEROME HERE ! Give me a list of player names, or ID in string format, thx buddy ;-)
    {
        if (m_isInitialized)
        {
            return;
        }
        m_isInitialized = true;
        
        
        FactionRED = gameObject.AddComponent<Faction>();
        FactionRED.FactionColor = Color.red;
        //FactionRED.
        FactionBLUE = gameObject.AddComponent<Faction>();
        FactionBLUE.FactionColor = Color.blue;

        if (ListOfPlayerNames.Count > 8)
        {
            FactionGREEN = gameObject.AddComponent<Faction>();
            FactionGREEN.FactionColor = Color.green;
            FactionYELLOW = gameObject.AddComponent<Faction>();
            FactionYELLOW.FactionColor = Color.yellow;
        }
            
        int PlayerNum = 0;
        for(int i=0; PlayerNum< ListOfPlayerNames.Count; PlayerNum++)
        {
            GameObject NewPlayerGameObject = Instantiate(m_playerCharPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
            NewPlayerGameObject.name = ListOfPlayerNames[PlayerNum];
            NewPlayerGameObject.GetComponentInChildren<TextMesh>().text = "" + PlayerNum;

            PlayerCharacter NewPlayerScript = NewPlayerGameObject.GetComponent<PlayerCharacter>();
            NewPlayerScript.NumPlayer = PlayerNum;
            NewPlayerScript.PlayerName = ListOfPlayerNames[PlayerNum];
            
            if (i==0)
            {
                NewPlayerScript.Faction = FactionRED;
                FactionRED.AddPlayer(NewPlayerScript);
                
                NewPlayerGameObject.transform.position = new Vector3(0f,0f,0f);
                NewPlayerScript.CurrentTerritory = m_AxeY[0][0].gameObject;
            }
            else if (i == 1)
            {
                NewPlayerScript.Faction = FactionBLUE;
                FactionBLUE.AddPlayer(NewPlayerScript);
                
                NewPlayerGameObject.transform.position = new Vector3(m_nbrXTerritories-1, m_nbrYTerritories-1, 0f);
                NewPlayerScript.CurrentTerritory = m_AxeY[m_nbrXTerritories - 1][m_nbrYTerritories - 1].gameObject;
            }
            else if (i == 2)
            {
                NewPlayerScript.Faction = FactionGREEN;
                FactionGREEN.AddPlayer(NewPlayerScript);
                
                NewPlayerGameObject.transform.position = new Vector3(m_nbrXTerritories-1, 0f, 0f);
                NewPlayerScript.CurrentTerritory = m_AxeY[m_nbrXTerritories - 1][0].gameObject;
            }
            else if (i == 3)
            {
                NewPlayerScript.Faction = FactionYELLOW;
                FactionYELLOW.AddPlayer(NewPlayerScript);
                
                NewPlayerGameObject.transform.position = new Vector3(0f, m_nbrYTerritories - 1, 0f);
                NewPlayerScript.CurrentTerritory = m_AxeY[0][m_nbrYTerritories - 1].gameObject;
            }
            i++;
            if (ListOfPlayerNames.Count > 8 )
            {
                if(i > 3)
                {
                    i = 0;
                }
            }
            else if(i>1)
            {
                i = 0;
            }
            NewPlayerScript.MyManager = this;
            m_listPlayer.Add(NewPlayerGameObject);
        }
    }

    IEnumerator TimerPayDay()
    {
        m_timerFinished = false;
        yield return new WaitForSeconds(m_timeBetweenPayDay);
        FactionRED.GoldReserves += FactionRED.NbrTerritories * m_incomePerTerritory;
        FactionBLUE.GoldReserves += FactionBLUE.NbrTerritories * m_incomePerTerritory;
        FactionGREEN.GoldReserves += FactionGREEN.NbrTerritories * m_incomePerTerritory;
        FactionYELLOW.GoldReserves += FactionYELLOW.NbrTerritories * m_incomePerTerritory;
        m_timerFinished = true;
        FactionRED.DispatchMoney();
        FactionBLUE.DispatchMoney();
        FactionGREEN.DispatchMoney();
        FactionYELLOW.DispatchMoney();

           
    }
 
    public void GameStart()
    {
        InitializeBoard();

        //List<string> DebugList = new List<string>();
        //for (int i = 0; i < 20; i++)
        //{
        //    DebugList.Add("hoho" + i);
        //}

        //AssignFactionToPlayers(DebugList);
    }

    public void GetCommandFromPlayer(string PName, string Command)              //JEROME HERE ! Give me a player names and the command he sends ;-)       
    {
        foreach(GameObject PlayerChar in m_listPlayer)
        {
            if(PlayerChar.GetComponent<PlayerCharacter>().PlayerName==PName)
            {
                PlayerChar.GetComponent<PlayerCharacter>().Move(Command);
            }
        }
    }

    #region System
    void Start () 
    {
		
	}

    void Awake () 
    {
        GameStart();
    }
	
	void Update () 
    {
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            m_listPlayer[0].GetComponent<PlayerCharacter>().Move("UP");

        }
        if (Input.GetButtonDown("Fire2"))
        {
            Move("DOWN");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Move("LEFT");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            m_listPlayer[0].GetComponent<PlayerCharacter>().Move("RIGHT");
        }*/



        if(m_gameHasStarted)
        {
            if (m_timerFinished)
            {
                StartCoroutine("TimerPayDay");
            }
        }
        
    }

    #endregion

    #region Private Void
    private void InitializeBoard()
    {
        InstanciateTerritories();
        PlaceCenterZone();
        PlaceFactionHQ();
        PlaceSpecials();
    }
    private void InstanciateTerritories()
    {
        for (int y = 0; y < m_nbrYTerritories; y++)
        {
            m_AxeX = new List<GameObject>();//obligatoire de faire ds boucle ou Y remplace la même list encore et encore au lieu d'Add -.-'
            for (int x = 0; x < m_nbrXTerritories; x++)
            {
                Vector3 positionOfCell = new Vector3(x * 1f, y * 1f, 0);
                GameObject Ter = Instantiate(m_territoryPrefab, positionOfCell, Quaternion.identity, transform);
                Ter.transform.position = positionOfCell;
                Ter.name = "y=" + positionOfCell.y + "x=" + positionOfCell.x;
                Ter.GetComponent<Territory>().Manager = this;
                m_AxeX.Add(Ter);
            }
            m_AxeY.Add(m_AxeX);
        }
    }
    private void PlaceFactionHQ()
    {
        //LeftBottom
        m_AxeY[1][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.red;
        m_AxeY[1][2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][2].gameObject.GetComponent<Territory>().CurrentColor = Color.red;
        m_AxeY[2][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[2][1].gameObject.GetComponent<Territory>().CurrentColor = Color.red;
        m_AxeY[2][2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[2][2].gameObject.GetComponent<Territory>().CurrentColor = Color.red;
        //RightBottom
        m_AxeY[1][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().CurrentColor = Color.yellow;
        m_AxeY[1][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().CurrentColor = Color.yellow;
        m_AxeY[2][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().CurrentColor = Color.yellow;
        m_AxeY[2][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().CurrentColor = Color.yellow;
        //LeftTop
        m_AxeY[m_nbrYTerritories - 1][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.green;
        m_AxeY[m_nbrYTerritories - 1][2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.green;
        m_AxeY[m_nbrYTerritories - 2][1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.green;
        m_AxeY[m_nbrYTerritories - 2][2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.green;
        //RightTop
        m_AxeY[m_nbrYTerritories - 1][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.blue;
        m_AxeY[m_nbrYTerritories - 1][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.blue;
        m_AxeY[m_nbrYTerritories - 2][m_nbrXTerritories - 1].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.blue;
        m_AxeY[m_nbrYTerritories - 2][m_nbrXTerritories - 2].gameObject.GetComponent<Territory>().IsHQ = true;
        m_AxeY[1][1].gameObject.GetComponent<Territory>().CurrentColor = Color.blue;

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
    private void PlaceSpecials()
    {
        int A = Random.Range(1, 10);//decides which central territory gets the glasses
        int cptCenter=1;
        for (int y = 0; y < m_nbrYTerritories; y++)
        {
            for (int x = 0; x < m_nbrXTerritories; x++)
            {
                if(!m_AxeY[y][x].GetComponent<Territory>().IsHQ)//no special in HQs
                {
                    if (m_AxeY[y][x].GetComponent<Territory>().IsCenter)//only key in center
                    {
                        if (cptCenter==A)
                        {
                            m_AxeY[y][x].GetComponent<Territory>().HasSpecial = true;
                            m_AxeY[y][x].AddComponent<Special>();
                            m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.GLASSES);
                        }
                        cptCenter++;
                    }
                    else
                    {
                        int B = Random.Range(1, m_sizeOfDiceSpecial+1); //random range takes argument 1 inclusive argument 2 exclusive
                        if(B==1)//1 chance out of sizeofdice...
                        {
                            m_AxeY[y][x].GetComponent<Territory>().HasSpecial = true;
                            m_AxeY[y][x].AddComponent<Special>();
                            B= Random.Range(1, 7);//random range takes argument 1 inclusive argument 2 exclusive
                            switch (B)
                            {
                                case 1: m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.COINCHEST); break;
                                case 2: m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.PARCHEMENT); break;
                                case 3: m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.GRENADES); break;
                                case 4: m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.PEBBLE); break;
                                case 5: m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.SHOVEL); break;
                                case 6: m_AxeY[y][x].GetComponent<Special>().ChooseTypeOfSpecial(Special.e_specialType.STRAIN); break;
                            }
                        }
                    }
                }
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

    private bool m_timerFinished=true;
    private bool m_gameHasStarted;
    private bool m_isInitialized;
    #endregion

}
