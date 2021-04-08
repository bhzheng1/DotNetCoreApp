using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Persistence;

namespace NSBus
{
    public class UnitOfWorkProvider<T> where T : DbContext
    {
        private readonly Func<DbConnection, T> _contextFactory;
        private T _context;

        public UnitOfWorkProvider(Func<DbConnection, T> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public T GetDataContext(SynchronizedStorageSession storageSession)
        {
            if (_context == null)
            {
                _context = _contextFactory(storageSession.SqlPersistenceSession().Connection);
                _context.Database.UseTransaction(storageSession.SqlPersistenceSession().Transaction);
                storageSession.SqlPersistenceSession().OnSaveChanges(_ => _context.SaveChangesAsync());
            }

            return _context;
        }
    }
}