using System;

namespace orchestration.service.core.Common
{
    public interface IMessageBus: IDisposable
    {
         void Listen(string channelName);
    }
}