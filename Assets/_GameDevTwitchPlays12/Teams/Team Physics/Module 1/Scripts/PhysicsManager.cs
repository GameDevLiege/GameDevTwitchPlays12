using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerManager), typeof(TerritoryManager), typeof(FactionManager))]
public class PhysicsManager : MonoBehaviour {
    private TerritoryManager territoryManager;
    private PlayerManager playerManager;

    private FactionManager factionManager;

    private void Awake()
    {
        territoryManager = gameObject.GetComponent<TerritoryManager>();
        playerManager = gameObject.GetComponent<PlayerManager>();
        factionManager= gameObject.GetComponent<FactionManager>();
        playerManager.m_territoryManager = territoryManager;

    }


    // Use this for initialization
    void Start () {
        StartGame();
      
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

    public void SetCommandFromPlayer(string name, string command)    
    {
        var p = playerManager.GetPlayer(name);
        if (p == null)
        {
            p=playerManager.CreatePlayer(name);
            playerManager.DoAction(command, p);
        }
        else
        {
            playerManager.DoAction(command, p);
        }
    }
    
}
