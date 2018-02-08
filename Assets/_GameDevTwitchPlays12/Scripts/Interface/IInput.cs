using System.Collections.Generic;
using DidzNeil.ChatAPI;

public interface IInput
{
    void SendFeedback(ICommand command);
}