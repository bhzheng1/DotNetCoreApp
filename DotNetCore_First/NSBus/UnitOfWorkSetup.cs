using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NServiceBus.Pipeline;

namespace NSBus
{   
    public class UnitOfWorkSetup<T> : Behavior<IIncomingLogicalMessageContext> where T : DbContext
    {
        private readonly Func<DbConnection, T> _unitOfWorkMaker;
        public UnitOfWorkSetup(Func<DbConnection,T> unitOfWorkMaker)
        {
            _unitOfWorkMaker = unitOfWorkMaker;
        }
        
        public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
        {
            var uow = new UnitOfWorkProvider<T>(_unitOfWorkMaker);
            context.Extensions.Set(uow);
            await next();
            context.Extensions.Remove<UnitOfWorkProvider<T>>();
        }
    }
}