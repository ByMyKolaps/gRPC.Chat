using gRPC.Chat.Server.Models;
using System.Collections.Concurrent;

namespace gRPC.Chat.Server.Services
{
    public class ServerGrpcSubscribers
    {
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, SubscriberModel> Subscribers = new ConcurrentDictionary<string, SubscriberModel>();

        public ServerGrpcSubscribers(ILogger<ServerGrpcSubscribers> logger)
        {
            _logger = logger;
        }

        public void AddSubscriber(SubscriberModel subscriber)
        {
            bool isAdded = Subscribers.TryAdd(subscriber.Name, subscriber);
            _logger.LogInformation($"New subscriber added: {subscriber.Name}");
            if (!isAdded)
                _logger.LogInformation($"Could not add subscriber: {subscriber.Name}");
        }

        public void RemoveSubscriber(SubscriberModel subscriber)
        {
            try
            {
                Subscribers.TryRemove(subscriber.Name, out SubscriberModel item);
                _logger.LogInformation($"Force Remove: {item.Name} - no longer works");
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"Could not remove {subscriber.Name}");
            }
        }

        public async Task BroadcastMessageAsync(MyMessage message)
        {
            await BroadcastMessage(message);
        }

        private async Task BroadcastMessage(MyMessage message)
        {
            foreach(var subscriber in Subscribers.Values)
            {
                var item = await SendMessageToSubscriber(subscriber, message);
                if (item != null)
                {
                    RemoveSubscriber(item);
                }
            }
        }

        private async Task<SubscriberModel> SendMessageToSubscriber(SubscriberModel subscriber, MyMessage message)
        {
            try
            {
                _logger.LogInformation($"Broadcasting: {message.Name} - {message.Message}");
                await subscriber.Subscriber.WriteAsync(message);
                return null;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Could not send");
                return subscriber;
            }
        }
    }
}