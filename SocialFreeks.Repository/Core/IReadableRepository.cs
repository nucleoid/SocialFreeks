using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialFreeks.Repository.Core
{
    /// <summary>
    /// A generic repository interface, which when implemented provides read-only find operations.
    /// </summary>
    public interface IReadableRepository<T>
    {
        /// <summary>
        /// Returns a specified entity based on the passed id
        /// </summary>
        /// <returns>The found entity, otherwise null</returns>
        T Get(object id);

        /// <summary>
        /// Returns an IQueryable representing all the instances in the repository
        /// </summary>
        /// <returns>An IQueryable</returns>
        IQueryable<T> All();

        /// <summary>
        /// Evicts the specified entity from the NHibernate cache.
        /// </summary>
        /// <param name="entity">The entity instance to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed instance is null.
        /// </exception>
        void Evict(T entity);

        /// <summary>
        /// Evicts the specified entities from the NHibernate cache.
        /// </summary>
        /// <param name="entities">The entity instances to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed collection is null.
        /// </exception>
        void Evict(IEnumerable<T> entities);

        /// <summary>
        /// Refreshes an entity instance with the current database values.
        /// </summary>
        /// <param name="entity">The entity instance to be refreshed</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed instance is null.
        /// </exception>
        void Refresh(T entity);

        /// <summary>
        /// Refreshes a collection of entity instance with the current database values.
        /// </summary>
        /// <param name="entities">The collection of entity instances to be refreshed</param>
        /// <exception cref="ArgumentNullException">
        /// Occurs when the passed collection is null.
        /// </exception>
        void Refresh(IEnumerable<T> entities);
    }
}
