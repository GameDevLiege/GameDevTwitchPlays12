using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
public class AutoConnectTwitchIRC : MonoBehaviour {
    public string _userName;
    public string _oAuth;

    private TwitchIRC _irc;
	// Use this for initialization
	void Start () {

        Debug.Log("Hey mon ami", this.gameObject);
        PlayerPrefs.SetString("user", _userName);
        PlayerPrefs.SetString("oauth", _oAuth);
        PlayerPrefs.Save();

        _irc = GetComponent<TwitchIRC>();
        _irc.Login(_userName, _oAuth);
    }

    public static void GetOAuthFromWeb(string url) {
        Application.OpenURL(url);
    }

}
