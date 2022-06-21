using kino24_like.BL.Interfaces;
using kino24_like.BL.Models;
using kino24_like.BL.Serialization;
using StackExchange.Redis;

namespace kino24_like.BL.Streaming
{
    public class StreamPublisher : IStreamPublisher
    {
        private readonly IDatabaseAsync _db;
        private readonly IUniqueIdService _uniqueIdService;
        private readonly ISerializer _serializer;

        public StreamPublisher(IConnectionMultiplexer connectionMultiplexer, IUniqueIdService uniqueIdService, ISerializer serializer)
        {
            _db = connectionMultiplexer.GetDatabase();
            _uniqueIdService = uniqueIdService;
            _serializer = serializer;
        }

        public async Task PublishAsync<T>(T comand, User user, string payload) where T : class
        {
            var values = new NameValueEntry[]
            {
                new NameValueEntry("id", _uniqueIdService.GetUniqueId().ToString()),
                new NameValueEntry("eventType", (comand as IActionType).GetType()),
                new NameValueEntry("timestamp", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")),
                new NameValueEntry("user", _serializer.Serialize(user)),
                new NameValueEntry("payload", payload)
            };

            await _db.StreamAddAsync("feedback-events-v1", values);
        }
    }
}
