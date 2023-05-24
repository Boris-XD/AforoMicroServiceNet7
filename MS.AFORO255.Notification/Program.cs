using Microsoft.EntityFrameworkCore;
using MS.AFORO255.Notification.Data;
using MS.AFORO255.Notification.Persistences;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContextDatabase>(
    opt =>
    {
        opt.UseMySQL(builder.Configuration["mariadb:cn"]);
    });

var app = builder.Build();
DbCreated.CreateDbIfNotExists(app);
app.Run();
