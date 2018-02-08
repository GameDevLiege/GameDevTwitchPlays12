using System.Collections.Generic;

public interface IGameEngine
{
    void AssignFactionToPlayers(List<string> ListOfPlayerNames);
    void GetCommandFromPlayer(string PName, string Command);
}