using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PhysicsManager))]
public class PhysicsMockup : MonoBehaviour {
    public List<string> commandList;
    public List<string> userList;
    public PhysicsManager physicsManager;
    private bool launchInputDone=true;
    public bool isRandom=false;
    public float timeRoutine=0.5f;
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
        launchInputDone = false;
        yield return new WaitForSeconds(timeRoutine);
        if (isRandom) {
            int rangeCommand = Random.Range(0, commandList.Count);
            int rangeUser = Random.Range(0, userList.Count);
            physicsManager.SetCommandFromPlayer(userList[rangeUser], commandList[rangeCommand]);
        }
        else
        {
            for(int i=0;i<userList.Count ;i++)
            {
                for (int j = 0; j < commandList.Count; j++)
                {
                    physicsManager.SetCommandFromPlayer(userList[i], commandList[j]);
                }
            }
        }
        
        launchInputDone = true;
    }
}
