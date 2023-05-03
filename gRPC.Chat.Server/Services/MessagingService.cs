using gRPC.Chat.Server;
using gRPC.Chat.Server.Models;
using Grpc.Core;

namespace gRPC.Chat.Server.Services
{
    public class MessagingService : Messaging.MessagingBase
    {
        private readonly ILogger<MessagingService> _logger;
        private readonly ServerGrpcSubscribers _serverGrpcSubscribers;
        public MessagingService(ILogger<MessagingService> logger, ServerGrpcSubscribers serverGrpcSubscribers)
        {
            _logger = logger;
            _serverGrpcSubscribers = serverGrpcSubscribers;
        }

        public override async Task SendMessage(IAsyncStreamReader<MyMessage> requestStream, IServerStreamWriter<MyMessage> responseStream, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            _logger.LogInformation($"Connection id: {httpContext.Connection.Id}");
            if (!await requestStream.MoveNext())
                return;
            var user = requestStream.Current.Name;
            _logger.LogInformation($"{user} connected");
            var subscriber = new SubscriberModel(responseStream, user);
            _serverGrpcSubscribers.AddSubscriber(subscriber);
            do
            {
                await _serverGrpcSubscribers.BroadcastMessageAsync(requestStream.Current);
            } while (await requestStream.MoveNext());

            _serverGrpcSubscribers.RemoveSubscriber(subscriber);
            _logger.LogInformation($"{user} disconnected");
        }
    }
}