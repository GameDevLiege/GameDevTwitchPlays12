[System.Serializable]
public class FBMessage
{
    public string created_time;
    public User from;
    public string message;
    public string id;
}

[System.Serializable]
public class User
{
    public string name;
    public string id;
}