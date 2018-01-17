public interface IMessage
{
    IPlayer author { get; set; }
    string message { get; set; }
}