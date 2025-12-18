using MakeenCo_Work.Configurations;
using MakeenCo_Work.Extentions;

var builder = WebApplication.CreateBuilder(args);

//Add Application Services/Repositories
builder.Services.AddDependency();

//Add FluentValidation
builder.Services.AddApplicationFluentValidations();

//Add Jwt Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

//redis , memory  Cahce Configuration
builder.Services.AddCacheConfiguration(builder.Configuration);

// DbContext
builder.Services.AddDatabaseConfiguration(builder.Configuration);

//Identity
builder.Services.AddIdentityConfiguration();

builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

//Configure MiddleWare PipeLine
app.UseApplicationMiddleWare(app.Environment);

//Seed DataBase
await app.SeedDatabaseAsync();

app.Run();
