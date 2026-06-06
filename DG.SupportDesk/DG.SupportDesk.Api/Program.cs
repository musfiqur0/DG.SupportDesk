using DG.SupportDesk.Application;
using DG.SupportDesk.Infrastructure;
using DG.SupportDesk.Infrastructure.Persistence;
using DG.SupportDesk.Infrastructure.Persistence.Seeds;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Host.UseWolverine(opts =>
{
    opts.UseRuntimeCompilation();

    opts.Discovery.IncludeAssembly(typeof(ApplicationDependencyInjection).Assembly);

    // Enables FluentValidation for Wolverine message handlers.
    opts.UseFluentValidation();

    opts.CodeGeneration.AlwaysUseServiceLocationFor<ApplicationDbContext>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// data seeding if no data exists only after DB creation
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //context.Database.Migrate(); //"update-database" command
    var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
    seedData.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Swagger UI
    //app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "DG SupportDesk API v1");
        c.RoutePrefix = "swagger"; // https://localhost:7215/swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
