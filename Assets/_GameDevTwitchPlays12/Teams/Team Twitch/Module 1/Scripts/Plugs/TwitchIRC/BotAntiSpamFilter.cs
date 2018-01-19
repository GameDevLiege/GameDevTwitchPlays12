using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotAntiSpamFilter : MonoBehaviour {

    public TwitchIRC _twitchIRC;
    public Queue<string> _messageToSend = new Queue<string>();
    public Queue<string> _messageToSendWithPriority = new Queue<string>();

    public float _timeBetweenMessage = 2f;
    public float _coolDown;

    public void Start() {
       // StartCoroutine(TDD());
    }

    public void Update() {

        if (_coolDown >= 0f) {

            _coolDown -= Time.deltaTime;

            
            if (_coolDown < 0f)
                _coolDown = 0f;
        }

        if (_coolDown <= 0f) {

            if (_messageToSendWithPriority.Count > 0)
            {
                SendMessageToIrc(ref _messageToSendWithPriority);
            }
            else if (_messageToSend.Count > 0)
            {
                SendMessageToIrc(ref _messageToSend);
            }
        }

    }


    public void SendMessageToIrc(ref Queue<string> mToRelease) {
        if (_coolDown > 0f)
            return;
        _coolDown = _timeBetweenMessage;
        
        string messageToSend = mToRelease.Dequeue();

       
        _twitchIRC.SendMsg(messageToSend);

    }

    public void SendMessageWithFilter(string message)
    {
        if (!string.IsNullOrEmpty(message))
            _messageToSend.Enqueue(message);

    }
    public void SendMessageWithPriorityFilter(string message)
    {
        if (!string.IsNullOrEmpty(message))
            _messageToSendWithPriority.Enqueue(message);

    }
    public void SendMessageWithNoProtection(string message)
    {
        _twitchIRC.SendMsg(message);


    }


    //IEnumerator TDD() {


    //    SendMessageWithNoProtection("/clear");
    //    SendMessageWithPriorityFilter("Hey mon ami");
    //    for (int i = 0; i < 20; i++)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        SendMessageWithFilter("M" + Random.Range(0, int.MaxValue));

    //    }
    //    for (int i = 0; i < 20; i++)
    //    {
    //        yield return new WaitForSeconds(5f);
    //        SendMessageWithPriorityFilter("I" + Random.Range(0, int.MaxValue));

    //    }
    //    SendMessageWithPriorityFilter("Tu aimes ça les patates");

    //}
}
