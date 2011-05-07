using NHibernate;

namespace SocialFreeks.Repository.Core
{
    public abstract class BaseRepository
    {
        private readonly ISession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        protected BaseRepository()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="session">
        /// A <see cref="ISession"/> for underlying <c>ISession</c> management.
        /// </param>
        protected BaseRepository(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        protected ISession Session
        {
            get
            {
                if (_session == null)
                {
                    return SessionManager.Current.GetCurrentSession();
                }

                return _session;
            }
        }
    }
}
