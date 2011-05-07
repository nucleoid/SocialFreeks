using System;
using System.Collections.Generic;
using NHibernate;

namespace SocialFreeks.Repository.Core
{
    /// <summary>
    /// A generic repository class, which when inherited provides type-safe CRUD and find operations.
    /// </summary>
    public class WritableRepository<T> : ReadableRepository<T>, IWritableRepository<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WritableRepository&lt;T&gt;"/> class.
        /// </summary>
        public WritableRepository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WritableRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="session">
        /// A <see cref="ISession"/> for underlying <c>ISession</c> management.
        /// </param>
        public WritableRepository(ISession session)
            : base(session)
        {
        }

        /// <summary>
        /// Saves a specified entity.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <remarks>
        /// This method will do an insert or an update depending on the state of the entity.
        /// </remarks>
        public virtual void Save(T entity)
        {
            EnsureArgumentNotNull(entity);
            Transact(() => Session.SaveOrUpdate(entity));
        }

        /// <summary>
        /// Saves the specified entities.
        /// </summary>
        /// <param name="entities">The entities to be saved.</param>
        /// <remarks>
        /// This method will do an insert or an update depending on the state of each individual entity.
        /// </remarks>
        public void Save(IEnumerable<T> entities)
        {
            EnsureArgumentNotNull(entities);
            Transact(() =>
            {
                foreach (var entity in entities)
                {
                    Session.SaveOrUpdate(entity);
                }
            });
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public virtual void Delete(T entity)
        {
            EnsureArgumentNotNull(entity);
            Transact(() => Session.Delete(entity));
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities to be deleted.</param>
        public void Delete(IEnumerable<T> entities)
        {
            EnsureArgumentNotNull(entities);
            Transact(() =>
            {
                foreach (var entity in entities)
                {
                    Session.Delete(entity);
                }
            });
        }

        /// <summary>
        /// Factories a IDbTransaction based on the underlying connection. 
        /// The implementor is responsible for committing and disposing of this object.
        /// </summary>
        /// <returns>IDbTransaction</returns>
        public virtual ITransaction BeginTransaction()
        {
            return new Transaction(Session.BeginTransaction());
        }

        /// <summary>
        /// Flushes the current session associated with the repository
        /// </summary>
        public virtual void Flush()
        {
            Session.Flush();
        }

        private void EnsureArgumentNotNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
        }

        private TResult Transact<TResult>(Func<TResult> func)
        {
            if (!Session.Transaction.IsActive)
            {
                // Wrap in a transaction
                TResult result;

                using (var tx = Session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }

                return result;
            }

            // Don't wrap
            return func.Invoke();
        }

        private void Transact(Action action)
        {
            Transact(() =>
            {
                action.Invoke();
                return false;
            });
        }
    }
}
