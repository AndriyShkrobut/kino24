using kino24_like.BL.Interfaces;

namespace kino24_like.BL.Services
{
    public class UniqueIdService : IUniqueIdService
    {
        public Guid GetUniqueId()
        {
            return Guid.NewGuid();
        }
    }
}
