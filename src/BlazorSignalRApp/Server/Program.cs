using Microsoft.AspNetCore.ResponseCompression;
using BlazorSignalRApp.Server.Data;
using BlazorSignalRApp.Server.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// TODO: add required configuration for SignalR
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// In-memory database configuration. Do not change!
builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseInMemoryDatabase("appdata"));


var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

// TODO: add required configuration for SignalR hubs
app.MapHub<ChatHub>("/chatter");
app.MapHub<WeatherHub>("/observations");
app.MapFallbackToFile("index.html");

app.Run();


// The Program class declaration below is needed for the automated tests. 
// DO NOT remove the following line!!!
public partial class Program { }
