using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DidzNeil.ChatAPI
{
    public class Mockup_GameToServer : MonoBehaviour
    {
   
        private void Start()
        {
            StartCoroutine(SendTimeToAllUsers());
        }

        private IEnumerator SendTimeToAllUsers()
        {


            while (true)
            {
                DateTime now = Message.CreateFromTimestamp();
                Message msg = new Message("Game Admin","Time: "+now.ToString("hh:mm:ss"), Message.GetCurrentTimeUTC(), Platform.Game);
                
                ChatAPI.SendMessageToEveryUsers(msg);

                yield return new WaitForSeconds(5f);
            }
        }

    }
}
