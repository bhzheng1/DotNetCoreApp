using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using NServiceBus.Pipeline;
using Optional;

namespace NSBus
{
    public class NServiceBusConfigurationException : Exception
    {
        public NServiceBusConfigurationException(string message) : base(message){}
    }

    public class DefaultNServiceBusBuilder : INServiceBusBuilder
    {
        private readonly IServiceCollection _services;
        private string _endpointName;
        private string _addressName;
        private string _rabbitMqConnectionString;
        private readonly IList<Action<EndpointConfiguration>> _behaviors = new List<Action<EndpointConfiguration>>();
        private Action<EndpointConfiguration> _persistenceRegisterAction;
        private string _dbConnectionString;
        private string _dbSchema;
        private string _dbTablePrefix;
        private Option<IDatabaseInitializer> _databaseInitializer;
        private string _userIdHeaderName;
        private string _clientIdHeaderName;

        public DefaultNServiceBusBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public INServiceBusBuilder ConfigureEndpointName(string endpointName)
        {
            Guard.ArgumentNotNull(endpointName, nameof(endpointName));
            _endpointName = endpointName;
            return this;
        }

        public INServiceBusBuilder ConfigureUniqueAddressName(string addressName)
        {
            _addressName = addressName;
            return this;
        }

        public INServiceBusBuilder ConfigureRabbitMQConnectionString(string connectionString)
        {
            Guard.ArgumentNotNull(connectionString, nameof(connectionString));
            _rabbitMqConnectionString = connectionString;
            return this;
        }

        public INServiceBusBuilder ConfigureSecurityContextHeaders(string userIdHeaderName, string clientIdHeaderName)
        {
            Guard.ArgumentNotNull(userIdHeaderName, nameof(userIdHeaderName));
            Guard.ArgumentNotNull(clientIdHeaderName, nameof(clientIdHeaderName));

            _userIdHeaderName = userIdHeaderName;
            _clientIdHeaderName = clientIdHeaderName;

            return this;
        }

        public INServiceBusBuilder AddPipelineBehavior<T>(T behavior, string description) where T : Behavior<IIncomingLogicalMessageContext>
        {
            _behaviors.Add(ec => ec.Pipeline.Register(behavior, description));
            return this;
        }

        public INServiceBusBuilder ConfigurePersistence<T>(Func<DbConnection, T> dbContextMaker, string connectionString, string schema, string tablePrefix, Option<IDatabaseInitializer> databaseInitializer) where T : DbContext
        {
            Guard.ArgumentNotNull(dbContextMaker, nameof(dbContextMaker));
            Guard.ArgumentNotNull(connectionString, nameof(connectionString));
            Guard.ArgumentNotNull(schema, nameof(schema));
            Guard.ArgumentNotNull(tablePrefix, nameof(tablePrefix));

            _persistenceRegisterAction = endpointConfiguration =>
                endpointConfiguration.Pipeline.Register(new UnitOfWorkSetup<T>(dbContextMaker), "unit of work setup");
            _dbConnectionString = connectionString;
            _dbSchema = schema;
            _dbTablePrefix = tablePrefix;
            _databaseInitializer = databaseInitializer;

            return this;
        }

        public IServiceProvider Build()
        {
            ValidateConfiguration();

            var securityHeaderConfig = new SecurityContextHeaderConfig
            {
                UserIdHeaderName = _userIdHeaderName,
                ClientIdHeaderName = _clientIdHeaderName
            };

            _services.TryAddScoped<IHttpContextAccessor, HttpContextAccessor>();
            _services.AddSingleton(securityHeaderConfig);
            
            var builder = new ContainerBuilder();
            builder.Populate(_services);
            
            IEndpointInstance endpointInstance = null;
            // ReSharper disable once AccessToModifiedClosure
            builder.Register(c => endpointInstance).SingleInstance();
            
            var container = builder.Build();
            
            var endpointConfiguration = new EndpointConfiguration(_endpointName);
            endpointConfiguration.LicensePath("NServiceBusLicense.xml");
            endpointConfiguration.UseContainer<AutofacBuilder>(_ => _.ExistingLifetimeScope(container));
            endpointConfiguration.RegisterComponents(c => c.ConfigureComponent<SecurityContextMutator>(DependencyLifecycle.InstancePerUnitOfWork));
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableOutbox();
            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.Pipeline.Register(new SecurityContextSetup(securityHeaderConfig), "security context setup");
            if (!string.IsNullOrEmpty(_addressName))
                endpointConfiguration.MakeInstanceUniquelyAddressable(_addressName);



            
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(_rabbitMqConnectionString);
            transport.UseConventionalRoutingTopology();

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.ConnectionBuilder(() => new SqlConnection(_dbConnectionString));

            persistence.SqlDialect<SqlDialect.MsSqlServer>().Schema(_dbSchema);
            persistence.TablePrefix(_dbTablePrefix);

            _databaseInitializer.Match(
                async databaseInitializer => await databaseInitializer.Apply(),
                () => persistence.DisableInstaller()
            );
                
            _persistenceRegisterAction(endpointConfiguration);

            foreach (var registerAction in _behaviors)
                registerAction(endpointConfiguration);

            endpointInstance = Endpoint.Start(endpointConfiguration).Result;
            NServiceBus.Logging.LogManager.Use<NLogFactory>();
            return new AutofacServiceProvider(container);
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(_endpointName))
                throw new NServiceBusConfigurationException("must configure endpoint name");
            
            if (string.IsNullOrEmpty(_rabbitMqConnectionString))
                throw new NServiceBusConfigurationException("must configure rabbitmq connection string");
            
            if (string.IsNullOrEmpty(_userIdHeaderName) || string.IsNullOrEmpty(_clientIdHeaderName))
                throw new NServiceBusConfigurationException("must configure security context header names");
            
            if (_persistenceRegisterAction == null || string.IsNullOrEmpty(_dbConnectionString) || string.IsNullOrEmpty(_dbSchema) || _dbTablePrefix == null)
                throw new NServiceBusConfigurationException("must configure persistence");
        }
    }
}