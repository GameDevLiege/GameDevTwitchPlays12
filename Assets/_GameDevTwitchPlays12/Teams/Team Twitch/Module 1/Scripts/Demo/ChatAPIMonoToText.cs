using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DidzNeil.ChatAPI;

public class ChatAPIMonoToText : MonoBehaviour
{
    public Text m_debugText;

    public void DisplayGivenMessage(Message message)
    {
        m_debugText.text = message.GetUserName() +"/ "+ message.GetMessage();
    }
	
}
