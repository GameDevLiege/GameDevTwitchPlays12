using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager  : MonoBehaviour, IGameEngine
{

    #region Public Members
    public List<PlayerCharacter> m_listPlayer = new List<PlayerCharacter>();
    public List<PlayerInfo> m_listPlayerInfo = new List<PlayerInfo>();
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
        
        int PlayerNum = 0;
        for(int faction = 0; PlayerNum< ListOfPlayerNames.Count; PlayerNum++)
        {
            GameObject NewPlayerGameObject = Instantiate(m_playerCharPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
            NewPlayerGameObject.name = ListOfPlayerNames[PlayerNum];
            NewPlayerGameObject.GetComponentInChildren<TextMesh>().text = "" + PlayerNum;

            PlayerCharacter NewPlayerScript = NewPlayerGameObject.GetComponent<PlayerCharacter>();
            NewPlayerScript.MyManager = this;
            NewPlayerScript.NumPlayer = PlayerNum;
            NewPlayerScript.PlayerName = ListOfPlayerNames[PlayerNum];
            if (faction == 0)

            {
                NewPlayerScript.Faction = FactionRED;
                FactionRED.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionRED.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[0][0].gameObject;
            }
            else if (faction == 1)
            {
                NewPlayerScript.Faction = FactionBLUE;
                FactionBLUE.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionBLUE.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[m_nbrXTerritories - 1][m_nbrYTerritories - 1].gameObject;
            }
            else if (faction == 2)
            {
                NewPlayerScript.Faction = FactionGREEN;
                FactionGREEN.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionGREEN.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[m_nbrXTerritories - 1][0].gameObject;
            }
            else if (faction == 3)
            {
                NewPlayerScript.Faction = FactionYELLOW;
                FactionYELLOW.AddPlayer(NewPlayerScript);
                NewPlayerGameObject.transform.position = FactionYELLOW.RespawnPosition;
                NewPlayerScript.CurrentTerritory = m_AxeY[0][m_nbrYTerritories - 1].gameObject;
            }
            faction++;
            if (ListOfPlayerNames.Count > 8 )
            {
                if(faction > 3)
                {
                    faction = 0;
                }
            }
            else if(faction > 1)
            {
                faction = 0;
            }

            NewPlayerGameObject.GetComponent<ColorHelmet>().UpdateColor(NewPlayerScript.FactionColor);

            m_listPlayer.Add(NewPlayerScript);
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
    public void FillPlayerInfoList()
    {
        m_listPlayerInfo.Clear();
        int nbrRed = 1;
        int nbrBlue = 1;
        int nbrGreen = 1;
        int nbrYellow = 1;
        for (int i=0; i<m_listPlayer.Count;i++)
        {
            PlayerInfo _playerInfo = new PlayerInfo();
            PlayerCharacter playerC = m_listPlayer[i];
            _playerInfo.num = m_listPlayer[i].NumPlayer;
            _playerInfo.name = m_listPlayer[i].PlayerName;
            _playerInfo.level = m_listPlayer[i].PlayerCharLVL;
            _playerInfo.gold = m_listPlayer[i].Gold;
            _playerInfo.faction = m_listPlayer[i].FactionColor.ToString().ToUpper();
            switch(_playerInfo.faction)
            {
                case "GREEN":
                    _playerInfo.posOnScreen = nbrGreen;
                    nbrGreen++;
                    break;
                case "RED":
                    _playerInfo.posOnScreen = nbrRed;
                    nbrRed++;
                    break;
                case "BLUE":
                    _playerInfo.posOnScreen = nbrBlue;
                    nbrBlue++;
                    break;
                case "YELLOW":
                    _playerInfo.posOnScreen = nbrYellow;
                    nbrYellow++;
                    break;
            }
            m_listPlayerInfo.Add(_playerInfo);
        }
        
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
        foreach(PlayerCharacter PlayerChar in m_listPlayer)
        {
            if(PlayerChar.PlayerName==PName)
            {
                PlayerChar.Move(Command);
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

    void Update()
    {
        //testing only !!
        if (Input.GetButtonDown("Fire1"))
        {
            m_listPlayer[0].GetComponent<PlayerCharacter>().Move("UP");

        }
        if (Input.GetButtonDown("Fire2"))
        {
            m_listPlayer[0].GetComponent<PlayerCharacter>().Move("RIGHT");
        }
        //--------------


        if (m_gameHasStarted)
        {
            if (m_timerFinished)
            {
                StartCoroutine("TimerPayDay");
            }
        }

        foreach (PlayerInfo _playerInfo in m_listPlayerInfo)
        {
            //_placementPlayer(_playerInfo.faction, _playerInfo.posOnScreen, _playerInfo.num, _playerInfo.name, _playerInfo.level, _playerInfo.gold);
        }
    }

    #endregion

    #region Private Void
    private void InitializeBoard()
    {
        InstanciateTerritories();
        PlaceCenterZone();
        CreateFactions();
        PlaceFactionHQ();
        PlaceSpecials();
    }
    private Faction CreateAFaction(Color col , Vector3 respawnPosition)
    {
        Faction newFaction = gameObject.AddComponent<Faction>();
        col.a = 100f;
        newFaction.FactionColor = col;
        newFaction.RespawnPosition = respawnPosition;
        return newFaction;
    }
    private void CreateFactions()
    {
        FactionRED = CreateAFaction(Color.red, new Vector3(0f, 0f, 0f));
        FactionBLUE = CreateAFaction(Color.blue, new Vector3(m_nbrYTerritories - 1, m_nbrXTerritories - 1, 0f));
        FactionGREEN = CreateAFaction(Color.green, new Vector3(m_nbrYTerritories - 1, 0f, 0f));
        FactionYELLOW = CreateAFaction(Color.yellow, new Vector3(0f, m_nbrXTerritories - 1, 0f));
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
    public void MakeHq(GameObject HQTerritoryObject, Faction faction)
    {
        HQTerritoryObject.GetComponent<Territory>().IsHQ = true;
        HQTerritoryObject.GetComponentInChildren<MeshRenderer>().material.color = faction.FactionColor;
        HQTerritoryObject.GetComponent<Territory>().CurrentColor = faction.FactionColor;
    }
    private void PlaceFactionHQ()
    {
        //LeftBottom
        MakeHq(m_AxeY[0][0].gameObject, FactionRED);
        MakeHq(m_AxeY[0][1].gameObject, FactionRED);
        MakeHq(m_AxeY[1][0].gameObject, FactionRED);
        MakeHq(m_AxeY[1][1].gameObject, FactionRED);
        //RightBottom
        MakeHq(m_AxeY[m_nbrYTerritories - 1][m_nbrXTerritories - 1].gameObject, FactionBLUE);
        MakeHq(m_AxeY[m_nbrYTerritories - 1][m_nbrXTerritories - 2].gameObject, FactionBLUE);
        MakeHq(m_AxeY[m_nbrYTerritories - 2][m_nbrXTerritories - 1].gameObject, FactionBLUE);
        MakeHq(m_AxeY[m_nbrYTerritories - 2][m_nbrXTerritories - 2].gameObject, FactionBLUE);
        //LeftTop
        MakeHq(m_AxeY[m_nbrYTerritories - 1][0].gameObject, FactionYELLOW);
        MakeHq(m_AxeY[m_nbrYTerritories - 1][1].gameObject, FactionYELLOW);
        MakeHq(m_AxeY[m_nbrYTerritories - 2][0].gameObject, FactionYELLOW);
        MakeHq(m_AxeY[m_nbrYTerritories - 2][1].gameObject, FactionYELLOW);
        //RightTop
        MakeHq(m_AxeY[0][m_nbrXTerritories - 1].gameObject, FactionGREEN);
        MakeHq(m_AxeY[0][m_nbrXTerritories - 2].gameObject, FactionGREEN);
        MakeHq(m_AxeY[1][m_nbrXTerritories - 1].gameObject, FactionGREEN);
        MakeHq(m_AxeY[1][m_nbrXTerritories - 2].gameObject, FactionGREEN);

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
    public void RePopSpecial()
    {
        bool FoundRightPlace=false;
        
        while(!FoundRightPlace)
        {
            int x = Random.Range(0, m_nbrXTerritories - 1);
            int y = Random.Range(0, m_nbrYTerritories - 1);
            if (!m_AxeY[y][x].GetComponent<Territory>().IsHQ)
            {
                if (!m_AxeY[y][x].GetComponent<Territory>().IsCenter)
                {
                    if (m_AxeY[y][x].GetComponent<Territory>().GetPlayerNumOnTerritory() == 0)
                    {
                        if (!m_AxeY[y][x].GetComponent<Territory>().HasSpecial)
                        {
                            FoundRightPlace = true;
                            m_AxeY[y][x].AddComponent<Special>();
                            int B = Random.Range(1, 7);//random range takes argument 1 inclusive argument 2 exclusive
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
