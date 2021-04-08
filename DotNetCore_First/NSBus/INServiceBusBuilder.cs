using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using NServiceBus.Pipeline;
using Optional;

namespace NSBus
{
    public interface INServiceBusBuilder
    {
        INServiceBusBuilder ConfigureEndpointName(string endpointName);
        INServiceBusBuilder ConfigureUniqueAddressName(string addressName);
       // INServiceBusBuilder ConfigureDatabaseInitializer(MyContext context);
        INServiceBusBuilder ConfigureRabbitMQConnectionString(string connectionString);
        INServiceBusBuilder ConfigureSecurityContextHeaders(string userIdHeaderName, string clientIdHeaderName);

        INServiceBusBuilder AddPipelineBehavior<T>(T behavior, string description)
            where T : Behavior<IIncomingLogicalMessageContext>;

        INServiceBusBuilder ConfigurePersistence<T>(Func<DbConnection, T> dbContextMaker, string connectionString,
            string schema, string tablePrefix, Option<IDatabaseInitializer> databaseInitializer) where T : DbContext;
        IServiceProvider Build();
    }
}