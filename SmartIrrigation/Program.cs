using SmartIrrigation.API;
using SmartIrrigation.Data;
using SmartIrrigation.Logic;
using SmartIrrigation.MQTT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Weather forecast API client
builder.Services.AddSingleton<WeatherForecastClient>();

// Data stores
builder.Services.AddSingleton<HumidityData>();
builder.Services.AddSingleton<MoistureData>();
builder.Services.AddSingleton<TemperatureData>();
builder.Services.AddSingleton<WeatherForecastData>();
builder.Services.AddSingleton<ConfiguredData>();

// Subscribers
builder.Services.AddSingleton<HumiditySubscriber>();
builder.Services.AddSingleton<MoistureSubscriber>();
builder.Services.AddSingleton<TemperatureSubscriber>();

// Publishers
builder.Services.AddSingleton<ValvePublisher>();

// Algorithm
builder.Services.AddSingleton<IrrigationAlgorithm>();

// Schedulers
builder.Services.AddSingleton<IrrigationScheduler>();
builder.Services.AddSingleton<WeatherForecastScheduler>();

var app = builder.Build();

// Initialize services
app.Services.GetService<HumiditySubscriber>();
app.Services.GetService<MoistureSubscriber>();
app.Services.GetService<TemperatureSubscriber>();
app.Services.GetService<IrrigationScheduler>();
app.Services.GetService<WeatherForecastScheduler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

