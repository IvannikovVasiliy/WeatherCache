using WeatherCache.OpenWeather;

// Build application
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<OpenWeatherClient>();

// Middleware
var app = builder
    .Build();

DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
DateTime dtNow = DateTime.Now;
TimeSpan result = dtNow.Subtract(dt);   
int seconds = Convert.ToInt32(result.TotalSeconds);
app.MapGet("/", () => seconds);

app.UseRouting();
app.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapControllers());

app.Run();