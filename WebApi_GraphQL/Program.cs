using GraphQL;
using GraphQL.Types;
using WebApi_GraphQL;
using WebApi_GraphQL.GraphTypes;
using WebApi_GraphQL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Start GraphQL services
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<EmployeeDetailsType>();
builder.Services.AddSingleton<EmployeeGroupGraphType>();
builder.Services.AddSingleton<ApplicationQuery>();
builder.Services.AddSingleton<ISchema, GraphQLSchema>();
builder.Services.AddGraphQL(b => b
    .AddAutoSchema<ApplicationQuery>()  // schema
    .AddSystemTextJson());   // serializer
// End GraphQL services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseGraphQL<ISchema>("/graphql");            // url to host GraphQL endpoint
app.UseGraphQLPlayground(
    "/graphql-playground",                               // url to host Playground at
    new GraphQL.Server.Ui.Playground.PlaygroundOptions
    {
        GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
        SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
    });

app.Run();
