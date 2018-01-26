using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
public class AutoConnectTwitchIRC : MonoBehaviour
{
    public TwitchIRCCredentials Credentials
    {
        set { m_credentials = value; }
    }

	void Start () {
        if(m_credentials == null)
        {
            // There is probably a better way to handle this 
            // but this will do for now.
            //
            // It's better than nothing

            Debug.LogError(
                "Please create your Twitch IRC Credentials, fill it and link it to your AutoConnectTwitchIRC component in order to connect! - "+
                "To create your TwitchIRCCredentials, right click in your Project > Create > Twitch IRC Credentials  " + 
                "(MAKE SURE TO AUTO-IGNORE IT TO NOT COMMIT YOUR PRIVATE INFO)",
                this.gameObject
            );

            return;
        }

        //PlayerPrefs.SetString("user", m_credentials._userName);
        //PlayerPrefs.SetString("oauth", m_credentials._oAuth);
        //PlayerPrefs.Save();

        m_irc = GetComponent<TwitchIRC>();
        m_irc.Login(m_credentials._userName, m_credentials._oAuth);
    }

    public static void GetOAuthFromWeb(string url) {
        Application.OpenURL(url);
    }

    private TwitchIRC m_irc;

    [SerializeField]
    private TwitchIRCCredentials m_credentials;
}
