using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
public class AutoStartTwitchIRC : MonoBehaviour {
    public string _userName;
    public string _oAuth;

    private TwitchIRC _irc;
	// Use this for initialization
	void Start () {

        PlayerPrefs.SetString("user", _userName);
        PlayerPrefs.SetString("oauth", _oAuth);
        PlayerPrefs.Save();

        _irc = GetComponent<TwitchIRC>();
        //_irc.Connected += DansTaGueuleLeChat;
        _irc.Login(_userName, _oAuth);

        //_irc.SendCommand("JOIN #" + _userName);
    }

    private void DansTaGueuleLeChat()
    {
        Debug.Log("Miaouw");
    }
}
