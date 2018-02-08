using UnityEngine;
using System.Collections;
using System;

public class BotSendMessageControl :MonoBehaviour {

    public TwitchIRC _twitchIRC;
    public BotAntiSpamFilter _antiSpamFilter;


    public void Start() {
        if (_antiSpamFilter == null)
        {
            Debug.LogWarning("/!\\ There is not filter to prevent Anti-Spam /!\\", this.gameObject);
            Debug.Break();
        }

       // StartCoroutine(TDD());
       
    }

    //private IEnumerator TDD()
    //{

    //    //SendMessageToIRC("Activate Slow Mode");
    //    //SendCmd_ActivateSlowMode(true, 2,MessagePriority.Direct);
    //    SendCmd_LaunchCommercialTrailer(CommercialTimeType.T030);
    //    SendMessageToIRC("Commercial");
    //    yield return new WaitForSeconds(20f);

    //    yield break;
    //}

    public enum MessagePriority {Classic, Important, Direct}
    public void SendMessageToIRC(string message,MessagePriority priority = MessagePriority.Classic) {

        if (_antiSpamFilter)
        {
            switch (priority)
            {
                case MessagePriority.Classic:
                    _antiSpamFilter.SendMessageWithFilter(message);
                    break;
                case MessagePriority.Important:
                    _antiSpamFilter.SendMessageWithPriorityFilter(message);
                    break;
                case MessagePriority.Direct:
                    _antiSpamFilter.SendMessageWithNoProtection (message);
                    break;
                default:
                    break;
            }

        }
        else {
            _twitchIRC.SendMsg(message);
        }

    }


    public void SendCmd_ClearConsole(MessagePriority priority = MessagePriority.Direct)
    {
        SendMessageToIRC("/clear", priority);

    }


    public void SendCmd_ActivateChatForPremiumOnly(bool activatePremiumOnly, MessagePriority priority = MessagePriority.Direct)
    {

        if(activatePremiumOnly)
            SendMessageToIRC("/subscribers", priority);
        else
            SendMessageToIRC("/subscribersoff", priority);
    }

    public void SendCmd_ActivateSlowMode(bool activateSlowMode, int secondBetweenMessage = 5, MessagePriority priority = MessagePriority.Direct)
    {
        if (activateSlowMode)
            SendMessageToIRC("/slow "+ secondBetweenMessage, priority);
        else
            SendMessageToIRC("/slowoff", priority);

    }


    public void SendCmd_BanUser(string nameOfTheUser, MessagePriority priority = MessagePriority.Direct)
    {

        SendMessageToIRC("/ban " + nameOfTheUser, priority);
    }
    public void SendCmd_UnbanUser(string nameOfTheUser, MessagePriority priority = MessagePriority.Direct)
    {

        SendMessageToIRC("/unban " + nameOfTheUser, priority);
    }
    public void SendCmd_BanUserWithTimer(string nameOfTheUser,int secondesOfBan, MessagePriority priority = MessagePriority.Direct)
    {

        SendMessageToIRC("/timeout " + nameOfTheUser+" "+ secondesOfBan, priority);
    }

    public enum CommercialTimeType : int { T030=30, T060 = 60, T090 = 90, T120 = 120, T150 = 150, T180 = 180 }
    public void SendCmd_LaunchCommercialTrailer(CommercialTimeType commercialType = CommercialTimeType.T030, MessagePriority priority = MessagePriority.Direct) {
        
        SendMessageToIRC("/commercial " + (int) commercialType , priority );
        
    }

    public void SendCmd_OfferGodModeTo(string userId,bool giveThePower=true, MessagePriority priority = MessagePriority.Direct) {

        if (giveThePower)
            SendMessageToIRC("/mod " + userId, priority);
        else
            SendMessageToIRC("/unmod " + userId, priority);
    }

    public void Whisper(string userid, string messageToSend, MessagePriority priority = MessagePriority.Direct) {
        SendMessageToIRC("/w "+ userid + " "+messageToSend, priority);
    }
    public void ToTheAttentionOf(string userid, string messageToSend, MessagePriority priority = MessagePriority.Direct)
    {
        SendMessageToIRC("@" + userid + " " + messageToSend, priority);
    }

}
