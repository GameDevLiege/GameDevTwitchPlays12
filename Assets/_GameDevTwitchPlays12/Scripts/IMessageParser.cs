namespace GameManager
{
    public interface ICommandManager
    {
        ICommand Parse(string _username, string _message, long _timestamp);
    }
}