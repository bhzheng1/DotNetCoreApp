using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using WebApplication_API.DbContexts;

namespace WebApplication_API.Filters
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly ContosoContext _dbContext;
        public DbContextTransactionFilter(ContosoContext contosoContext)
        {
            _dbContext = contosoContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await _dbContext.BeginTransactionAsync();
                var actionExecuted = await next();
                if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
                {
                    _dbContext.RollbackTransaction();
                }
                else {
                    await _dbContext.CommitTransactionAsync();
                }
            }
            catch (Exception)
            {

                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
