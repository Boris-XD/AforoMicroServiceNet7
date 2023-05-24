using Aforo255.Cross.Event.Src;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.AFORO255.Deposit.Data;
using MS.AFORO255.Deposit.Messages.CommandHandlers;
using MS.AFORO255.Deposit.Messages.Commands;
using MS.AFORO255.Deposit.Persistences;
using MS.AFORO255.Deposit.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services);

var app = builder.Build();
ConfigureMiddleware(app, app.Services);
ConfigureEndpoints(app, app.Services);
DbCreated.CreateDbIfNotExists(app);
app.Run();

void ConfigureConfiguration(ConfigurationManager configuration)
{

}
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddDbContext<ContextDatabase>(
        options =>
        {
            options.UseNpgsql(builder.Configuration["postgres:cn"]);
        });
    services.AddScoped<ITransactionService, TransactionService>();
    services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
    services.AddRabbitMQ();
    services.AddTransient<IRequestHandler<TransactionCreateCommand, bool>, TransactionCommandHandler>();

}
void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services)
{
    app.UseAuthorization();
}
void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
{
    app.MapControllers();
}

