using GameStore;
using GameStore.BusinessLogic.AutomapperProfiles;
using GameStore.Data.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext(builder.Configuration)
                .AddRepositories()
                .AddServices();

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(GameProfile).Assembly);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("WebClient", config =>
    {
        config.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

DBInitializer.Init(app.Services);
app.UseMiddleware<ExceptionHandler>();
app.UseCors("WebClient");
app.UseRouting();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStoreAPI");
        c.RoutePrefix = string.Empty;
    });
}

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();