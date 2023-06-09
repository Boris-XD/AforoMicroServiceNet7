using MS.AFORO255.History.Features.Services;
using MS.AFORO255.History.Persistences.Settings;
using MS.AFORO255.History.Persistences;
using Carter;
using Aforo255.Cross.Event.Src.Bus;
using Aforo255.Cross.Event.Src;
using MediatR;
using MS.AFORO255.History.Messages.EventHandlers;
using MS.AFORO255.History.Messages.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.Configure<Mongosettings>(opt =>
{
    opt.Connection = builder.Configuration.GetSection("mongo:cn").Value;
    opt.DatabaseName = builder.Configuration.GetSection("mongo:database").Value;
});
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IMongoBookDBContext, MongoBookDBContext>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddRabbitMQ();

builder.Services.AddTransient<TransactionEventHandler>();
builder.Services.AddTransient<IEventHandler<TransactionCreatedEvent>, TransactionEventHandler>();

var app = builder.Build();
app.MapCarter();
ConfigureEventBus(app);
app.Run();


void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<TransactionCreatedEvent, TransactionEventHandler>();
}
