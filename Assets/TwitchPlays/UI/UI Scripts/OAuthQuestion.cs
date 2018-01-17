using UnityEngine;
using System.Collections;

public class OAuthQuestion : MonoBehaviour {

	public void Click () 
	{
		Debug.Log("Clicked");
		Application.OpenURL("http://www.twitchapps.com/tmi");
	}

}
