using System;

namespace SocialFreeks.Repository.Core
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
