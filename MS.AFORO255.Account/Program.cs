using MS.AFORO255.Account;
using MS.AFORO255.Account.Data;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Lifetime);
startup.ConfigureEndpoints(app, app.Lifetime);
DbCreated.CreateDbIfNotExists(app);
app.Run();
