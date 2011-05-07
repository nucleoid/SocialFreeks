using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace SocialFreeks.Repository.Core
{
    public class ReadableRepository<T> : BaseRepository, IReadableRepository<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableRepository&lt;T&gt;"/> class.
        /// </summary>
        public ReadableRepository()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="session">
        /// A <see cref="ISession"/> for underlying <c>ISession</c> management
        /// </param>
        public ReadableRepository(ISession session)
            : base(session)
        {

        }

        /// <summary>
        /// Returns a specified entity based on the passed id
        /// </summary>
        /// <returns>The found entity, otherwise null</returns>
        public virtual T Get(object id)
        {
            return Session.Get<T>(id);
        }

        /// <summary>
        /// Returns an IQueryable representing all the instances in the repository
        /// </summary>
        /// <returns>An IQueryable</returns>
        public virtual IQueryable<T> All()
        {
            return Session.Linq<T>();
        }

        /// <summary>
        /// Refreshes an entity instance with the current database values.
        /// </summary>
        /// <param name="entity">The entity instance to be refreshed</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed instance is null.
        /// </exception>
        public virtual void Refresh(T entity)
        {
            EnsureArgumentNotNull(entity);
            Session.Refresh(entity);
        }

        /// <summary>
        /// Refreshes a collection of entity instance with the current database values.
        /// </summary>
        /// <param name="entities">The collection of entity instances to be refreshed</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed collection is null.
        /// </exception>
        public void Refresh(IEnumerable<T> entities)
        {
            EnsureArgumentNotNull(entities);

            foreach (T entity in entities)
            {
                Refresh(entity);
            }
        }

        /// <summary>
        /// Evicts the specified entity from the NHibernate cache.
        /// </summary>
        /// <param name="entity">The entity instance to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed instance is null.
        /// </exception>
        public virtual void Evict(T entity)
        {
            EnsureArgumentNotNull(entity);
            Session.Evict(entity);
        }

        /// <summary>
        /// Evicts the specified entities from the NHibernate cache.
        /// </summary>
        /// <param name="entities">The entity instances to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed collection is null.
        /// </exception>
        public void Evict(IEnumerable<T> entities)
        {
            EnsureArgumentNotNull(entities);

            foreach (T entity in entities)
            {
                Evict(entity);
            }
        }

        private void EnsureArgumentNotNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
        }
    }
}
