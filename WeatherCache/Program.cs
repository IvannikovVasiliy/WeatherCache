/*var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();*/
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WeatherCache.OpenWeather;

// Build application
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<OpenWeatherClient>();

// Middleware
var app = builder
	.Build();

app.UseRouting();
app.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapControllers());

app.Run();

