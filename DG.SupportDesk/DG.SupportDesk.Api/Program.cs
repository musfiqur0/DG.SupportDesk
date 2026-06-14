using DG.SupportDesk.Api.Extensions;
using DG.SupportDesk.Api.Middlewares;
using DG.SupportDesk.Application;
using DG.SupportDesk.Infrastructure;
using DG.SupportDesk.Infrastructure.Persistence;
using DG.SupportDesk.Infrastructure.Persistence.Seeds;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog to the builder
builder.Host.SerilogConfiguration();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();  //for Serilog.Enrichers.ClientInfo
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

// 1. Define your policies in the Service Collection
builder.Services.AddCors(options =>
{
    // A relaxed policy for Development
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    // A strict policy for Production (Highly Recommended)
    options.AddPolicy("ProdCorsPolicy", policy =>
    {
        // ONLY allow your actual frontend domains
        policy.WithOrigins("https://my-frontend.com", "https://admin.my-frontend.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Required if using cookies/auth tokens
    });
});

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

app.UseMiddleware<ClientInfoLogEnricherMiddleware>();

var policyName = app.Environment.IsDevelopment() ? "DevCorsPolicy" : "ProdCorsPolicy";
app.UseCors(policyName);

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
