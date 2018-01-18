using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent (typeof(TwitchIRC))]
public class PlayCommands : MonoBehaviour 
{
	[Tooltip("List of available twitch commands")]
	public List<TwitchCommand> Commands;
	[Tooltip("Optional delimiter for command options\n   ie. \'vote: 1\', delimiter would be \':\'")]
	public string delimiter;

	TwitchIRC twitch;

	void Start()
	{
		twitch = GetComponent<TwitchIRC>();

		twitch.messageRecievedEvent.AddListener(getCommand);
	}

	void getCommand(string str)
	{
		Debug.Log(str);
		//Remove non-command parts of message (like username)
		int msgIndex = str.IndexOf("PRIVMSG #");
        str = str.Substring(msgIndex + twitch.nickName.Length + 11);
		//Allow non delimited commands using the entire string (ie 'A' for A-button instead of 'button: A')
		string cmd = str;
		if(delimiter.Length > 0 && str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).Length > 1)
		{
			string[] blocks = str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			cmd = blocks[0];
			str = "";
			for(int i=1; i<blocks.Length; i++)
			{
				str += blocks[i];
			}
		}
		Debug.Log("Got Command: " + cmd);
		foreach(var v in Commands)
		{
			
			if(cmd.Trim().ToLower().Equals(v.commandKey.Trim().ToLower()))
			{
				v.onCommand.Invoke(str);
			}
		}
	}
}

[System.Serializable]
public class TwitchCommand
{
	[Tooltip("Name of Command, not relevant to code")]
	public string name;
	[Tooltip("string key for command\n   ie \'vote\', \'A\', \'up\'")]
	public string commandKey;
	[Tooltip("Method to call, will pass command options as a string")]
	[SerializeField]
 	public stringEvent onCommand;
}

[System.Serializable]
public class stringEvent : UnityEvent<string>{}
