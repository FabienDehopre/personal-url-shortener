var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache")
    .WithDataVolume("redis_data");

var dbUser = builder.AddParameter("db-user", secret: true);
var dbPass = builder.AddParameter("db-pass", secret: true);
var db = builder.AddPostgres("postgres", dbUser, dbPass)
    .WithDataVolume("postgres_data")
    .AddDatabase("personal-url-shortener-db");

builder.AddProject<Projects.PersonalUrlShortener>("frontend")
    .WithHttpsEndpoint(port: 7173)
    .WithReference(redis)
    .WithReference(db);

builder.AddProject<Projects.PersonalUrlShortener_MigrationService>("db-migration")
    .WithReference(db);

builder.Build().Run();