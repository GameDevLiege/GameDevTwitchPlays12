public interface ICommand
{
    string response { get; }
    bool feedbackUser { get; }
}