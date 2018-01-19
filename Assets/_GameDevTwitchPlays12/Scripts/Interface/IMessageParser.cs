using System;
using System.Collections.Generic;

namespace GameManager
{
    public interface ICommandManager
    {
        ICommand Parse(string _username, int _platform, string _message, long _timestamp);

        Dictionary<string, PlayerCTRL> userDataBase { get; }
    }
}