using SocialFreeks.Entities;
using SocialFreeks.Repository.Core;

namespace SocialFreeks.Repository
{
    public interface IUserRepository : IWritableRepository<User>
    {
    }
}
