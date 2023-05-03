using gRPC.Chat.Server.Services;

namespace Company.WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddGrpc(options =>
            {
            options.EnableDetailedErrors = true;
            });
            builder.Services.AddSingleton<ServerGrpcSubscribers>();

            var app = builder.Build();

            app.MapGrpcService<MessagingService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}