using System;
using NHibernate;

namespace SocialFreeks.Repository.Core
{
    public interface ISessionManager : IDisposable
    {
        void Bind();
        void Unbind();
        bool HasBind();
        ISession GetCurrentSession();
    }
}
