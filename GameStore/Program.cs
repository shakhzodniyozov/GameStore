using GameStore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext(builder.Configuration)
                .AddRepositories()
                .AddServices();

var app = builder.Build();

app.UseRouting();

app.Run();
