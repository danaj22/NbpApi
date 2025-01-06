using CurrencyAppApi;
using CurrencyAppApi.DAL;
using CurrencyAppApi.Entities;
using CurrencyAppApi.Extensions;
using CurrencyAppApi.Middlewares;
using CurrencyAppApi.Models.Validators;
using CurrencyAppApi.Repositories;
using CurrencyAppApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddHangfire(configuration.GetConnectionString("HangfireConnection"), configuration.GetConnectionString("HangfireDatabase"));
builder.Services.AddRedis(configuration.GetConnectionString("Redis"));

builder.Services.AddValidatorsFromAssemblyContaining<ExchangeRatesByDateDtoValidator>();
builder.Services.AddScoped<CurrencySeeder>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IExchangeRatesService, ExchangeRatesService>();
builder.Services.AddScoped<IExchangeRatesJob, ExchangeRatesJob>();
builder.Services.AddScoped<IHistoricalExchangeRatesJob, HistoricalExchangeRatesJob>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<IHistoricalExchangeRateRepository, HistoricalExchangeRateRepository>();
builder.Services.AddScoped<ISourceRepository, SourceRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendClient", builder =>
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        builder.AllowAnyMethod().AllowAnyHeader().WithOrigins(allowedOrigins);
    });
});

builder.Services.AddHttpClient<NbpApiClient>(
    client =>
    {
        client.BaseAddress = new Uri(configuration.GetValue<string>("Source:BaseUrl"));
    });

builder.Services.AddDbContext<CurrencyDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("CurrencyDbConnection")));

var app = builder.Build();

app.MigrateDb();
app.UseCors("FrontendClient");

var seeder = app.Services
            .CreateAsyncScope()
            .ServiceProvider
            .GetService<CurrencySeeder>();
seeder.Seed();

app.UsePathBase("/api");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseHangfireDashboard();
app.MapControllers();

app.Run();




