using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryManager  : MonoBehaviour
{

    #region Public Members
    public bool m_debug = true;

    public int m_sizeOfDiceItem = 5;
    public int m_nbrXTerritories =33;
    public int m_nbrYTerritories =33;
    public GameObject m_territoryPrefab;
    public int numberOfitems =100;
    public int m_territoryInCentralZone=0;
    public int m_headQuarter=0;
    public Territory[,] m_battleField;
    private List<Territory> eligibleTerritoryItem=new List<Territory>();

    private bool isPlaying;
    #endregion


    #region Public Void





    #endregion


    void Awake()
    {
        Faction.RED = new Faction();
        Faction.BLUE = new Faction();
        Faction.GREEN = new Faction();
        Faction.YELLOW = new Faction();
        GameStart();

        
    }



   
    /*
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
                Player playerC = m_listPlayer[i];
                _playerInfo.num = m_listPlayer[i].NumPlayer;
                _playerInfo.name = m_listPlayer[i].Name;
                _playerInfo.level = m_listPlayer[i].Level;
                _playerInfo.gold = m_listPlayer[i].Gold;
                _playerInfo.faction = m_listPlayer[i].PlayerFaction.FactionColor.ToString().ToUpper();
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
       */


        #region System
        void Start () 
        {

        }



        void Update()
        {
            //testing only !!
            if (Input.GetButtonDown("Fire1"))
            {
              //  m_listPlayer[0].GetComponent<PlayerAction>().Move("UP");

            }
            if (Input.GetButtonDown("Fire2"))
            {
              //  m_listPlayer[0].GetComponent<PlayerAction>().Move("RIGHT");
            }
            //--------------

/*
            foreach (PlayerInfo _playerInfo in m_listPlayerInfo)
            {
                //_placementPlayer(_playerInfo.faction, _playerInfo.posOnScreen, _playerInfo.num, _playerInfo.name, _playerInfo.level, _playerInfo.gold);
            }*/
        }

        #endregion
        
    #region Private Void
    public void GameStart()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            InitializeBoard();
        }

        //List<string> DebugList = new List<string>();
        //for (int i = 0; i < 20; i++)
        //{
        //    DebugList.Add("hoho" + i);
        //}

        //AssignFactionToPlayers(DebugList);
    }
    private void InitializeBoard()
    {
        // InstanciateTerritories();
        CreateBattleField();
        PlaceCenterZone();
        //CreateFactions();
        PlaceFactionHQ();
        InitializeTerritorryItems();
       // PlaceItems();
    }

    private void CreateBattleField() {
        
        Vector3 positionOfCell = new Vector3();
        m_battleField = new Territory[m_nbrXTerritories,m_nbrYTerritories];

        for (int x = 0; x < m_battleField.GetLength(0); x++) {

            for(int y=0;y < m_battleField.GetLength(1); y++)
            {
                
                positionOfCell = new Vector3(x * 1f, y * 1f, 0);
                GameObject territoryPrefab = Instantiate(m_territoryPrefab, positionOfCell, Quaternion.identity, transform);
                territoryPrefab.name = "y=" + positionOfCell.y + "x=" + positionOfCell.x;
                territoryPrefab.GetComponent<Territory>().Manager = this;
                m_battleField[x,y] = territoryPrefab.GetComponent<Territory>();
                m_battleField[x, y].TerritoryID = "x"+x+"y"+y;
               // Debug.Log(m_battleField[x, y].TerritoryID);

            }

        }

    }
    private void PlaceCenterZone()
    {
        int xTrueCenter = (((m_battleField.GetLength(0)) - 1) / 2);
        int yTrueCenter = (((m_battleField.GetLength(1)) - 1) / 2);
        for (int y = yTrueCenter-1; y <= yTrueCenter +1; y++)
        {
            for (int x = xTrueCenter - 1; x <= xTrueCenter + 1; x++)
            {
               // Debug.Log(m_battleField[x, y]);

                m_battleField[x,y].GetComponent<Territory>().IsCenter = true;
                m_territoryInCentralZone++;
                m_battleField[x, y].GetComponent<Territory>().ColorChange(Color.red);
            }
        }
    }
    private Faction AddFaction(Territory territory, Color color)
    {
        
        territory.TerritoryGameObject.AddComponent<Faction>();
        Faction faction=territory.TerritoryTransform.GetComponent<Faction>();
        color.a = 100f;
        faction.FactionColor = color;
        faction.RespawnPosition = territory;

        return faction;
        //FactionBLUE = CreateAFaction(Color.blue, new Vector3(m_nbrYTerritories - 1, m_nbrXTerritories - 1, 0f));
        //FactionGREEN = CreateAFaction(Color.green, new Vector3(m_nbrYTerritories - 1, 0f, 0f));
        //FactionYELLOW = CreateAFaction(Color.yellow, new Vector3(0f, m_nbrXTerritories - 1, 0f));
    }
    private void PlaceFactionHQ()
    {
        //LeftBottom
        Faction.RED = AddFaction(m_battleField[0, 0],Color.red);
        MakeHq(m_battleField[0, 0], Faction.RED);
        MakeHq(m_battleField[0, 1], Faction.RED);
        MakeHq(m_battleField[1, 0], Faction.RED);
        MakeHq(m_battleField[1, 1], Faction.RED);
        //RightBottom
        Faction.BLUE= AddFaction(m_battleField[0, m_battleField.GetLength(1)-1], Color.blue);
        MakeHq(m_battleField[0, m_battleField.GetLength(1)-1], Faction.BLUE);
        MakeHq(m_battleField[1, m_battleField.GetLength(1) - 1], Faction.BLUE);
        MakeHq(m_battleField[1, m_battleField.GetLength(1) - 2], Faction.BLUE);
        MakeHq(m_battleField[0, m_battleField.GetLength(1) - 2], Faction.BLUE);

        //LeftTop
        Faction.GREEN= AddFaction(m_battleField[m_battleField.GetLength(0) - 1, 0], Color.green);
        MakeHq(m_battleField[m_battleField.GetLength(0)-1, 0], Faction.GREEN);
        MakeHq(m_battleField[m_battleField.GetLength(0)-1, 1], Faction.GREEN);
        MakeHq(m_battleField[m_battleField.GetLength(0)-2, 1], Faction.GREEN);
        MakeHq(m_battleField[m_battleField.GetLength(0)-2, 0], Faction.GREEN);

        //RightTop
        Faction.YELLOW = AddFaction(m_battleField[m_battleField.GetLength(1) - 1, m_battleField.GetLength(1) - 1], Color.yellow);
        MakeHq(m_battleField[m_battleField.GetLength(0) - 1, m_battleField.GetLength(1) - 1], Faction.YELLOW);
        MakeHq(m_battleField[m_battleField.GetLength(0) - 1, m_battleField.GetLength(1) - 2], Faction.YELLOW);
        MakeHq(m_battleField[m_battleField.GetLength(0) - 2, m_battleField.GetLength(1) - 1], Faction.YELLOW);
        MakeHq(m_battleField[m_battleField.GetLength(0) - 2, m_battleField.GetLength(1) - 2], Faction.YELLOW);

    }
    public void MakeHq(Territory HQTerritory, Faction faction)
    {
        HQTerritory.IsHQ = true;
        HQTerritory.TerritoryMeshRenderer.material.color = faction.FactionColor;
        HQTerritory.CurrentColor = faction.FactionColor;
        m_headQuarter++;
    }

    public void InitializeTerritorryItems()
    {
        int randomCenterZone = Random.Range(1, m_territoryInCentralZone+1);//decides which central territory gets the glasses

        int centerCount = 0;
        int itemCount = Item.ItemTypeLength();
        bool hasGlasses = false;
        int i = 1;
        foreach (Territory t in m_battleField) {
           
            if (!hasGlasses)
            {
                if (t.IsCenter && !t.HasItem && centerCount == randomCenterZone)
                {
                    t.HasItem = true;
                    Item item= t.TerritoryGameObject.AddComponent<Item>();
                    item.ItemType= Item.e_itemType.GLASSES;
                    item.ItemEffectType = Item.e_itemEffectType.INVENTORY;
                    t.TerritoryItem = item;
                    t.HasItem = true;
                    hasGlasses = true;
                    t.ColorChange(Color.grey);
                }
                else if (t.IsCenter)
                {
                    centerCount++;
                }
                
            }
            if (!t.IsCenter && !t.HasItem && !t.IsHQ)
                {
                    eligibleTerritoryItem.Add(t);
                    i++;
                }
            
        }
        InitializeItems();
    }
    private void InitializeItems()
    {
        //Voir comment améliorer le random
        
        if (m_debug)
        {
            Debug.Log("Total eligibleTerritory:"+eligibleTerritoryItem.Count);
        }

        for (int i = 1; i < numberOfitems; i++)
        {
            if (Random.Range(1, 4) == Random.Range(1, 4))
            {
                {
                    int intRndItem = Random.Range(1, Item.ItemTypeLength());
                    int intRndTerritory = Random.Range(0, eligibleTerritoryItem.Count);
                    if (m_debug)
                    {
                        Debug.Log("Rnd eligibleTerritory1------>" + intRndTerritory);
                        Debug.Log("Rnd item1------>" + intRndItem);
                        eligibleTerritoryItem[intRndTerritory].ColorChange(Color.magenta);
                    }
                    Item item = eligibleTerritoryItem[intRndTerritory].TerritoryGameObject.AddComponent<Item>();
                    item.ItemType = (Item.e_itemType)intRndItem;
                    eligibleTerritoryItem[intRndTerritory].TerritoryItem = item;
                    eligibleTerritoryItem.Remove(eligibleTerritoryItem[intRndTerritory]);

                }
            }
            else i--;
        }
       
    }
    public void RePopSpecial()
    {
        bool FoundRightPlace = false;

        while (!FoundRightPlace)
        {
            int x = Random.Range(0, m_nbrXTerritories - 1);
            int y = Random.Range(0, m_nbrYTerritories - 1);
            if (!m_battleField[y,x].GetComponent<Territory>().IsHQ)
            {
                if (!m_battleField[y,x].GetComponent<Territory>().IsCenter)
                {
                    if (m_battleField[y,x].GetComponent<Territory>().GetPlayerNumOnTerritory() == 0)
                    {
                        if (!m_battleField[y,x].GetComponent<Territory>().HasItem)
                        {
                            FoundRightPlace = true;
                            m_battleField[y,x].TerritoryGameObject.AddComponent<Item>();
                            int B = Random.Range(1, 7);//random range takes argument 1 inclusive argument 2 exclusive
                            switch (B)
                            {
                                case 1: m_battleField[y,x].GetComponent<Item>().ChooseTypeOfItem(Item.e_itemType.COINCHEST); break;
                                case 2: m_battleField[y,x].GetComponent<Item>().ChooseTypeOfItem(Item.e_itemType.PARCHEMENT); break;
                                case 3: m_battleField[y,x].GetComponent<Item>().ChooseTypeOfItem(Item.e_itemType.GRENADES); break;
                                case 4: m_battleField[y,x].GetComponent<Item>().ChooseTypeOfItem(Item.e_itemType.PEBBLE); break;
                                case 5: m_battleField[y,x].GetComponent<Item>().ChooseTypeOfItem(Item.e_itemType.SHOVEL); break;
                                case 6: m_battleField[y,x].GetComponent<Item>().ChooseTypeOfItem(Item.e_itemType.STRAIN); break;
                            }
                        }
                    }
                }
            }
        }
    }
        //lié à un evenement
        public void PopItem()
    {

    }
    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private List<GameObject> m_AxeX = new List<GameObject>();
    private List<List<GameObject>> m_AxeY = new List<List<GameObject>>();


    #endregion

}
