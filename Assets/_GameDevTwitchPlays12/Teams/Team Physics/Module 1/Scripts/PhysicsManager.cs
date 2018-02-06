using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerManager), typeof(TerritoryManager), typeof(FactionManager))]
[RequireComponent(typeof(PlayerAction))]
public class PhysicsManager : MonoBehaviour {
    private TerritoryManager territoryManager;
    private PlayerManager playerManager;
    private PlayerAction playerAction;
    private FactionManager factionManager;

    private void Awake()
    {
        territoryManager = gameObject.GetComponent<TerritoryManager>();
        playerManager = gameObject.GetComponent<PlayerManager>();
        playerAction = gameObject.GetComponent<PlayerAction>();
        factionManager= gameObject.GetComponent<FactionManager>();
        playerAction.m_territoryManager = territoryManager;

    }


    // Use this for initialization
    void Start () {
        StartGame();
        
        GetCommandFromPlayer("Oho1", "");
        GetCommandFromPlayer("Oho2", "");
        GetCommandFromPlayer("Oho1", "DOWN");
        GetCommandFromPlayer("Oho1", "DOWN");
        //  GetCommandFromPlayer("Oho", "RIGHT");
        GetCommandFromPlayer("Oho2", "RIGHT");
        GetCommandFromPlayer("Oho2", "RIGHT");
        //GetCommandFromPlayer("Oho2", "LEFT");
        //GetCommandFromPlayer("Oho2", "LEFT");
        //  GetCommandFromPlayer("Oho", "LEFT");
        //  GetCommandFromPlayer("Oho", "LEFT");
        //   GetCommandFromPlayer("Oho", "LEFT");
        /*GetCommandFromPlayer("Oho2", "");
        GetCommandFromPlayer("Oho3", "");
        GetCommandFromPlayer("Oho4", "");
        GetCommandFromPlayer("Oho5", "");
        GetCommandFromPlayer("Oho6", "");
        GetCommandFromPlayer("Oho7", "");*/

        Debug.Log(FactionManager.RED.ListPlayer.Count);
        Debug.Log(FactionManager.BLUE.ListPlayer.Count);
        Debug.Log(FactionManager.GREEN.ListPlayer.Count);
        Debug.Log(FactionManager.YELLOW.ListPlayer.Count);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        territoryManager.GameStart();
    }

    public void CreatePlayer(string playerName)
    {
        playerManager.CreatePlayer(name);
    }

    public void GetCommandFromPlayer(string name, string command)    
    {
        var p = playerManager.GetPlayer(name);
        if (p == null)
        {
            p=playerManager.CreatePlayer(name);
            playerAction.DoAction(command, p);
        }
        else
        {
            playerAction.DoAction(command, p);
        }
    }
}
