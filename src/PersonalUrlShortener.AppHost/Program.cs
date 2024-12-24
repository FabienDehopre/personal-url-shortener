var builder = DistributedApplication.CreateBuilder(args);

var dbUser = builder.AddParameter("db-user", secret: true);
var dbPass = builder.AddParameter("db-pass", secret: true);
var db = builder.AddPostgres("postgres", dbUser, dbPass)
    .WithDataVolume("postgres_data", isReadOnly: false)
    .WithPgWeb()
    .AddDatabase("personal-url-shortener-db");

builder.AddProject<Projects.PersonalUrlShortener_Web>("frontend")
    .WithHttpsEndpoint(port: 7173)
    .WithReference(db);

builder.AddProject<Projects.PersonalUrlShortener_MigrationService>("db-migration")
    .WithReference(db);

builder.Build().Run();