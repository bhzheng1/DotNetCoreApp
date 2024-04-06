using System;
namespace WebApi_Application.Services
{
    public interface IConcurrentTaskDemo
    {
        public Task FireAndForgetTask();
    }
    public class ConcurrentTaskDemo
    {
        private ILogger<ConcurrentTaskDemo> _logger;
        public ConcurrentTaskDemo(ILogger<ConcurrentTaskDemo> logger)
        {
            _logger = logger;
        }

        public async Task FireAndForgetTask()
        {
            for (int i = 0; i < 10; i++)
            {
                _logger.LogInformation($"{i}");
                await Task.Delay(1000);

            }
        }
    }
}

