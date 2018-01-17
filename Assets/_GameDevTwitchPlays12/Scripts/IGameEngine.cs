using System.Collections.Generic;

public interface IGameEngine
{
    void SendInputs(List<ICommand> list);
}