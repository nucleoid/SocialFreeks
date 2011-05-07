using System;
using NHibernate;
using NHibernate.Context;

namespace SocialFreeks.Repository.Core
{
    public class SessionManager : ISessionManager
    {
        private readonly ISessionFactory _sessionFactory;
        private bool _isDisposed;
        private readonly object _thisLock;

        public SessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _thisLock = new object();
        }

        public static ISessionManager Current { get; set; }

        public bool HasBind()
        {
            return CurrentSessionContext.HasBind(_sessionFactory);
        }

        public void Bind()
        {
            if (HasBind())
            {
                throw new Exception("The current session context already is bound");
            }

            var session = _sessionFactory.OpenSession();

            CurrentSessionContext.Bind(session);
        }

        public void Unbind()
        {
            var session = CurrentSessionContext.Unbind(_sessionFactory);

            if (session != null)
            {
                session.Dispose();
            }
        }

        public ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        ~SessionManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            lock (_thisLock)
            {
                if (_isDisposed == false)
                {
                    try
                    {
                        Unbind();
                        _sessionFactory.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        _isDisposed = true;

                        if (disposing)
                            GC.SuppressFinalize(this);
                    }
                }
            }
        }
    }
}
