using System.Text;
using System.Text.Json.Serialization;
using DatabaseMigrationSystem.ApplicationServices;
using DatabaseMigrationSystem.DataAccess;
using DatabaseMigrationSystem.Exceptions;
using DatabaseMigrationSystem.Infrastructure;
using DatabaseMigrationSystem.Infrastructure.Configurations;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.SwaggerConfig;
using DatabaseMigrationSystem.UseCases;
using DatabaseMigrationSystem.UseCases.Account.Mappings;
using DatabaseMigrationSystem.UseCases.Settings.Mappings;
using DatabaseMigrationSystem.Utils.Modules;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(opts => opts
        .SerializerSettings.Converters.Add(new StringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration["Postgres:ConnectionString"];
var secret = builder.Configuration["JwtSettings:Secret"];

var key = Encoding.ASCII.GetBytes(secret);

var dbContextBuilder = new DbContextOptionsBuilder();
dbContextBuilder.UseNpgsql(connectionString);
builder.Services.AddDbContextPool<ApplicationDbContext>(dbContextOptionsBuilder =>
{
    dbContextOptionsBuilder.UseNpgsql(connectionString);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Func<ApplicationDbContext>>(s => () => new ApplicationDbContext(dbContextBuilder.Options));

builder.Services.AddAutoMapper(typeof(AccountAutoMapperProfile), typeof(SettingsAutoMapperProfile));



builder.Services.AddOptions();
builder.Services.Configure<PostgresSettings>(builder.Configuration.GetSection(nameof(PostgresSettings)));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.RegisterModule<ApplicationServicesModule>(builder.Configuration);
builder.Services.RegisterModule<DataAccessModule>(builder.Configuration);
builder.Services.RegisterModule<InfrastructureModule>(builder.Configuration);
builder.Services.RegisterModule<UseCasesModule>(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddSwagger();
builder.Services.AddTransient<ExceptionsMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionsMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseProjectSwagger();
app.MapControllers();


app.Run();


