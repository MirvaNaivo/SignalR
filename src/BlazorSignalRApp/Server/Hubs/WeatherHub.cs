
using Microsoft.AspNetCore.SignalR;
using BlazorSignalRApp.Shared;
using BlazorSignalRApp.Server.Data;

namespace BlazorSignalRApp.Server.Hubs;

public class WeatherHub : Hub
{
    private readonly AppDataContext _db;

    public WeatherHub(AppDataContext db)
    {
        _db = db;
    }

    public async Task SubmitObservation(WeatherForecast forecast, WeatherObservation observation)
    {
        WeatherObservation weatherObservation = new WeatherObservation
        {
            Date = DateTime.Now,
            TemperatureC = observation.TemperatureC,
            ObservationText = observation.ObservationText
        };

        _db.WeatherObservations.Add(weatherObservation);
        await Forecast(weatherObservation);
    }

    public async Task Forecast(WeatherObservation weatherObservation)
    {
        await Clients.All.SendAsync("ReceiveMessage", weatherObservation);
    }
}

