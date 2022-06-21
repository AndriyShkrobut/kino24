using kino24_like.BL.Models;

namespace kino24_like.BL.Interfaces
{
    public interface IUserTokenService
    {
        User GetUserFromToken(string toket);
    }
}
