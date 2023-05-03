using Grpc.Core;

namespace gRPC.Chat.Server.Models
{
    public class SubscriberModel
    {
        public IServerStreamWriter<MyMessage> Subscriber { get; set; }
        public string Name { get; set; }

        public SubscriberModel(IServerStreamWriter<MyMessage> subscriber, string name)
        {
            Subscriber = subscriber;
            Name = name;
        }
    }
}
