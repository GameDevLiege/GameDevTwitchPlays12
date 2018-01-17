using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{

    public Color factionColor;
    public float nbterritory;
  
    public bool hasMiddle = false;
    //public list<player> playerlist;


    void StartCountDown()
    {

    }
    private void Start()
    {
        
    }
    void Update ()
    {
        //if (hasGlasses == true)
            Debug.Log("the faction blue,red, green ,yellow : to the Glass");

        if (hasMiddle == true)
            Debug.Log("the faction blue,red, green ,yellow : takes the middle ");
    }
   
}
