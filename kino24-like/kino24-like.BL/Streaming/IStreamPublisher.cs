using kino24_like.BL.Models;

namespace kino24_like.BL.Streaming
{
    public interface IStreamPublisher
    {
        Task PublishAsync<T>(T comand, User user, string payload) where T : class;
    }
}
