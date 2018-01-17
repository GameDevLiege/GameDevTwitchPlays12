using System.Collections.Generic;

public interface ITwitchMessageFetcher
{
    List<string> GetRawMessages();
    List<IMessage> GetMessages();
}
