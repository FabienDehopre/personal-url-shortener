using PersonalUrlShortener.Infrastructure.Data;
using PersonalUrlShortener.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));
builder.AddNpgsqlDbContext<AppDbContext>("personal-url-shortener-db");

var host = builder.Build();
host.Run();