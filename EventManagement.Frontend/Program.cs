
using EventManagement.Frontend.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthorizationCore();

// Custom services
builder.Services.AddScoped<AuthService>();
// Configure HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7147/api/") });
builder.Services.AddScoped<ApiService>();
//builder.Services.AddScoped(sp =>
//{
//    var handler = new AuthenticatedHttpClientHandler(sp.GetRequiredService<AuthService>());
//    return new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7147/api/") };
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
