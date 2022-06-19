using kino24_user.BL.Interfaces.UniqueId;

namespace kino24_user.BL.Services.UniqueId
{
    public class UniqueIdService : IUniqueIdService
    {
        public Guid GetUniqueId()
        {
            return Guid.NewGuid();
        }
    }
}
