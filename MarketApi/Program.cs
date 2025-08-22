using Market.Infrastructure.DataBase;
using Market.Infrastructure.Extantions;
using MarketApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Market application APIs", Version = "v1" }));

var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    //{
    app.UseSwagger();
    app.UseSwaggerUI(op => op.EnableTryItOutByDefault());
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
