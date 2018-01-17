using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayersStateManager : MonoBehaviour {

    public TwitchIRC _twitchIRC;

    public enum ConnectionState { Deconnected, Connected }


    public delegate void OnFullCommandeDetected(string fullCommande);
    public delegate void OnUserSendMessage(string userName, string message);
    public delegate void OnUserConnectionStateChange(string userName, ConnectionState connectionState);
   
    public OnFullCommandeDetected _onFullCommandeDetected;
    public OnUserSendMessage _onUserSendMessage;
    public OnUserConnectionStateChange _onConnectionStateChange;


    public Dictionary<string, PlayerState> _playersState = new Dictionary<string, PlayerState>();
    public List<PlayerState> _debugListPlayerRegistered = new List<PlayerState>();

    void Start()
    {
        if(_twitchIRC==null)
            _twitchIRC = GetComponent<TwitchIRC>();
        
        _twitchIRC.messageRecievedEvent.AddListener(ListenToCommand);
    }

    public List<PlayerState> GetPlayersList()
    {
        return _debugListPlayerRegistered;
    }


    // Update is called once per frame
    void ListenToCommand(string cmdStr) {

        Debug.Log("Full Message :" + cmdStr);
        int messageIndex = cmdStr.IndexOf("PRIVMSG #");
        if (messageIndex >= 0) {
            if(_onFullCommandeDetected!=null)
                _onFullCommandeDetected(cmdStr);
            string playerName = GetPlayerName(cmdStr);
            AddPlayerIfNotExisting(playerName);
            PlayerState player = GetPlayerIfExist(playerName);
            string cmd = GetCommandAsked(cmdStr);
            player.AddCommandToTheList(cmd);
            if (_onUserSendMessage != null)
                _onUserSendMessage(playerName, cmd);
        }

    }

    private string GetCommandAsked(string cmdStr)
    {
        string strTmp = cmdStr;

        //REGEX SHOULD BE BETTER OPTION
        int messageWithChanelIndex = strTmp.IndexOf("PRIVMSG #");
        if (messageWithChanelIndex < 0) return "";

        strTmp = strTmp.Substring(messageWithChanelIndex + 9);
        int messageIndex = strTmp.IndexOf(":");
        strTmp = strTmp.Substring(messageIndex + 1);

        return strTmp;
    }

    private void AddPlayerIfNotExisting(string playerName)
    {
        if (!_playersState.ContainsKey(playerName))
        {
            PlayerState playerState = new PlayerState(playerName);
            _playersState.Add(playerName, playerState);
            _debugListPlayerRegistered.Add(playerState);
            if (_onConnectionStateChange != null)
                _onConnectionStateChange(playerName, ConnectionState.Connected);
        }
    }


    public void DisconnectPlayer(string playerName)
    {
        if (_playersState.ContainsKey(playerName))
        {
            PlayerState playerState = _playersState[playerName];
            _playersState.Remove(playerName);
            _debugListPlayerRegistered.Remove(playerState);

            if (_onConnectionStateChange != null)
                _onConnectionStateChange(playerName, ConnectionState.Deconnected);
        }
    }
    private PlayerState GetPlayerIfExist(string playerName)
    {
        if (!_playersState.ContainsKey(playerName))
        {
            return null;
        }
        else return _playersState[playerName];
    }

    private string GetPlayerName(string cmdStr)
    {
        int indexStartName = cmdStr.IndexOf("!")+1;
        int indexEndName = cmdStr.IndexOf("@");
        return cmdStr.Substring(indexStartName, indexEndName - indexStartName);
    }

    
}
[System.Serializable]
public class PlayerState
{

    public const int MAX_RECORD_CMD = 10;
    public PlayerState(string idName)
    {
        _userName = idName;
    }

    [Header("Debug view (do not touch)")]
    [SerializeField]
    [Tooltip("The id name of the user")]
    private string _userName;

    public string GetUserName() { return _userName; }

    [SerializeField]
    [Tooltip("When was the last command")]
    private float _lastActionAppTime;

    [SerializeField]
    [Tooltip("List of command as FIFO")]
    //Should use Queue but use a list for serialization debug
    private List<string> _lastCommand = new List<string>();

    public void AddCommandToTheList(string cmd)
    {
        _lastActionAppTime = Time.time;
        _lastCommand.Add(cmd);
        while (_lastCommand.Count >= MAX_RECORD_CMD)
            _lastCommand.RemoveAt(0);
    }

    public float GetTimeSinceLastAction()
    {
        return Time.time - _lastActionAppTime;
    }
}