using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayersStateManager))]
public class PlayerStateAfkChecker : MonoBehaviour {

    [SerializeField]
    private PlayersStateManager _playerStateManager;


    void Reset()
    {

        _playerStateManager = GetComponent<PlayersStateManager>();
    }
    void OnValide()
    {
        _playerStateManager = GetComponent<PlayersStateManager>();
    }

    [SerializeField]
    [Tooltip("Time allow to a player before being kick for inactivity")]
    private float _timeToBeKickForAfk = 180f;
    [SerializeField]
    [Tooltip("Time before each check of afk state")]
    private float _checkDelayDuration = 2f;


    void Start() {

        InvokeRepeating("CheckForAfkPlayer", 0, _checkDelayDuration);
    }
    void CheckForAfkPlayer() {
      List<PlayerState> players =  _playerStateManager.GetPlayersList();
        for (int i = 0; i < players.Count; i++)
        {
            PlayerState player = players[i];
            if (player.GetTimeSinceLastAction() > _timeToBeKickForAfk)
                _playerStateManager.DisconnectPlayer(player.GetUserName());
        }
    }





}
