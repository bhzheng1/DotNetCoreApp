using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupModule.TestWebApp
{
    public class FooAppInitializer : IApplicationInitializer
    {
        public Task Invoke()
        {
            return Task.CompletedTask;
        }
    }
}
