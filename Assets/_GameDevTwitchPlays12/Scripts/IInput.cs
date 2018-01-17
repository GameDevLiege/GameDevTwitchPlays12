using System.Collections.Generic;
using DidzNeil.ChatAPI;

public interface IInput
{
    List<IMessage> GetInput();
}