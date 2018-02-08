using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TwitchLogin : MonoBehaviour {

	public InputField user;
	public InputField oauth;

    public TwitchIRC irc;

	bool connected;

	public GameObject LoginPanel;
    public GameObject[] _activateWhenLogin;



	IEnumerator Start()
	{
		yield return true;
		user.text = PlayerPrefs.GetString("user");
        oauth.text = PlayerPrefs.GetString("oauth");
        if(irc==null)
            irc = FindObjectOfType<TwitchIRC>();
        if(irc!=null)
            irc.Connected = Connected;

    }

	public void Submit ()
	{
		if(irc == null)
			Debug.LogError("No IRC client Found, make sure the \'TwitchPlays Client\' prefab is in the scene!");
		else
		{
			irc.Login(user.text, oauth.text);
			StopCoroutine("reconnect");
			StartCoroutine("reconnect");
		}
	}

	void Connected()
	{
		connected = true;
		Debug.Log("Connected to Chat");
	}

    void OnDestroy() {

        SaveInformation();
    }
    void OnDisable() {
        SaveInformation();
    }
    void SaveInformation()
    {
        PlayerPrefs.SetString("user", user.text);
        if (!string.IsNullOrEmpty(oauth.text))
            PlayerPrefs.SetString("oauth", oauth.text);
        PlayerPrefs.Save();
    }

    void Update()
	{
		if(connected)
        {
            PlayerPrefs.SetString("user", user.text);
            if (!string.IsNullOrEmpty(oauth.text)) 
            PlayerPrefs.SetString("oauth", oauth.text);
            PlayerPrefs.Save();
			if(LoginPanel != null)
				LoginPanel.SetActive(false);
            for (int i = 0; i < _activateWhenLogin.Length; i++)
            {
                if (_activateWhenLogin[i] != null)
                    _activateWhenLogin[i].SetActive(true);
            }
		}
			
	}

	public void Disconnect()
	{
		 connected = false;
	}

	IEnumerator reconnect()
	{
		yield return new WaitForSeconds(5.0f);
		if(!connected)
		{
			Debug.Log("Failed to connect");
		}
	}

}
