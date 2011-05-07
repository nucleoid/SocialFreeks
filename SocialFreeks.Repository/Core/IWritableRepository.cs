using System.Collections.Generic;
using NHibernate;

namespace SocialFreeks.Repository.Core
{
    /// <summary>
    /// A generic repository interface, which when implemented provides type-safe CRUD and find operations.
    /// </summary>
    public interface IWritableRepository<T> : IReadableRepository<T>
    {
        /// <summary>
        /// Saves a specified entity.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <remarks>
        /// This method will do an insert or an update depending on the state of the entity.
        /// </remarks>
        void Save(T entity);

        /// <summary>
        /// Saves the specified entities.
        /// </summary>
        /// <param name="entities">The entities to be saved.</param>
        /// <remarks>
        /// This method will do an insert or an update depending on the state of each individual entity.
        /// </remarks>
        void Save(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities to be deleted.</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Factories and opens a new transaction.  Commit and disposal is the implementors responsibility.
        /// </summary>
        /// <returns>ITransaction</returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// Flushes the current session associated with the repository
        /// </summary>
        void Flush();
    }
}
