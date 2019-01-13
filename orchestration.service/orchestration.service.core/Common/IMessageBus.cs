namespace orchestration.service.core.Common
{
    public interface IMessageBus
    {
         void Listen(string channelName);
    }
}