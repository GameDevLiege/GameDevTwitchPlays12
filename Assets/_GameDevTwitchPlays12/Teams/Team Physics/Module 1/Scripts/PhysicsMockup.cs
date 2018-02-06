using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMockup : MonoBehaviour {
    public List<string> commandList;
    public List<string> userList;
    public PhysicsManager physicsManager;
    private bool launchInputDone=true;
    // Use this for initialization
    private void Awake()
    {
        physicsManager = GetComponent<PhysicsManager>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (launchInputDone && commandList.Count!=0 && userList.Count!=0 )
        {
            StartCoroutine(LaunchInput());
        }
	}
    IEnumerator LaunchInput()
    {

        yield return new WaitForSeconds(0.5f);
        int rangeCommand=Random.Range(0, commandList.Count);
        int rangeUser = Random.Range(0, userList.Count);
        physicsManager.SetCommandFromPlayer(userList[rangeUser],commandList[rangeCommand]);
    }
}
