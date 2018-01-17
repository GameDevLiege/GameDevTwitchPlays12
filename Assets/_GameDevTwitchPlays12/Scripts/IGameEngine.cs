using System.Collections.Generic;

public interface IGameEngine
{
    void Do(List<ICommand> _commands);
    void GenerateMap();
    void Do(ICommand command);
}