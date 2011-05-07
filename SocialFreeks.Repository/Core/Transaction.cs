using System;

namespace SocialFreeks.Repository.Core
{
    internal class Transaction : ITransaction
    {
        private readonly NHibernate.ITransaction _tx;
        private bool isDisposed;
        private object thisLock;

        internal Transaction(NHibernate.ITransaction tx)
        {
            _tx = tx;
            thisLock = new object();
        }

        public void Commit()
        {
            _tx.Commit();
        }

        public void Rollback()
        {
            _tx.Rollback();
        }

        ~Transaction()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            lock (thisLock)
            {
                if (isDisposed == false)
                {
                    try
                    {
                        _tx.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        isDisposed = true;

                        if (disposing)
                            GC.SuppressFinalize(this);
                    }
                }
            }
        }
    }
}
