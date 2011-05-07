
using SocialFreeks.Entities;
using SocialFreeks.Repository.Core;

namespace SocialFreeks.Repository
{
    public class UserRepository : WritableRepository<User>, IUserRepository
    {
    }
}
