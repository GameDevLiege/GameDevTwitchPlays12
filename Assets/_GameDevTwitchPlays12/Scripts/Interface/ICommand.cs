public interface ICommand
{
    string response { get; }
    int numberOfIteration { get; }
    bool feedbackUser { get; }
}